using System;

public class UserGearEquipmentDetail
{
    public string UserId { get; set; }
    public int NumberOfGear { get; set; }
    public string EquipmentId { get; set; }
    public int Position { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}