using System.Collections;
using UnityEngine;

public class TileSwirlModule
{
    Tile tile;
    Transform transform;

    public TileSwirlModule(Tile tile)
    {
        this.tile = tile;
        transform = tile.transform;
    }

    public void InitialSwirlMove(Vector2 targetPosition)
    {
        if (!tile.IsMoving)
        {
            tile.StartCoroutine(DoSwirlMove(targetPosition));
        }
        else
        {
            Debug.Log("Shouldn't happen");
        }
    }

    IEnumerator DoSwirlMove(Vector2 targetPosition)
    {
        Debug.DrawLine(transform.position, targetPosition, Color.red, 10f);
        yield return new WaitForSeconds(0.5f);

        tile.SetIsMoving(true);
        Vector3 start = transform.position;
        for (float t = 0; t < 1f; t += Time.deltaTime * Settings.TileMoveSpeed * 0.25f)
        {
            transform.position = start + ParabolicMove.SmoothMove(start, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition;

        tile.SetIsMoving(false);
    }
}