namespace project_game_blazor.Models
{
    public class Game
    {
        private TicTacToeAI _ai; // AI-instans


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

        //Hålla koll på hur många gånger spelare har vunnit
        public int OWinner { get; set; }
        public int XWinner { get; set; }

        //Lista med alla rutor
        public List<Square> Squares { get; protected set; }

        //kolla vems tur det är
        public MarkEnum NextTurn { get; set; }

        //Vinnare
        public MarkEnum? Winner { get; set; }

        public Game()
        {
            _ai = new TicTacToeAI(); // initiera AI
            //Kalla på metoden ResetGame => skapa spelplanen
            ResetGame();

        }

        // Kolla efter AI-drag
        public void AITurn()
        {
            if (NextTurn == MarkEnum.X && Winner == null) // Om det är AI:s tur
            {
                var (row, col) = _ai.GetBestMove(Squares.ToArray());
                if (row >= 0 && col >= 0)
                {
                    Squares[row * 3 + col].Mark = MarkEnum.X; // Datorns drag
                    Next();
                }
            }
        }


        //Ändra spelare
        public void Next()
        {
            //Har någon vunnit?
            //Loopa igenom alla olika sätt man kan vinna på
            foreach (var winningCombination in WinningCombinations)
            {
                //Har spelare O vunnit
                if (Squares[winningCombination.Square1 - 1].Mark == MarkEnum.O && 
                    Squares[winningCombination.Square2 - 1].Mark == MarkEnum.O && 
                    Squares[winningCombination.Square3 - 1].Mark == MarkEnum.O)
                {
                    Winner = MarkEnum.O;
                }
                // Har spelare X vunnit
                else if (Squares[winningCombination.Square1 - 1].Mark == MarkEnum.X && 
                         Squares[winningCombination.Square2 - 1].Mark == MarkEnum.X && 
                         Squares[winningCombination.Square3 - 1].Mark == MarkEnum.X)
                {
                    Winner = MarkEnum.X;
                }
            }
            //Om det finns en vinnare
            if (Winner.HasValue)
            {
                //Öka antal vinster för vinnaren
                if (Winner == MarkEnum.O)
                {
                    //Öka med 1
                    OWinner += 1;
                }
                if (Winner == MarkEnum.X)
                {
                    //Öka med 1
                    XWinner += 1;
                }

                NextTurn = Winner.Value;
            }
            else
            {
                if (NextTurn == MarkEnum.O)
                {
                    NextTurn = MarkEnum.X;
                    AITurn(); // Låt AI spela
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