using UnityEngine;
using System.Collections.Generic;

public abstract class BoardStateBase
{
    protected Board board;
    protected BoardStatus status;

    public BoardStateBase(Board board)
    {
        this.board = board;
        status = board.Status;
    }

    public virtual void StateEntry()
    {

    }

    public virtual void TickUpdate() 
    {

    }

    public virtual void TickFixedUpdate()
    {

    }

    public virtual void StateExit()
    {

    }
}