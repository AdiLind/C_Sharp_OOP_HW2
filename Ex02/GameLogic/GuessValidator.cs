using System;

namespace Ex02.GameLogic
{
    public static class GuessValidator
    {
        public static bool IsValidGuess(string i_Input, out string o_ErrorMessage)
        {
            o_ErrorMessage = string.Empty;
            if (isQuitCommand(i_Input))
            {
                return true;
            }

            if (!hasValidLength(i_Input, out o_ErrorMessage))
            {
                return false;
            }

            if (!hasOnlyUppercaseLetters(i_Input, out o_ErrorMessage))
            {
                return false;
            }

            if (!hasLettersInValidRange(i_Input, out o_ErrorMessage))
            {
                return false;
            }

            if (!hasNoDuplicateLetters(i_Input, out o_ErrorMessage))
            {
                return false;
            }

            return true;
        }

        private static bool isQuitCommand(string i_Input)
        {
            return i_Input == GameConstants.k_QuitButton.ToString();
        }

        private static bool hasValidLength(string i_Input, out string o_ErrorMessage)
        {
            o_ErrorMessage = null;

            if (i_Input.Length != GameConstants.k_SequenceLength)
            {
                o_ErrorMessage = string.Format(
                    GameConstants.k_InvalidLengthMessage,
                    GameConstants.k_SequenceLength);

                return false;
            }

            return true;
        }
        private static bool hasOnlyUppercaseLetters(string i_Input, out string o_ErrorMessage)
        {
            o_ErrorMessage = null;

            foreach (char letter in i_Input)
            {
                if (!char.IsUpper(letter))
                {
                    o_ErrorMessage = string.Format(
                        GameConstants.k_NotUppercaseMessage,
                        GameConstants.k_MinLetter,
                        GameConstants.k_MaxLetter);

                    return false;
                }
            }

            return true;
        }

        private static bool hasLettersInValidRange(string i_Input, out string o_ErrorMessage)
        {
            o_ErrorMessage = null;

            foreach (char letter in i_Input)
            {
                if (!isLetterInRange(letter))
                {
                    o_ErrorMessage = string.Format(
                        GameConstants.k_OutOfRangeMessage,
                        letter,
                        GameConstants.k_MinLetter,
                        GameConstants.k_MaxLetter);

                    return false;
                }
            }

            return true;
        }

        private static bool isLetterInRange(char i_Letter)
        {
            return i_Letter >= GameConstants.k_MinLetter &&
                   i_Letter <= GameConstants.k_MaxLetter;
        }

        private static bool hasNoDuplicateLetters(string i_Input, out string o_ErrorMessage)
        {
            o_ErrorMessage = null;
            bool[] usedLetterFlags = new bool[GameConstants.k_NumOfLegalLetters];

            foreach (char letter in i_Input)
            {
                int letterIndex = letter - GameConstants.k_MinLetter;

                if (usedLetterFlags[letterIndex])
                {
                    o_ErrorMessage = string.Format(
                        GameConstants.k_DuplicateMessage,
                        letter);

                    return false;
                }

                usedLetterFlags[letterIndex] = true;
            }

            return true;
        }

        public static bool IsValidNumberOfGuesses(int i_MaxGuesses)
        {
            return i_MaxGuesses >= GameConstants.k_MinGuessAttempts &&
                   i_MaxGuesses <= GameConstants.k_MaxGuessAttempts;
        }
    }
}
