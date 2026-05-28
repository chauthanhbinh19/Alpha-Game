// using UnityEditor;
// using UnityEngine;

// [CustomEditor(typeof(Test))]
// public class TestEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();

//         Test script = (Test)target;

//         if (GUILayout.Button("Generate Data"))
//         {
//             _ = script.InitiateAsync();
//         }
//     }
// }