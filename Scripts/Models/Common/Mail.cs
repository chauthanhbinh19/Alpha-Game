public class Mail : FullAuditedEntity
{
    public string Id { get; set; }
    public string ReceiverId { get; set; }
    public string SenderId { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string Type { get; set; }
    public string ObjectId { get; set; }
    public string ObjectType { get; set; }
    public bool IsRead { get; set; }
}