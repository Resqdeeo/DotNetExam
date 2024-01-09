using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;
using DnDExam.Models;
using DnDExam.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DnDExam.Controllers;

public class GameController : Controller
{
    private readonly HttpClient? _client;

    public GameController(HttpClient? client)
    {
        _client = client;
        SetupClientHeaders();
    }

    private void SetupClientHeaders()
    {
        _client!.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }
    
    public async Task<IActionResult> Game(string name, int hp, int attackModifier, int apr, string damage,
        int damageModifier, int ac)
    {
        var gotName = HttpUtility.HtmlDecode(name);

        var monster = await GetMonster();

        var player = new Player
        {
            Id = 0,
            Name = gotName,
            HealthPoints = hp,
            AttackModifier = attackModifier,
            AttackPerRound = apr,
            Damage = damage,
            DamageModifier = damageModifier,
            Ac = ac
        };

        var fight = new Fight
        {
            Player = player, 
            Monster = monster
        };
        
        var fightLog = await GetFightResult(fight);

        var viewModel = new GameViewModel
        {
            Player = player,
            Monster = monster,
            FightLog = fightLog.Item1,
            IsPlayerLose = fightLog.Item2
        };

        return View(viewModel);
    }

    public async Task<Monster> GetMonster()
    {
        var response = await _client!.GetAsync("https://localhost:4444/GetRandomMonsterFromDatabase");

        if (!response.IsSuccessStatusCode)
            throw new ArgumentException("Wrong with getting monster from Database");

        var monster = await response.Content.ReadFromJsonAsync<Monster>();
        return monster!;
    }

    public async Task<(List<string>, bool)> GetFightResult(Fight fight)
    {
        var json = JsonSerializer.Serialize(fight);
        var requestMessage = new HttpRequestMessage();
        requestMessage.Method = HttpMethod.Post;
        requestMessage.RequestUri = new Uri($"https://localhost:4444/GetFightResult?fight={json}");
        using var response = await _client!.SendAsync(requestMessage);

        if (!response.IsSuccessStatusCode)
            throw new ArgumentException("Wrong with getting logs from server");
        
        var content = await response.Content.ReadFromJsonAsync<List<Round>>();
        var fightLog = LogService.ConvertToLog(content!);
        return fightLog;
    }
}