using UnityEngine;

public static class AdjacentTiles
{
    public static Vector2Int Left => AdjacentDir[0];
    public static Vector2Int Right => AdjacentDir[1];
    public static Vector2Int Up => AdjacentDir[2];
    public static Vector2Int Down => AdjacentDir[3];

    private static Vector2Int[] AdjacentDir = new Vector2Int[] {
        new Vector2Int(-1, 0), //Left
        new Vector2Int(1, 0), //Right
        new Vector2Int(0, 1), //Up
        new Vector2Int(0, -1)}; //down
}