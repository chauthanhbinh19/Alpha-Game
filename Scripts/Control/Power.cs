using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Power : MonoBehaviour
{
    public static Power Instance { get; private set; }
    private GameObject PowerPrefab;
    private Transform popupPanel;
    private double countDuration = 1;
    private VertexGradient greenGradient;
    private VertexGradient redGradient;
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
        PowerPrefab = UIManager.Instance.GetGameObject("PowerPrefab");
        popupPanel = UIManager.Instance.GetTransform("popupPanel");
        greenGradient = new VertexGradient(
            HexToColor("#C5FFBFFF"),
            HexToColor("#6DFA2EFF"),
            HexToColor("#2BA400FF"),
            HexToColor("#0CCA00FF")
        );
        redGradient = new VertexGradient(
            HexToColor("#FF7547FF"),
            HexToColor("#FA5B2EFF"),
            HexToColor("#A41200FF"),
            HexToColor("#CA1000FF")
        );
    }
    private static Color HexToColor(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color color);
        return color;
    }
    public void ShowPower(double currentPower, double nextPower, int status)
    {
        Canvas.ForceUpdateCanvases(); // Cập nhật Canvas ngay lập tức
        GameObject powerObject = Instantiate(PowerPrefab, popupPanel);
        TextMeshProUGUI currentPowerText = powerObject.transform.Find("CurrentPowerText").GetComponent<TextMeshProUGUI>();
        currentPowerText.text = currentPower.ToString();

        TextMeshProUGUI nextPowerText = powerObject.transform.Find("NextPowerText").GetComponent<TextMeshProUGUI>();
        if (status == 1)
        {
            nextPowerText.colorGradient = greenGradient;
            nextPowerText.text = "+" + nextPower.ToString();
        }
        else if (status == 0)
        {
            nextPowerText.colorGradient = redGradient;
            nextPowerText.text = "-" + nextPower.ToString();
        }

        StartCoroutine(CountTo(currentPower, nextPower, currentPowerText, status));
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
        nextPowerText.gameObject.SetActive(false);

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
        var rate = Math.Abs((targetPower - currentPower)) / countDuration;
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
