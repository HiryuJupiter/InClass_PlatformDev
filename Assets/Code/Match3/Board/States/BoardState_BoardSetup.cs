using UnityEngine;
using System.Collections;

public class BoardState_BoardSetup : BoardStateBase
{

    public BoardState_BoardSetup(Board board) : base(board) { }
    public override void StateEntry()
    {
        base.StateEntry();
        board.StartCoroutine(WaitForPiecesToComeToStatic());
    }

    public override void TickUpdate()
    {
        base.TickUpdate();
    }

    public override void StateExit()
    {

    }

    private IEnumerator WaitForPiecesToComeToStatic ()
    {
        while (!AllTilesAreStill())
        {
            yield return null;
        }
        board.SwitchToState(BoardStates.Normal);
    }

    private bool AllTilesAreStill()
    {
        foreach (var tile in board.Status.tiles)
        {
            if (tile == null || tile.IsMoving)
                return false;
        }
        return true;
    }
}