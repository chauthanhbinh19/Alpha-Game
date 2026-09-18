using System;

public class UserMails
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string MailId { get; set; }
    public bool IsRead { get; set; }
    public bool IsClaimed { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime? ClaimedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation Properties
    public Mails Mail { get; set; }
}