using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UIPrefabDatabase", menuName = "Database/UIPrefabDatabase")]
public class UIPrefabDatabase : ScriptableObject
{
    [System.Serializable]
    public class PrefabEntry
    {
        public string key;                 // Tên gọi trong code (AptitudeSlotPrefab…)
        public GameObject prefab;          // Prefab tương ứng
    }

    public List<PrefabEntry> prefabs = new List<PrefabEntry>();
}