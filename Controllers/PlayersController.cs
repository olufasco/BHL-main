using Microsoft.AspNetCore.Mvc;
using BHL.Tournament.Data;

namespace BHL.Tournament.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly TournamentDbContext _context;

    public PlayersController(TournamentDbContext context) => _context = context;

    [HttpGet]
    public IActionResult GetPlayers() => Ok(_context.Players.ToList());

    [HttpPost]
    public IActionResult AddPlayer(Player player)
    {
        _context.Players.Add(player);
        _context.SaveChanges();
        return Ok(player);
    }
}
