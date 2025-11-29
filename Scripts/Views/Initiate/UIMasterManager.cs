using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMasterManager : MonoBehaviour
{
    public static UIMasterManager Instance { get; private set; }
    [Header("Master")]
    public GameObject MasterOfBeastPanelPrefab;
    public GameObject MasterOfBeastSlotPrefab;
    public GameObject MasterOfDragonPanelPrefab;
    public GameObject MasterOfDragonSlotPrefab;
    public GameObject MasterOfMagicPanelPrefab;
    public GameObject MasterOfMagicSlotPrefab;
    public GameObject MasterOfMusicPanelPrefab;
    public GameObject MasterOfMusicSlotPrefab;
    public GameObject MasterOfSciencePanelPrefab;
    public GameObject MasterOfScienceSlotPrefab;
    public GameObject MasterOfSpiritPanelPrefab;
    public GameObject MasterOfSpiritSlotPrefab;
    public GameObject MasterOfWeaponPanelPrefab;
    public GameObject MasterOfWeaponSlotPrefab;
    public GameObject MasterOfChemicalPanelPrefab;
    public GameObject MasterOfChemicalSlotPrefab;
    public GameObject MasterOfPhysicalPanelPrefab;
    public GameObject MasterOfPhysicalSlotPrefab;
    public GameObject MasterOfAtomicPanelPrefab;
    public GameObject MasterOfAtomicSlotPrefab;
    public GameObject MasterOfMentalPanelPrefab;
    public GameObject MasterOfMentalSlotPrefab;
    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    public GameObject GetGameObjectMaster(string prefabName)
    {
        switch (prefabName)
        {
            case "MasterOfBeastPanelPrefab":
                return MasterOfBeastPanelPrefab;
            case "MasterOfBeastSlotPrefab":
                return MasterOfBeastSlotPrefab;

            case "MasterOfDragonPanelPrefab":
                return MasterOfDragonPanelPrefab;
            case "MasterOfDragonSlotPrefab":
                return MasterOfDragonSlotPrefab;

            case "MasterOfMagicPanelPrefab":
                return MasterOfMagicPanelPrefab;
            case "MasterOfMagicSlotPrefab":
                return MasterOfMagicSlotPrefab;

            case "MasterOfMusicPanelPrefab":
                return MasterOfMusicPanelPrefab;
            case "MasterOfMusicSlotPrefab":
                return MasterOfMusicSlotPrefab;

            case "MasterOfSciencePanelPrefab":
                return MasterOfSciencePanelPrefab;
            case "MasterOfScienceSlotPrefab":
                return MasterOfScienceSlotPrefab;

            case "MasterOfSpiritPanelPrefab":
                return MasterOfSpiritPanelPrefab;
            case "MasterOfSpiritSlotPrefab":
                return MasterOfSpiritSlotPrefab;

            case "MasterOfWeaponPanelPrefab":
                return MasterOfWeaponPanelPrefab;
            case "MasterOfWeaponSlotPrefab":
                return MasterOfWeaponSlotPrefab;

            case "MasterOfChemicalPanelPrefab":
                return MasterOfChemicalPanelPrefab;
            case "MasterOfChemicalSlotPrefab":
                return MasterOfChemicalSlotPrefab;

            case "MasterOfPhysicalPanelPrefab":
                return MasterOfPhysicalPanelPrefab;
            case "MasterOfPhysicalSlotPrefab":
                return MasterOfPhysicalSlotPrefab;

            case "MasterOfAtomicPanelPrefab":
                return MasterOfAtomicPanelPrefab;
            case "MasterOfAtomicSlotPrefab":
                return MasterOfAtomicSlotPrefab;

            case "MasterOfMentalPanelPrefab":
                return MasterOfMentalPanelPrefab;
            case "MasterOfMentalSlotPrefab":
                return MasterOfMentalSlotPrefab;

            default:
                Debug.LogWarning($"Prefab name '{prefabName}' not found!");
                return null;
        }
    }
}
