using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PathFind : MonoBehaviour
{
    public Dropdown ddFromVertex;
    public Dropdown ddToVertex;
    public Button btnPathFind;

    private void OnEnable()
    {
        if (K.pathFind == null)
        {
            K.pathFind = this;
            gameObject.SetActive(false);
            return;
        }

        var optionDatas = new List<Dropdown.OptionData>();

        foreach (var vertex in K.vertices)
        {
            optionDatas.Add(new Dropdown.OptionData($"Vertex {vertex.ID}"));
        }

        ddFromVertex.ClearOptions();
        ddFromVertex.AddOptions(optionDatas);
        ddToVertex.ClearOptions();
        ddToVertex.AddOptions(optionDatas);

        btnPathFind.onClick.RemoveAllListeners();
        btnPathFind.onClick.AddListener(() =>
        {
            FloydWarshallManager.Instance.FloydWarshall();
            gameObject.SetActive(false);
        });
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
