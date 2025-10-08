using UnityEngine;

public class StaggeredSlideAnimation : MonoBehaviour
{
    public float delay = 0.001f;        // thời gian trễ giữa các child
    public float fadeDuration = 0.02f;  // thời gian fade in mỗi child

    private int currentIndex = 0;
    private float elapsed = 0f;
    private bool isPlaying = false;

    void OnEnable()
    {
        // Ẩn tất cả child trước và reset alpha
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            child.SetActive(false);

            CanvasGroup cg = child.GetComponent<CanvasGroup>();
            if (cg == null) cg = child.AddComponent<CanvasGroup>();
            cg.alpha = 0f;
        }

        currentIndex = 0;
        elapsed = 0f;
        isPlaying = true;
    }

    void Update()
    {
        if (!isPlaying) return;

        elapsed += Time.deltaTime;

        if (currentIndex < transform.childCount)
        {
            var child = transform.GetChild(currentIndex).gameObject;
            var cg = child.GetComponent<CanvasGroup>();

            if (!child.activeSelf)
            {
                // Bật child khi tới lượt
                child.SetActive(true);
            }

            // Tăng alpha dần để fade in
            if (cg.alpha < 1f)
            {
                cg.alpha += Time.deltaTime / fadeDuration;
            }

            // Nếu đã fade đủ và đã trễ xong thì chuyển sang child tiếp theo
            if (cg.alpha >= 1f && elapsed >= delay)
            {
                currentIndex++;
                elapsed = 0f;
            }
        }
        else
        {
            // Đã xong hết
            isPlaying = false;
        }
    }
}
