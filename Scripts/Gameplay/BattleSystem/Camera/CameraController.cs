using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Tốc độ di chuyển")]
    public float keyboardSpeed = 30f; 
    public float dragSpeed = 2f;      

    [Header("Cấu hình 4 góc hình vuông giới hạn (Chỉ lấy X và Z)")]
    [Tooltip("Góc phía dưới bên trái của vùng giới hạn")]
    public Vector3 bottomLeftCorner = new Vector3(-2, 36, -10);
    [Tooltip("Góc phía trên bên phải của vùng giới hạn")]
    public Vector3 topRightCorner = new Vector3(2, 36, -5);

    // Lưu lại biên tính toán sau cùng
    private float minX, maxX;
    private float minZ, maxZ;

    private Vector3 lastMousePosition;

    void Start()
    {
        // Tự động phân tích 4 góc để tìm ra biên Min/Max chuẩn xác nhất cho Hình vuông
        // Cách này giúp bạn điền tọa độ góc nào trước cũng được, không lo bị ngược dấu
        minX = Mathf.Min(bottomLeftCorner.x, topRightCorner.x);
        maxX = Mathf.Max(bottomLeftCorner.x, topRightCorner.x);
        
        minZ = Mathf.Min(bottomLeftCorner.z, topRightCorner.z);
        maxZ = Mathf.Max(bottomLeftCorner.z, topRightCorner.z);

        // Debug để bạn kiểm tra xem biên nhận đúng chưa
        Debug.Log($"[Cam Bounds] Đã thiết lập hình vuông giới hạn: X ({minX} -> {maxX}), Z ({minZ} -> {maxZ})");
    }

    void Update()
    {
        // 1. BẤM NÚT DI CHUYỂN (A, D | W, S)
        HandleKeyboardMovement();

        // 2. KÉO CHUỘT GIỮA ĐỂ DI CHUYỂN
        HandleMouseDragMovement();
    }

    void HandleKeyboardMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal"); // A (-1), D (1)
        float inputZ = Input.GetAxisRaw("Vertical");   // S (-1), W (1)

        if (inputX != 0 || inputZ != 0)
        {
            Vector3 moveDirection = (Vector3.right * inputX) + (Vector3.forward * inputZ);
            moveDirection.Normalize();

            Vector3 newPosition = transform.position + moveDirection * keyboardSpeed * Time.deltaTime;

            // Khóa vị trí trong hình vuông đã định cấu hình
            transform.position = ClampToSquareBounds(newPosition);
        }
    }

    void HandleMouseDragMovement()
    {
        if (Input.GetMouseButtonDown(2)) // Chuột giữa
        {
            lastMousePosition = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;

            float moveX = -deltaMouse.x * dragSpeed * Time.deltaTime;
            float moveZ = -deltaMouse.y * dragSpeed * Time.deltaTime;

            Vector3 dragDirection = (Vector3.right * moveX) + (Vector3.forward * moveZ);
            Vector3 newPosition = transform.position + dragDirection;

            transform.position = ClampToSquareBounds(newPosition);

            lastMousePosition = Input.mousePosition;
        }
    }

    // Hàm ép biên Camera chỉ được di chuyển lọt thỏm trong hình vuông tạo bởi các góc
    Vector3 ClampToSquareBounds(Vector3 targetPosition)
    {
        float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float clampedZ = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        // Giữ nguyên độ cao Y (ví dụ Y = 50) của camera khi di chuyển
        return new Vector3(clampedX, transform.position.y, clampedZ);
    }
}