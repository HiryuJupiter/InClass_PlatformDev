using System.Collections;
using UnityEngine;

public enum TileTypes { Up, Down, Left, Right }

public class Tile : MonoBehaviour
{
    const float ZPos_FG = -10f;

    [SerializeField] GameObject selectionBorder;
    [SerializeField] TileTypes type;
    [SerializeField] TextMesh textMeshA;
    [SerializeField] TextMesh textMeshB;

    //Status
    Vector2Int tileIndex;
    Vector2 targetPosition;
    float t_DirectLerp = -1f; //Lerp t 

    public TileTypes Type => type;
    public Vector2Int TileIndex => tileIndex;

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
    public void DirectLerpMoveTo(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
        bool inDirectMove = t_DirectLerp > 0f;
        t_DirectLerp = 0f;
        SetPositionZ(0f);

        if (!inDirectMove)
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

    #region Conversion
    public static Vector2 IndexToWorldPoint(Vector2Int tileIndex, Vector2 StartPoint, float cellSize)
    => new Vector2(
            StartPoint.x + cellSize * .5f + cellSize * tileIndex.x,
            StartPoint.y + cellSize * .5f + cellSize * tileIndex.y);
    public static Vector2 IndexToWorldPoint(int indexX, int indexY, Vector2 StartPoint, float cellSize)
        => new Vector2(
            StartPoint.x + cellSize * .5f + cellSize * indexX,
            StartPoint.y + cellSize * .5f + cellSize * indexY);
    #endregion
}