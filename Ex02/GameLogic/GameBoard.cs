using System;

namespace Ex02.GameLogic
{
    public class GameBoard
    {
        private readonly char[] r_ComputerChoice;
        private readonly GuessResult[] r_GuessHistory;
        private readonly int r_MaxGuesses;
        private int m_CurrentGuessIndex;
        private bool m_IsWon;

        public GameBoard(int i_MaxGuesses)
        {
            r_MaxGuesses = i_MaxGuesses;
            r_GuessHistory = new GuessResult[i_MaxGuesses];
            r_ComputerChoice = GameEngine.GenerateRandomSequence();
            m_CurrentGuessIndex = 0;
            m_IsWon = false;
        }

        public int MaxGuesses
        {
            get { return r_MaxGuesses; }
        }

        public int CurrentGuessNumber
        {
            get { return m_CurrentGuessIndex + 1; }
        }

        public int RemainingGuesses
        {
            get { return r_MaxGuesses - m_CurrentGuessIndex; }
        }

        public bool IsGameOver
        {
            get { return m_IsWon || m_CurrentGuessIndex >= r_MaxGuesses; }
        }

        public bool IsWon
        {
            get { return m_IsWon; }
        }

        public string RevealSecret()
        {
            return IsGameOver ? new string(r_ComputerChoice) : string.Empty;
        }

        public GuessResult GetGuessResult(int i_Index)
        {
            if (i_Index < 0 || i_Index >= m_CurrentGuessIndex)
            {
                throw new ArgumentOutOfRangeException("i_Index");
            }

            return r_GuessHistory[i_Index];
        }

        public int NumberOfGuesses
        {
            get { return m_CurrentGuessIndex; }
        }

        public bool MakeGuess(string i_Guess)
        {
            bool guessAccepted = true;

            if (IsGameOver)
            {
                guessAccepted = false;
            }
            else
            {
                char[] guessArray = i_Guess.ToCharArray();
                GuessResult result = GameEngine.EvaluateGuess(guessArray, r_ComputerChoice);
                r_GuessHistory[m_CurrentGuessIndex] = result;
                m_CurrentGuessIndex++;

                if (result.IsPerfectGuess())
                {
                    m_IsWon = true;
                }
            }

            return guessAccepted;
        }
    }
}