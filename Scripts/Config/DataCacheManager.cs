using UnityEngine;
using UnityEngine.UI;

public class DataCacheManager : MonoBehaviour
{
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        _=GameDataCacheConfig.Instance.LoadDataFirst();
    }
}