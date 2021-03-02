using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    const float TileGaps = 0.1f;
    const float TileOffsets = 135f;

    public static BoardManager Instance;

    [SerializeField] private int tileCountX, tileCountY;

    private TilesDirectory tilesDir;
    private Tile[,] tiles;

    public bool IsComboShifting { get; private set; } //Are all the tiles shuffling after a match-3 combo hit?
    public bool IsAnimatingSwap { get; private set; } //Are we in the swapping animation between 2 tiles?

    public void SwapTiles(Tile tile1, Tile tile2)
    {
        StartCoroutine(DoSwapTile(tile1, tile2));
    }

    IEnumerator DoSwapTile (Tile tile1, Tile tile2)
    {
        Vector3 pos1 = tile1.transform.position;
        Vector3 pos2 = tile2.transform.position;

        Debug.DrawLine(pos1, pos2, Color.red, 5f);

        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            //Lerp their positions
            tile1.transform.position = ParabolicMove.Move(pos1, pos2, t);
            //tile2.transform.position = ParabolicMove.Move(pos2, pos1, t);
            yield return null;
        }

        //then swap their tile index

        //Check if there is match

        //If there is a match, shuffle
    }

    private void Awake()
    {
        Instance = this;
        tilesDir = GetComponent<TilesDirectory>();
    }

    private void Start ()
    {
        CreateBoard();
    }

    private void CreateBoard ()
    {
        //Initialize tiles array
        tiles = new Tile[tileCountX, tileCountY];

        //Prepare for tile spawning
        float startX = -(tilesDir.TileSize.x + TileGaps) * (tileCountX -1) * .5f;
        float startY = -(tilesDir.TileSize.y + TileGaps) * (tileCountY - 1) * .5f;
        Vector2 offset = tilesDir.TileSize;
        float offsetX = offset.x;
        float offsetY = offset.y;

        //We cache what's left and below prevent a match at start
        Tile[] previousLeft = new Tile[tileCountY];

        Tile previousBelow = null;

        //Spawn tile prefabs
        for (int x = 0; x < tileCountX; x++)
        {
            for (int y = 0; y < tileCountY; y++)
            {
                Vector3 spawnPos = new Vector3(
                    startX + (offsetX + TileGaps) * x,
                    startY + (offsetY + TileGaps) * y, 0f);

                //Tile newTile = Instantiate(tilesDir.GetRandomPrefab(), spawnPos, Quaternion.identity, transform);
                Tile t = Instantiate(tilesDir.GetRandomPrefabExcept(previousBelow, previousLeft[y]), spawnPos, Quaternion.identity, transform);
                t.SetTileIndex(new Vector2Int(x, y));
                tiles[x, y] = t;

                //Cache for next loop
                previousLeft[y] = t;
                previousBelow = t;
            }
        }
    }

    #region Initialize 
    void FillBoardWithTile()
    {

    }
    #endregion

    #region Selection
    void SelectTile()
    {

    }

    void RemoveTile()
    {

    }
    #endregion

    #region Move
    void IncrementMove()
    {

    }
    #endregion

    #region Match check
    void CheckForMatch3()
    {

    }
    #endregion

    #region Helpers
    void InitializeTilesArray () => tiles = new Tile[tileCountX, tileCountY];
    #endregion
}
