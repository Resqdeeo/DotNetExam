using System.Text.Json;
using DatabaseProj.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DatabaseProj.Controllers;

[ApiController]
public class GameLogicController(IFightLogicService logic) : ControllerBase
{
    [HttpPost]
    [Route("GetFightResult")]
    public List<Round>? GetFightResult(string fight) => logic.ProcessGame(JsonSerializer.Deserialize<Fight>(fight)!);
}