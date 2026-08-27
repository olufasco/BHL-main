public class Result
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int WinnerId { get; set; }
    public int LoserId { get; set; }
    public int PointsAwarded { get; set; }
    public int GoalDifference { get; set; }
    public bool MultiplierApplied { get; set; }

    // Navigation
    public Match? Match { get; set; }
}
