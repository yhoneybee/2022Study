using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISayHelloState
{
    public void Set(FSM fsm);
    public void SayHello();
    public void MoreGood();
    public void MoreBad();
}