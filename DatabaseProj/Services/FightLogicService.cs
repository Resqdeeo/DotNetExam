using DatabaseProj.Models;
using Models;

namespace DatabaseProj.Services;

public class FightLogicService : IFightLogicService
{
    private FightResult? _fightResult;
    private List<Round>? _fightLog;
    private int _roundCounts;

    public List<Round>? ProcessGame(Fight fight)
    {
        _fightResult = new FightResult(fight);
        _fightLog = new List<Round>();
        while (!_fightResult.Win)
        {
            var round = Fight();
            _fightLog.Add(round);
        }

        return _fightLog;
    }

    private Round Fight()
    {
        var curRound = new Round { Id = ++_roundCounts, Rounds = new List<FightResult>() };
        var playerAttackCount = _fightResult!.Player!.AttackPerRound;
        var monsterAttackCount = _fightResult.Monster!.AttackPerRound;

        while (playerAttackCount != 0 || monsterAttackCount != 0)
        {
            if (_fightResult.IsPlayerTurn)
            {
                if (playerAttackCount == 0)
                {
                    _fightResult.IsPlayerTurn = false;
                    continue;
                }
                
                curRound.Rounds.Add(Attack(_fightResult.Player!, _fightResult.Monster!));
                _fightResult.IsPlayerTurn = false;
                playerAttackCount--;
            }
            else
            {
                if (monsterAttackCount == 0)
                {
                    _fightResult.IsPlayerTurn = true;
                    continue;
                }
                
                curRound.Rounds.Add(Attack(_fightResult.Monster!, _fightResult.Player!));
                _fightResult.IsPlayerTurn = true;
                monsterAttackCount--;
            }
        }

        return curRound;
    }

    private FightResult Attack(Creature attackCreature, Creature aimCreature)
    {
        var attackDice = Dice.DiceTwenty.Roll();
        _fightResult!.AttackDice = attackDice;
        switch (attackDice)
        {
            case 1:
                _fightResult.Status = HitStatusType.CriticalMiss;
                break;
            case 20:
                aimCreature.HealthPoints -= CalculateDamage(true, attackCreature);
                _fightResult.Status = HitStatusType.CriticalHit;
                break;
            default:
                if (attackDice + attackCreature.AttackModifier > aimCreature.Ac)
                {
                    aimCreature.HealthPoints -= CalculateDamage(false, attackCreature);
                    _fightResult.Status = HitStatusType.Hit;
                }
                else
                {
                    _fightResult.Status = HitStatusType.Miss;
                }

                break;
        }

        if (aimCreature.HealthPoints <= 0)
        {
            _fightResult.Win = true;
        }

        return _fightResult.DeepClone();
    }

    private int CalculateDamage(bool isCriticalHits, Creature creature)
    {
        var totalDamage = 0;
        var numOfThrows = int.Parse(creature.Damage![0].ToString());
        var damageDice = new Dice(int.Parse(creature.Damage![2].ToString()));

        for (var i = 0; i < numOfThrows; i++)
        {
            totalDamage += damageDice.Roll();
        }

        _fightResult!.DamageDice = totalDamage;
        totalDamage += creature.DamageModifier;
        if (isCriticalHits) totalDamage *= 2;
        _fightResult.Damage = totalDamage;
        return totalDamage;
    }
}