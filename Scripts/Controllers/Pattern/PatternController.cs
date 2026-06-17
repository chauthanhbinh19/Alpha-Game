using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PatternController : MonoBehaviour
{
    public static PatternController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject PatternPanelPrefab;
    private GameObject PatternDetailPanelPrefab;
    private GameObject PatternButtonPrefab;
    private GameObject PatternCellPrefab;
    // Cấu hình kích thước sàn đấu
    private const int GRID_SIZE = 12;
    
    // Chọn vị trí ô (5,5) làm tâm ngắm mặc định cho IsMain (0, 0 của offset)
    private const int CENTER_X = 5;
    private const int CENTER_Y = 5;
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
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        PatternPanelPrefab = UIManager.Instance.Get("PatternPanelPrefab");
        PatternDetailPanelPrefab = UIManager.Instance.Get("PatternDetailPanelPrefab");
        PatternButtonPrefab = UIManager.Instance.Get("PatternButtonPrefab");
        PatternCellPrefab = UIManager.Instance.Get("PatternCellPrefab");
    }
    public async Task CreatePatternPanel()
    {
        GameObject currentObject = Instantiate(PatternPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform content = transform.Find("Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.HIEN);

        var patternList = await PatternsService.Create().GetAllPatternsAsync();
        foreach(Patterns pattern in patternList)
        {
            GameObject patternObject = Instantiate(PatternButtonPrefab, content);
            TextMeshProUGUI patternTitleText = patternObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            patternTitleText.text = pattern.Name;

            Button button = patternObject.transform.GetComponent<Button>();
            button.onClick.AddListener(async () =>
            {
                await CreatePatternDetailPanel(pattern.Id);
            });
        }
    }
    public async Task CreatePatternDetailPanel(string patternId)
    {
        GameObject currentObject = Instantiate(PatternDetailPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform container = transform.Find("Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.HIEN);
        
        var pattern = await PatternsService.Create().GetPatternByIdAsync(patternId);
        if (pattern == null)
        {
            Debug.LogError($"Không tìm thấy Pattern với ID: {patternId}");
            return;
        }

        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        Dictionary<string, PatternCells> cellMap = new Dictionary<string, PatternCells>();
        if (pattern.Cells != null)
        {
            foreach (var cell in pattern.Cells)
            {
                string key = $"{cell.OffsetX}_{cell.OffsetY}";
                if (!cellMap.ContainsKey(key))
                {
                    cellMap.Add(key, cell);
                }
            }
        }

        for (int row = GRID_SIZE - 1; row >= 0; row--)
        {
            for (int col = 0; col < GRID_SIZE; col++)
            {
                // Khởi tạo ô UI mới
                GameObject patternCellObject = Instantiate(PatternCellPrefab, container);
                Image image = patternCellObject.transform.Find("Image").GetComponent<Image>();

                // Tính toán độ lệch (Offset) của ô hiện tại so với ô Trung Tâm (5,5)
                int currentOffsetX = col - CENTER_X;
                int currentOffsetY = row - CENTER_Y;

                // Tạo chuỗi Key để kiểm tra dữ liệu từ DB
                string checkKey = $"{currentOffsetX}_{currentOffsetY}";

                // 4. Kiểm tra logic để nhuộm màu theo yêu cầu
                if (col == CENTER_X && row == CENTER_Y)
                {
                    // Ô trung tâm gốc (Luôn là màu Xanh lá)
                    image.color = Color.green;
                }
                else if (cellMap.TryGetValue(checkKey, out PatternCells matchedCell))
                {
                    // Nếu ô này nằm trong danh sách Pattern từ DB
                    if (matchedCell.IsMain)
                    {
                        // Trường hợp đặc biệt nếu ô IsMain trong DB không nằm tại (0,0) 
                        image.color = Color.green;
                    }
                    else
                    {
                        // Cell phụ nằm trong vùng ảnh hưởng của chiêu thức (Màu Đỏ)
                        image.color = Color.red;
                    }
                }
                else
                {
                    // Cell trống không liên quan (Màu Xám)
                    image.color = Color.gray;
                }
            }
        }
    }
}