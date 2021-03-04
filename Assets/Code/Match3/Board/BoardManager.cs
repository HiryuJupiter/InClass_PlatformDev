using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TilesPrefabDirectory))]
public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    [Header("Tile Generation")]
    [SerializeField] float tileGap = 0.02f; //The gap offset between tiles.
    [SerializeField] int tilesPerRow = 4;

    [Header("Tile Swapping")]

    //Classes and components
    TilesPrefabDirectory directory;
    BoardStatus status;

    #region MonoBehavior
    private void Awake()
    {
        Instance = this;

        //Classes and components
        directory = GetComponent<TilesPrefabDirectory>();
        directory.Initialize();
        status = new BoardStatus(directory.GetTileSize, tileGap, tilesPerRow);

        //Initialize
        status.tiles = BoardBuilder.CreateBoard(status, directory, transform);
    }

    void Update()
    {
        status.UpdateHoveringTileIndex();
        TileSelectionUpdate();
        status.CacheStatusAsPrevious();
    }
    #endregion

    #region Tile interactions
    void TileSelectionUpdate () 
    {
        //Tile selection
        if (ClickedLMB && status.isMouseOnBoard)
        {
            status.RegisterInitialMouseClickPosition();
            status.highlightedTile = status.GetHoveringTile();
            status.highlightedTile.HighLightTile(true);
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
    }

    void MouseDrag ()
    {
        if (!InDragMode)
        {
            if (DraggedFarEnoughToEnterDragMode)
            {
                //Enter dragging mode
                status.inDragMode = true;

                status.highlightedTile.HighLightTile(false);
                status.draggingTile = status.highlightedTile;
                status.draggingTile.DragState_Enter();
                status.highlightedTile = null;
            }
        }

        if (InDragMode)
        {
            status.draggingTile.DragState_Update();

            //When entering new tile space
            if (EnteredNewTile)
            {
                Tile newTile = status.GetHoveringTile();
                if (newTile != null)
                {
                    //Tell it to go to old position
                    newTile.DirectLerpMoveTo(Tile.IndexToWorldPoint(status.SelectedTileIndex, status.startPoint, status.cellSize));

                    //Swap tile index
                    status.SwapTilesIndexs(newTile, status.draggingTile);

                    //Debug.Log("Swapping index. Active: " + activeIndex + ", newIndex" + newTile.TileIndex);
                    //Debug.Log("assigning activeTile (of index " + activeIndex + ") with newIndex: " + newTile.TileIndex);
                    //Debug.Log("assigning newTile (of index " + newTile.TileIndex + ") with activeIndex: " + activeIndex);
                }
            }
        }
    }

    void MouseRelease ()
    {
        if (status.inDragMode)
        {
            status.inDragMode = false;

            status.draggingTile.DragState_Exit();
            status.draggingTile.DirectLerpMoveTo(Tile.IndexToWorldPoint(status.draggingTile.TileIndex,
                status.startPoint, status.cellSize));

            status.draggingTile = null;
        }
        status.highlightedTile?.HighLightTile(false);
    }
    #endregion

    #region (old) Tile swapping - Click

    #endregion

    #region Helpers
    bool ClickedLMB => Input.GetMouseButtonDown(0);
    bool ClickReleasedLMB => Input.GetMouseButtonUp(0);
    bool ClickAndDragLMB => Input.GetMouseButton(0);
    bool HadDraggingTile => status.HadDraggingTile;
    bool HadHighlightedTile => status.HadHighlightedTile;
    bool InDragMode => status.inDragMode;
    bool DraggedFarEnoughToEnterDragMode => Vector2.Distance(
        (Vector2)Input.mousePosition, status.initialClickPosition) > Settings.MinDragDist;
    public bool EnteredNewTile => status.isMouseOnBoard && (status.hoverIndexPrev != status.hoverIndex);
    #endregion

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 0, 900, 20), "isMouseOnBoard: " + status.isMouseOnBoard);
        GUI.Label(new Rect(20, 20, 900, 20), "IsInAnimation: " + status.IsInAnimation);
        GUI.Label(new Rect(20, 40, 900, 20), "inDragMode: " + status.inDragMode);
        GUI.Label(new Rect(20, 60, 900, 20), "hoverIndex: "     + status.hoverIndex);

        GUI.Label(new Rect(20, 100, 900, 20), "StartingPos: "        + status.startPoint);
        GUI.Label(new Rect(20, 120, 900, 20), "HasSelectedTile: "   + status.HadDraggingTile);
        if (status.HadDraggingTile)
        GUI.Label(new Rect(20, 140, 900, 20), "SelectedTileIndex: " + status.SelectedTileIndex);

        GUI.Label(new Rect(20, 200, 900, 20), "mouseWorldPos: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        GUI.Label(new Rect(20, 220, 900, 20), "up: "    + status.up);
        GUI.Label(new Rect(20, 240, 900, 20), "down: "  + status.down);
        GUI.Label(new Rect(20, 260, 900, 20), "left: "  + status.left);
        GUI.Label(new Rect(20, 280, 900, 20), "right: " + status.right);

        for (int x = 0; x < status.tiles.GetLength(0); x++)
        {
            for (int y = 0; y < status.tiles.GetLength(1); y++)
            {
                GUI.Label(new Rect(400 + x * 50, 120 - y * 20, 500, 20), status.tiles[x, y].TileIndex.ToString());

            }
        }


    }
}