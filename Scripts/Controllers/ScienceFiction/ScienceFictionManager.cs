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
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_1, scienceFictionPanel, () => ReactorNumber1Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_2, scienceFictionPanel, () => ReactorNumber2Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_3, scienceFictionPanel, () => ReactorNumber3Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_4, scienceFictionPanel, () => ReactorNumber4Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_5, scienceFictionPanel, () => ReactorNumber5Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_6, scienceFictionPanel, () => ReactorNumber6Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_7, scienceFictionPanel, () => ReactorNumber7Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_8, scienceFictionPanel, () => ReactorNumber8Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_9, scienceFictionPanel, () => ReactorNumber9Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_10, scienceFictionPanel, () => ReactorNumber10Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_11, scienceFictionPanel, () => ReactorNumber11Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_12, scienceFictionPanel, () => ReactorNumber12Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_13, scienceFictionPanel, () => ReactorNumber13Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_14, scienceFictionPanel, () => ReactorNumber14Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_15, scienceFictionPanel, () => ReactorNumber15Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_16, scienceFictionPanel, () => ReactorNumber16Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_17, scienceFictionPanel, () => ReactorNumber17Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_18, scienceFictionPanel, () => ReactorNumber18Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_19, scienceFictionPanel, () => ReactorNumber19Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_20, scienceFictionPanel, () => ReactorNumber20Manager.Instance.CreateReactorPanel());
    }
}