using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesDirectory : MonoBehaviour
{
    [SerializeField] private Tile[] tilePfs;

    List<TileTypes> allTileTypes = new List<TileTypes>();
    Dictionary<TileTypes, Tile> prefabLookUp = new Dictionary<TileTypes, Tile>();

    public Vector2 TileSize { get; private set; }
    public Tile[] TilePfs => tilePfs;
    public List<TileTypes> AllTileTypes => allTileTypes;

    public Tile GetPrefab(TileTypes type) => prefabLookUp[type];

    public Tile GetRandomPrefab() => tilePfs[Random.Range(0, tilePfs.Length)];

    public Tile GetRandomPrefabExcept(Tile except1, Tile except2)
    {
        List<TileTypes> validTypes = new List<TileTypes>(AllTileTypes);
        if (except1 != null && validTypes.Contains(except1.Type))
            validTypes.Remove(except1.Type);
        if (except2 != null && validTypes.Contains(except2.Type))
            validTypes.Remove(except2.Type);
        return prefabLookUp[validTypes[Random.Range(0, validTypes.Count)]];
    }

    public Tile GetRandomPrefabExcept (List<TileTypes> excepts)
    {
        List<TileTypes> validTypes = new List<TileTypes>(AllTileTypes);
        foreach (var t in excepts)
        {
            validTypes.Remove(t);
        }
        return prefabLookUp[validTypes[Random.Range(0, validTypes.Count)]];
    }


    void Awake()
    {
        //Cache
        TileSize = tilePfs[0].SprRenderer.bounds.size;

        //Initialize refernce collections
        foreach (var tile in tilePfs)
        {
            if (!prefabLookUp.ContainsKey(tile.Type) && tile.Type != TileTypes.None)
            {
                allTileTypes.Add(tile.Type);
                prefabLookUp.Add(tile.Type, tile);
            }
        }
    }
}