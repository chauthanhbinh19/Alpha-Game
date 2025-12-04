using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;
public class ScienceFictionManager : MonoBehaviour
{
    public static ScienceFictionManager Instance { get; private set; }
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
    public void GetScienceFictionButton(Transform scienceFictionPanel)
    {
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_1, scienceFictionPanel, async () => await ReactorNumber1Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_2, scienceFictionPanel, async () => await ReactorNumber2Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_3, scienceFictionPanel, async () => await ReactorNumber3Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_4, scienceFictionPanel, async () => await ReactorNumber4Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_5, scienceFictionPanel, async () => await ReactorNumber5Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_6, scienceFictionPanel, async () => await ReactorNumber6Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_7, scienceFictionPanel, async () => await ReactorNumber7Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_8, scienceFictionPanel, async () => await ReactorNumber8Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_9, scienceFictionPanel, async () => await ReactorNumber9Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_10, scienceFictionPanel, async () => await ReactorNumber10Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_11, scienceFictionPanel, async () => await ReactorNumber11Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_12, scienceFictionPanel, async () => await ReactorNumber12Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_13, scienceFictionPanel, async () => await ReactorNumber13Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_14, scienceFictionPanel, async () => await ReactorNumber14Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_15, scienceFictionPanel, async () => await ReactorNumber15Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_16, scienceFictionPanel, async () => await ReactorNumber16Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_17, scienceFictionPanel, async () => await ReactorNumber17Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_18, scienceFictionPanel, async () => await ReactorNumber18Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_19, scienceFictionPanel, async () => await ReactorNumber19Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_20, scienceFictionPanel, async () => await ReactorNumber20Manager.Instance.CreateReactorPanelAsync());
    }
}