public class GameArchitectureTools : FullAuditedEntity
{
    public string Id { get; set; }
    public string CategoryId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public int MaxLevel { get; set; }
}