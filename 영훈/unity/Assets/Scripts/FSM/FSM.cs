using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct States
{
    public States(Angry angry, None none, Fine fine, Good good, Why why)
    {
        this.angry = angry;
        this.none = none;
        this.fine = fine;
        this.good = good;
        this.why = why;
    }
    public ISayHelloState angry;
    public ISayHelloState none;
    public ISayHelloState fine;
    public ISayHelloState good;
    public ISayHelloState why;
}

public class FSM : MonoBehaviour
{
    public Button btnBow;
    public Button btnYes;
    public Button btnNo;
    public Button btnQ;
    public Text txtChat;

    public States states;
    public ISayHelloState curState;

    private void Start()
    {
        states = new States(new Angry(), new None(), new Fine(), new Good(), new Why());
        curState = states.none;

        btnBow.onClick.AddListener(() =>
        {
            curState.SayHello();
        });

        btnYes.onClick.AddListener(() => 
        {
            curState.MoreGood();
        });

        btnNo.onClick.AddListener(() =>
        {
            curState.MoreBad();
        });

        btnQ.onClick.AddListener(() =>
        {
            curState = states.why;
        });

        states.angry.Set(this);
        states.none.Set(this);
        states.fine.Set(this);
        states.good.Set(this);
        states.why.Set(this);
    }
}
