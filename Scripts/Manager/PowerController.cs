using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class PowerController : MonoBehaviour
{
    public static PowerController Instance { get; private set; }
    private GameObject PowerPrefab;
    private GameObject powerObject;
    private Transform popupPanel;
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
        PowerPrefab = UIManager.Instance.Get("PowerPrefab");
        popupPanel = UIManager.Instance.GetTransform("popupPanel");
    }
    public void ShowPower(double currentPower, double nextPower, int status)
    {
        Canvas.ForceUpdateCanvases(); // Cập nhật Canvas ngay lập tức
        // Destroy(powerObject);
        powerObject = Instantiate(PowerPrefab, popupPanel);
        Transform transform = powerObject.transform;
        TextMeshProUGUI currentPowerText = transform.Find("CurrentPowerText").GetComponent<TextMeshProUGUI>();
        currentPowerText.text = currentPower.ToString();

        TextMeshProUGUI nextPowerText = transform.Find("NextPowerText").GetComponent<TextMeshProUGUI>();
        if (status == 1)
        {
            nextPowerText.colorGradient = ColorConstants.INCREASE_POWER_COLOR;
            nextPowerText.text = "+" + nextPower.ToString();
        }
        else if (status == 0)
        {
            nextPowerText.colorGradient = ColorConstants.DECREASE_POWER_COLOR;
            nextPowerText.text = "-" + nextPower.ToString();
        }

        countCoroutine = StartCoroutine(CountTo(currentPower, nextPower, currentPowerText, status));
        StartCoroutine(HandlePowerDisplay(powerObject, nextPowerText, 2f)); // Hiển thị trong 1 giây
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
