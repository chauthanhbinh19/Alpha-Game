using System.Collections;
using UnityEngine;

public class LoadingSystem : MonoBehaviour
{
    private Transform LoadingPanel;
    private GameObject LoadingPanelPrefab;
    private float DeltaTime = 0.35f;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        LoadingPanel = UIManager.Instance.GetTransform("LoadingPanel");
        LoadingPanelPrefab = UIManager.Instance.Get("LoadingPanelPrefab");
    }
    public void Loading(Transform firstPanel, Transform secondPanel)
    {
        GameObject loadingObject = Instantiate(LoadingPanelPrefab, LoadingPanel);
        Transform transform = loadingObject.transform;
        RectTransform leftBackground = transform.Find("LeftBackground").GetComponent<RectTransform>();
        RectTransform rightBackground = transform.Find("RightBackground").GetComponent<RectTransform>();
        RectTransform leftPathBackground = transform.Find("LeftPathBackground").GetComponent<RectTransform>();
        RectTransform rightPathBackground = transform.Find("RightPathBackground").GetComponent<RectTransform>();
        RectTransform circle = transform.Find("Circle").GetComponent<RectTransform>();
        RectTransform bottomLeftDecoration = transform.Find("BottomLeftDecoration").GetComponent<RectTransform>();
        RectTransform bottomRightDecoration = transform.Find("BottomRightDecoration").GetComponent<RectTransform>();
        RectTransform topLeftDecoration = transform.Find("TopLeftDecoration").GetComponent<RectTransform>();
        RectTransform topRightDecoration = transform.Find("TopRightDecoration").GetComponent<RectTransform>();

        // Đặt vị trí ban đầu
        leftBackground.anchoredPosition = new Vector2(-1600, leftBackground.anchoredPosition.y);
        rightBackground.anchoredPosition = new Vector2(1600, rightBackground.anchoredPosition.y);
        leftPathBackground.anchoredPosition = new Vector2(-1600, leftPathBackground.anchoredPosition.y);
        rightPathBackground.anchoredPosition = new Vector2(1600, rightPathBackground.anchoredPosition.y);
        bottomLeftDecoration.anchoredPosition = new Vector2(-1600, bottomLeftDecoration.anchoredPosition.y);
        bottomRightDecoration.anchoredPosition = new Vector2(1600, bottomRightDecoration.anchoredPosition.y);
        topLeftDecoration.anchoredPosition = new Vector2(-1600, topLeftDecoration.anchoredPosition.y);
        topRightDecoration.anchoredPosition = new Vector2(1600, topRightDecoration.anchoredPosition.y);

        // Chạy Animation theo thứ tự
        StartCoroutine(AnimateLoading(firstPanel, secondPanel, leftBackground, rightBackground, leftPathBackground, rightPathBackground, circle, 
        bottomLeftDecoration, bottomRightDecoration, topLeftDecoration, topRightDecoration, loadingObject));
    }
    IEnumerator AnimateLoading(Transform firstPanel, Transform secondPanel,RectTransform leftBg, RectTransform rightBg, 
    RectTransform leftPath, RectTransform rightPath, RectTransform circle, 
    RectTransform bottomLeftDecoration, RectTransform bottomRightDecoration, 
    RectTransform topLeftDecoration, RectTransform topRightDecoration, GameObject loadingObject)
    {
        // Chạy leftBackground & rightBackground CÙNG LÚC
        Coroutine leftMove = StartCoroutine(MoveUI(leftBg, -500, DeltaTime));
        Coroutine rightMove = StartCoroutine(MoveUI(rightBg, 500, DeltaTime));
        Coroutine bottomLeftMove = StartCoroutine(MoveUI(bottomLeftDecoration, -570, DeltaTime));
        Coroutine bottomRightMove = StartCoroutine(MoveUI(bottomRightDecoration, 560, DeltaTime));
        Coroutine topLeftMove = StartCoroutine(MoveUI(topLeftDecoration, -570, DeltaTime));
        Coroutine topRightMove = StartCoroutine(MoveUI(topRightDecoration, 560, DeltaTime));

        // Đợi cả 2 hoàn thành trước khi tiếp tục
        yield return leftMove;
        yield return rightMove;
        yield return bottomLeftMove;
        yield return bottomRightMove;
        yield return topLeftMove;
        yield return topRightMove;

        // Sau đó, chạy leftPathBackground & rightPathBackground CÙNG LÚC
        Coroutine leftPathMove = StartCoroutine(MoveUI(leftPath, -56, DeltaTime));
        Coroutine rightPathMove = StartCoroutine(MoveUI(rightPath, 68, DeltaTime));

        // Đợi cả 2 hoàn thành
        yield return leftPathMove;
        yield return rightPathMove;
        firstPanel.gameObject.SetActive(false);
        secondPanel.gameObject.SetActive(true);

        circle.gameObject.SetActive(true);
        leftPath.gameObject.SetActive(false);
        rightPath.gameObject.SetActive(false);
        Coroutine circleRotate = StartCoroutine(RotateUI(circle, 360, DeltaTime));
        yield return circleRotate;
        circle.gameObject.SetActive(false);
        leftPath.gameObject.SetActive(true);
        rightPath.gameObject.SetActive(true);

        // Sau đó, chạy leftPathBackground & rightPathBackground CÙNG LÚC
        leftPathMove = StartCoroutine(MoveUI(leftPath, -1600, DeltaTime));
        rightPathMove = StartCoroutine(MoveUI(rightPath, 1600, DeltaTime));

        // Đợi cả 2 hoàn thành
        yield return leftPathMove;
        yield return rightPathMove;

        // Chạy leftBackground & rightBackground CÙNG LÚC
        leftMove = StartCoroutine(MoveUI(leftBg, -1600, DeltaTime));
        rightMove = StartCoroutine(MoveUI(rightBg, 1600, DeltaTime));
        bottomLeftMove = StartCoroutine(MoveUI(bottomLeftDecoration, -1600, DeltaTime));
        bottomRightMove = StartCoroutine(MoveUI(bottomRightDecoration, 1600, DeltaTime));
        topLeftMove = StartCoroutine(MoveUI(topLeftDecoration, -1600, DeltaTime));
        topRightMove = StartCoroutine(MoveUI(topRightDecoration, 1600, DeltaTime));

        // Đợi cả 2 hoàn thành trước khi tiếp tục
        yield return leftMove;
        yield return rightMove;
        yield return bottomLeftMove;
        yield return bottomRightMove;
        yield return topLeftMove;
        yield return topRightMove;
        Destroy(loadingObject);
    }
    IEnumerator MoveUI(RectTransform uiElement, float targetX, float duration)
    {
        float elapsedTime = 0;
        Vector2 startPos = uiElement.anchoredPosition;

        while (elapsedTime < duration)
        {
            float newX = Mathf.Lerp(startPos.x, targetX, elapsedTime / duration);
            uiElement.anchoredPosition = new Vector2(newX, startPos.y);
            elapsedTime += Time.deltaTime;
            yield return null; // Chờ frame tiếp theo
        }

        // Đảm bảo vị trí cuối cùng chính xác
        uiElement.anchoredPosition = new Vector2(targetX, startPos.y);
    }
    IEnumerator RotateUI(RectTransform uiElement, float angle, float duration)
    {
        float elapsedTime = 0;
        float startAngle = uiElement.rotation.eulerAngles.z; // Lấy góc ban đầu

        while (elapsedTime < duration)
        {
            float newAngle = Mathf.Lerp(startAngle, startAngle + angle, elapsedTime / duration);
            uiElement.rotation = Quaternion.Euler(0, 0, newAngle);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Đảm bảo xoay đúng góc cuối cùng
        uiElement.rotation = Quaternion.Euler(0, 0, startAngle + angle);
    }
}
