using System.Collections;
using UnityEngine;

public class BoardStatus
{
    //Board
    public Tile[,] tiles;

    //Selection status
    public Vector2 initialClickPosition;
    public Tile highlightedTile;
    public Tile draggingTile;
    public bool isMouseOnBoard; //Is mouse hovering over a valid tile
    public bool IsInAnimation;
    public Vector2 offsettedMousePos;
    public bool inDragMode;
    public Vector2Int hoverIndex { get; private set; }
    public Vector2Int hoverIndexPrev { get; private set; }

    //Tile dimensions
    public int tileCount { get; private set; }
    public float tileSize { get; private set; }
    public float tileGap { get; private set; }
    public float cellSize { get; private set; } //tileSize + tileGap
    public float halfCellSize { get; private set; }

    //Board dimensions
    public float startPointX { get; private set; }
    public float startPointY { get; private set; }
    public Vector2 startPoint { get; private set; }
    public float up { get; private set; } //Upper bound
    public float down { get; private set; } //Lower bound
    public float left { get; private set; } //Left bound
    public float right { get; private set; } //Right bound

    float halfBoardSize;

    public bool HadDraggingTile => draggingTile != null;
    public bool HadHighlightedTile => highlightedTile != null;
    public Vector2Int SelectedTileIndex => new Vector2Int(draggingTile.TileIndex.x, draggingTile.TileIndex.y);
    
    public BoardStatus(float tileSize, float tileGap, int tileCount)
    {
        //Tile dimensions
        this.tileCount = tileCount;
        this.tileSize = tileSize;
        this.tileGap = tileGap;
        cellSize = tileSize + tileGap;
        halfCellSize = halfCellSize * .5f;

        //Board dimensions
        startPointX = -cellSize * tileCount * .5f;
        startPointY = startPointX;
        startPoint = new Vector2(startPointX, startPointY);
        up = -startPoint.y;
        down = startPoint.y;
        left = startPoint.x;
        right = -startPoint.x;

        //Debug.DrawLine(new Vector2(left, down), new Vector2(left, up), Color.green, 30f);
        //Debug.DrawLine(new Vector2(left, up), new Vector2(right, up), Color.red, 30f);
        //Debug.DrawLine(new Vector2(right, up), new Vector2(right, down), Color.white, 30f);
        //Debug.DrawLine(new Vector2(left, down), new Vector2(right, down), Color.black, 30f);
       

        //Cache
        //indexOffset = Mathf.CeilToInt(tileCount / 2f);
        halfBoardSize = right;

        //Debug.DrawLine(new Vector2(-halfBoardSize, -halfBoardSize),
        //   new Vector2(halfBoardSize, halfBoardSize), Color.cyan, 30f);
    }

    public bool IsSelectedTileOfIndex(Vector2Int index) => draggingTile != null && draggingTile.TileIndex == index;

    public bool TrySetHoveringTileAsActive()
    {
        draggingTile = GetHoveringTile();
        return draggingTile != null;
    }

    public void SwapTilesIndexs (Tile t1, Tile t2)
    {
        Vector2Int t1Index = t1.TileIndex;

        t1.ReassignTileIndex(t2.TileIndex);
        t2.ReassignTileIndex(t1Index);

        tiles[t1.TileIndex.x, t1.TileIndex.y] = t1;
        tiles[t2.TileIndex.x, t2.TileIndex.y] = t2;
    }


    public Tile GetHoveringTile ()
    {
        if (hoverIndex.x >= 0 && hoverIndex.x < tiles.GetLength(0) &&
            hoverIndex.y >= 0 && hoverIndex.y < tiles.GetLength(1))
        {
            //Debug.Log("GetHoveringTile: " + hoverIndex.x + ", " + hoverIndex.y);
            return tiles[hoverIndex.x, hoverIndex.y];
        }
        return null;
    }

    public void CacheStatusAsPrevious ()
    {
        hoverIndexPrev = hoverIndex;
    }

    public void UpdateHoveringTileIndex()
    {
        //Check in which tile index is the mouse hovering within.
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        isMouseOnBoard = mousePos.x > left && mousePos.x < right &&
            mousePos.y > down && mousePos.y < up;

        //offsettedMousePos = new Vector2(mousePos.x + halfBoardSize,
        //mousePos.y + halfBoardSize);
        //Debug.DrawLine(mousePos, offsettedMousePos, Color.blue);

        //return;
        if (isMouseOnBoard)
        {
            hoverIndex = new Vector2Int(Mathf.FloorToInt((mousePos.x + halfBoardSize) / cellSize),
                Mathf.FloorToInt((mousePos.y + halfBoardSize) / cellSize));
        }
    }

    public void RegisterInitialMouseClickPosition () => 
        initialClickPosition = Input.mousePosition;

    public Vector2 GetTileWorldPosition (Vector2Int tileIndex) => new Vector2(
            startPoint.x + cellSize * .5f + cellSize * tileIndex.x,
            startPoint.y + cellSize * .5f + cellSize * tileIndex.y);
}