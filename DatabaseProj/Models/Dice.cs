namespace DatabaseProj.Models;

public class Dice(int count)
{
    public static Dice DiceTwenty => new(20);

    public int NumberOfSides { get; } = count;

    public int Roll()
    {
        var random = new Random();
        return random.Next(1, NumberOfSides);
    }
}