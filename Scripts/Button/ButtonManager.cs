using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class ButtonManager : MonoBehaviour
{
    private Button GalleryButton;
    private Button CollectionButton;
    private Button EquipmentsButton;
    private GameObject GalleryPanel;
    private GameObject CollectionPanel;
    private GameObject EquipmentsPanel;
    void Start()
    {
        GalleryButton=UIManager.Instance.GetButton("GalleryButton");
        CollectionButton=UIManager.Instance.GetButton("CollectionButton");
        EquipmentsButton=UIManager.Instance.GetButton("EquipmentsButton");
        GalleryPanel=UIManager.Instance.GetGameObject("GalleryPanel");
        CollectionPanel=UIManager.Instance.GetGameObject("CollectionPanel");
        EquipmentsPanel=UIManager.Instance.GetGameObject("EquipmentsPanel");

        GalleryButton.onClick.AddListener(OnGalleryButtonClick);
        CollectionButton.onClick.AddListener(OnCollectionButtonClick);
        EquipmentsButton.onClick.AddListener(onEquipmentsButtonClick);

        // CloseMainMenuButton.onClick.AddListener(ClosePanel);
        // NextButton.onClick.AddListener(ChangeNextPage);
        // PreviousButton.onClick.AddListener(ChangePreviousPage);
    }

    void Update()
    {

    }
    public void OnGalleryButtonClick()
    {
        GalleryPanel.SetActive(true);
        CollectionPanel.SetActive(false);
        EquipmentsPanel.SetActive(false);
    }
    public void OnCollectionButtonClick()
    {
        GalleryPanel.SetActive(false);
        CollectionPanel.SetActive(true);
        EquipmentsPanel.SetActive(false);
    }
    public void onEquipmentsButtonClick()
    {
        EquipmentsPanel.SetActive(true);
        GalleryPanel.SetActive(false);
        CollectionPanel.SetActive(false);
    }
    
}
