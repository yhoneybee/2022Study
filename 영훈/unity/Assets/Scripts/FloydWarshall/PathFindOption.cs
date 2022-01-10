using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PathFindOption : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (K.vertices.Count < 3) return;
        K.pathFind.gameObject.SetActive(true);
    }
}
