namespace TimeTracker.DAL.Entities;

public class CardEntity
{
    /// <summary>
    /// NFC card UID
    /// </summary>
    public string CardUid { get; set; } = default!;
    public DateTime AssignedAt { get; set; }
    
    public int UserId { get; set; }
    
    public UserEntity User { get; set; } = default!;
}