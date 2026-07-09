using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Tốc độ di chuyển")]
    public float KeyboardSpeed = 30f; 
    public float DragSpeed = 2f;      
    public float HeightChangeSpeed = 15f; // Tốc độ nâng/hạ độ cao camera

    [Header("Cấu hình giới hạn Độ cao (Trục Y)")]
    public float MinHeight = 28f;
    public float MaxHeight = 36f;

    [Header("Cấu hình 4 góc hình vuông giới hạn (Chỉ lấy X và Z)")]
    [Tooltip("Góc phía dưới bên trái của vùng giới hạn")]
    public Vector3 BottomLeftCorner = new Vector3(-2, 36, -10);
    [Tooltip("Góc phía trên bên phải của vùng giới hạn")]
    public Vector3 TopRightCorner = new Vector3(2, 36, -3);

    // Lưu lại biên tính toán sau cùng
    private float MinX, MaxX;
    private float MinZ, MaxZ;

    private Vector3 lastMousePosition;

    void Start()
    {
        // Tự động phân tích 4 góc để tìm ra biên Min/Max chuẩn xác nhất cho Hình vuông
        MinX = Mathf.Min(BottomLeftCorner.x, TopRightCorner.x);
        MaxX = Mathf.Max(BottomLeftCorner.x, TopRightCorner.x);
        
        MinZ = Mathf.Min(BottomLeftCorner.z, TopRightCorner.z);
        MaxZ = Mathf.Max(BottomLeftCorner.z, TopRightCorner.z);

        // Debug để bạn kiểm tra xem biên nhận đúng chưa
        Debug.Log($"[Cam Bounds] Đã thiết lập hình vuông giới hạn: X ({MinX} -> {MaxX}), Z ({MinZ} -> {MaxZ}) | Y ({MinHeight} -> {MaxHeight})");
    }

    void Update()
    {
        // 1. BẤM NÚT DI CHUYỂN (A, D | W, S)
        HandleKeyboardMovement();

        // 2. KÉO CHUỘT GIỮA ĐỂ DI CHUYỂN
        HandleMouseDragMovement();

        // 3. BẤM NÚT THAY ĐỔI ĐỘ CAO (Q, E)
        HandleHeightMovement();
    }

    void HandleKeyboardMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal"); // A (-1), D (1)
        float inputZ = Input.GetAxisRaw("Vertical");   // S (-1), W (1)

        if (inputX != 0 || inputZ != 0)
        {
            Vector3 moveDirection = (Vector3.right * inputX) + (Vector3.forward * inputZ);
            moveDirection.Normalize();

            Vector3 newPosition = transform.position + moveDirection * KeyboardSpeed * Time.deltaTime;

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

            float moveX = -deltaMouse.x * DragSpeed * Time.deltaTime;
            float moveZ = -deltaMouse.y * DragSpeed * Time.deltaTime;

            Vector3 dragDirection = (Vector3.right * moveX) + (Vector3.forward * moveZ);
            Vector3 newPosition = transform.position + dragDirection;

            transform.position = ClampToSquareBounds(newPosition);

            lastMousePosition = Input.mousePosition;
        }
    }

    // Hàm xử lý tăng/giảm độ cao bằng phím Q và E
    void HandleHeightMovement()
    {
        float heightInput = 0f;

        if (Input.GetKey(KeyCode.E)) // E để bay cao lên (Nâng Y)
        {
            heightInput = 1f;
        }
        else if (Input.GetKey(KeyCode.Q)) // Q để hạ thấp xuống (Giảm Y)
        {
            heightInput = -1f;
        }

        if (heightInput != 0f)
        {
            // Tính toán độ cao Y mới dựa trên input và tốc độ
            float newY = transform.position.y + heightInput * HeightChangeSpeed * Time.deltaTime;
            
            // Ép biên độ cao lọt vào khoảng MinHeight và MaxHeight
            newY = Mathf.Clamp(newY, MinHeight, MaxHeight);

            // Cập nhật lại vị trí camera
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    // Hàm ép biên Camera chỉ được di chuyển lọt thỏm trong hình vuông tạo bởi các góc
    Vector3 ClampToSquareBounds(Vector3 targetPosition)
    {
        float clampedX = Mathf.Clamp(targetPosition.x, MinX, MaxX);
        float clampedZ = Mathf.Clamp(targetPosition.z, MinZ, MaxZ);

        // Giữ nguyên độ cao Y hiện tại của camera khi di chuyển trên mặt phẳng XZ
        return new Vector3(clampedX, transform.position.y, clampedZ);
    }
}