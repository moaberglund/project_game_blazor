﻿using project_game_blazor.Models;

namespace DT071G_Project_TicTacToe.Components.Models
{
    public class Game
    {

        //Kolla efter vinst
        public List<WinningCombination> WinningCombinations = new List<WinningCombination>
        {
            //Horisontellt
            new WinningCombination(1, 2, 3),
            new WinningCombination(4, 5, 6),
            new WinningCombination(7, 8, 9),
            //Vertikalt
            new WinningCombination(1, 4, 7),
            new WinningCombination(2, 5, 8),
            new WinningCombination(3, 6, 9),
            //Diagonalt
            new WinningCombination(1, 5, 9),
            new WinningCombination(3, 5, 7)
        };

        //Lista med alla rutor
        public List<Square> Squares { get; protected set; }

        //kolla vems tur det är
        public MarkEnum NextTurn { get; set; }

        //Vinnare
        public MarkEnum? Winner { get; set; }

        public Game()
        {
            //Kalla på metoden ResetGame => skapa spelplanen
            ResetGame();

        }

        //Ändra spelare
        public void Next()
        {
            //Har någon vunnit?
            //Loopa igenom alla olika sätt man kan vinna på
            foreach (var winningCombination in WinningCombinations)
            {
                //Har spelare O vunnit
                if (Squares[winningCombination.Square1 - 1].Mark == MarkEnum.O && Squares[winningCombination.Square2 - 1].Mark == MarkEnum.O && Squares[winningCombination.Square3 - 1].Mark == MarkEnum.O)
                {
                    Winner = MarkEnum.O;
                }
                // Har spelare X vunnit
                else if (Squares[winningCombination.Square1 - 1].Mark == MarkEnum.X && Squares[winningCombination.Square2 - 1].Mark == MarkEnum.X && Squares[winningCombination.Square3 - 1].Mark == MarkEnum.X)
                {
                    Winner = MarkEnum.X;
                }
            }
            //Om det finns en vinnare
            if (Winner.HasValue)
            {
                NextTurn = Winner.Value;
            }
            else
            {
                if (NextTurn == MarkEnum.O)
                {
                    NextTurn = MarkEnum.X;
                }
                else
                {
                    NextTurn = MarkEnum.O;
                }
            }
        }


        //Vid start och restart av spelet
        public void ResetGame()
        {
            //skapa instans av listan
            Squares = new List<Square>();

            //Sätt startspelare (O default)
            //Om det finns en vinnare från förra rondend, låt denne börja
            NextTurn = (Winner.HasValue ? Winner.Value : MarkEnum.O);
            //nollställ vinnare
            Winner = null;

            //Skapa 9 rutor
            for (var tt = 1; tt <= 9; tt++)
            {
                Squares.Add(new Square(tt));
            }
        }
    }
}