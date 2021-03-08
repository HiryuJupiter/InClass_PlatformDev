using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* There are 4 possible states: Stationary, Drag, MatchClear, Fall
 You can only enter the other 3 states from Stationary, meaning they have to 
go through Stationary first. Therefore, we simply use a bool isStationary to 
represent the tile's status.
 */

public class Tile : MonoBehaviour, IPoolable
{
    [SerializeField] TileTypes tileType;
    [SerializeField] TextMesh textMeshA;
    [SerializeField] TextMesh textMeshB;

    //Status
    bool isMoving;

    //Class
    TileFeedback            tileFeedback;
    TileDragModule          dragModule;
    TileFallingModule       fallingModule;
    TileMatchModule         matchModule;

    //Cache
    Pool pool;

    public TileTypes TileType => tileType;
    public Vector2Int TileIndex { get; private set; }
    public TileStates TileState { get; private set; } = TileStates.Normal;
    public bool IsMoving => isMoving;
    public void SetIsMoving(bool value) => isMoving = value;

    #region Mono
    void Awake ()
    {
        tileFeedback        = GetComponent<TileFeedback>();
        dragModule          = new TileDragModule(this);
        fallingModule       = new TileFallingModule(this);
        matchModule         = new TileMatchModule(this);
    }
    #endregion

    #region Initial swirl move
    public void InitialSwirlMove(Vector2 targetPosition)
    {
        fallingModule.SetFallingTargetPosition(targetPosition);
        //swirlModule.InitialSwirlMove(targetPosition);
    }
    #endregion

    #region Falling
    public void SetFallingTargetPosition(Vector2 targetPosition)
    {
        fallingModule.SetFallingTargetPosition(targetPosition);
    }
    #endregion

    #region Drag
    public void DragEntry()
    {
        dragModule.DragEntry();
    }

    public void DragUpdate()
    {
        dragModule.DragUpdate();
    }

    public void DragRelease()
    {
        dragModule.DragRelease();
    }

    public void TileSwapMoveToPosition(Vector2 position)
    {
        dragModule.TileSwapMoveToPosition(position);
    }
    #endregion

    // Match
    public void MatchedAndWaitForDespawn()
    {
        matchModule.MatchedAndWaitForDespawn();
    }

    #region Public - IPoolable spawning and despawning
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

    #region Public - Minor method
    //Assign tile index
    public void SetTileIndex(Vector2Int tileIndex)
    {
        TileIndex = tileIndex;
        textMeshA.text = tileIndex.ToString();
    }

    public void ReassignTileIndex(Vector2Int newIndex)
    {
        //Debug.Log("Reasign index. Old: " + newIndex + ", new: " + newIndex);
        TileIndex = newIndex;
        textMeshB.text = newIndex.ToString();
    }

    //Border highlight
    public void SetTileHighlight(bool isOn)
    {
        tileFeedback.SetTileHighlight(isOn);
    }

    public void Reactivation(Vector3 pos)
    {
    }

    public void Reactivation(Vector3 pos, Quaternion rot)
    {
    }

    public bool InNormalMode => TileState == TileStates.Normal;
    #endregion
}