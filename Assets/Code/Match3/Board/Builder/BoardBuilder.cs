using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TilesDirectory))]

public class BoardBuilder : MonoBehaviour
{
    TilesDirectory tilesDirectory;
    public float StartPointX { get; private set; }
    public float StartPointY { get; private set; }

    public Tile[,] CreateBoard (float tileGap, float tileSize, int tileCount)
    {
        //float halfGap = tileGap * .5f;
        float cellSize = tileSize + tileGap;
        float halfCellSize = cellSize * .5f;

        tilesDirectory = GetComponent<TilesDirectory>();

        //Initialize tiles array
        Tile[,] tiles = new Tile[tileCount, tileCount];

        //Find that starting point (at bottom left corner) to prepare for tile spawning
        StartPointX = -cellSize * tileCount * .5f;
        StartPointY = StartPointX;

        //We cache what's on the left and down below to prevent a match at start.
        Tile[] previousLeft = new Tile[tileCount];
        Tile previousBelow = null;

        //Spawn tile prefabs
        for (int x = 0; x < tileCount; x++)
        {
            for (int y = 0; y < tileCount; y++)
            {
                Vector3 spawnPos = new Vector3(
                    StartPointX + halfCellSize + cellSize * x ,
                    StartPointY + halfCellSize + cellSize * y , 0f);

                Tile t = Instantiate(tilesDirectory.GetRandomPrefabExcept(previousBelow, previousLeft[y]), spawnPos, Quaternion.identity, transform);
                t.SetTileIndex(new Vector2Int(x, y));
                tiles[x, y] = t;

                //Cache for next loop
                previousLeft[y] = t;
                previousBelow = t;
            }
        }

        return tiles;
    }
}

/*
 //STANDARD MATCH 3 GENERATION

public Tile[,] CreateBoard (float tileGap, float tileSize, int tileCountX, int tileCountY)
    {
        float halfWidth = tileGap * .5f;

        tilesDirectory = GetComponent<TilesDirectory>();

        //Initialize tiles array
        Tile[,]  tiles = new Tile[tileCountX, tileCountY];

        //Prepare for tile spawning
        StartPointX = -(tileSize + tileGap) * (tileCountX -1) * .5f;
        StartPointY = -(tileSize + tileGap) * (tileCountY - 1) * .5f;

        //We cache what's left and below to prevent a match at start
        Tile[] previousLeft = new Tile[tileCountY];
        Tile previousBelow = null;

        //Spawn tile prefabs
        for (int x = 0; x < tileCountX; x++)
        {
            for (int y = 0; y < tileCountY; y++)
            {
                Vector3 spawnPos = new Vector3(
                    StartPointX + (tileSize + tileGap) * x,
                    StartPointY + (tileSize + tileGap) * y, 0f);

                Tile t = Instantiate(tilesDirectory.GetRandomPrefabExcept(previousBelow, previousLeft[y]), spawnPos, Quaternion.identity, transform);
                t.SetTileIndex(new Vector2Int(x, y));
                tiles[x, y] = t;

                //Cache for next loop
                previousLeft[y] = t;
                previousBelow = t;
            }
        }

        return tiles;
    }
 */