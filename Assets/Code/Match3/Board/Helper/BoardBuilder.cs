using System.Collections.Generic;
using UnityEngine;

//public static class BoardBuilder
//{
//    public static Tile[,] CreateBoard (Board board, TilesPoolManager poolManager, Transform parent)
//    {
//        //Initialize tiles array
//        Tile[,] tiles = new Tile[board.TileCount, board.TileCount];

//        //We cache what's on the left and down below to prevent a match at start.
//        Tile[] previousLeft = new Tile[board.TileCount];
//        Tile previousBelow = null;

//        //Spawn tile prefabs
//        for (int x = 0; x < board.TileCount; x++)
//        {
//            for (int y = 0; y < board.TileCount; y++)
//            {
//                Vector3 spawnPos = Tile.IndexToWorldPoint(x, y, board.StartPoint, board.CellSize);

//                Tile t = poolManager.SpawnRandomExcept(new List<Tile>() { previousBelow, previousLeft[y] });
//                t.transform.position = spawnPos;
//                //Tile t = MonoBehaviour.Instantiate(pfs.GetRandomPrefabExcept(previousBelow, previousLeft[y]), spawnPos, Quaternion.identity, parent);
//                //t.transform.name = x.ToString() + "_"  + y.ToString();
//                t.SetTileIndex(new Vector2Int(x, y));
//                tiles[x, y] = t;

//                //Cache for next loop
//                previousLeft[y] = t;
//                previousBelow = t;
//            }
//        }
//        return tiles;
//    }
//}

                //Tile t = MonoBehaviour.Instantiate(poolManager.GetRandomPrefabExcept(previousBelow, previousLeft[y]), spawnPos, Quaternion.identity, parent);
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