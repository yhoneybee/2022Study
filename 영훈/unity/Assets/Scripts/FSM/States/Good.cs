using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Good : ISayHelloState
{
    public FSM fsm;

    public void Set(FSM fsm)
    {
        this.fsm = fsm;
    }

    public void MoreBad()
    {
        fsm.curState = fsm.states.fine;
    }

    public void MoreGood()
    {
        fsm.txtChat.text = $"�ȳ��Ͻʴϱ� ����~^^";
    }

    public void SayHello()
    {
        fsm.txtChat.text = $"�ȳ��ϼ���~^^";
    }
}
