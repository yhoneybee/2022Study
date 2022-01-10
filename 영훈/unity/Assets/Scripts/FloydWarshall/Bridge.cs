using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bridge : MonoBehaviour
{
    public Text txtVertexInfo;
    public LineRenderer lr;
    public Vertex oVertex;
    public Vertex tVertex;
    public int dis = 0;

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

        txtVertexInfo.text = $"{oVertex.ID} >>> {tVertex.ID}\n{dis}";
        transform.position = Vector3.Lerp(oVertex.transform.position, tVertex.transform.position, 0.5f);
        lr.SetPosition(0, oVertex.rtrn.position);
        lr.SetPosition(1, tVertex.rtrn.position);
    }
}
