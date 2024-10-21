namespace project_game_blazor.Models
{
    //Klass för att spela mot datorn med hjälp av AI som beräknar bästa draget
    public class TicTacToeAI
    {

        private const char Player = 'O'; // Anta att spelaren är 'O'
        private const char AI = 'X'; // Datorn är 'X'

        //Hitta den bästa rutan för datorn att spela
        public (int, int) GetBestMove(Square[] squares)
        {
            int bestValue = int.MinValue; // Håller det bästa värdet för AI:s drag
            (int bestRow, int bestCol) = (-1, -1); // Håller den bästa raden och kolumnen

            // Loopa genom varje ruta i 3x3 brädet
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Kolla om rutan är tom
                    if (squares[i * 3 + j].Mark == null)
                    {
                        // Gör datorns drag
                        squares[i * 3 + j].Mark = MarkEnum.X;
                        // Beräkna värdet av detta drag
                        int moveValue = Minimax(squares, 0, false);
                        // Återställ rutan till tom
                        squares[i * 3 + j].Mark = null;

                        // Om det här draget är bättre än tidigare, uppdatera det bästa draget
                        if (moveValue > bestValue)
                        {
                            bestRow = i;
                            bestCol = j;
                            bestValue = moveValue;
                        }
                    }
                }
            }

            // Returnera bästa draget
            return (bestRow, bestCol);
        }

        // Implementerar Minimax-algoritmen för att simulera spelrundor
        // Minimax är en algoritm som används inom bla AI för att minimera den möjliga förlusten/det värsta scenariot
        private int Minimax(Square[] squares, int depth, bool isMaximizing)
        {
            // Utvärdera brädet
            int score = Evaluate(squares);

            // Kontrollera om någon har vunnit
            if (score == 10) return score - depth;  // Om AI vunnit
            if (score == -10) return score + depth; // Om spelaren vunnit
            if (IsBoardFull(squares)) return 0;     // Om det är oavgjort

            // Om det är AI:s tur
            if (isMaximizing)
            {

                // Håll bästa värdet för AI
                int best = int.MinValue;
                // Loopa genom brädet
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Kolla om rutan är tom
                        if (squares[i * 3 + j].Mark == null)
                        {
                            squares[i * 3 + j].Mark = MarkEnum.X; // Datorns drag
                            best = Math.Max(best, Minimax(squares, depth + 1, false)); //Uppdatera bästa draget
                            squares[i * 3 + j].Mark = null; // Återställ rutan
                        }
                    }
                }
                return best;  // Returnera bästa värdet för AI
            }
            else // Om det är spelarens tur
            {
                int best = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (squares[i * 3 + j].Mark == null)
                        {
                            squares[i * 3 + j].Mark = MarkEnum.O; // Motståndarens drag
                            best = Math.Min(best, Minimax(squares, depth + 1, true)); // Uppdatera bästa draget
                            squares[i * 3 + j].Mark = null; // Återställ rutan
                        }
                    }
                }
                return best; // Returnera bästa värdet för spelaren
            }
        }

        // Utvärdera brädet för att avgöra vinnare
        // Kontrollerar om det finns en vinnare och ger poäng baserat på resultatet
        private int Evaluate(Square[] squares)
        {
            // Kolla för vinnande kombinationer
            var winningCombinations = new List<WinningCombination>
            {
                new WinningCombination(1, 2, 3),
                new WinningCombination(4, 5, 6),
                new WinningCombination(7, 8, 9),
                new WinningCombination(1, 4, 7),
                new WinningCombination(2, 5, 8),
                new WinningCombination(3, 6, 9),
                new WinningCombination(1, 5, 9),
                new WinningCombination(3, 5, 7)
            };

            foreach (var combo in winningCombinations)
            {
                if (squares[combo.Square1 - 1].Mark == MarkEnum.X &&
                    squares[combo.Square2 - 1].Mark == MarkEnum.X &&
                    squares[combo.Square3 - 1].Mark == MarkEnum.X)
                {
                    return 10; // AI:n har vunnit
                }
                if (squares[combo.Square1 - 1].Mark == MarkEnum.O &&
                    squares[combo.Square2 - 1].Mark == MarkEnum.O &&
                    squares[combo.Square3 - 1].Mark == MarkEnum.O)
                {
                    return -10; // Spelaren har vunnit
                }
            }

            return 0; // Ingen vinnare
        }

        //Kollar om spelbrädet är fullt => oavgjort
        private bool IsBoardFull(Square[] squares)
        {
            // Retur sann om alla rutor är markerade
            return squares.All(square => square.Mark.HasValue);
        }
    }
}
