using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class ButtonManagement : MonoBehaviour
{
    public Button GalleryButton;
    public Button CollectionButton;
    public Button EquipmentsButton;
    public GameObject GalleryPanel;
    public GameObject CollectionPanel;
    public GameObject EquipmentsPanel;
    void Start()
    {
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
