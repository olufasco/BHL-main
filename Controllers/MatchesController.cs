using Microsoft.AspNetCore.Mvc;
using BHL.Tournament.Data;
using BHL.Tournament.Services;

namespace BHL.Tournament.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchesController : ControllerBase
{
    private readonly TournamentDbContext _context;
    private readonly ResultsService _resultsService;

    public MatchesController(TournamentDbContext context, ResultsService resultsService)
    {
        _context = context;
        _resultsService = resultsService;
    }

    [HttpGet]
    public IActionResult GetMatches() => Ok(_context.Matches.ToList());

    [HttpPost]
    public IActionResult CreateMatch(Match match)
    {
        match.PlayedDate = match.PlayedDate == default ? DateTime.UtcNow : match.PlayedDate;
        _context.Matches.Add(match);
        _context.SaveChanges();

        // Reload saved match with ids and navigate
        var savedMatch = _context.Matches.Find(match.Id);

        _resultsService.ProcessMatch(savedMatch);

        return Ok(savedMatch);
    }
}
