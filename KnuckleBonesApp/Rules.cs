using System.Collections.Generic;

namespace KnuckleBones
{
    public static class Rules
    {
        public static int CalculateScore(int[][] board)
        {
            int total = 0;
            for (int col = 0; col < 3; col++)
            {
                var counts = new Dictionary<int, int>();
                for (int row = 0; row < 3; row++)
                {
                    int val = board[col][row];
                    if (val > 0)
                    {
                        if (!counts.ContainsKey(val)) counts[val] = 0;
                        counts[val]++;
                    }
                }

                foreach (var pair in counts)
                {
                    int val = pair.Key;
                    int count = pair.Value;
                    // Rule: sum of dice values * count
                    // (val * count) * count
                    total += (val * count) * count;
                }
            }
            return total;
        }

        public static void HandleDestruction(int col, int dieValue, int[][] opponentBoard)
        {
            // Rule: "Placing a die in a column directly opposite an opponent's die 
            // destroys all dice of that same value in the opponent's corresponding column."
            for (int row = 0; row < 3; row++)
            {
                if (opponentBoard[col][row] == dieValue)
                {
                    opponentBoard[col][row] = 0;
                }
            }
            // Compacting is usually standard for Knucklebones to keep dice at the "bottom" 
            // but we will keep them in place if preferred, or compact them. 
            // The prompt implies a "stack" so we will compact to maintain the stack appearance.
            CompactColumn(opponentBoard, col);
        }

        private static void CompactColumn(int[][] board, int col)
        {
            int[] newCol = new int[3];
            int index = 0;
            for (int r = 0; r < 3; r++)
            {
                if (board[col][r] != 0) newCol[index++] = board[col][r];
            }
            board[col] = newCol;
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
