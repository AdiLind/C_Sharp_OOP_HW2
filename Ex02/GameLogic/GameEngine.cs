using System;

namespace Ex02.GameLogic
{
    public static class GameEngine
    {
        private static Random s_Random = new Random();

        public static char[] GenerateRandomSequence()
        {
            char[] sequence = new char[GameConstants.k_SequenceLength];
            bool[] usedLettersFlags = new bool[GameConstants.k_NumOfLegalLetters];

            for (int i = 0; i < GameConstants.k_SequenceLength; i++)
            {
                int randomLetterIndex = s_Random.Next(GameConstants.k_NumOfLegalLetters);

                while (usedLettersFlags[randomLetterIndex])
                {
                    randomLetterIndex = s_Random.Next(GameConstants.k_NumOfLegalLetters);
                }

                usedLettersFlags[randomLetterIndex] = true;
                sequence[i] = (char)(GameConstants.k_MinLetter + randomLetterIndex);
            }

            return sequence;
        }

        public static bool IsValidGuessAttempts(int i_MaxGuess)
        {
            return i_MaxGuess >= GameConstants.k_MinGuessAttempts && i_MaxGuess <= GameConstants.k_MaxGuessAttempts;
        }

        public static bool IsAllUpper(string i_Input)
        {
            bool isAllUpper = true;

            foreach (char c in i_Input)
            {
                if (!char.IsUpper(c) && c != GameConstants.k_QuitButton)
                {
                    isAllUpper = false;
                    break;
                }
            }

            return isAllUpper;
        }

        public static bool IsValidGuess(string i_Input, out string o_ErrorMessage)
        {
            bool isValid= true;
            o_ErrorMessage = null;

            if (i_Input == GameConstants.k_QuitButton.ToString())
            {
                isValid = true;
            }

            else if (i_Input.Length != GameConstants.k_SequenceLength)
            {
                o_ErrorMessage = string.Format(
                    "Your guess must contain exactly {0} letters.", GameConstants.k_SequenceLength);
                isValid = false;
            }
            else if (!IsAllUpper(i_Input))
            {
                o_ErrorMessage = string.Format(
                    "Please enter only uppercase letters from {0} to {1}.",
                    GameConstants.k_MinLetter, GameConstants.k_MaxLetter);
                isValid = false;
            }
            else
            {
                bool[] usedFlags = new bool[GameConstants.k_NumOfLegalLetters];

                foreach (char letter in i_Input)
                {
                    if (letter < GameConstants.k_MinLetter || letter > GameConstants.k_MaxLetter)
                    {
                        o_ErrorMessage = string.Format(
                            "Letter '{0}' is outside the valid range ({1}-{2}).",
                            letter, GameConstants.k_MinLetter, GameConstants.k_MaxLetter);
                        isValid = false;
                        break;
                    }

                    int index = letter - GameConstants.k_MinLetter;
                    if (usedFlags[index])
                    {
                        o_ErrorMessage = string.Format(
                            "You have already used the letter '{0}' more than once.", letter);
                        isValid = false;
                        break;
                    }

                    usedFlags[index] = true;
                }
            }

            return isValid;
        }

        public static GuessResult EvaluateGuess(char[] i_GuessInput, char[] i_Secret)
        {
            int bullShotsCount = 0;
            int hitsCount = 0;
            bool[] guessUsedFlags = new bool[GameConstants.k_SequenceLength];
            bool[] secretLetterUsageFlags = new bool[GameConstants.k_SequenceLength];

            for (int i = 0; i < GameConstants.k_SequenceLength; i++)
            {
                if (i_GuessInput[i] == i_Secret[i])
                {
                    guessUsedFlags[i] = true;
                    secretLetterUsageFlags[i] = true;
                    bullShotsCount++;
                }
            }

            for (int i = 0; i < GameConstants.k_SequenceLength; i++)
            {
                if (!guessUsedFlags[i])
                {
                    for (int j = 0; j < GameConstants.k_SequenceLength; j++)
                    {
                        if (!secretLetterUsageFlags[j] && i_Secret[j] == i_GuessInput[i])
                        {
                            secretLetterUsageFlags[j] = true;
                            hitsCount++;
                            break;
                        }
                    }
                }
            }

            return new GuessResult(i_GuessInput, bullShotsCount, hitsCount);
        }
    }
}