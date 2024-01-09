using DatabaseProj.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DatabaseProj.Controllers;

[ApiController]
public class MonsterController(ITakeRandomMonsterService monsterService) : ControllerBase
{
    [HttpGet]
    [Route("GetRandomMonsterFromDatabase")]
    public Monster GetRandomMonster() => monsterService.GetRandomMonster();
}