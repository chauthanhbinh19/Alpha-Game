using UnityEngine;

public class StaggeredSlideAnimation : MonoBehaviour
{
    public float Delay = 0.001f;        // thời gian trễ giữa các child
    public float FadeDuration = 0.02f;  // thời gian fade in mỗi child

    private int CurrentIndex = 0;
    private float Elapsed = 0f;
    private bool IsPlaying = false;

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

        CurrentIndex = 0;
        Elapsed = 0f;
        IsPlaying = true;
    }

    void Update()
    {
        if (!IsPlaying) return;

        Elapsed += Time.deltaTime;

        if (CurrentIndex < transform.childCount)
        {
            var child = transform.GetChild(CurrentIndex).gameObject;
            var cg = child.GetComponent<CanvasGroup>();

            if (!child.activeSelf)
            {
                // Bật child khi tới lượt
                child.SetActive(true);
            }

            // Tăng alpha dần để fade in
            if (cg.alpha < 1f)
            {
                cg.alpha += Time.deltaTime / FadeDuration;
            }

            // Nếu đã fade đủ và đã trễ xong thì chuyển sang child tiếp theo
            if (cg.alpha >= 1f && Elapsed >= Delay)
            {
                CurrentIndex++;
                Elapsed = 0f;
            }
        }
        else
        {
            // Đã xong hết
            IsPlaying = false;
        }
    }
}
