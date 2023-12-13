using System;
using System.Collections.Generic;

public class Game
{
    private Player player1;
    private Player player2;

    public Game(Player p1, Player p2)
    {
        player1 = p1;
        player2 = p2;
    }

    public Player GetRandomStartingPlayer()
    {
        return (new Random().Next(2) == 0) ? player1 : player2;
    }

    public List<Card> DrawThreeCards()
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < 3; i++)
        {
            Card card = GenerateRandomCard();
            cards.Add(card);
        }
        return cards;
    }
    public bool CheckForPenalty(List<Card> cards)
    {
        int penaltyCount = cards.Count(card => card.Type == Card.CardType.Penalti);

        return penaltyCount == 3;
    }
    public bool CheckForFalta(List<Card> cards)
    {
        return cards.All(card => card.Type == Card.CardType.Falta);
    }
    public bool CheckForYellowCard(List<Card> cards)
    {
        return cards.All(card => card.Type == Card.CardType.CartaoAmarelo);
    }
    public bool CheckForRedCard(List<Card> cards)
    {
        return cards.All(card => card.Type == Card.CardType.CartaoVermelho);
    }

    private Card GenerateRandomCard()
    {
        Array cardTypes = Enum.GetValues(typeof(Card.CardType));
        Card.CardType randomType = (Card.CardType)cardTypes.GetValue(new Random().Next(cardTypes.Length));
        return new Card(randomType);
    }

    public bool TryPenalty(Player attacker, Player defender, string choice)
    {
        Console.WriteLine($"{attacker.Name} chuta o pênalti em direção ao {choice}!");

        bool penaltyDefended = defender.DefendPenalty(choice);

        if (penaltyDefended)
        {
            Console.WriteLine($"{defender.Name} defendeu o pênalti!");
        }
        else
        {
            Console.WriteLine($"O pênalti foi convertido por {attacker.Name}!");
            attacker.UpdateLigaPoints(1);
        }

        return penaltyDefended;
    }
    public bool CheckForThreePenalties(List<Card> cards)
    {
        int penaltyCount = cards.Count(card => card.Type == Card.CardType.Penalti);

        return penaltyCount == 3;
    }

    public bool ChoosePenaltyDirection(Player attacker, Player defender, out string choice)
    {
        Console.WriteLine($"{attacker.Name} escolha a direção do pênalti (Direita, Esquerda, Centro): ");
        choice = Console.ReadLine();

        Console.WriteLine($"{defender.Name} escolha a direção da defesa (Direita, Esquerda, Centro): ");
        string defendChoice = Console.ReadLine();

        return choice == defendChoice;
    }

    public void PenaltyOutcome(bool penaltyDefended, Player attacker, Player defender)
    {
        if (penaltyDefended)
        {
            Console.WriteLine("DEFENDEU!!!");
        }
        else
        {
            Console.WriteLine("GOOOLAÇOOOO!!!");
            attacker.UpdateLigaPoints(1);
        }
    }
}
