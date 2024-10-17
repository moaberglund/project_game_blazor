namespace DT071G_Project_TicTacToe.Components.Models
{
    public class Game
    {

        //Lista med alla rutor
        public List<Square> Squares { get; }

        //kolla vems tur det är
        public MarkEnum NextTurn { get; set; }

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
            if (NextTurn == MarkEnum.O)
            {
                NextTurn = MarkEnum.X;
            }
            else
            {
                NextTurn = MarkEnum.O;
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