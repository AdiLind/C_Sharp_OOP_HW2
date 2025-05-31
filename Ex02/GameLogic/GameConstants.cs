using System;

namespace Ex02.GameLogic
{
    public static class GameConstants
    {
        // Game Settings
        public const int k_MinGuessAttempts = 4;
        public const int k_MaxGuessAttempts = 10;
        public const int k_SequenceLength = 4;
        public const char k_MinLetter = 'A';
        public const char k_MaxLetter = 'H';
        public const int k_NumOfLegalLetters = 8;
        public const char k_QuitButton = 'Q';
        public const char k_BullChar = 'V';
        public const char k_HitChar = 'X';
        // Error Messages
        public const string k_InvalidLengthMessage = "Your guess must contain exactly {0} letters.";
        public const string k_NotUppercaseMessage = "Please enter only uppercase letters from {0} to {1}.";
        public const string k_OutOfRangeMessage = "Letter '{0}' is outside the valid range ({1}-{2}).";
        public const string k_DuplicateMessage = "You have already used the letter '{0}' more than once.";
        // UI Foramt
        public const string k_BoardHeader = "Current board status:";
        public const string k_PinsHeader = "|Pins:   |Result: |";
        public const string k_HeaderSeparator = "|========|========|";
        public const string k_RowSeparator = "|--------|--------|";
        public const string k_EmptyCell = "|        |        |";
        public const int k_CellWidth = 8;
    }
}