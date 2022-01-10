using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BridgeOption : MonoBehaviour
{
    public InputField inputDistance;
    public Dropdown ddOriginVertex;
    public Dropdown ddToVertex;
    public Button btnCreate;

    private void OnEnable()
    {
        if (K.bridgeOption == null)
        {
            K.bridgeOption = this;
            gameObject.SetActive(false);
            return;
        }

        var optionDatas = new List<Dropdown.OptionData>();

        foreach (var vertex in K.vertices)
        {
            optionDatas.Add(new Dropdown.OptionData($"Vertex {vertex.ID}"));
        }

        ddOriginVertex.ClearOptions();
        ddOriginVertex.AddOptions(optionDatas);
        ddToVertex.ClearOptions();
        ddToVertex.AddOptions(optionDatas);

        btnCreate.onClick.RemoveAllListeners();
        btnCreate.onClick.AddListener(() =>
        {
            if (inputDistance.text == "") return;
            FloydWarshallManager.Instance.CreateBridge();
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
