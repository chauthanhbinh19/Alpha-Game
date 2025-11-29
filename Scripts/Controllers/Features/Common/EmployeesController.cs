using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmployeesController : MonoBehaviour
{
    public static EmployeesController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject EmployeeButtonPrefab;
    private GameObject EmployeePanelPrefab;
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
        Initialize();
    }

    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        EmployeeButtonPrefab = UIManager.Instance.GetGeneralButton("EmployeeButtonPrefab");
        EmployeePanelPrefab = UIManager.Instance.GetGeneralPanel("EmployeePanelPrefab");
    }
    public void CreateEmployeePanel()
    {
        GameObject currentObject = Instantiate(EmployeePanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        Button CloseButton = currentObject.transform.Find("CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("HomeButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        var Employees = EmployeesService.Create().GetEmployees(User.CurrentUserId, 1000, 0);
        CreateEmployees(Employees, contentPanel);
    }
    public void CreateEmployees(List<Employees> Employees, Transform contentPanel)
    {
        foreach (var Employee in Employees)
        {
            GameObject EmployeeObject = Instantiate(EmployeeButtonPrefab, contentPanel);

            TextMeshProUGUI Title = EmployeeObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = Employee.Name.Replace("_", " ");

            RawImage Image = EmployeeObject.transform.Find("MainImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Employee.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = EmployeeObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(Employee, MainPanel);
            });

            RawImage rareImage = EmployeeObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{Employee.Rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(400, 520);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
