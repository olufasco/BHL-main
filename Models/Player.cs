public class Player
{
    public int Id { get; set; }
    public string GamerTag { get; set; } = string.Empty;
    public int DivisionId { get; set; }

    // Performance stats
    public int Points { get; set; }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int GoalDifference => GoalsFor - GoalsAgainst;

    // Navigation
    public Division? Division { get; set; }
    public ICollection<Match>? Matches { get; set; }
}
