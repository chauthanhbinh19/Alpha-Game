public class TeamEmblems
{
    public string UserId { get; set; }
    public string TeamId { get; set; }
    public int Position { get; set; }
    public string CardType { get; set; }
    public string EmblemId { get; set; }
    public int EmblemQuantity { get; set; }
    public Emblems Emblem { get; set; } = new Emblems();
    public TeamEmblems()
    {

    }
}
