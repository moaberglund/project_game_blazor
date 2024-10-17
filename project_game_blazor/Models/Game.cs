using project_game_blazor.Models;

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
        public List<Square> Squares { get; }

        //kolla vems tur det är
        public MarkEnum NextTurn { get; set; }

        //Vinnare
        public MarkEnum? Winner { get; set; }

        public Game()
        {
            //skapa instans av listan
            Squares = new List<Square>();
            //Kalla på metoden ResetGame => skapa spelplanen
            ResetGame();

        }

        //Ändra spelare
        public void Next()
        {
            //Starta med null
            Winner = null;

            //Har någon vunnit?
            foreach (var winningCombination in WinningCombinations)
            {
                //Square1, Square2 och Square3 är index i listan Squares
                if (Squares[winningCombination.Square1 - 1].Mark == Squares[winningCombination.Square2 - 1].Mark &&
                   Squares[winningCombination.Square2 - 1].Mark == Squares[winningCombination.Square3 - 1].Mark &&
                   Squares[winningCombination.Square1 - 1].Mark != null)
                {
                    Winner = Squares[winningCombination.Square1 - 1].Mark;
                    return;
                }
            }
            if (Winner.HasValue)
            {
                ResetGame();
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
            //sätt default startspelare
            NextTurn = MarkEnum.O;

            //Skapa 9 rutor
            for (var tt = 1; tt <= 9; tt++)
            {
                Squares.Add(new Square(tt));
            }
        }
    }
}