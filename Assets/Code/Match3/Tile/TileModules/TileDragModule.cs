using System.Collections;
using UnityEngine;

public class TileDragModule
{
    const float ZPos_FG = -10f;

    private Tile tile;
    private Transform transform;

    private Vector2 targetPosition;
    private float lerp_t = -1f;

    public TileDragModule(Tile tile)
    {
        this.tile = tile;
        transform = tile.transform;
    }

    public void DragEntry()
    {
        StopDirectLerp();
        SetPositionZ(1f);
    }

    public void DragUpdate()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = ZPos_FG;
        transform.position = p;
    }

    public void DragRelease()
    {
        SetPositionZ(0f);
    }

    public void TileSwapMoveToPosition(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;

        //Reset values used for movement
        lerp_t = 0f;
        SetPositionZ(0f);

        if (!inSwappingAnimation)
        {
            tile.StartCoroutine(DoStraightLerp());
        }
    }

    private IEnumerator DoStraightLerp()
    {
        while (lerp_t < 1f)
        {
            lerp_t += Time.deltaTime * Settings.TileMoveSpeed;
            transform.position = Vector2.Lerp(transform.position, targetPosition,
                lerp_t);
            yield return null;
        }
        lerp_t = -1f;
    }

    private void SetPositionZ(float z)
    {
        //We set the z value to make the dragging tile appear above other tiles
        Vector3 p = transform.position;
        p.z = z;
        transform.position = p;
    }

    private bool inSwappingAnimation => lerp_t > 0f;
    private void StopDirectLerp() => lerp_t = -1;

}