using System.Collections.Generic;

namespace KnuckleBones
{
    public static class Rules
    {
        public static int CalculateScore(int[][] board)
        {
            return CalculateRowsScore(board) + CalculateColsScore(board);
        }

        public static int CalculateRowsScore(int[][] board)
        {
            int total = 0;
            for (int row = 0; row < 3; row++)
            {
                total += GetLineScore(GetRowValues(board, row));
            }
            return total;
        }

        public static int CalculateColsScore(int[][] board)
        {
            int total = 0;
            for (int col = 0; col < 3; col++)
            {
                total += GetLineScore(GetColValues(board, col));
            }
            return total;
        }

        public static int GetLineScore(int[] values)
        {
            int total = 0;
            var counts = new Dictionary<int, int>();
            int distinctCount = 0;
            bool isFull = true;

            foreach (int val in values)
            {
                if (val > 0)
                {
                    if (!counts.ContainsKey(val))
                    {
                        counts[val] = 0;
                        distinctCount++;
                    }
                    counts[val]++;
                }
                else
                {
                    isFull = false;
                }
            }

            foreach (var pair in counts)
            {
                int val = pair.Key;
                int count = pair.Value;
                total += (val * count) * count;
            }

            // Option 3: Full House / Diversity Bonus
            // If row/column is full and has 3 different numbers, add 15 points
            if (isFull && distinctCount == 3)
            {
                total += 15;
            }

            return total;
        }

        public static int[] GetRowValues(int[][] board, int row)
        {
            return new int[] { board[0][row], board[1][row], board[2][row] };
        }

        public static int[] GetColValues(int[][] board, int col)
        {
            return new int[] { board[col][0], board[col][1], board[col][2] };
        }

        public static void HandleDestruction(int row, int dieValue, int[][] opponentBoard)
        {
            // Rule: "Whenever the player places a die in a row and the opponent has 
            // that same number in that same row, the opponent's die with that same number need to be removed"
            for (int col = 0; col < 3; col++)
            {
                if (opponentBoard[col][row] == dieValue)
                {
                    opponentBoard[col][row] = 0;
                }
            }
        }

        public static bool IsBoardFull(int[][] board)
        {
            for (int c = 0; c < 3; c++)
            {
                for (int r = 0; r < 3; r++)
                {
                    if (board[c][r] == 0) return false;
                }
            }
            return true;
        }
    }
}
