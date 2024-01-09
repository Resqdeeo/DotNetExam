using Models;

namespace DatabaseProj.Services;

public class TakeRandomMonsterService(AppDbContext context) : ITakeRandomMonsterService
{
    public Monster GetRandomMonster()
    {
        IEnumerable<Monster> monsters = context.Monsters!;
        var random = new Random();
        var id = random.Next(1, monsters.Count() + 1);
        return monsters.First(m => m.Id == id);
    }
}