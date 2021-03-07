using System.Collections;
using UnityEngine;

public class TileDragModule
{
    const float ZPos_FG = -10f;

    Tile tile;
    Transform transform;

    Vector2 targetPosition;
    float lerp_t = -1f;

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

    IEnumerator DoStraightLerp()
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

    void SetPositionZ(float z)
    {
        //We set the z value to make the dragging tile appear above other tiles
        Vector3 p = transform.position;
        p.z = z;
        transform.position = p;
    }

    bool inSwappingAnimation => lerp_t > 0f;
    void StopDirectLerp() => lerp_t = -1;

}