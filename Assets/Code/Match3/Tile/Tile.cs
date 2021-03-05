using System.Collections;
using UnityEngine;

public enum TileTypes { Up, Down, Left, Right }

enum TileMode { LockedInFalling, }

public class Tile : MonoBehaviour, IPoolable
{
    const float ZPos_FG = -10f;
    const float MaxFallSpeed = -0.05f;


    [SerializeField] GameObject selectionBorder;
    [SerializeField] TileTypes type;
    [SerializeField] TextMesh textMeshA;
    [SerializeField] TextMesh textMeshB;

    //Status
    Vector2Int tileIndex;
    Vector2 targetPosition;
    float t_DirectLerp = -1f; //Lerp t 
    bool lockedInFallingAnimation;
    Vector2 fallVelocity;

    //Cache
    Pool pool;

    public TileTypes Type => type;
    public Vector2Int TileIndex => tileIndex;
    public bool IsTileLocked { get; private set; }
    public bool LockedInFallingAnimation => lockedInFallingAnimation;

    #region Assign index
    public void SetTileIndex(Vector2Int tileIndex)
    {
        this.tileIndex = tileIndex;
        textMeshA.text = tileIndex.ToString();
    }

    public void ReassignTileIndex(Vector2Int newIndex)
    {
        //Debug.Log("Reasign index. Old: " + newIndex + ", new: " + newIndex);
        this.tileIndex = newIndex;
        textMeshB.text = newIndex.ToString();
    }
    #endregion

    #region Pool
    public void InitialActivation(Pool pool)
    {
        this.pool = pool;
    }

    public void Reactivation()
    {

    }

    public void Despawn()
    {
        pool.Despawn(gameObject);
    }
    #endregion

    #region Drag state
    public void DragState_Enter()
    {
        StopDirectLerp();
        SetPositionZ(1f);
    }

    public void DragState_Update()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = ZPos_FG;
        transform.position = p;
    }

    public void DragState_Exit()
    {
        SetPositionZ(0f);
    }
    #endregion

    #region Highlight
    public void HighLightTile(bool isTrue)
    {
        selectionBorder.SetActive(isTrue);
    }
    #endregion

    #region Move
    public void SetFallingTargetPosition(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
        if (!lockedInFallingAnimation)
        { 
            if ((Vector2)transform.position != targetPosition)
            {
                StartCoroutine(DoFall());
            }
        }
    }

    IEnumerator DoFall()
    {
        StopDirectLerp();
        lockedInFallingAnimation = true;

        //Move towards target position while preventing overshoot
        while ((transform.position.y + fallVelocity.y * Time.deltaTime) >
            targetPosition.y)
        {
            fallVelocity.y = fallVelocity.y > MaxFallSpeed ?
                 fallVelocity.y - Settings.TileFallAcceleration * Time.deltaTime :
                 MaxFallSpeed;
            transform.Translate(fallVelocity);
            yield return null;
        }

        lockedInFallingAnimation = false;

        //Do bouncing animation while this tile is not asked to perform direct lerp
        //while (!inSwappingAnimation)
        //{
        //    //do bounce
        //    break;
        //}

        //Check for a match

        fallVelocity = Vector2.zero;
    }

    public void TileSwapMoveToPosition(Vector2 targetPosition) //Used for tile swapping
    {
        this.targetPosition = targetPosition;
        t_DirectLerp = 0f;
        SetPositionZ(0f);

        if (!inSwappingAnimation)
        {
            //StartCoroutine(DoParabolicLerp());
            StartCoroutine(DoDirectLerp());
        }
    }

    IEnumerator DoParabolicLerp()
    {
        Vector3 start = transform.position;
        while (t_DirectLerp < 1f)
        {
            t_DirectLerp += Time.deltaTime * Settings.TileMoveSpeed;
            transform.position = start + ParabolicMove.SmoothMove(start, targetPosition, t_DirectLerp);
            yield return null;
        }
        transform.position = targetPosition;

        t_DirectLerp = -1f;
    }

    IEnumerator DoDirectLerp()
    {
        while (t_DirectLerp < 1f)
        {
            t_DirectLerp += Time.deltaTime * Settings.TileMoveSpeed;
            transform.position = Vector2.Lerp(transform.position, targetPosition,
                t_DirectLerp);
            yield return null;
        }
        t_DirectLerp = -1f;
    }
    void StopDirectLerp() => t_DirectLerp = -1;
    void SetPositionZ(float z)
    {
        Vector3 p = transform.position;
        p.z = z;
        transform.position = p;
    }
    #endregion



    #region Helper
    bool inSwappingAnimation => t_DirectLerp > 0f;
    #endregion
}