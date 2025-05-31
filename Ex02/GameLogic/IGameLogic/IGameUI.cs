using System;

namespace Ex02.GameLogic
{
    public interface IGameUI
    {
        void ShowWelcomeMessage();
        int GetNumberOfAttempts();
        string GetGuessFromUser();
        void DisplayBoard(GameBoard i_Game);
        void ShowWinMessage(int i_NumberOfSteps);
        void ShowLoseMessage(string i_CorrectSequence);
        bool AskToPlayAgain();
        void ShowGoodbyeMessage();
        void ClearScreen();
    }
}
