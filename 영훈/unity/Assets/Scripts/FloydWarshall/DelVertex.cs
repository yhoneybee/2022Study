using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DelVertex : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        FloydWarshallManager.Instance.DelVertex();
    }
}
