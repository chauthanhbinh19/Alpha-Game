using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class PowerController : MonoBehaviour
{
    public static PowerController Instance { get; private set; }
    private GameObject PowerPrefab;
    private GameObject PowerObject;
    private Transform PopupPanel;
    private const double COUNT_DURATION = 1;
    private Coroutine countCoroutine;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        PowerPrefab = UIManager.Instance.Get(PrefabConstants.General.POWER_PREFAB);
        PopupPanel = UIManager.Instance.GetTransform(AppConstants.Transform.POPUP_PANEL);
    }
    public void ShowPower(double currentPower, double deltaPower, int status)
    {
        Canvas.ForceUpdateCanvases(); // Cập nhật Canvas ngay lập tức

        PowerObject = Instantiate(PowerPrefab, PopupPanel);
        Transform transform = PowerObject.transform;

        TextMeshProUGUI currentPowerText = transform.Find("CurrentPowerText").GetComponent<TextMeshProUGUI>();
        currentPowerText.text = currentPower.ToString();

        TextMeshProUGUI nextPowerText = transform.Find("NextPowerText").GetComponent<TextMeshProUGUI>();

        if (status == 1) // Tăng lực chiến
        {
            nextPowerText.colorGradient = ColorConstants.INCREASE_POWER_COLOR;
            nextPowerText.text = "+" + deltaPower.ToString();
        }
        else if (status == -1) // Giảm lực chiến
        {
            nextPowerText.colorGradient = ColorConstants.DECREASE_POWER_COLOR;
            nextPowerText.text = "-" + deltaPower.ToString();
        }
        else // status == 0: Không đổi / Không ảnh hưởng
        {
            nextPowerText.text = "0"; // Hoặc ẩn đi tùy ý bạn
        }

        // Nếu coroutine CountTo cần giá trị đích, bạn có thể truyền targetPower = currentPower + deltaPower (nếu tăng) hoặc trừ đi (nếu giảm)
        double targetPower = currentPower + (status * deltaPower);

        countCoroutine = StartCoroutine(CountTo(currentPower, targetPower, currentPowerText, status));
        StartCoroutine(HandlePowerDisplay(PowerObject, nextPowerText, 2f));
    }
    private IEnumerator HandlePowerDisplay(GameObject powerObject, TextMeshProUGUI nextPowerText, float duration)
    {
        // Chờ cho `nextPowerText` biến mất
        yield return new WaitForSeconds(duration / 2); // `nextPowerText` biến mất sau nửa thời gian
        // if (nextPowerText != null)
        // {
        //     nextPowerText.gameObject.SetActive(false);
        // }
        // nextPowerText.gameObject.SetActive(false);
        nextPowerText.text = "";

        // Chờ thêm trước khi toàn bộ object biến mất
        yield return new WaitForSeconds(duration / 2);
        // if (powerObject != null)
        // {
        //     Destroy(powerObject); // Xóa toàn bộ object
        // }
        Destroy(powerObject);
    }
    private IEnumerator CountTo(double currentPower, double nextPower, TextMeshProUGUI numberText, int status)
    {
        double targetPower = 0;
        if (status == 1)
        {
            targetPower = currentPower + nextPower;
        }
        else
        {
            targetPower = currentPower - nextPower;
        }
        var rate = Math.Abs((targetPower - currentPower)) / COUNT_DURATION;
        while (currentPower != targetPower)
        {
            currentPower = MoveTowards(currentPower, targetPower, rate * Time.deltaTime);
            currentPower = Math.Floor(currentPower);
            numberText.text = ((double)currentPower).ToString();
            yield return null;
        }
    }
    public static double MoveTowards(double current, double target, double maxDelta)
    {
        if (Math.Abs(target - current) <= maxDelta)
        {
            return target;
        }
        return current + Math.Sign(target - current) * maxDelta;
    }
}
