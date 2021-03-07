using UnityEngine;
using System.Collections.Generic;

public class BoardState_NullStandby : BoardStateBase
{

    public BoardState_NullStandby(Board board) : base(board) { }
    public override void StateEntry()
    {
        base.StateEntry();
    }

    public override void TickUpdate()
    {
        base.TickUpdate();
    }

    public override void StateExit()
    {

    }
}