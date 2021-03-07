using System.Collections;
using UnityEngine;

public class BoardStatus
{
    //Selection status
    public Vector2 initialClickPosition;
    public Tile highlightedTile;
    public Tile draggingTile;
    public bool isMouseOnBoard; //Is mouse hovering over a valid tile
    public bool IsInAnimation;
    public bool inDragMode;
    public Vector2Int HoverIndex;
    public Vector2Int HoverIndexPrev;

    //Board
    public Tile[,] tiles;
    public Tile[] lastSpawnedTileInColumn;  //In each column

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
    public float bound_top { get; private set; } //Upper bound
    public float bound_bot { get; private set; } //Lower bound
    public float bound_left { get; private set; } //Left bound
    public float bound_right { get; private set; } //Right bound
    public float halfBoardSize { get; private set; }

    public bool HasDraggingTile => draggingTile != null;
    public bool HasHighlightedTile => highlightedTile != null;
    public Vector2Int SelectedTileIndex => new Vector2Int(draggingTile.TileIndex.x, draggingTile.TileIndex.y);


    public BoardStatus(int tileCount, float tileSize, float tileGap)
    {
        //Board
        tiles = new Tile[tileCount, tileCount];
        lastSpawnedTileInColumn = new Tile[tileCount];

        //Tile dimensions
        this.tileCount = tileCount;
        this.tileSize = tileSize;
        this.tileGap = tileGap;
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
    }

}