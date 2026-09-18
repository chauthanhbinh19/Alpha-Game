using System;

public class MailItems
{
    public string Id { get; set; }
    public string MailId { get; set; }
    public string ObjectId { get; set; }       // ID của Card, Item, Gem, Gold...
    public string ObjectType { get; set; }     // CARD_HERO, ITEM, CURRENCY...
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }

    // Dữ liệu tạm/DTO hiển thị Client (Không map vào bảng mail_items trong DB)
    public string ObjectName { get; set; }
    public string ObjectImage { get; set; }

    // Navigation Property
    public Mails Mail { get; set; }
}