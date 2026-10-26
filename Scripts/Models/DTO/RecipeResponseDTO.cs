using System.Collections.Generic;

public class RecipeResponseDTO
{
    public List<RecipeItemDTO> Recipes { get; set; } = new List<RecipeItemDTO>();

    // Key: ObjectId (hoặc Key: "ObjectType_ObjectId" nếu ObjectId giữa các bảng trùng nhau)
    // Value: Số lượng sở hữu thực tế của User
    public Dictionary<string, double> UserInventory { get; set; } = new Dictionary<string, double>();
}