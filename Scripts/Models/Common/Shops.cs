using System.Collections.Generic;

public class Shops : FullAuditedEntity
{
    public string ShopId { get; set; }
    public string ShopName { get; set; }
    public string ShopCodeName { get; set; }
    public string ShopType { get; set; }
    public string ResetType { get; set; }
    public string Description { get; set; }
    public List<ShopDetails> ShopDetails { get; set; }
}