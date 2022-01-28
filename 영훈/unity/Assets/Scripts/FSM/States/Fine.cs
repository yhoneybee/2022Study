using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fine : ISayHelloState
{
    public FSM fsm;

    public void Set(FSM fsm)
    {
        this.fsm = fsm;
    }

    public void MoreBad()
    {
        fsm.curState = fsm.states.none;
    }

    public void MoreGood()
    {
        fsm.curState = fsm.states.good;
    }

    public void SayHello()
    {
        fsm.txtChat.text = $"æ»≥Á«œººø‰^^";
    }
}
