using Models;

namespace DnDExam.Models;

public class GameViewModel
{
    internal Player? Player { get; init; }
    
    internal Monster? Monster { get; init; }
    
    internal List<string>? FightLog { get; init; }
    
    internal bool IsPlayerLose { get; init; }
}