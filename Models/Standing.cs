public class Standing
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int DivisionId { get; set; }

    public int Played { get; set; }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int GoalDifference => GoalsFor - GoalsAgainst;
    public int Points { get; set; }
    public int Rank { get; set; }

    /// Navigation
    public Player? Player { get; set; }
    public Division? Division { get; set; }
}
