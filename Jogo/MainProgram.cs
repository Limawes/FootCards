using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Bem-vindo ao Jogo de Futebol!");

        Console.Write("Digite o nome do jogador 1: ");
        string player1Name = Console.ReadLine();
        Player player1 = new Player(player1Name);

        Console.Write("Digite o nome do jogador 2: ");
        string player2Name = Console.ReadLine();
        Player player2 = new Player(player2Name);

        Game game = new Game(player1, player2);
        Player currentPlayer = game.GetRandomStartingPlayer();
        Player otherPlayer = (currentPlayer == player1) ? player2 : player1;

        while (player1.Energy > 0 && player2.Energy > 0)
        {
            Console.WriteLine($"\n{currentPlayer.Name}'s Turn");
            List<Card> playerCards = game.DrawThreeCards();
            List<Card> opponentCards = game.DrawThreeCards();

            Console.WriteLine($"{currentPlayer.Name}:");
            PrintCards(playerCards);

            Console.WriteLine($"{otherPlayer.Name}:");
            PrintCards(opponentCards);

            int playerScore = CalculateRoundScore(playerCards);
            int opponentScore = CalculateRoundScore(opponentCards);

            Console.WriteLine($"{currentPlayer.Name} pontuou: {playerScore}");
            Console.WriteLine($"{otherPlayer.Name} pontuou: {opponentScore}");

            if (playerScore > opponentScore)
            {
                currentPlayer.UpdateLigaPoints(1);
                Console.WriteLine($"{currentPlayer.Name} ganha a rodada!");
            }
            else if (opponentScore > playerScore)
            {
                otherPlayer.UpdateLigaPoints(1);
                Console.WriteLine($"{otherPlayer.Name} ganha a rodada!");
            }
            else
            {
                Console.WriteLine("Empate!");
            }

            Console.WriteLine($"Pontuação Atual: {player1.Name} - {player1.LigaPoints} vs {player2.Name} - {player2.LigaPoints}");

            if (game.CheckForThreePenalties(playerCards))
            {
                string penaltyChoice;
                bool penaltyDefended = game.ChoosePenaltyDirection(currentPlayer, otherPlayer, out penaltyChoice);
                game.PenaltyOutcome(penaltyDefended, currentPlayer, otherPlayer);

                if (penaltyDefended)
                {
                    Console.WriteLine("TAAFFAREEEEEU, VAI QUE É SUA TAFFAREL!!");
                }
                else
                {
                    Console.WriteLine("GOOOOOOOOOOOOOOOOOOOL!!!");
                }

                Console.WriteLine($"Pontuação Atual: {player1.Name} - {player1.LigaPoints} vs {player2.Name} - {player2.LigaPoints}");
            }
        }
        if (player1.LigaPoints > player2.LigaPoints)
        {
            Console.WriteLine($"\nPARABÉNS {player1.Name}!\nVocê venceu com {player1.LigaPoints} pontos.");
            Console.WriteLine($"{player2.Name} fez {player2.LigaPoints} pontos.");
        }
        else if (player2.LigaPoints > player1.LigaPoints)
        {
            Console.WriteLine($"\nPARABÉNS {player2.Name}!\nVocê venceu com {player2.LigaPoints} pontos.");
            Console.WriteLine($"{player1.Name} fez {player1.LigaPoints} pontos.");
        }
        else
        {
            Console.WriteLine("\nA partida terminou em empate!");
            Console.WriteLine($"Ambos os jogadores fizeram {player1.LigaPoints} pontos.");
        }

        Console.WriteLine("\nDigite '0' para uma nova partida ou '-1' para sair.");
        string choice = Console.ReadLine();

        if (choice == "0")
        {
            Main(args);
        }
        else if (choice == "-1")
        {
            Environment.Exit(0);
        }
    }

    private static void PrintCards(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cards[i].Type}");
        }
    }

    private static int CalculateRoundScore(List<Card> cards)
    {
        int score = 0;

        foreach (var card in cards)
        {
            switch (card.Type)
            {
                case Card.CardType.Gol:
                    score += 3;
                    break;
                case Card.CardType.Penalti:
                    score += 2;
                    break;
                case Card.CardType.Falta:
                case Card.CardType.CartaoAmarelo:
                    score += 1;
                    break;
                case Card.CardType.CartaoVermelho:
                    score += 0;
                    break;
                case Card.CardType.Energia:
                    score += 2;
                    break;
            }
        }

        return score;
    }
}
