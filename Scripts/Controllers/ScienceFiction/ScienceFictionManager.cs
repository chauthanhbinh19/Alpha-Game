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
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber1, scienceFictionPanel, () => ReactorNumber1Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber2, scienceFictionPanel, () => ReactorNumber2Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber3, scienceFictionPanel, () => ReactorNumber3Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber4, scienceFictionPanel, () => ReactorNumber4Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber5, scienceFictionPanel, () => ReactorNumber5Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber6, scienceFictionPanel, () => ReactorNumber6Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber7, scienceFictionPanel, () => ReactorNumber7Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber8, scienceFictionPanel, () => ReactorNumber8Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber9, scienceFictionPanel, () => ReactorNumber9Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber10, scienceFictionPanel, () => ReactorNumber10Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber11, scienceFictionPanel, () => ReactorNumber11Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber12, scienceFictionPanel, () => ReactorNumber12Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber13, scienceFictionPanel, () => ReactorNumber13Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber14, scienceFictionPanel, () => ReactorNumber14Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber15, scienceFictionPanel, () => ReactorNumber15Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber16, scienceFictionPanel, () => ReactorNumber16Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber17, scienceFictionPanel, () => ReactorNumber17Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber18, scienceFictionPanel, () => ReactorNumber18Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber19, scienceFictionPanel, () => ReactorNumber19Manager.Instance.CreateReactorPanel());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.ReactorNumber20, scienceFictionPanel, () => ReactorNumber20Manager.Instance.CreateReactorPanel());
    }
}