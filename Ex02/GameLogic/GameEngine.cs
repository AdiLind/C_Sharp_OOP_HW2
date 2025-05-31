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