using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TeamsLektion3;

// Steg:
// 1. Skapa modeller (super hero och super power)
// 2. Skapa ett DbContext (DatabaseContext)
// 3. Länka / registrera DbContext med 'builder.Services.AddDbContext'
// 4. Skapa en migration (InitialCreate)
// 5. Uppdatera databasens struktur (dotnet ef database update)
// 6. Skapa lite DTOs som kan användas lite här och där (skapa heroes, returnera heroes m.m)
// 7. Skapa controller med endpoints för att hämta och skapa heroes

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseNpgsql(
                "Host=localhost;Database=teamslektion3;Username=postgres;Password=password"
            );
        });
        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}

public class SuperHero
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public List<SuperPower> SuperPowers { get; set; }

    public SuperHero() { }

    public SuperHero(string name, int age, List<SuperPower> superPowers)
    {
        this.Name = name;
        this.Age = age;
        this.SuperPowers = superPowers;
    }
}

// super hero : one to many : super power
// super power : many to one : super hero

public class SuperPower
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PowerLevel { get; set; }
    public SuperHero SuperHero { get; set; }

    public SuperPower() { }

    public SuperPower(string name, int powerLevel, SuperHero hero)
    {
        this.Name = name;
        this.PowerLevel = powerLevel;
        this.SuperHero = hero;
    }
}

public class DatabaseContext : DbContext
{
    public DbSet<SuperHero> SuperHeroes { get; set; }
    public DbSet<SuperPower> SuperPowers { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }
}

public class CreateSuperPowerDto
{
    public string Name { get; set; } = "";
    public int PowerLevel { get; set; } = 0;
}

public class CreateSuperHeroDto
{
    public string Name { get; set; } = "";
    public int Age { get; set; } = 0;
    public List<CreateSuperPowerDto> SuperPowers { get; set; } = new List<CreateSuperPowerDto>();
}

// Vi behöver en DTO här för att den riktiga modellen innehåller en relation till SuperPower, och SuperPower
// har en koppling tillbaka till SuperHero.
// Det bildar ett parsing problem som vi löser med denna DTO. I denna version så har SuperPowerDto inte längre
// en koppling tillbaka till SuperHeroDto.
public class SuperHeroDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public List<SuperPowerDto> SuperPowers { get; set; }

    public SuperHeroDto(SuperHero hero)
    {
        this.Id = hero.Id;
        this.Name = hero.Name;
        this.Age = hero.Age;
        this.SuperPowers = hero.SuperPowers.Select(power => new SuperPowerDto(power)).ToList();
    }
}

public class SuperPowerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PowerLevel { get; set; }

    public SuperPowerDto(SuperPower power)
    {
        this.Id = power.Id;
        this.Name = power.Name;
        this.PowerLevel = power.PowerLevel;
    }
}

[ApiController]
[Route("api/superhero")]
public class SuperHeroController : ControllerBase
{
    DatabaseContext context;

    public SuperHeroController(DatabaseContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult CreateSuperHero([FromBody] CreateSuperHeroDto dto)
    {
        SuperHero hero = new SuperHero(dto.Name, dto.Age, new List<SuperPower>());
        foreach (var superPowerDto in dto.SuperPowers)
        {
            SuperPower power = new SuperPower(superPowerDto.Name, superPowerDto.PowerLevel, hero);
            hero.SuperPowers.Add(power);

            // Spara SuperPower till databas
            context.SuperPowers.Add(power);
        }

        // Spara SuperHero till databas
        context.SuperHeroes.Add(hero);

        // Utför ändringar.
        context.SaveChanges();

        return Ok(new SuperHeroDto(hero));
    }

    [HttpGet]
    public List<SuperHeroDto> GetAll()
    {
        var list = context
            .SuperHeroes
            // Inkludera superpowers när den hämtar alla super heroes på nästa rad
            .Include(hero => hero.SuperPowers)
            // Hämta alla super heroes
            .ToList();
        return list.Select(hero => new SuperHeroDto(hero)).ToList();
    }
}
