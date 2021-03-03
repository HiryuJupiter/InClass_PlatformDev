using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoardBuilder))]
[RequireComponent(typeof(TilesDirectory))]
public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    [Header("Tile Generation")]
    [SerializeField] float tileOffsetMultiplier = 0.1f; //The gap offset between tiles.
    [SerializeField] int tilesPerRow = 4;

    [Header("Tile Swapping")]
    [SerializeField] float SwapSpeed = 5f;

    //Status
    Tile[,] tiles;
    Vector2Int hoverIndex;
    bool isMouseOnTile;

    //Ref
    BoardBuilder builder;
    TilesDirectory tilesDirectory;
    MouseHoveringTileIndexChecker mouseHoverTileIndex;
     
    //Cache
    float tileSize;
    float tileGap;

    public bool IsComboShifting { get; private set; } //Are all the tiles shuffling after a match-3 combo hit?
    public bool IsAnimatingSwap { get; private set; } //Are we in the swapping animation between 2 tiles?

    #region MonoBehavior
    private void Awake()
    {
        Instance = this;

        //Ref
        builder = GetComponent<BoardBuilder>();
        tilesDirectory = GetComponent<TilesDirectory>();

        //Cache
        tileSize = tilesDirectory.GetTileColliderWidth;
        tileGap = tileSize * tileOffsetMultiplier;
    }

    private void Start()
    {
        //Create board
        tiles = builder.CreateBoard(tileGap, tileSize, tilesPerRow);

        //Initialize
        mouseHoverTileIndex = new MouseHoveringTileIndexChecker(tileGap, tileSize, new Vector2(builder.StartPointX, builder.StartPointY));
    }

    void Update()
    {
        UpdateMouseHoverIndex();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
    }
    #endregion

    #region Update mouse hover index
    void UpdateMouseHoverIndex () //Get the tile index that the mouse is hovering above
    {
        if (isMouseOnTile = mouseHoverTileIndex.TryGetIndex(ref hoverIndex))
        {

        }
    }
    #endregion

    #region Tile swapping
    public void SwapTiles(Tile tile1, Tile tile2)
    {
        StartCoroutine(DoSwapTile(tile1, tile2));
    }

    IEnumerator DoSwapTile(Tile tile1, Tile tile2)
    {
        Vector3 pos1 = tile1.transform.position;
        Vector3 pos2 = tile2.transform.position;

        Debug.DrawLine(pos1, pos2, Color.red, 5f);

        for (float t = 0; t < 1f; t += Time.deltaTime * SwapSpeed)
        {
            //Lerp their positions
            tile1.transform.position = pos1 + ParabolicMove.Move(pos1, pos2, t);
            tile2.transform.position = pos2 + ParabolicMove.Move(pos2, pos1, t);
            yield return null;
        }

        //then swap their tile index

        //Check if there is match

        //If there is a match, shuffle
    }
    #endregion

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 900, 20), "Mouse position: " + Input.mousePosition);
        GUI.Label(new Rect(20, 40, 900, 20), "isMouseOnTile: " + isMouseOnTile);
        GUI.Label(new Rect(20, 60, 900, 20), "hoverIndex: " + hoverIndex);
        GUI.Label(new Rect(20, 80, 900, 20), "StartingPos: " + builder.StartPointX + ", " + builder.StartPointY);
        GUI.Label(new Rect(20, 100, 900, 20), "mouseWorldPos: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        GUI.Label(new Rect(20, 120, 900, 20), "up: " + mouseHoverTileIndex.up);
        GUI.Label(new Rect(20, 140, 900, 20), "down: " + mouseHoverTileIndex.down);
        GUI.Label(new Rect(20, 160, 900, 20), "left: " + mouseHoverTileIndex.left);
        GUI.Label(new Rect(20, 180, 900, 20), "right: " + mouseHoverTileIndex.right);
    }
}