public class Card
{
    public enum CardType { Gol, Penalti, Falta, CartaoAmarelo, CartaoVermelho, Energia }

    public CardType Type { get; private set; }

    public Card(CardType type)
    {
        Type = type;
    }
}
