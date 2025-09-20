using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReactorNumber18Manager : MonoBehaviour
{
    private Transform MainPanel;
    public GameObject ReactorPanelNumberPrefab;
    public static ReactorNumber18Manager Instance { get; private set; }
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
    // Start is called before the first frame update
    void Start()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ReactorPanelNumberPrefab = UIManager.Instance.GetGameObjectScienceFiction("ReactorPanelNumberPrefab");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateReactorPanel()
    {
        GameObject currentObject = Instantiate(ReactorPanelNumberPrefab, MainPanel);
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => ButtonEvent.Instance.Close(MainPanel));
        CloseButton.onClick.AddListener(() => Destroy(currentObject));

        RawImage leftSideCounduit1Image = currentObject.transform.Find("DictionaryCards/LeftSideConduit1/LeftSideConduiltImage").GetComponent<RawImage>();
        RawImage leftSideCounduit2Image = currentObject.transform.Find("DictionaryCards/LeftSideConduit2/LeftSideConduiltImage").GetComponent<RawImage>();
        RawImage rightSideCounduit1Image = currentObject.transform.Find("DictionaryCards/RightSideConduit1/RightSideConduiltImage").GetComponent<RawImage>();
        RawImage rightSideCounduit2Image = currentObject.transform.Find("DictionaryCards/RightSideConduit2/RightSideConduiltImage").GetComponent<RawImage>();
        RawImage MainReactorBackgroundImage = currentObject.transform.Find("DictionaryCards/MainReactorBackgroundCircle/MainReactorImage").GetComponent<RawImage>();
        RawImage MainReactorImage = currentObject.transform.Find("DictionaryCards/MainReactor/MainReactorImage").GetComponent<RawImage>();
        RawImage MainReactorCoreImage = currentObject.transform.Find("DictionaryCards/MainReactorBackgroundCore/MainReactorCoreImage").GetComponent<RawImage>();
        TextMeshProUGUI ReactorLevelText = currentObject.transform.Find("DictionaryCards/ReactorLevel/ReactorLevelText").GetComponent<TextMeshProUGUI>();

        Texture conduitTexture = Resources.Load<Texture>("UI/Background2/Conduit_1");
        leftSideCounduit1Image.texture = conduitTexture;
        leftSideCounduit2Image.texture = conduitTexture;
        rightSideCounduit1Image.texture = conduitTexture;
        rightSideCounduit2Image.texture = conduitTexture;

        MainReactorBackgroundImage.AddComponent<RotateAnimation>();

        Texture mainReactorTexture = Resources.Load<Texture>("UI/Background2/Reactor_1");
        MainReactorImage.texture = mainReactorTexture;

        // RotateAnimation anim = MainReactorCoreImage.AddComponent<RotateAnimation>();
        // anim.direction = 1;
        leftSideCounduit1Image.AddComponent<SlideLeftToRightAnimation>();
        leftSideCounduit2Image.AddComponent<SlideLeftToRightAnimation>();
        rightSideCounduit1Image.AddComponent<SlideRightToLeftAnimation>();
        rightSideCounduit2Image.AddComponent<SlideRightToLeftAnimation>();
    }
}
