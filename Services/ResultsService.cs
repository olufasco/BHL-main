using BHL.Tournament.Data;

namespace BHL.Tournament.Services;

public class ResultsService
{
    private readonly TournamentDbContext _context;

    public ResultsService(TournamentDbContext context) => _context = context;

    public void ProcessMatch(Match match)
    {
        var playerA = _context.Players.Find(match.PlayerAId);
        var playerB = _context.Players.Find(match.PlayerBId);

        if (playerA == null || playerB == null) return;

        // Ensure match is tracked
        if (!_context.Matches.Local.Any(m => m.Id == match.Id))
            _context.Matches.Attach(match);

        var result = new Result
        {
            Match = match,
            MatchId = match.Id,
            PointsAwarded = 0,
            MultiplierApplied = false,
            GoalDifference = 0,
            WinnerId = 0,
            LoserId = 0
        };

        if (match.Forfeited && match.ForfeitedById.HasValue)
        {
            // Determine forfeiter and opponent
            var forfeiterId = match.ForfeitedById.Value;
            var forfeiter = forfeiterId == playerA.Id ? playerA : playerB;
            var opponent = forfeiterId == playerA.Id ? playerB : playerA;

            opponent.Points += 3;
            opponent.Wins++;
            forfeiter.Losses++;

            // Apply the -15 goals penalty to the forfeiter as goals against and +15 goals for opponent
            forfeiter.GoalsAgainst += 15;
            opponent.GoalsFor += 15;

            result.WinnerId = opponent.Id;
            result.LoserId = forfeiter.Id;
            result.PointsAwarded = 3;
            result.GoalDifference = 15;
            result.MultiplierApplied = false;
        }
        else
        {
            // Regular match
            if (match.ScoreA > match.ScoreB)
            {
                playerA.Points += 3;
                playerA.Wins++;
                playerB.Losses++;
                playerA.GoalsFor += match.ScoreA;
                playerA.GoalsAgainst += match.ScoreB;
                playerB.GoalsFor += match.ScoreB;
                playerB.GoalsAgainst += match.ScoreA;

                result.WinnerId = playerA.Id;
                result.LoserId = playerB.Id;
                result.PointsAwarded = 3;
                result.GoalDifference = match.ScoreA - match.ScoreB;
            }
            else if (match.ScoreA < match.ScoreB)
            {
                // Apply multiplier rule: winner's score is multiplied by 2
                var effectiveWinnerScore = match.ScoreB * 2;
                playerB.Points += 3;
                playerB.Wins++;
                playerA.Losses++;

                playerB.GoalsFor += effectiveWinnerScore;
                playerB.GoalsAgainst += match.ScoreA;
                playerA.GoalsFor += match.ScoreA;
                playerA.GoalsAgainst += effectiveWinnerScore;

                result.WinnerId = playerB.Id;
                result.LoserId = playerA.Id;
                result.PointsAwarded = 3;
                result.GoalDifference = effectiveWinnerScore - match.ScoreA;
                result.MultiplierApplied = true;
            }
            else
            {
                // Draw
                playerA.Points += 1;
                playerB.Points += 1;
                playerA.Draws++;
                playerB.Draws++;

                playerA.GoalsFor += match.ScoreA;
                playerA.GoalsAgainst += match.ScoreB;
                playerB.GoalsFor += match.ScoreB;
                playerB.GoalsAgainst += match.ScoreA;

                result.WinnerId = 0;
                result.LoserId = 0;
                result.PointsAwarded = 1;
                result.GoalDifference = 0;
            }
        }

        _context.Results.Add(result);
        _context.SaveChanges();
    }
}
