using System;
using System.Collections.Generic;

public class Mails : FullAuditedEntity
{
    public string Id { get; set; }
    public string SenderId { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string Type { get; set; }           // SYSTEM, REWARD, RANKING, ANNOUNCEMENT...
    public bool IsGlobal { get; set; }         // TRUE: Thư gửi toàn server
    public DateTime? ExpiredAt { get; set; }   // Hạn chót nhận quà/thư

    // Navigation Properties
    public List<MailItems> Items { get; set; } = new List<MailItems>();
    public List<UserMails> UserMails { get; set; } = new List<UserMails>();
}