using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryButton : MonoBehaviour
{
    public GameObject panel;
    public void ShowPanel()
    {
        panel.SetActive(true);
    }
}
