using System.Collections;
using UnityEngine;

public class BoardStatus
{
    //Board
    public Tile[,] tiles;

    //Selection status
    public Vector2Int hoverIndex;
    public Tile selectedTile;
    public bool isMouseOnBoard; //Is mouse hovering over a valid tile
    public bool IsInAnimation;
    public Vector2 offsettedMousePos;

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


    int indexOffset;
    float halfBoardSize;
    Vector2 halfBoardOffset;

    public bool HasSelectedTile => selectedTile != null;
    public Vector2 SelectedTileIndex => new Vector2Int(selectedTile.TileIndex.x, selectedTile.TileIndex.y);


    public bool IsSelectedTileOfIndex(Vector2Int index) => selectedTile != null && selectedTile.TileIndex == index;

    public bool TrySetHoveringTileAsActive()
    {
        try
        {
            selectedTile = tiles[hoverIndex.x, hoverIndex.y];
            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log("Tile not found. hoverIndex = " + hoverIndex + ". tiles array size = " +
                tiles.GetLength(0) + "," + tiles.GetLength(1));
            selectedTile = null;
            return false;
        }
    }

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

        Debug.DrawLine(new Vector2(left, down), new Vector2(left, up), Color.green, 30f);
        Debug.DrawLine(new Vector2(left, up), new Vector2(right, up), Color.red, 30f);
        Debug.DrawLine(new Vector2(right, up), new Vector2(right, down), Color.white, 30f);
        Debug.DrawLine(new Vector2(left, down), new Vector2(right, down), Color.black, 30f);
       

        //Cache
        //indexOffset = Mathf.CeilToInt(tileCount / 2f);
        halfBoardSize = right;
        halfBoardOffset = new Vector2(halfBoardSize, halfBoardSize);

        Debug.DrawLine(new Vector2(-halfBoardSize, -halfBoardSize),
           new Vector2(halfBoardSize, halfBoardSize), Color.cyan, 30f);
    }

    public void UpdateHoveringTileIndex()
    {
        //Check in which tile index is the mouse hovering within.
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        isMouseOnBoard = mousePos.x > left && mousePos.x < right &&
            mousePos.y > down && mousePos.y < up;

        offsettedMousePos = new Vector2(mousePos.x + halfBoardSize,
        mousePos.y + halfBoardSize);
        //Debug.DrawLine(mousePos, offsettedMousePos, Color.blue);

        //return;
        if (isMouseOnBoard)
        {
            hoverIndex.x = Mathf.FloorToInt((mousePos.x + halfBoardSize) / cellSize);
            hoverIndex.y = Mathf.FloorToInt((mousePos.y + halfBoardSize) / cellSize);
        }
    }
}