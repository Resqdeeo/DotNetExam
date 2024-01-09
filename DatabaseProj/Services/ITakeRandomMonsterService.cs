using Models;

namespace DatabaseProj.Services;

public interface ITakeRandomMonsterService
{
    public Monster GetRandomMonster();
}