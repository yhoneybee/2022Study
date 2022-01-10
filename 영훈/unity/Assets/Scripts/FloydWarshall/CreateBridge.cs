using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateBridge : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (K.vertices.Count < 2) return;
        K.bridgeOption.gameObject.SetActive(true);
    }
}
