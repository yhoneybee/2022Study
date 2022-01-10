using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class K
{
    public static Vertex selectVertex;
    public static List<Vertex> vertices = new List<Vertex>();
    public static BridgeOption bridgeOption;
    public static List<Bridge> bridges = new List<Bridge>();
    public static readonly int INF = 1000000;
    public static PathFind pathFind;
}
