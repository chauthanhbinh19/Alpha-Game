using System.Collections.Generic;

public class ShopDTO
{
    public string ShopId { get; set; }
    public string ShopName { get; set; }
    public string ShopCodeName { get; set; }
    public string ShopType { get; set; }
    public string ResetType { get; set; }
    public string Description { get; set; }
    public ShopDetails ShopDetail { get; set; }
    public List<ShopDetails> ShopDetails { get; set; }
}