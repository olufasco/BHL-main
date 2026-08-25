using Microsoft.EntityFrameworkCore;

namespace BHL.Tournament.Data;

public class TournamentDbContext : DbContext
{
    public TournamentDbContext(DbContextOptions<TournamentDbContext> options) : base(options) { }

    public DbSet<Player> Players => Set<Player>();
    public DbSet<Division> Divisions => Set<Division>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Result> Results => Set<Result>();
    public DbSet<Standing> Standings => Set<Standing>();
    public DbSet<Season> Seasons => Set<Season>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>()
            .HasOne(p => p.Division)
            .WithMany(d => d.Players)
            .HasForeignKey(p => p.DivisionId);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.PlayerA)
            .WithMany(p => p.Matches)
            .HasForeignKey(m => m.PlayerAId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.PlayerB)
            .WithMany()
            .HasForeignKey(m => m.PlayerBId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Result>()
            .HasOne(r => r.Match)
            .WithOne(m => m.Result)
            .HasForeignKey<Result>(r => r.MatchId);
    }
}
