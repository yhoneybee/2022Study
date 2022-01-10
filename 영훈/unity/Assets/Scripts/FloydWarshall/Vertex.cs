using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Vertex : MonoBehaviour, IDragHandler
{
    public RectTransform rtrn;
    public Text txtVertexID;
    public int ID = 0;

    private void FixedUpdate()
    {
        txtVertexID.text = $"{ID}";
    }

    public void OnDrag(PointerEventData eventData)
    {
        K.selectVertex = this;
        rtrn.anchoredPosition = (eventData.position) - new Vector2(Screen.width / 2, Screen.height / 2);
    }
}
