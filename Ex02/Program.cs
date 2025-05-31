using System;
using Ex02.GameLogic;
using Ex02.ConsoleUtils;
using Ex02.ConsoleUI;

namespace Ex02
{
    public class Program
    {
        public static void Main()
        {
            IGameUI consoleUI = new ConsoleGameUI();
            GameManager gameManager = new GameManager(consoleUI);
            gameManager.RunGame();
        }
    }
}