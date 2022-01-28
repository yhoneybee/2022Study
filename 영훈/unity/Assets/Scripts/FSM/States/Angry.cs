using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angry : ISayHelloState
{
    public FSM fsm;

    public void Set(FSM fsm)
    {
        this.fsm = fsm;
    }

    public void MoreBad()
    {
        fsm.txtChat.text = $"具捞 肮さあ具!";
    }

    public void MoreGood()
    {
        fsm.curState = fsm.states.none;
    }

    public void SayHello()
    {
        fsm.txtChat.text = $"具 构窍衬";
    }
}
