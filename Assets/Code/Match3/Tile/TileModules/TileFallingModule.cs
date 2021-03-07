using System.Collections;
using UnityEngine;

public class TileFallingModule
{
    const float MaxFallSpeed = -0.05f;

    Vector2 fallVelocity = Vector2.zero;
    Vector2 targetPosition;

    Tile tile;
    Transform transform;
    public TileFallingModule(Tile tile)
    {
        this.tile = tile;
        transform = tile.transform;
    }

    public void SetFallingTargetPosition(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;

        if (!tile.IsMoving)
        {
            tile.StartCoroutine(DoFall(targetPosition));
        }
        else
        {
            Debug.Log("Shouldn't happen");
        }
    }

    IEnumerator DoFall(Vector2 targetPosition)
    {
        tile.SetIsMoving(true);

        while (CurrentFallVelocityWillNotCauseOvershoot)
        {
            fallVelocity.y = NotAtMaxFallSpeed ? GetIncreasedFallSpeed() : MaxFallSpeed;
            transform.Translate(fallVelocity);
            yield return null;
        }

        transform.position = targetPosition;
        tile.SetIsMoving(false);

        //Do bouncing animation while this tile is not asked to perform direct lerp
        //while (!inSwappingAnimation)
        //{
        //    //do bounce
        //    break;
        //}

        fallVelocity = Vector2.zero;
    }

    bool CurrentFallVelocityWillNotCauseOvershoot => (transform.position.y + fallVelocity.y * Time.deltaTime) >
            targetPosition.y;
    float GetIncreasedFallSpeed() => fallVelocity.y - Settings.TileFallAcceleration * Time.deltaTime;
    bool NotAtMaxFallSpeed => fallVelocity.y > MaxFallSpeed;
}