using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesPrefabDirectory : MonoBehaviour
{
    [SerializeField] private Tile[] tilePfs;

    List<TileTypes> allTileTypes = new List<TileTypes>();
    Dictionary<TileTypes, Tile> prefabLookUp = new Dictionary<TileTypes, Tile>();

    public float GetTileSize => tilePfs[0].GetComponent<BoxCollider2D>().size.x;

    #region Get prefab
    public Tile GetPrefab(TileTypes type) => prefabLookUp[type];

    public Tile GetRandomPrefab() => tilePfs[Random.Range(0, tilePfs.Length)];

    public Tile GetRandomPrefabExcept(Tile except1, Tile except2)
    {
        List<TileTypes> validTypes = new List<TileTypes>(allTileTypes);
        if (except1 != null && validTypes.Contains(except1.Type))
            validTypes.Remove(except1.Type);
        if (except2 != null && validTypes.Contains(except2.Type))
            validTypes.Remove(except2.Type);
        return prefabLookUp[validTypes[Random.Range(0, validTypes.Count)]];
    }

    public Tile GetRandomPrefabExcept(List<TileTypes> excepts)
    {
        List<TileTypes> validTypes = new List<TileTypes>(allTileTypes);
        foreach (var t in excepts)
        {
            validTypes.Remove(t);
        }
        return prefabLookUp[validTypes[Random.Range(0, validTypes.Count)]];
    }
    #endregion

    public void Initialize()
    {
        //Initialize reference collections
        foreach (var tile in tilePfs)
        {
            //If we haven't stored this key in the look up dictionary, then store it.
            if (!prefabLookUp.ContainsKey(tile.Type))
            {
                allTileTypes.Add(tile.Type);
                prefabLookUp.Add(tile.Type, tile);
            }
        }
    }
}