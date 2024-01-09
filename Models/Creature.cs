namespace Models;

public class Creature
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int HealthPoints { get; set; }
    public int AttackModifier { get; set; }
    public int AttackPerRound { get; set; }
    public string? Damage { get; set; }
    public int DamageModifier { get; set; }
    public int Ac { get; set; }
}