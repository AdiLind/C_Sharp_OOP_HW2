namespace Ex02.GameLogic
{
    public class GameManager
    {
        private readonly IGameUI r_GameUI;

        public GameManager(IGameUI i_GameUI)
        {
            r_GameUI = i_GameUI;
        }

        public void RunGame()
        {
            r_GameUI.ShowWelcomeMessage();
            int maxGuesses = r_GameUI.GetNumberOfAttempts();
            bool continuePlaying = true;

            while (continuePlaying)
            {
                playGame(maxGuesses);
                continuePlaying = r_GameUI.AskToPlayAgain();
            }

            r_GameUI.ShowGoodbyeMessage();
        }

        private void playGame(int i_MaxGuesses)
        {
            GameBoard gameBoard = new GameBoard(i_MaxGuesses);

            while (!gameBoard.IsGameOver)
            {
                r_GameUI.ClearScreen();
                r_GameUI.DisplayBoard(gameBoard);
                string guess = r_GameUI.GetGuessFromUser();
                if (isQuitCommand(guess))
                {
                    r_GameUI.ShowGoodbyeMessage();

                    return;
                }
                gameBoard.MakeGuess(guess);
            }

            r_GameUI.ClearScreen();
            r_GameUI.DisplayBoard(gameBoard);
            displayGameResult(gameBoard);
        }

        private bool isQuitCommand(string i_UserInput)
        {
            return i_UserInput == GameConstants.k_QuitButton.ToString();
        }

        private void displayGameResult(GameBoard i_GameBoard)
        {
            if (i_GameBoard.IsWon)
            {
                r_GameUI.ShowWinMessage(i_GameBoard.CurrentGuessNumber - 1);
            }
            else
            {
                r_GameUI.ShowLoseMessage(i_GameBoard.RevealSecret());
            }
        }
    }
}
