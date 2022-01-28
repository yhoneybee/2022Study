using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class None : ISayHelloState
{
    public FSM fsm;

    public void Set(FSM fsm)
    {
        this.fsm = fsm;
    }

    public void MoreBad()
    {
        fsm.curState = fsm.states.angry;
    }

    public void MoreGood()
    {
        fsm.curState = fsm.states.fine;
    }

    public void SayHello()
    {
        fsm.txtChat.text = $"æ»≥Á«œººø‰.";
    }
}
