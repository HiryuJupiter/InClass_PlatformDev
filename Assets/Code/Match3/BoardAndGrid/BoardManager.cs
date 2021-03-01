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

    public bool IsShifting { get; private set; }

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
        float startX = transform.position.x;
        float startY = transform.position.y;
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
                Tile newTile = Instantiate(tilesDir.GetRandomPrefabExcept(previousBelow, previousLeft[y]), spawnPos, Quaternion.identity, transform);
                tiles[x, y] = newTile;

                //Cache for next loop
                previousLeft[y] = newTile;
                previousBelow = newTile;
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
