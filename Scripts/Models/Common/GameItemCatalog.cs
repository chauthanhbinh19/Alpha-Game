public class GameItemCatalog : BaseEntity
{
    public string ObjectId { get; set; }
    public string ObjectType { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public double Quality { get; set; }
}