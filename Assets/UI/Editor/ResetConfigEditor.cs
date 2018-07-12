using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Driver))]
public class ResetConfigEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if (GUILayout.Button("Rewrite config with default.")) {
            DefaultConfigCreator.InitializeDefaults();
        }
    }
}
