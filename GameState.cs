using Raylib_cs;

namespace KnuckleBones
{
    public class GameState
    {
        public int[][] Player1Board = new int[3][] { new int[3], new int[3], new int[3] };
        public int[][] Player2Board = new int[3][] { new int[3], new int[3], new int[3] };
        public int Player1Score => Rules.CalculateScore(Player1Board);
        public int Player2Score => Rules.CalculateScore(Player2Board);
        public int CurrentDie;
        public bool Player1Turn = true;
        public bool GameOver = false;
        public Difficulty CurrentDifficulty = Difficulty.Medium;

        public GameState()
        {
            CurrentDie = Raylib.GetRandomValue(1, 6);
        }

        public bool PlaceDie(int col, int preferredRow = -1)
        {
            int[][] myBoard = Player1Turn ? Player1Board : Player2Board;
            int[][] opponentBoard = Player1Turn ? Player2Board : Player1Board;

            int actualRow = -1;

            if (preferredRow >= 0 && preferredRow < 3 && myBoard[col][preferredRow] == 0)
            {
                actualRow = preferredRow;
            }
            else
            {
                for (int row = 0; row < 3; row++)
                {
                    if (myBoard[col][row] == 0)
                    {
                        actualRow = row;
                        break;
                    }
                }
            }

            if (actualRow != -1)
            {
                myBoard[col][actualRow] = CurrentDie;

                // Rule: "Whenever the player places a die in a row/col and the opponent has
                // that same number in that same row/col, the opponent's die with that same number need to be removed"
                Rules.HandleDestruction(col, actualRow, CurrentDie, opponentBoard);

                AdvanceTurn();
                return true;
            }
            return false;
        }

        private void AdvanceTurn()
        {
            if (Rules.IsBoardFull(Player1Board) || Rules.IsBoardFull(Player2Board))
            {
                GameOver = true;
            }

            if (!GameOver)
            {
                Player1Turn = !Player1Turn;
                CurrentDie = Raylib.GetRandomValue(1, 6);
            }
        }
    }
}
