

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FloydWarshallManager : Singletone<FloydWarshallManager>
{
    [SerializeField] private Text txtVertexCount;
    [SerializeField] private Bridge originBridge;
    [SerializeField] private RectTransform rtrnBridgeParent;
    [SerializeField] private Text txtPath;

    private int index = 0;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnVertex();
            }

            for (int i = 0; i < 9; i++)
            {
                CreateBridge(true);
            }

            K.bridges[0].oVertex = K.vertices[0];
            K.bridges[0].tVertex = K.vertices[1];
            K.bridges[0].dis = 3;

            K.bridges[1].oVertex = K.vertices[0];
            K.bridges[1].tVertex = K.vertices[2];
            K.bridges[1].dis = 8;

            K.bridges[2].oVertex = K.vertices[0];
            K.bridges[2].tVertex = K.vertices[4];
            K.bridges[2].dis = -4;

            K.bridges[3].oVertex = K.vertices[1];
            K.bridges[3].tVertex = K.vertices[4];
            K.bridges[3].dis = 7;

            K.bridges[4].oVertex = K.vertices[1];
            K.bridges[4].tVertex = K.vertices[3];
            K.bridges[4].dis = 1;

            K.bridges[5].oVertex = K.vertices[2];
            K.bridges[5].tVertex = K.vertices[1];
            K.bridges[5].dis = 4;

            K.bridges[6].oVertex = K.vertices[3];
            K.bridges[6].tVertex = K.vertices[0];
            K.bridges[6].dis = 2;

            K.bridges[7].oVertex = K.vertices[3];
            K.bridges[7].tVertex = K.vertices[2];
            K.bridges[7].dis = -5;

            K.bridges[8].oVertex = K.vertices[4];
            K.bridges[8].tVertex = K.vertices[3];
            K.bridges[8].dis = 6;
        }
    }

    public void SpawnVertex()
    {
        var vertex = ObjPool.GetObj(Vector3.zero, ObjPool.Instance.transform, false);
        vertex.ID = ++index;
        K.vertices.Add(vertex);
        txtVertexCount.text = $"정점 : {K.vertices.Count}";
    }

    public void DelVertex()
    {
        K.vertices.Remove(K.selectVertex);
        ObjPool.ReturnObj(K.selectVertex);
        txtVertexCount.text = $"정점 : {K.vertices.Count}";
    }

    public void CreateBridge(bool isJustCreate = false)
    {
        var bridge = Instantiate(originBridge, rtrnBridgeParent);
        if (!isJustCreate)
        {
            bridge.oVertex = K.vertices.Find(x => x.ID == K.bridgeOption.ddOriginVertex.captionText.text.Remove(0, 7)[0] - '0');
            bridge.tVertex = K.vertices.Find(x => x.ID == K.bridgeOption.ddToVertex.captionText.text.Remove(0, 7)[0] - '0');
            bridge.dis = System.Convert.ToInt32(K.bridgeOption.inputDistance.text);
        }
        K.bridges.Add(bridge);
    }

    public void FloydWarshall()
    {
        List<List<int>> w = new List<List<int>>();
        for (int i = 0; i < K.vertices.Count; i++) w.Add(Enumerable.Repeat(K.INF, K.vertices.Count).ToList());


        List<List<int>> pi = new List<List<int>>();
        for (int i = 0; i < K.vertices.Count; i++) pi.Add(Enumerable.Repeat(0, K.vertices.Count).ToList());

        foreach (var bridge in K.bridges)
        {
            if (bridge.state == eSTATE.Right || bridge.state == eSTATE.BothSides)
            {
                w[bridge.oVertex.ID - 1][bridge.tVertex.ID - 1] = bridge.dis;
                pi[bridge.oVertex.ID - 1][bridge.tVertex.ID - 1] = bridge.oVertex.ID;
            }

            if (bridge.state == eSTATE.Reverce || bridge.state == eSTATE.BothSides)
            {
                w[bridge.tVertex.ID - 1][bridge.oVertex.ID - 1] = bridge.dis;
                pi[bridge.tVertex.ID - 1][bridge.oVertex.ID - 1] = bridge.tVertex.ID;
            }
        }

        for (int i = 0; i < K.vertices.Count; i++)
            w[i][i] = 0;

        List<List<int>> d = new List<List<int>>(w);

        for (int k = 0; k < K.vertices.Count; k++)
        {
            for (int i = 0; i < K.vertices.Count; i++)
            {
                for (int j = 0; j < K.vertices.Count; j++)
                {
                    if (d[i][j] > d[i][k] + d[k][j])
                    {
                        d[i][j] = d[i][k] + d[k][j];
                        pi[i][j] = pi[k][j];
                    }
                }
            }
        }

        Vertex from = K.vertices.Find(x => x.ID == K.pathFind.ddFromVertex.captionText.text.Remove(0, 7)[0] - '0');
        Vertex to = K.vertices.Find(x => x.ID == K.pathFind.ddToVertex.captionText.text.Remove(0, 7)[0] - '0');

        int fromID = from.ID;
        int toID = to.ID;

        fromID--;
        toID--;

        Stack<int> path = new Stack<int>();
        path.Push(toID);
        while (pi[fromID][toID] != fromID + 1)
        {
            toID = pi[fromID][toID] - 1;
            path.Push(toID);
        }
        path.Push(fromID);

        txtPath.text = "";
        string log = $"{path.Pop() + 1}";
        txtPath.text = log;

        System.Console.Write(log);

        string temp;
        while (path.Count > 0)
        {
            temp = $" -> {path.Pop() + 1}";
            log += temp;
            txtPath.text = log;
            System.Console.Write(temp);
        }

        System.Console.WriteLine();
    }
}
