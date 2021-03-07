using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TilesPoolManager))]
public class Board : MonoBehaviour
{
    public static Board Instance;

    [Header("Tile Generation")]
    [SerializeField] private float tileGap = 0.02f; //The gap offset between tiles.
    [SerializeField] private int tileCount = 4; //Tiles per row and collumn
    [SerializeField, Range(0f, 0.2f)] private float cardSpawnInterval = .05f;
    //Classes and components
    private BoardStatus status;
    private TileBuilder tileBuilder;

    //Cache
    private BoardStates currentStateType;
    private BoardStateBase currentStateClass;
    private Dictionary<BoardStates, BoardStateBase> stateClassLookup;

    public BoardStatus Status => status;

    #region MonoBehavior
    private void Awake()
    {
        Instance = this;

        //Tile pool
        TilesPoolManager tilePool = GetComponent<TilesPoolManager>();
        tilePool.Initialize();

        //Status
        status = new BoardStatus(tileCount, tilePool.GetTileSize(), tileGap);

        //Tile builder
        tileBuilder = new TileBuilder(this, tilePool, tileCount);
        tileBuilder.BuildBoard(cardSpawnInterval);

        //FSM
        stateClassLookup = new Dictionary<BoardStates, BoardStateBase>
        {
            {BoardStates.BoardSetup,        new BoardState_BoardSetup(this)},
            {BoardStates.Normal,            new BoardState_Normal(this)},
            {BoardStates.MatchCheck,        new BoardState_MatchCheck(this)},
            {BoardStates.Fall,              new BoardState_Fall(this)},
        };

        currentStateType = BoardStates.BoardSetup;
        currentStateClass = stateClassLookup[currentStateType];
        currentStateClass.StateEntry();
    }

    private void Update()
    {
        currentStateClass.TickUpdate();
        status.HoverIndexPrev = status.HoverIndex;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 900, 20), "currentStateType: " + currentStateType);
        GUI.Label(new Rect(20, 40, 900, 20), "currentStateClass: " + currentStateClass);
        GUI.Label(new Rect(20, 60, 900, 20), "TileSize: " + status.tileSize);
        GUI.Label(new Rect(20, 80, 900, 20), "CellSize: " + status.cellSize);

        GUI.Label(new Rect(20, 120, 900, 20), "isMouseOnBoard: " + status.isMouseOnBoard);
        GUI.Label(new Rect(20, 140, 900, 20), "IsInAnimation: " + status.IsInAnimation);
        GUI.Label(new Rect(20, 160, 900, 20), "inDragMode: " + status.inDragMode);
        GUI.Label(new Rect(20, 180, 900, 20), "hoverIndex: " + status.HoverIndex);

        GUI.Label(new Rect(20, 220, 900, 20), "StartingPos: " + status.startPoint);
        GUI.Label(new Rect(20, 240, 900, 20), "HasSelectedTile: " + status.HasDraggingTile);
        if (status.HasDraggingTile)
            GUI.Label(new Rect(20, 260, 900, 20), "SelectedTileIndex: " + status.SelectedTileIndex);

        GUI.Label(new Rect(20, 300, 900, 20), "mouseWorldPos: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        GUI.Label(new Rect(20, 320, 900, 20), "up: " + status.bound_top);
        GUI.Label(new Rect(20, 340, 900, 20), "down: " + status.bound_bot);
        GUI.Label(new Rect(20, 360, 900, 20), "left: " + status.bound_left);
        GUI.Label(new Rect(20, 380, 900, 20), "right: " + status.bound_right);

        for (int x = 0; x < status.tiles.GetLength(0); x++)
        {
            for (int y = 0; y < status.tiles.GetLength(1); y++)
            {
                if (status.tiles[x, y] != null)
                    GUI.Label(new Rect(400 + x * 50, 120 - y * 20, 500, 20), status.tiles[x, y].TileIndex.ToString());
            }
        }
    }
    #endregion

    #region FSM
    public void SwitchToState(BoardStates newStateType)
    {
        if (currentStateType != newStateType)
        {
            currentStateType = newStateType;

            currentStateClass.StateExit();
            currentStateClass = stateClassLookup[newStateType];
            currentStateClass.StateEntry();
        }
    }
    #endregion

    #region Public 
    public void SpawnNewTile (Vector2Int index)
    {
        tileBuilder.SpawnNewTile(index);
    }
    #endregion

    #region Public - Conversion
    public Vector2 TileIndexToWorldPoint(Vector2Int tileIndex)
    => TileIndexToWorldPoint(tileIndex.x, tileIndex.y);
    public Vector2 TileIndexToWorldPoint(int indexX, int indexY)
        => new Vector2(
            status.startPoint.x + status.cellSize * .5f + status.cellSize * indexX,
            status.startPoint.y + status.cellSize * .5f + status.cellSize * indexY);
    #endregion
}

/*
     public Vector2 IndexToWorldPoint(Vector2Int tileIndex)
    => new Vector2(
            status.startPoint.x + status.cellSize * .5f + status.cellSize * tileIndex.x,
            status.startPoint.y + status.cellSize * .5f + status.cellSize * tileIndex.y);
 */