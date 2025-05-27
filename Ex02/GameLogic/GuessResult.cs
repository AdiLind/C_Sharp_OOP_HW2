using System;

namespace Ex02.GameLogic
{
    public struct GuessResult
    {
        private readonly char[] r_Guess;
        private readonly int r_BullsCount;
        private readonly int r_HitsCount;

        public GuessResult(char[] i_Guess, int i_BullsCount, int i_HitsCount)
        {
            r_Guess = new char[i_Guess.Length];

            for (int index = 0; index < i_Guess.Length; index++)
            {
                r_Guess[index] = i_Guess[index];
            }

            r_BullsCount = i_BullsCount;
            r_HitsCount = i_HitsCount;
        }

        public string GuessString 
        { 
            get { return new string(r_Guess); } 
        }

        public int BullsCount
        {
            get { return r_BullsCount; }
        }

        public int HitsCount
        {
            get { return r_HitsCount; }
        }

        public string GetResultString()
        {
            char[] result = new char[r_BullsCount + r_HitsCount];
            int index = 0;

            for (int i = 0; i < r_BullsCount; i++)
            {
                result[index++] = GameConstants.k_BullChar;
            }

            for (int i = 0; i < r_HitsCount; i++)
            {
                result[index++] = GameConstants.k_HitChar;
            }

            string resultString = new string(result);

            return resultString;
        }

        public bool IsPerfectGuess()
        {
            return r_BullsCount == GameConstants.k_SequenceLength;
        }
    }
}