using DT071G_Project_TicTacToe.Components.Models;

namespace project_game_blazor.Models
{
    public class TicTacToeAI
    {

        private const char Player = 'O'; // Anta att spelaren är 'O'
        private const char AI = 'X'; // Datorn är 'X'

        //Hitta den bästa rutan för datorn att spela
        public (int, int) GetBestMove(Square[] squares)
        {
            int bestValue = int.MinValue;
            (int bestRow, int bestCol) = (-1, -1);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (squares[i * 3 + j].Mark == null) // Om rutan är tom
                    {
                        squares[i * 3 + j].Mark = MarkEnum.X; // Gör datorns drag
                        int moveValue = Minimax(squares, 0, false);
                        squares[i * 3 + j].Mark = null; // Återställ rutan

                        if (moveValue > bestValue)
                        {
                            bestRow = i;
                            bestCol = j;
                            bestValue = moveValue;
                        }
                    }
                }
            }

            return (bestRow, bestCol);
        }

        // Implementerar Minimax-algoritmen för att simulera spelrundor
        private int Minimax(Square[] squares, int depth, bool isMaximizing)
        {
            int score = Evaluate(squares);

            // Kontrollera om någon har vunnit
            if (score == 10) return score - depth;
            if (score == -10) return score + depth;
            if (IsBoardFull(squares)) return 0;

            if (isMaximizing)
            {
                int best = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (squares[i * 3 + j].Mark == null)
                        {
                            squares[i * 3 + j].Mark = MarkEnum.X; // Datorns drag
                            best = Math.Max(best, Minimax(squares, depth + 1, false));
                            squares[i * 3 + j].Mark = null; // Återställ rutan
                        }
                    }
                }
                return best;
            }
            else
            {
                int best = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (squares[i * 3 + j].Mark == null)
                        {
                            squares[i * 3 + j].Mark = MarkEnum.O; // Motståndarens drag
                            best = Math.Min(best, Minimax(squares, depth + 1, true));
                            squares[i * 3 + j].Mark = null; // Återställ rutan
                        }
                    }
                }
                return best;
            }
        }

        //Kontrollerar om det finns en vinnare och ger poäng baserat på resultatet
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
                    return 10;
                }
                if (squares[combo.Square1 - 1].Mark == MarkEnum.O &&
                    squares[combo.Square2 - 1].Mark == MarkEnum.O &&
                    squares[combo.Square3 - 1].Mark == MarkEnum.O)
                {
                    return -10;
                }
            }

            return 0; // Ingen vinnare
        }

        //Kollar om spelbrädet är fullt
        private bool IsBoardFull(Square[] squares)
        {
            return squares.All(square => square.Mark.HasValue);
        }
    }
}
