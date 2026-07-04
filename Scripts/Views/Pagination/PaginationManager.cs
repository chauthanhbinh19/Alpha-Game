using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaginationManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform container; // Kéo PageButtonsContainer vào đây

    [Header("Prefab Duy Nhất")]
    [SerializeField] private GameObject pageButtonPrefab; // Kéo bản Prefab ô số vào đây

    [Header("Settings/Colors")]
    [SerializeField] private Color activeColor = new Color(0.93f, 0.26f, 0.35f);   // Màu hồng (Trang hiện tại)
    [SerializeField] private Color inactiveColor = new Color(0.88f, 0.88f, 0.88f); // Màu xám (Trang thường)
    [SerializeField] private Color ellipsisColor = new Color(0.88f, 0.88f, 0.88f); // Màu xám cho ô chứa "..."

    // Event bắn ra bên ngoài khi người dùng bấm chuyển trang
    public event Action<int> OnPageChanged;

    private int totalItems;
    private int itemsPerPage;
    private int totalPages;
    private int currentPage = 1;

    // Hàm khởi tạo ban đầu (Truyền vào tổng số item và offset/limit)
    public void InitPagination(int totalItems, int itemsPerPage, int startPage = 1)
    {
        this.totalItems = totalItems;
        this.itemsPerPage = itemsPerPage > 0 ? itemsPerPage : 10;
        
        // Tính tổng số trang (làm tròn lên)
        this.totalPages = Mathf.CeilToInt((float)this.totalItems / this.itemsPerPage);
        if (this.totalPages < 1) this.totalPages = 1;

        GoToPage(startPage);
    }

    // Hàm chuyển trang công khai
    public void GoToPage(int page)
    {
        currentPage = Mathf.Clamp(page, 1, totalPages);
        RenderPagination();
        
        // Phát sự kiện ra ngoài
        OnPageChanged?.Invoke(currentPage);
    }

    private void RenderPagination()
    {
        // 1. Xóa sạch các ô cũ trong Container
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        // 2. Nếu tổng số trang nhỏ hơn hoặc bằng 5, hiện thẳng hết từ 1 -> hết
        if (totalPages <= 5)
        {
            for (int i = 1; i <= totalPages; i++)
            {
                CreatePageButton(i);
            }
            return;
        }

        // 3. LOGIC HIỂN THỊ ĐỘNG THEO HÌNH ẢNH (Khi tổng trang >= 6)
        int pageBuffer = 2; // Hiển thị 2 trang xung quanh trang hiện tại (TrangHiệnTại - 2 và TrangHiệnTại + 2)
        
        bool showLeftEllipsis = currentPage > (1 + pageBuffer + 1); // Hiện ... bên trái khi currentPage > 4
        bool showRightEllipsis = currentPage < (totalPages - pageBuffer - 1); // Hiện ... bên phải khi currentPage < (Cuối - 3)

        // --- LUÔN VẼ Ô SỐ 1 ---
        CreatePageButton(1);

        // --- VẼ CỤM GIỮA VÀ DẤU BA CHẤM ---
        if (showLeftEllipsis && !showRightEllipsis)
        {
            // Trạng thái Gần Cuối: 1 ... 12 13 14 15 16
            CreateEllipsisButton(); // Hiện "..." bên trái
            int startPage = totalPages - 4; 
            for (int i = startPage; i < totalPages; i++)
            {
                CreatePageButton(i);
            }
        }
        else if (!showLeftEllipsis && showRightEllipsis)
        {
            // Trạng thái Gần Đầu (Hình 2 và Hình 3): 1 2 3 4 5 ... 16
            for (int i = 2; i <= 5; i++)
            {
                CreatePageButton(i);
            }
            CreateEllipsisButton(); // Hiện "..." bên phải
        }
        else if (showLeftEllipsis && showRightEllipsis)
        {
            // Trạng thái Ở Giữa (Hình 1): 1 ... 8 9 10 11 12 ... 16
            CreateEllipsisButton(); // Hiện "..." bên trái
            
            for (int i = currentPage - pageBuffer; i <= currentPage + pageBuffer; i++)
            {
                CreatePageButton(i);
            }
            
            CreateEllipsisButton(); // Hiện "..." bên phải
        }

        // --- LUÔN VẼ Ô SỐ CUỐI CÙNG ---
        if (totalPages > 1)
        {
            CreatePageButton(totalPages);
        }
    }

    // Tạo ô số (Bấm được)
    private void CreatePageButton(int pageNumber)
    {
        GameObject buttonObject = Instantiate(pageButtonPrefab, container);
        Button button = buttonObject.GetComponent<Button>();
        Image image = buttonObject.GetComponent<Image>();
        TextMeshProUGUI text = buttonObject.GetComponentInChildren<TextMeshProUGUI>();

        if (text != null) text.text = pageNumber.ToString();

        // Đổi màu dựa theo trang có đang được chọn hay không
        bool isCurrent = (pageNumber == currentPage);
        if (image != null) image.color = isCurrent ? activeColor : inactiveColor;
        if (text != null) text.color = isCurrent ? Color.white : Color.black;

        button.interactable = true; 
        
        // Xóa hết sự kiện cũ (để an toàn) và gán sự kiện click chuyển trang
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            GoToPage(pageNumber);
        });
    }

    // Tạo ô dấu "..." (Không bấm được, giữ nguyên background)
    private void CreateEllipsisButton()
    {
        GameObject buttonObject = Instantiate(pageButtonPrefab, container);
        Button button = buttonObject.GetComponent<Button>();
        Image image = buttonObject.GetComponent<Image>();
        TextMeshProUGUI text = buttonObject.GetComponentInChildren<TextMeshProUGUI>();

        if (text != null)
        {
            text.text = "...";
            text.color = Color.black;
        }
        
        if (image != null) image.color = ellipsisColor;

        // Khóa tương tác để người chơi không click được vào ô "..."
        button.interactable = false; 
    }
}