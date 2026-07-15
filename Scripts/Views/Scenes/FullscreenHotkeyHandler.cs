// #if UNITY_EDITOR

// using System;
// using System.Reflection;
// using UnityEditor;
// using UnityEngine;

// public class FullscreenHotkeyHandler : MonoBehaviour
// {
//     bool makeFullscreenAtStart = true;
    
//     // Enable fullscreen when starting game
//     void Start() { if (makeFullscreenAtStart) { FullscreenGameView.Toggle(); } }

//     void Update() 
//     {
//         // Toggle fullscreen when hotkey pressed
//         if (Input.GetKeyDown(KeyCode.Backslash))
//         {
//             FullscreenGameView.Toggle();
//         }
//     }
// }

// public static class FullscreenGameView
// {
//     static readonly Type GameViewType = Type.GetType("UnityEditor.GameView,UnityEditor");
//     static readonly PropertyInfo ShowToolbarProperty = GameViewType.GetProperty("showToolbar", BindingFlags.Instance | BindingFlags.NonPublic);
//     static readonly object False = false; // Only box once. This is a matter of principle.

//     static EditorWindow instance;

//     // Exit fullscreen when re-compiling game during Game session (to fix bug where can't leave fullscreen)
//     static FullscreenGameView() { AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload; }
//     private static void OnBeforeAssemblyReload() { if (instance != null) { instance.Close(); instance = null; } }
    
//     [MenuItem("Window/General/Game (Fullscreen) %#&2", priority = 2)]
//     public static void Toggle()
//     {
//         if (GameViewType == null)
//         {
//             Debug.LogError("GameView type not found.");
//             return;
//         }

//         if (ShowToolbarProperty == null)
//         {
//             Debug.LogWarning("GameView.showToolbar property not found.");
//         }

//         if (instance != null)
//         {
//             instance.Close();
//             instance = null;
//         }
//         else
//         {
//             instance = (EditorWindow) ScriptableObject.CreateInstance(GameViewType);

//             ShowToolbarProperty?.SetValue(instance, False);

//             // KHẮC PHỤC LỖI TRÀN: Lấy tỷ lệ scale màn hình của OS (ví dụ: 1.25 hoặc 1.5)
//             float scaleFactor = EditorGUIUtility.pixelsPerPoint;

//             // Chia độ phân giải thực tế cho tỉ lệ scale để ra kích thước cửa sổ chuẩn
//             var width = Screen.currentResolution.width / scaleFactor;
//             var height = Screen.currentResolution.height / scaleFactor;

//             var desktopResolution = new Vector2(width, height);
//             var fullscreenRect = new Rect(Vector2.zero, desktopResolution);
            
//             instance.ShowPopup();
//             instance.position = fullscreenRect;
//             instance.Focus();
//         }
//     }
// }

// #endif