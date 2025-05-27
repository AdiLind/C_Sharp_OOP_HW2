using System;


namespace Ex02.GameLogic
{
    public static class GameLogic
    {
        private static Random s_Random = new Random();

        public static char[] GenerateRandomSequence()
        {
            char[] sequence = new char[GameConstants.k_SequenceLength];
            bool[] usedLettersFlags = new bool[GameConstants.k_NumOfLegalLetters];

            for (int i = 0; i < GameConstants.k_SequenceLength; i++)
            {
                int randomLetterIndex = s_Random.Next(GameConstants.k_NumOfLegalLetters);

                while(usedLettersFlags[randomLetterIndex])
                {
                    randomLetterIndex = s_Random.Next(GameConstants.k_NumOfLegalLetters);
                }

                usedLettersFlags[randomLetterIndex] = true;
                sequence[i] = (char)(GameConstants.k_MinLetter + randomLetterIndex);
            }

            return sequence;
        }

        public static bool IsValidGuessAttempts(int i_MaxGuess) // we need to check before calling this function that the input is a number in the main method
        {
            return i_MaxGuess >= GameConstants.k_MinGuessAttempts && i_MaxGuess <= GameConstants.k_MaxGuessAttempts;
        }

        public static bool IsAllUpper(string i_Input)
        {
            foreach (char c in i_Input)
            {
                if (!char.IsUpper(c) && c != GameConstants.k_QuitButton)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsValidGuess(string i_Input)
        {
            if ((i_Input.ToUpper() == GameConstants.k_QuitButton.ToString()))
            {
                return true; /// maybe break? we need to continue and recheck this section after we will implement the exit function
            }

            if ((i_Input.Length != GameConstants.k_SequenceLength))
            {
                return false;
            }

            if (!IsAllUpper(i_Input))
            {
                Console.WriteLine(string.Format("Please enter only uppercase letters from {0} to {1}.",
                    GameConstants.k_MinLetter , GameConstants.k_MaxLetter));
                return false;
            }

            bool[] usedLettersFlags = new bool[GameConstants.k_NumOfLegalLetters];
            foreach (char letter in  i_Input)
            {
                if (letter < GameConstants.k_MinLetter || letter > GameConstants.k_MaxLetter)
                {
                    Console.WriteLine(string.Format("Please enter only letters from {0} to {1}.",
                        GameConstants.k_MinLetter, GameConstants.k_MaxLetter));
                    return false;
                }
                int letterIndex = letter - GameConstants.k_MinLetter;
                if (usedLettersFlags[letterIndex])
                {
                    Console.WriteLine("You have already used the letter '{0}' more than once. Please try again.", letter);
                    return false;
                }
                usedLettersFlags[letterIndex] = true;
            }

            return true;
        }
    }
}
