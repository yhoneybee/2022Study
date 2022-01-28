using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Why : ISayHelloState
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
        fsm.txtChat.text = $"왜 그러세요?";
    }
}
