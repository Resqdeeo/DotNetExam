using Microsoft.EntityFrameworkCore;
using Models;

namespace DatabaseProj;

public class AppDbContext : DbContext
{
    public DbSet<Monster>? Monsters { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var skeleton = new Monster
        {
            Id = 1,
            Name = "Скелет Бладфина",
            HealthPoints = 10,
            AttackModifier = 0,
            AttackPerRound = 1,
            Damage = "1d6",
            DamageModifier = 2,
            Ac = 10
        };
        var aarakocra = new Monster
        {
            Id = 2,
            Name = "Aarakocra",
            HealthPoints = 25,
            AttackModifier = 2,
            AttackPerRound = 1,
            Damage = "2d6",
            DamageModifier = 3,
            Ac = 8
        };
        var wizard = new Monster
        {
            Id = 3,
            Name = "Wizard",
            HealthPoints = 15,
            AttackModifier = 5,
            AttackPerRound = 2,
            Damage = "3d3",
            DamageModifier = 2,
            Ac = 20
        };
        var acererak = new Monster
        {
            Id = 4,
            Name = "Acererak",
            HealthPoints = 30,
            AttackModifier = 1,
            AttackPerRound = 2,
            Damage = "2d5",
            DamageModifier = 2,
            Ac = 6
        };
        var dragon = new Monster
        {
            Id = 5,
            Name = "Dragon",
            HealthPoints = 25,
            AttackModifier = 2,
            AttackPerRound = 1,
            Damage = "2d6",
            DamageModifier = 3,
            Ac = 10
        };
        var assasin = new Monster
        {
            Id = 6,
            Name = "Assasin",
            HealthPoints = 15,
            AttackModifier = 5,
            AttackPerRound = 3,
            Damage = "2d3",
            DamageModifier = 3,
            Ac = 4
        };
        modelBuilder.Entity<Monster>().HasData(skeleton, aarakocra, wizard, acererak, dragon, assasin);
        base.OnModelCreating(modelBuilder);
    }
}