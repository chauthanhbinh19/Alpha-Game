using UnityEngine;

public class CardVisual : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    void Awake()
    {
        // Lấy MeshRenderer gắn trên chính tấm Plane này
        meshRenderer = GetComponent<MeshRenderer>();
        
        if (meshRenderer == null)
        {
            Debug.LogError($"GameObject {gameObject.name} không có MeshRenderer! Hãy chắc chắn đây là một Plane.");
        }
    }

    /// <summary>
    /// Hàm nạp dữ liệu và đổi hình ảnh hiển thị cho thẻ bài
    /// </summary>
    public void SetupVisual(CardBase cardData)
    {
        if (cardData == null || meshRenderer == null) return;

        Texture2D cardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(cardData.Image));

        if (cardTexture != null)
        {
            meshRenderer.material.SetTexture("_MainTex", cardTexture);
            
            Debug.Log($"[Visual] Đã đổi ảnh thành công cho {cardData.Name} từ đường dẫn: {imagePath}");
        }
        else
        {
            Debug.LogError($"[Visual Error] Không tìm thấy ảnh tại đường dẫn: Assets/Resources/{imagePath}");
        }
    }
}