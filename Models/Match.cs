public class Match
{
    public int Id { get; set; }
    public int PlayerAId { get; set; }
    public int PlayerBId { get; set; }
    public int ScoreA { get; set; }
    public int ScoreB { get; set; }
    // If true, one of the players forfeited. Use `ForfeitedById` to indicate who.
    public bool Forfeited { get; set; }
    public int? ForfeitedById { get; set; }
    public DateTime PlayedDate { get; set; }

    // Navigation
    public Player? PlayerA { get; set; }
    public Player? PlayerB { get; set; }
    public Result? Result { get; set; }
}
