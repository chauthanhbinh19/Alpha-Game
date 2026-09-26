public class ShopDetails : FullAuditedEntity
{
    public string ShopId { get; set; }  
    public string ObjectId { get; set; } 
    public string ObjectType { get; set; } 
    public double ObjectQuantity { get; set; } 
    public string CurrencyId { get; set; }  
    public double Price { get; set; } 
    public int StockLimit { get; set; } 
    public int BuyLimitPerUser { get; set; }

    public int PurchaseCount { get; set; }
    public string ObjectName { get; set; }
    public string ObjectImage { get; set; }
    public string CurrencyImage { get; set; }
}