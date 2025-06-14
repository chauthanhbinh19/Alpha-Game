using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    private Transform LoadingPanel;
    private GameObject LoadingPanelPrefab;
    private float deltaTime = 0.35f;
    // Start is called before the first frame update
    void Start()
    {
        LoadingPanel = UIManager.Instance.GetTransform("LoadingPanel");
        LoadingPanelPrefab = UIManager.Instance.GetGameObject("LoadingPanelPrefab");
    }

    public void Loading(Transform firstPanel, Transform secondPanel)
    {
        GameObject loadingObject = Instantiate(LoadingPanelPrefab, LoadingPanel);
        RectTransform leftBackground = loadingObject.transform.Find("LeftBackground").GetComponent<RectTransform>();
        RectTransform rightBackground = loadingObject.transform.Find("RightBackground").GetComponent<RectTransform>();
        RectTransform leftPathBackground = loadingObject.transform.Find("LeftPathBackground").GetComponent<RectTransform>();
        RectTransform rightPathBackground = loadingObject.transform.Find("RightPathBackground").GetComponent<RectTransform>();
        RectTransform circle = loadingObject.transform.Find("Circle").GetComponent<RectTransform>();
        RectTransform bottomLeftDecoration = loadingObject.transform.Find("BottomLeftDecoration").GetComponent<RectTransform>();
        RectTransform bottomRightDecoration = loadingObject.transform.Find("BottomRightDecoration").GetComponent<RectTransform>();
        RectTransform topLeftDecoration = loadingObject.transform.Find("TopLeftDecoration").GetComponent<RectTransform>();
        RectTransform topRightDecoration = loadingObject.transform.Find("TopRightDecoration").GetComponent<RectTransform>();

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
        Coroutine leftMove = StartCoroutine(MoveUI(leftBg, -500, deltaTime));
        Coroutine rightMove = StartCoroutine(MoveUI(rightBg, 500, deltaTime));
        Coroutine bottomLeftMove = StartCoroutine(MoveUI(bottomLeftDecoration, -570, deltaTime));
        Coroutine bottomRightMove = StartCoroutine(MoveUI(bottomRightDecoration, 560, deltaTime));
        Coroutine topLeftMove = StartCoroutine(MoveUI(topLeftDecoration, -570, deltaTime));
        Coroutine topRightMove = StartCoroutine(MoveUI(topRightDecoration, 560, deltaTime));

        // Đợi cả 2 hoàn thành trước khi tiếp tục
        yield return leftMove;
        yield return rightMove;
        yield return bottomLeftMove;
        yield return bottomRightMove;
        yield return topLeftMove;
        yield return topRightMove;

        // Sau đó, chạy leftPathBackground & rightPathBackground CÙNG LÚC
        Coroutine leftPathMove = StartCoroutine(MoveUI(leftPath, -56, deltaTime));
        Coroutine rightPathMove = StartCoroutine(MoveUI(rightPath, 68, deltaTime));

        // Đợi cả 2 hoàn thành
        yield return leftPathMove;
        yield return rightPathMove;
        firstPanel.gameObject.SetActive(false);
        secondPanel.gameObject.SetActive(true);

        circle.gameObject.SetActive(true);
        leftPath.gameObject.SetActive(false);
        rightPath.gameObject.SetActive(false);
        Coroutine circleRotate = StartCoroutine(RotateUI(circle, 360, deltaTime));
        yield return circleRotate;
        circle.gameObject.SetActive(false);
        leftPath.gameObject.SetActive(true);
        rightPath.gameObject.SetActive(true);

        // Sau đó, chạy leftPathBackground & rightPathBackground CÙNG LÚC
        leftPathMove = StartCoroutine(MoveUI(leftPath, -1600, deltaTime));
        rightPathMove = StartCoroutine(MoveUI(rightPath, 1600, deltaTime));

        // Đợi cả 2 hoàn thành
        yield return leftPathMove;
        yield return rightPathMove;

        // Chạy leftBackground & rightBackground CÙNG LÚC
        leftMove = StartCoroutine(MoveUI(leftBg, -1600, deltaTime));
        rightMove = StartCoroutine(MoveUI(rightBg, 1600, deltaTime));
        bottomLeftMove = StartCoroutine(MoveUI(bottomLeftDecoration, -1600, deltaTime));
        bottomRightMove = StartCoroutine(MoveUI(bottomRightDecoration, 1600, deltaTime));
        topLeftMove = StartCoroutine(MoveUI(topLeftDecoration, -1600, deltaTime));
        topRightMove = StartCoroutine(MoveUI(topRightDecoration, 1600, deltaTime));

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
