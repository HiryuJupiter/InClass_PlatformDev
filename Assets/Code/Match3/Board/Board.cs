using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TilesPoolManager))]
public class Board : MonoBehaviour
{
    public static Board Instance;

    [Header("Tile Generation")]
    [SerializeField] float tileGap = 0.02f; //The gap offset between tiles.
    [SerializeField] int tileCount = 4; //Tiles per row and collumn

    //Board
    public Tile[,] tiles;

    //Selection status
    Vector2 initialClickPosition;
    Tile highlightedTile;
    Tile draggingTile;
    bool isMouseOnBoard; //Is mouse hovering over a valid tile
    bool IsInAnimation;
    Vector2 offsettedMousePos;
    bool inDragMode;
    Vector2Int HoverIndex;
    Vector2Int HoverIndexPrev;

    //Tile dimensions
    float tileSize;
    float cellSize; //tileSize + tileGap
    float halfCellSize;

    //Board dimensions
    float startPointX;
    float startPointY;
    Vector2 startPoint;
    float bound_top; //Upper bound
    float bound_bot; //Lower bound
    float bound_left; //Left bound
    float bound_right; //Right bound

    bool HadDraggingTile => draggingTile != null;
    bool HadHighlightedTile => highlightedTile != null;
    Vector2Int SelectedTileIndex => new Vector2Int(draggingTile.TileIndex.x, draggingTile.TileIndex.y);

    //Classes and components
    TilesPoolManager poolManager;

    //Cache
    float halfBoardSize;

    #region MonoBehavior
    private void Awake()
    {
        Instance = this;

        //Classes and components
        poolManager = GetComponent<TilesPoolManager>();
        poolManager.Initialize();

        //Tile dimensions
        tileSize = poolManager.GetTileSize();
        cellSize = tileSize + tileGap;
        halfCellSize = cellSize * .5f;

        //Board dimensions
        startPointX = -cellSize * tileCount * .5f;
        startPointY = startPointX;
        startPoint = new Vector2(startPointX, startPointY);
        bound_top = -startPoint.y;
        bound_bot = startPoint.y;
        bound_left = startPoint.x;
        bound_right = -startPoint.x;
        halfBoardSize = bound_right;

        //Initialize
        tiles = CreateBoard();
    }

