using System.Collections;
using UnityEngine;

public class ClickSwapping : MonoBehaviour
{
    void ClickSwapTiles(Tile tile1, Tile tile2, float swapSpeed)
    {
        StartCoroutine(DoClickSwapTile(tile1, tile2, swapSpeed));
    }

    IEnumerator DoClickSwapTile(Tile tile1, Tile tile2, float swapSpeed)
    {
        Vector3 pos1 = tile1.transform.position;
        Vector3 pos2 = tile2.transform.position;

        Debug.DrawLine(pos1, pos2, Color.red, 5f);

        for (float t = 0; t < 1f; t += Time.deltaTime * swapSpeed)
        {
            //Lerp their positions
            tile1.transform.position = pos1 + ParabolicMove.LinearMove(pos1, pos2, t);
            tile2.transform.position = pos2 + ParabolicMove.LinearMove(pos2, pos1, t);
            yield return null;
        }

        //then swap their tile index

        //Check if there is match

        //If there is a match, shuffle
    }
}