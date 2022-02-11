using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test03ShaderCPU : MonoBehaviour
{
    public GameObject _gameobject;
    public ComputeShader computeShader;

    public Color color;
    private int kernel_Index;
    private RenderTexture resultTexture;
    
    void Start()
    {
        resultTexture = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGB32);
        resultTexture.enableRandomWrite = true;
        resultTexture.Create();
        kernel_Index = computeShader.FindKernel("CSMain");
        computeShader.SetTexture(kernel_Index, "ResultBufferTexture", resultTexture);
    }

    void Update()
    {
        computeShader.SetVector("Color", color);
        computeShader.SetFloat("TimeVar", Mathf.PingPong(Mathf.Cos(Time.time * 3f), 1f));
        computeShader.Dispatch(kernel_Index, 2048 / 16, 2048 / 16, 1);
        _gameobject.GetComponent<Renderer>().material.mainTexture = resultTexture;
    }
}
