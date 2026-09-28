public class ShopRequestDTO
{
    public string ShopId { get; set; }
    public string ShopName { get; set; }
    public string ShopCodeName { get; set; }
    public string ShopType { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public string ObjectType { get; set; }
    public string ObjectTable { get; set; }
    public int Sequence { get; set; }
}