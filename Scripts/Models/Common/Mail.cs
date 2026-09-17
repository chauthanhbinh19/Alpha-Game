using System.Collections.Generic;

public class Mail : FullAuditedEntity
{
    public string Id { get; set; }
    public string ReceiverId { get; set; }
    public string SenderId { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string Type { get; set; }
    public bool IsRead { get; set; }
    public bool IsClaimed { get; set; } // Trạng thái đã nhận quà hay chưa

    // Danh sách quà đính kèm
    public List<MailItems> Items { get; set; } = new List<MailItems>();
}