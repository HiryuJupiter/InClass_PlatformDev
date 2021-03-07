using System.Collections;
using UnityEngine;

public class TileMatchModule
{
    Tile tile;
    Transform transform;
    public TileMatchModule(Tile tile)
    {
        this.tile = tile;
        transform = tile.transform;
    }

    public void MatchedAndWaitForDespawn ()
    {

    }
}