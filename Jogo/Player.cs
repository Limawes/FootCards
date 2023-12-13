public class Player
{
    public string Name { get; private set; }
    public int LigaPoints { get; private set; }
    public int Energy { get; private set; }
    public int YellowCards { get; private set; }

    public Player(string name)
    {
        Name = name;
        LigaPoints = 10;
        Energy = 10;
        YellowCards = 0;
    }

    public void UpdateYellowCards(int cards)
    {
        YellowCards += cards;
    }
    public bool DefendPenalty(string choice)
    {
        return choice != "Centro" && new Random().Next(2) == 0;
    }

    public void UpdateLigaPoints(int points)
    {
        LigaPoints += points;
    }

    public void UpdateEnergy(int energy)
    {
        Energy += energy;
    }
}
