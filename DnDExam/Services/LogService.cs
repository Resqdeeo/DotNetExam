using Models;

namespace DnDExam.Services;

public static class LogService
{
    public static (List<string>, bool) ConvertToLog(List<Round> fightLog)
    {
        var builder = new List<string>();
        var isPlayerLose = false;

        foreach (var round in fightLog)
        {
            builder.Add($"Round {round.Id}");
            foreach (var fight in round.Rounds!)
            {
                Creature attackCreature, aimCreature;
                if (fight.IsPlayerTurn)
                {
                    attackCreature = fight.Player!;
                    aimCreature = fight.Monster!;
                }
                else
                {
                    attackCreature = fight.Monster!;
                    aimCreature = fight.Player!;
                }
                
                builder.Add(attackCreature.Name!);
                switch (fight.Status)
                {
                    case HitStatusType.CriticalHit:
                        builder.Add($"{fight.AttackDice} (+extra {attackCreature.AttackModifier}) critical damage! " +
                                    $"{fight.DamageDice} (+extra {attackCreature.DamageModifier}) * 2 takes {fight.Damage} " +
                                    $"damage to player {aimCreature.Name}({aimCreature.HealthPoints}). ");
                        break;
                    case HitStatusType.CriticalMiss:
                        builder.Add($"{fight.AttackDice}(+extra {attackCreature.AttackModifier}) critical miss! ");
                        break;
                    case HitStatusType.Hit:
                        builder.Add(
                            $"{fight.AttackDice} (+extra {attackCreature.AttackModifier}) more than {aimCreature.Ac}, hit! " +
                            $"{fight.DamageDice} (+extra {attackCreature.DamageModifier}) takes {fight.Damage} " +
                            $"damage to player {aimCreature.Name}({aimCreature.HealthPoints}). ");
                        break;
                    case HitStatusType.Miss:
                        builder.Add($"{fight.AttackDice}(+extra {attackCreature.AttackModifier}) lower than {aimCreature.Ac}, miss! ");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                if (!fight.Win)
                    continue;

                if (attackCreature == fight.Monster!)
                    isPlayerLose = true;
                
                builder.Add($"{aimCreature.Name} dead. {attackCreature.Name} win!!!");
                break;
            }
        }

        return (builder, isPlayerLose);
    }
}