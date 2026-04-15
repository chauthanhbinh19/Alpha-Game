using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        EmployeeButtonPrefab = UIManager.Instance.Get("EmployeeButtonPrefab");
        EmployeePanelPrefab = UIManager.Instance.Get("EmployeePanelPrefab");
    }
    public async Task CreateEmployeePanelAsync()
    {
        GameObject currentObject = Instantiate(EmployeePanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        Button closeButton = currentObject.transform.Find("CloseButton").GetComponent<Button>();
        Button homeButton = currentObject.transform.Find("HomeButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        var Employees = await EmployeesService.Create().GetEmployeesAsync(User.CurrentUserId, 1000, 0);
        CreateEmployees(Employees, contentPanel);
    }
    public void CreateEmployees(List<Employees> employees, Transform contentPanel)
    {
        foreach (var employee in employees)
        {
            GameObject employeeObject = Instantiate(EmployeeButtonPrefab, contentPanel);

            TextMeshProUGUI titleText = employeeObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = employee.Name.Replace("_", " ");

            RawImage Image = employeeObject.transform.Find("MainImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(employee.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = employeeObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(employee, MainPanel);
            });

            RawImage rareImage = employeeObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{employee.Rare}");
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
