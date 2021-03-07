using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class TileBuilder
{
    Board board;
    BoardStatus status;
    TilesPoolManager tilePool;
    int tileCount;

    public TileBuilder(Board board, TilesPoolManager tilePool, int tileCount)
    {
        this.board = board;
        this.status = board.Status;
        this.tilePool = tilePool;
        this.tileCount = tileCount;
    }

    public void BuildBoard()
    {
        //We will cache what's on the left and below to prevent a match 3 on start.
        Tile[] previousLeft = new Tile[tileCount];
        Tile previousBelow = null;

        //Spawn tile prefabs
        for (int x = 0; x < tileCount; x++)
        {
            for (int y = 0; y < tileCount; y++)
            {
                List<TileTypes> tilesToAvoid = new List<TileTypes>();
                if (previousLeft[y] != null) tilesToAvoid.Add(previousLeft[y].TileType);
                if (previousBelow != null) tilesToAvoid.Add(previousBelow.TileType);

                Tile t = SpawnNewTile(new Vector2Int(x, y), tilesToAvoid);
                t.InitialSwirlMove(board.TileIndexToWorldPoint(t.TileIndex));

                //Cache for next loop
                previousLeft[y] = t;
                previousBelow = t;
            }
        }
    }

    public Tile SpawnNewTile(Vector2Int targetSlot, List<TileTypes> tileTypesToAvoid)
    {
        Tile t = tilePool.SpawnRandomExcept(tileTypesToAvoid);
        return InitializeNewlySpawnedTile(t, targetSlot);
    }

    public Tile SpawnNewTile(Vector2Int targetSlot)
    {
        Tile t = tilePool.SpawnRandom();
        return InitializeNewlySpawnedTile(t, targetSlot);
    }

    Tile InitializeNewlySpawnedTile(Tile t, Vector2Int targetSlot)
    {
        //Spawn position calculation
        t.transform.position = GetTileSpawnPoint(targetSlot.x);

        //Assign tile properties
        //t.SetFallingTargetPosition(IndexToWorldPoint(targetSlot.x, targetSlot.y));
        t.transform.name = targetSlot.x.ToString() + "_" + targetSlot.y.ToString();
        t.SetTileIndex(targetSlot);
        status.tiles[targetSlot.x, targetSlot.y] = t;

        //Cache 
        status.lastSpawnedTileInColumn[targetSlot.x] = t;

        return t;
    }

    Vector2 GetTileSpawnPoint(int xColumnIndex)
    {
        Vector2 defaultSpawnPoint = new Vector2(
        status.startPoint.x + status.cellSize * .5f + status.cellSize * xColumnIndex,
        status.startPoint.y + status.cellSize * .5f + status.cellSize * tileCount);

        //See if the last tile spawned in this column is above the default spawn point
        Tile previousTile = status.lastSpawnedTileInColumn[xColumnIndex];
        if (previousTile != null && defaultSpawnPoint.y < previousTile.transform.position.y + status.cellSize)
            return (Vector2)previousTile.transform.position + new Vector2(0f, status.cellSize);
        else
        {
            return defaultSpawnPoint;
        }
    }
}

//List<Tile> tilesToAvoid = new List<Tile>() { previousBelow, previousLeft[y] };
//List<TileTypes> tileTypesToAvoid = tilesToAvoid.Where(x => x != null).Select(x => x.Type).ToList();

//Tile t = poolManager.SpawnRandomExcept(new List<Tile>() { previousBelow, previousLeft[y] });
//t.transform.position = spawnPos;
//t.transform.name = x.ToString() + "_" + y.ToString();
//t.SetTileIndex(new Vector2Int(x, y));
//tiles[x, y] = t;