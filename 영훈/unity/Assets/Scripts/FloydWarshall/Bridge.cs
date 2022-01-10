using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum eSTATE
{
    Right,
    Reverce,
    BothSides,
    End,
}

public class Bridge : MonoBehaviour, IPointerClickHandler
{
    public Text txtVertexInfo;
    public LineRenderer lr;
    public Vertex oVertex;
    public Vertex tVertex;
    public int dis = 0;
    public eSTATE state = eSTATE.Right;

    private void Start()
    {
        lr.startWidth = .05f;
        lr.endWidth = .05f;
        lr.startColor = Color.white;
        lr.endColor = Color.white;
    }

    private void FixedUpdate()
    {
        if (!oVertex || !tVertex) return;

        txtVertexInfo.text = state switch
        {
            eSTATE.Right => $"{oVertex.ID} >> {tVertex.ID}\n{dis}",
            eSTATE.Reverce => $"{oVertex.ID} << {tVertex.ID}\n{dis}",
            eSTATE.BothSides => $"{oVertex.ID} <|> {tVertex.ID}\n{dis}",
            _ => "",
        };
        transform.position = Vector3.Lerp(oVertex.transform.position, tVertex.transform.position, 0.5f);
        lr.SetPosition(0, oVertex.rtrn.position);
        lr.SetPosition(1, tVertex.rtrn.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        K.selectBridge = this;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            K.bridgeOption.SetDistance();
        }
        else
        {
            state++;
            state = (eSTATE)((int)state % (int)eSTATE.End);
        }
    }
}
