using System;
using Ex02.GameLogic;
using Ex02.ConsoleUtils;

namespace Ex02.GameUi
{
    public class Program
    {
        public static void Main()
        {
            RunGame();
        }

        private static void RunGame()
        {
            showWelcomeMessage();
            int maxGuesses = getNumberOfAttemptsFromUser();
            bool playAgain = true;

            while (playAgain)
            {
                GameBoard game = new GameBoard(maxGuesses);

                while (!game.IsGameOver)
                {
                    Screen.Clear();
                    displayBoard(game);
                    string userInput = getGuessFromUser();

                    if (userInput == GameConstants.k_QuitButton.ToString())
                    {
                        Console.WriteLine("Thanks for playing!");
                        return;
                    }

                    game.MakeGuess(userInput);
                }

                Screen.Clear();
                displayBoard(game);

                if (game.IsWon)
                {
                    Console.WriteLine("You Win!");
                }
                else
                {
                    Console.WriteLine("You’ve used all your guesses, you lost. The correct sequence was: {0}", game.RevealSecret());
                }

                playAgain = askToPlayAgain();
            }

            Console.WriteLine("Goodbye!");
        }

        private static void showWelcomeMessage()
        {
            Console.WriteLine("=== Welcome to Bulls and Cows ===\n");
        }

        private static int getNumberOfAttemptsFromUser()
        {
            int maxGuesses = 0;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("Enter number of guess attempts ({0}-{1}): ",
                    GameConstants.k_MinGuessAttempts, GameConstants.k_MaxGuessAttempts);

                string input = Console.ReadLine();

                if (int.TryParse(input, out maxGuesses) && GameEngine.IsValidGuessAttempts(maxGuesses))
                {
                    isValid = true;
                }

                else
                {
                    Console.WriteLine("Invalid number. Try again.\n");
                }
            }

            return maxGuesses;
        }

        private static string getGuessFromUser()
        {
            string userInput = string.Empty;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("Enter your guess (or '{0}' to quit): ", GameConstants.k_QuitButton);
                userInput = Console.ReadLine();

                if (userInput == GameConstants.k_QuitButton.ToString())
                {
                    isValid = true;
                }
                else
                {
                    string errorMessage;
                    if (GameEngine.IsValidGuess(userInput, out errorMessage))
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine(errorMessage + "\n");
                    }
                }
            }

            return userInput;
        }

        private static void displayBoard(GameBoard i_Game)
        {
            Console.WriteLine("Current board status:\n");
            Console.WriteLine("|Pins:   |Result: |");
            Console.WriteLine("|========|========|");

            for (int i = 0; i < i_Game.NumberOfGuesses; i++)
            {
                GuessResult result = i_Game.GetGuessResult(i);
                string guess = formatCell(result.GuessString);
                string feedback = formatCell(result.GetResultString());

                Console.WriteLine(string.Format("|{0}|{1}|", guess, feedback));
                Console.WriteLine("|--------|--------|");
            }

            for (int i = i_Game.NumberOfGuesses; i < i_Game.MaxGuesses; i++)
            {
                Console.WriteLine("|        |        |");
                Console.WriteLine("|--------|--------|");
            }

            Console.WriteLine();
        }

        private static string formatCell(string i_Content)
        {
            return i_Content.PadRight(8);
        }

        private static bool askToPlayAgain()
        {
            Console.Write("\nDo you want to play again? (Y/N): ");
            string answer = Console.ReadLine();

            return answer.Trim().ToUpper() == "Y";
        }
    }
}