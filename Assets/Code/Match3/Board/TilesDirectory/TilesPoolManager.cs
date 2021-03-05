using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesPoolManager : MonoBehaviour
{
    [SerializeField] Tile[] tilePfs;

    List<TileTypes> allTileTypes = new List<TileTypes>();
    Dictionary<TileTypes, Pool> prefabLookUp = new Dictionary<TileTypes, Pool>();

    public float GetTileSize() => tilePfs[0].GetComponent<BoxCollider2D>().size.x;

    public void Initialize()
    {
        //Initialize reference collections
        foreach (var tile in tilePfs)
        {
            //If we haven't stored this key in the look up dictionary, then store it.
            if (!prefabLookUp.ContainsKey(tile.Type))
            {
                allTileTypes.Add(tile.Type);
                prefabLookUp.Add(tile.Type, new Pool(tile.gameObject, transform));
            }
        }
    }

    #region Spawn tile
    public Tile Spawn(TileTypes type)
    {
        GameObject go = prefabLookUp[type].Spawn();
        return go.GetComponent<Tile>();
    }

    public Tile SpawnRandom() => 
        Spawn(allTileTypes[Random.Range(0, allTileTypes.Count)]);

    public Tile SpawnRandomExcept(List<Tile> excepts)
    {
        List<TileTypes> validTypes = new List<TileTypes>(allTileTypes);
        foreach (var e in excepts)
        {
            if (e != null && validTypes.Contains(e.Type)) 
                validTypes.Remove(e.Type);
        }

        return Spawn(allTileTypes[Random.Range(0, allTileTypes.Count)]);
    }
    #endregion
}


//public Tile GetRandomPrefabExcept(List<TileTypes> excepts)
//{
//    List<TileTypes> validTypes = new List<TileTypes>(allTileTypes);
//    foreach (var t in excepts)
//    {
//        validTypes.Remove(t);
//    }
//    return prefabLookUp[validTypes[Random.Range(0, validTypes.Count)]];
//}