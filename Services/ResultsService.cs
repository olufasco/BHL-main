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

        if (match.Forfeited)
        {
            playerB.Points += 3;
            playerA.GoalsAgainst += 15;
        }
        else
        {
            if (match.ScoreA > match.ScoreB)
            {
                playerA.Points += 3;
                playerA.Wins++;
                playerB.Losses++;
            }
            else if (match.ScoreA < match.ScoreB)
            {
                match.ScoreB *= 2; // multiplier rule
                playerB.Points += 3;
                playerB.Wins++;
                playerA.Losses++;
            }
            else
            {
                playerA.Points++;
                playerB.Points++;
                playerA.Draws++;
                playerB.Draws++;
            }
        }

        _context.SaveChanges();
    }
}
