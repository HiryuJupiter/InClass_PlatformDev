using UnityEngine;
using System.Collections.Generic;

public class BoardState_Fall : BoardStateBase
{
    public BoardState_Fall(Board board) : base(board) { }

    public override void StateEntry()
    {
        base.StateEntry();
        //loop and find all empty slots on the board, 
        
        //create new tiles to 
        //e.g. For loop running starting from the bottom row, check to fill all empty tiles

        //Tag all the tiles, wait until all tiles are rested, then go to BoardState_Match
    }

    public override void TickUpdate()
    {
        base.TickUpdate();
    }

    public override void StateExit()
    {

    }

    void FillEmptyTile(Vector2Int emptySlot)
    {
        if (!IsTileInBound(emptySlot) || HasTileHere(emptySlot))
        {
            Debug.Log("Exit shuffle down, at index " + emptySlot);
            return;
        }

        //Finding the next solid tile
        Vector2Int current = emptySlot;
        current.y++;

        while (IsTileInBound(current))
        {
            //If we found a tile, then tell this tile to shuffle down
            if (HasTileHere(current))
            {
                Debug.Log("HasTileHere " + current);
                ShuffleTileToLowestEmptyIndex(current);
                break;
            }

            //Spawn current slot is at the top edge, then spwn new tile
            if (IsTileLocatedAtTopEdge(current))
            {
                Debug.Log("SpawnNewTile targeting at " + current);

                board.SpawnNewTile(emptySlot);
                break;
            }
            current.y++;
        }

        //Recursively tell the next tile above, which might've just shuffled down,
        //to see if it needs filling.
        FillEmptyTile(emptySlot + new Vector2Int(0, 1));
    }

    void ShuffleTileToLowestEmptyIndex(Vector2Int index)
    {
        //Set the tile's index to the lowest empty slot, 
        int lowestY = index.y - 1;
        int x = index.x;
        int y = index.y;

        while (lowestY > 0 && !HasTileHere(new Vector2Int(x, lowestY - 1)))
        {
            lowestY--;
        }

        //then tell it to linearly lerp down
        Tile tile = status.tiles[x, y];
        tile.SetTileIndex(new Vector2Int(x, lowestY));
        status.tiles[x, lowestY] = tile;
        status.tiles[x, y] = null;

        Vector2 tgtPos = board.TileIndexToWorldPoint(new Vector2Int(x, lowestY));
        tile.SetFallingTargetPosition(tgtPos);
    }

    bool HasTileHere(Vector2Int index) => status.tiles[index.x, index.y] != null;
    void VacateSlot(Vector2Int index) => status.tiles[index.x, index.y] = null;
    bool IsTileLocatedAtTopEdge(Vector2Int index) => index.y < status.tiles.GetLength(1);
    bool IsTileInBound(Vector2Int index) => index.x < status.tiles.GetLength(0) && index.y < status.tiles.GetLength(1);
}