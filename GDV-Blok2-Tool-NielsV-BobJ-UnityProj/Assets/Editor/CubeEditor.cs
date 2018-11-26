using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cube))]
public class CubeEditor : Editor {
    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();

        Cube cube = (Cube)target;

        GUILayout.Label(
            "Oscillates around a base size"
            );

        cube.baseSize = EditorGUILayout.Slider("Size", cube.baseSize, .1f, 2f);
        cube.transform.localScale = Vector3.one * cube.baseSize;

        GUILayout.Space(4);

        GUILayout.BeginVertical("box");

        GUILayout.Label(
            "Pick a random color or reset to white"
            );

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate Color")) {
            cube.GenerateColor();
            Debug.Log("Generated new color");
        }
        if (GUILayout.Button("Reset")) {
            cube.Reset();
            Debug.Log("Reset color to white");
        }

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

    }
}
