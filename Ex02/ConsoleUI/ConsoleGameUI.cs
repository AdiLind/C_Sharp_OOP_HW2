using System;
using Ex02.GameLogic;
using Ex02.ConsoleUtils;

namespace Ex02.ConsoleUI
{
    public class ConsoleGameUI : IGameUI
    {
        public void ShowWelcomeMessage()
        {
            Console.WriteLine("=== Welcome to Bulls and Cows ===\n");
        }
        public int GetNumberOfAttempts()
        {
            int maxGuesses = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.Write("Enter number of guess attempts ({0}-{1}): ",
                                    GameConstants.k_MinGuessAttempts, GameConstants.k_MaxGuessAttempts);

                string input = Console.ReadLine();

                if (int.TryParse(input, out maxGuesses))
                {
                    if (GuessValidator.IsValidNumberOfGuesses(maxGuesses))
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Number must be between {0} and {1}. Try again.",
                            GameConstants.k_MinGuessAttempts,
                            GameConstants.k_MaxGuessAttempts);
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.WriteLine();
                }
            }

            return maxGuesses;
        }

        public string GetGuessFromUser()
        {
            string userInput = string.Empty;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("Enter your guess (or '{0}' to quit): ",
                    GameConstants.k_QuitButton);
                userInput = Console.ReadLine();

                if (userInput == GameConstants.k_QuitButton.ToString())
                {
                    isValid = true;
                }
                else
                {
                    string errorMessage;

                    if (GuessValidator.IsValidGuess(userInput, out errorMessage))
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

        public void displayBoard(GameBoard i_Game)
        {
            Console.WriteLine(GameConstants.k_BoardHeader);
            Console.WriteLine(GameConstants.k_PinsHeader);
            Console.WriteLine(GameConstants.k_HeaderSeparator);

            for (int i = 0; i < i_Game.NumberOfGuesses; i++)
            {
                displayGuessRow(i_Game.GetGuessResult(i));
            }
            int remainingRows = i_Game.MaxGuesses - i_Game.NumberOfGuesses;
            for (int i = 0; i < remainingRows; i++)
            {
                Console.WriteLine(GameConstants.k_EmptyCell);
                Console.WriteLine(GameConstants.k_RowSeparator);
            }

            Console.WriteLine();
        }

        public void ShowWinMessage(int i_NumberOfSteps)
        {
            Console.WriteLine("Congratulations! You guessed the sequence in {0} steps!", i_NumberOfSteps);
        }

        public void ShowLoseMessage(string i_CorrectSequence)
        {
            Console.WriteLine("No more guesses allowed. You Lost.");
            Console.WriteLine("The correct sequence was: {0}", i_CorrectSequence);
        }

        public bool AskToPlayAgain()
        {
            bool isValidInput = false;
            bool playAgain = false;

            while (!isValidInput)
            {
                Console.Write("Would you like to start a new game? (Y/N): ");
                string answer = Console.ReadLine();

                if (!string.IsNullOrEmpty(answer))
                {
                    char userDecision = char.ToUpper(answer.Trim()[0]);

                    if (userDecision == 'Y')
                    {
                        playAgain = true;
                        isValidInput = true;
                    }
                    else if (userDecision == 'N')
                    {
                        playAgain = false;
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter Y or N.");
                        Console.WriteLine();
                    }
                }
            }

            return playAgain;
        }

        public void ShowGoodbyeMessage()
        {
            Console.WriteLine("Thank you for playing! Goodbye!");
        }

        public void ClearScreen()
        {
            Console.Clear();
        }

        private void displayGuessRow(GuessResult i_GuessResult)
        {
            string guessCell = formatCell(i_GuessResult.GuessString);
            string resultCell = formatCell(i_GuessResult.GetResultString());

            Console.WriteLine("|{0}|{1}|", guessCell, resultCell);
            Console.WriteLine(GameConstants.k_RowSeparator);
        }

        private string formatCell(string i_CellContent)
        {
            int padding = GameConstants.k_CellWidth - i_CellContent.Length;
            if (padding > 0)
            {
                return i_CellContent + new string(' ', padding);
            }
            else
            {
                return i_CellContent;
            }
        }

        public void DisplayBoard(GameBoard i_Game)
        {
            Console.WriteLine(GameConstants.k_BoardHeader);
            Console.WriteLine();
            Console.WriteLine(GameConstants.k_PinsHeader);
            Console.WriteLine(GameConstants.k_HeaderSeparator);

            for (int i = 0; i < i_Game.NumberOfGuesses; i++)
            {
                displayGuessRow(i_Game.GetGuessResult(i));
            }

            int remainingRows = i_Game.MaxGuesses - i_Game.NumberOfGuesses;
            for (int i = 0; i < remainingRows; i++)
            {
                Console.WriteLine(GameConstants.k_EmptyCell);
                Console.WriteLine(GameConstants.k_RowSeparator);
            }

            Console.WriteLine();
        }
    }
}