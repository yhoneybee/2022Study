using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGenerator = target as MapGenerator;

        if (DrawDefaultInspector())
            if (mapGenerator.isAutoUpdate)
                mapGenerator.GenerateMap();

        if (GUILayout.Button("Generate")) mapGenerator.GenerateMap();
    }
}
