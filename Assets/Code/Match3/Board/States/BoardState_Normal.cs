using UnityEngine;
using System.Collections.Generic;

public class BoardState_Normal : BoardStateBase
{
    public BoardState_Normal(Board board) : base(board) {}

    public override void StateEntry()
    {
        base.StateEntry();
    }

    public override void TickUpdate()
    {
        base.TickUpdate();
        UpdateHoveringTileIndex();
        TileSelectionUpdate();
    }

    public override void StateExit()
    {

    }

    void TileSelectionUpdate()
    {
        //Tile selection
        if (ClickedLMB && status.isMouseOnBoard)
        {
            RegisterInitialClickPosition();
            status.highlightedTile = GetHoveringTile();
            status.highlightedTile?.SetTileHighlight(true);
        }

        //Tile dragging
        if (ClickAndDragLMB && (status.HasDraggingTile || status.HasHighlightedTile))
        {
            MouseDrag();
        }

        //Tile deselection
        if (ClickReleasedLMB && (status.HasDraggingTile || status.HasHighlightedTile))
        {
            MouseRelease();
        }

        //Debug try destroy tile
        if (Input.GetMouseButtonDown(1))
        {
            DebugRemoveTile(status.HoverIndex);
        }
    }

    void MouseDrag()
    {
        if (!status.inDragMode && DraggedFarEnoughToEnterDragMode)
        {
            //Enter dragging mode
            status.inDragMode = true;

            status.highlightedTile.SetTileHighlight(false);
            status.draggingTile = status.highlightedTile;
            status.draggingTile.DragEntry();
            status.highlightedTile = null;
        }

        if (status.inDragMode)
        {
            status.draggingTile.DragUpdate();

            //When entering new tile space
            if (EnteredNewTile)
            {
                Tile newTile = GetHoveringTile();
                if (newTile != null && newTile.InNormalMode)
                {
                    //Tell it to go to old position
                    newTile.TileSwapMoveToPosition(
                        board.TileIndexToWorldPoint(status.SelectedTileIndex));

                    //Swap tile index
                    SwapTilesIndexs(newTile, status.draggingTile);

                    //Debug.Log("Swapping index. Active: " + activeIndex + ", newIndex" + newTile.TileIndex);
                    //Debug.Log("assigning activeTile (of index " + activeIndex + ") with newIndex: " + newTile.TileIndex);
                    //Debug.Log("assigning newTile (of index " + newTile.TileIndex + ") with activeIndex: " + activeIndex);
                }
            }
        }
    }

    void MouseRelease()
    {
        if (status.inDragMode)
        {
            status.inDragMode = false;

            status.draggingTile.DragRelease();
            status.draggingTile.TileSwapMoveToPosition(board.TileIndexToWorldPoint(status.draggingTile.TileIndex));

            status.draggingTile = null;
        }
        status.highlightedTile?.SetTileHighlight(false);

        board.SwitchToState(BoardStates.MatchCheck);
    }

    public bool TrySetHoveringTileAsActive()
    {
        status.draggingTile = GetHoveringTile();
        return status.draggingTile != null;
    }

    void DebugRemoveTile(Vector2Int index)
    {
        if (!status.inDragMode && status.HasTileAtIndex(index.x, index.y))
        {
            Debug.Log("RemoveTile " + index);
            //Remove tile  and shuffle down column
            Tile t = status.tiles[index.x, index.y];
            t.Despawn();
            status.tiles[index.x, index.y] = null;

            //Enter falling mode
            //ColumnFallOverEmptyTile(index);
            board.SwitchToState(BoardStates.Fall);
        }

    }

    #region Tile Index manipulation
    public void SwapTilesIndexs(Tile t1, Tile t2)
    {
        Vector2Int t1Index = t1.TileIndex;

        t1.ReassignTileIndex(t2.TileIndex);
        t2.ReassignTileIndex(t1Index);

        status.tiles[t1.TileIndex.x, t1.TileIndex.y] = t1;
        status.tiles[t2.TileIndex.x, t2.TileIndex.y] = t2;
    }
    #endregion

    #region Tile and index checking
    public Tile GetHoveringTile()
    {
        if (status.HoverIndex.x >= 0 && status.HoverIndex.x < status.tiles.GetLength(0) &&
            status.HoverIndex.y >= 0 && status.HoverIndex.y < status.tiles.GetLength(1))
        {
            //Debug.Log("GetHoveringTile: " + hoverIndex.x + ", " + hoverIndex.y);
            return status.tiles[status.HoverIndex.x, status.HoverIndex.y];
        }
        return null;
    }

    public void UpdateHoveringTileIndex()
    {
        //Check in which tile index is the mouse hovering within.
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        status.isMouseOnBoard = 
            mousePos.x > status.bound_left && mousePos.x < status.bound_right &&
            mousePos.y > status.bound_bot && mousePos.y < status.bound_top;

        //offsettedMousePos = new Vector2(mousePos.x + halfBoardSize,
        //mousePos.y + halfBoardSize);
        //Debug.DrawLine(mousePos, offsettedMousePos, Color.blue);

        //return;
        if (status.isMouseOnBoard)
        {
            status.HoverIndex = new Vector2Int(Mathf.FloorToInt((mousePos.x + status.halfBoardSize) / status.cellSize),
                Mathf.FloorToInt((mousePos.y + status.halfBoardSize) / status.cellSize));
        }
    }
    #endregion

    #region Match check

    #endregion

    #region Helper methods
    //Clicking
    bool ClickedLMB => Input.GetMouseButtonDown(0);
    bool IsSelectedTileOfIndex(Vector2Int index) => status.draggingTile != null && status.draggingTile.TileIndex == index;
    void RegisterInitialClickPosition() =>
       status.initialClickPosition = Input.mousePosition;

    //Dragging
    bool ClickReleasedLMB => Input.GetMouseButtonUp(0);
    bool DraggedFarEnoughToEnterDragMode => Vector2.Distance(
       (Vector2)Input.mousePosition, status.initialClickPosition) > Settings.MinDragDist;

    //Releasing
    bool ClickAndDragLMB => Input.GetMouseButton(0);
    bool EnteredNewTile => status.isMouseOnBoard && (status.HoverIndexPrev != status.HoverIndex);
    #endregion
}