    void Update()
    {
        UpdateHoveringTileIndex();
        TileSelectionUpdate();
        CacheStatusAsPrevious();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 0, 900, 20), "TileSize: " + tileSize);
        GUI.Label(new Rect(20, 20, 900, 20), "CellSize: " + cellSize);
        GUI.Label(new Rect(20, 40, 900, 20), "HalfCellSize: " + halfCellSize);


        GUI.Label(new Rect(20, 100, 900, 20), "isMouseOnBoard: " + isMouseOnBoard);
        GUI.Label(new Rect(20, 120, 900, 20), "IsInAnimation: " + IsInAnimation);
        GUI.Label(new Rect(20, 140, 900, 20), "inDragMode: " + inDragMode);
        GUI.Label(new Rect(20, 160, 900, 20), "hoverIndex: " + HoverIndex);

        GUI.Label(new Rect(20, 200, 900, 20), "StartingPos: " + startPoint);
        GUI.Label(new Rect(20, 220, 900, 20), "HasSelectedTile: " + HadDraggingTile);
        if (HadDraggingTile)
            GUI.Label(new Rect(20, 340, 900, 20), "SelectedTileIndex: " + SelectedTileIndex);

        GUI.Label(new Rect(20, 300, 900, 20), "mouseWorldPos: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        GUI.Label(new Rect(20, 320, 900, 20), "up: " + bound_top);
        GUI.Label(new Rect(20, 340, 900, 20), "down: " + bound_bot);
        GUI.Label(new Rect(20, 360, 900, 20), "left: " + bound_left);
        GUI.Label(new Rect(20, 380, 900, 20), "right: " + bound_right);

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (tiles[x, y] != null)
                    GUI.Label(new Rect(400 + x * 50, 120 - y * 20, 500, 20), tiles[x, y].TileIndex.ToString());
            }
        }
    }
    #endregion

    #region Tile mouse interactions
    void TileSelectionUpdate()
    {
        //Tile selection
        if (ClickedLMB && isMouseOnBoard)
        {
            RegisterInitialMouseClickPosition();
            highlightedTile = GetHoveringTile();
            highlightedTile?.HighLightTile(true);
        }

        //Tile dragging
        if (ClickAndDragLMB && (HadDraggingTile || HadHighlightedTile))
        {
            MouseDrag();
        }

        //Tile deselection
        if (ClickReleasedLMB && (HadDraggingTile || HadHighlightedTile))
        {
            MouseRelease();
        }

        //Debug try destroy tile
        if (Input.GetMouseButtonDown(1))
        {
            RemoveTile(HoverIndex);
        }
    }

    void MouseDrag()
    {
        if (!inDragMode)
        {
            if (DraggedFarEnoughToEnterDragMode)
            {
                //Enter dragging mode
                inDragMode = true;

                highlightedTile.HighLightTile(false);
                draggingTile = highlightedTile;
                draggingTile.DragState_Enter();
                highlightedTile = null;
            }
        }

        if (inDragMode)
        {
            draggingTile.DragState_Update();

            //When entering new tile space
            if (EnteredNewTile)
            {
                Tile newTile = GetHoveringTile();
                if (newTile != null)
                {
                    //Tell it to go to old position
                    newTile.TileSwapMoveToPosition(IndexToWorldPoint(SelectedTileIndex));

                    //Swap tile index
                    SwapTilesIndexs(newTile, draggingTile);

                    //Debug.Log("Swapping index. Active: " + activeIndex + ", newIndex" + newTile.TileIndex);
                    //Debug.Log("assigning activeTile (of index " + activeIndex + ") with newIndex: " + newTile.TileIndex);
                    //Debug.Log("assigning newTile (of index " + newTile.TileIndex + ") with activeIndex: " + activeIndex);
                }
            }
        }
    }

    void MouseRelease()
    {
        if (inDragMode)
        {
            inDragMode = false;

            draggingTile.DragState_Exit();
            draggingTile.TileSwapMoveToPosition(IndexToWorldPoint(draggingTile.TileIndex));

            draggingTile = null;
        }
        highlightedTile?.HighLightTile(false);
    }
    #endregion

    #region Tile removing
    public void RemoveTile(Vector2Int index)
    {
        Debug.Log("RemoveTile " + index);
        //Remove tile  and shuffle down column
        Tile t = tiles[index.x, index.y];
        t.Despawn();
        tiles[index.x, index.y] = null;

        ShuffleDownColumn(index);
    }

    void ShuffleDownColumn(Vector2Int emptySlot)
    {
        if (!IsTileInBound(emptySlot) || HasTileHere(emptySlot))
        {
            Debug.Log("Exit shuffle down, at index " + emptySlot);
            return;
        }

        //Finding the next solid tile
        Vector2Int current = emptySlot;
        current.y++;

        while (IsTileInBound(current))
        {
            //If we found a tile, then tell this tile to shuffle down
            if (HasTileHere(current))
            {
                Debug.Log("HasTileHere " + current);
                ShuffleTileToLowestEmptyIndex(current);
                break;
            }

            //Spawn current slot is at the top edge, then spwn new tile
            if (IsTileLocatedAtTopEdge(current))
            {
                Debug.Log("SpawnNewTile targeting at " + current);

                SpawnNewTile(emptySlot);
                break;
            }
            current.y++;
        }

        //Recursively tell the next tile above to see if it's empty and require filling.
        ShuffleDownColumn(emptySlot + new Vector2Int(0, 1));
    }


    void ShuffleTileToLowestEmptyIndex(Vector2Int index)
    {

        //Set the tile's index to the lowest empty slot, 
        int lowestY = index.y - 1;
        int x = index.x;
        int y = index.y;

        while (lowestY > 0 && !HasTileHere(new Vector2Int(x, lowestY - 1)))
        {
            lowestY--;
        }

        //then tell it to linearly lerp down
        Tile tile = tiles[x, y];
        tile.SetTileIndex(new Vector2Int(x, lowestY));
        tiles[x, lowestY] = tile;
        tiles[x, y] = null;

        Vector2 tgtPos = IndexToWorldPoint(new Vector2Int(x, lowestY));
        tile.SetFallingTargetPosition(tgtPos);
    }
    #endregion

    #region Tile creation
    public Tile[,] CreateBoard()
    {
        //Initialize tiles array
        Tile[,] tiles = new Tile[tileCount, tileCount];

        //We cache what's on the left and down below to prevent a match at start.
        Tile[] previousLeft = new Tile[tileCount];
        Tile previousBelow = null;

        //Spawn tile prefabs
        for (int x = 0; x < tileCount; x++)
        {
            for (int y = 0; y < tileCount; y++)
            {
                Vector3 spawnPos = IndexToWorldPoint(x, y);
                Tile t = poolManager.SpawnRandomExcept(new List<Tile>() { previousBelow, previousLeft[y] });
                t.transform.position = spawnPos;
                t.transform.name = x.ToString() + "_" + y.ToString();
                t.SetTileIndex(new Vector2Int(x, y));
                tiles[x, y] = t;

                //Cache for next loop
                previousLeft[y] = t;
                previousBelow = t;
            }
        }
        return tiles;
    }

    void SpawnNewTile(Vector2Int targetSlot)
    {
        Tile t = poolManager.SpawnRandom();
        t.transform.position  = GetAboveColumnSpawnPosition(targetSlot.x);
        t.SetFallingTargetPosition(IndexToWorldPoint(targetSlot.x, targetSlot.y));

        t.transform.name = targetSlot.x.ToString() + "_" + targetSlot.y.ToString();
        t.SetTileIndex(targetSlot);
        tiles[targetSlot.x, targetSlot.y] = t;
    }
    #endregion

    #region Tile and index checking
    public Tile GetHoveringTile()
    {
        if (HoverIndex.x >= 0 && HoverIndex.x < tiles.GetLength(0) &&
            HoverIndex.y >= 0 && HoverIndex.y < tiles.GetLength(1))
        {
            //Debug.Log("GetHoveringTile: " + hoverIndex.x + ", " + hoverIndex.y);
            return tiles[HoverIndex.x, HoverIndex.y];
        }
        return null;
    }

    public void UpdateHoveringTileIndex()
    {
        //Check in which tile index is the mouse hovering within.
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        isMouseOnBoard = mousePos.x > bound_left && mousePos.x < bound_right &&
            mousePos.y > bound_bot && mousePos.y < bound_top;

        //offsettedMousePos = new Vector2(mousePos.x + halfBoardSize,
        //mousePos.y + halfBoardSize);
        //Debug.DrawLine(mousePos, offsettedMousePos, Color.blue);

        //return;
        if (isMouseOnBoard)
        {
            HoverIndex = new Vector2Int(Mathf.FloorToInt((mousePos.x + halfBoardSize) / cellSize),
                Mathf.FloorToInt((mousePos.y + halfBoardSize) / cellSize));
        }
    }
    #endregion

    #region Tile Index manipulation
    public void SwapTilesIndexs(Tile t1, Tile t2)
    {
        Vector2Int t1Index = t1.TileIndex;

        t1.ReassignTileIndex(t2.TileIndex);
        t2.ReassignTileIndex(t1Index);

        tiles[t1.TileIndex.x, t1.TileIndex.y] = t1;
        tiles[t2.TileIndex.x, t2.TileIndex.y] = t2;
    }
    #endregion

    #region Tile status changing
    public bool TrySetHoveringTileAsActive()
    {
        draggingTile = GetHoveringTile();
        return draggingTile != null;
    }
    #endregion

    #region Utility and minor methods
    public bool IsSelectedTileOfIndex(Vector2Int index) => draggingTile != null && draggingTile.TileIndex == index;
    bool IsTileLocatedAtTopEdge(Vector2Int index) => index.y < tiles.GetLength(1);
    bool HasTileHere(Vector2Int index) => tiles[index.x, index.y] != null;
    bool IsTileInBound(Vector2Int index) => index.x < tiles.GetLength(0) && index.y < tiles.GetLength(1);
    void CacheStatusAsPrevious() => HoverIndexPrev = HoverIndex;

    void RegisterInitialMouseClickPosition() =>
        initialClickPosition = Input.mousePosition;


    #endregion

    #region Conversion
    public Vector2 IndexToWorldPoint(Vector2Int tileIndex)
    => new Vector2(
            startPoint.x + cellSize * .5f + cellSize * tileIndex.x,
            startPoint.y + cellSize * .5f + cellSize * tileIndex.y);
    public Vector2 IndexToWorldPoint(int indexX, int indexY)
        => new Vector2(
            startPoint.x + cellSize * .5f + cellSize * indexX,
            startPoint.y + cellSize * .5f + cellSize * indexY);

    public Vector2 GetAboveColumnSpawnPosition(int xColumnIndex) => new Vector2(
        startPoint.x + cellSize * .5f + cellSize * xColumnIndex,
        startPoint.y + cellSize * .5f + cellSize * tileCount);
    #endregion

    #region Helpers
    bool ClickedLMB => Input.GetMouseButtonDown(0);
    bool ClickReleasedLMB => Input.GetMouseButtonUp(0);
    bool ClickAndDragLMB => Input.GetMouseButton(0);
    bool DraggedFarEnoughToEnterDragMode => Vector2.Distance(
        (Vector2)Input.mousePosition, initialClickPosition) > Settings.MinDragDist;
    public bool EnteredNewTile => isMouseOnBoard && (HoverIndexPrev != HoverIndex);
    void VacateSlot(Vector2Int index) => tiles[index.x, index.y] = null;

    #endregion
}