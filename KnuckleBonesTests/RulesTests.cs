using Xunit;
using KnuckleBones;

namespace KnuckleBonesTests
{
    public class RulesTests
    {
        [Fact]
        public void CalculateScore_SingleDie_ReturnsValue()
        {
            int[][] board = new int[3][] { new int[3], new int[3], new int[3] };
            board[0][0] = 5;
            
            // Per new rules: Row 0 has a 5 (score 5) + Col 0 has a 5 (score 5) = 10
            Assert.Equal(10, Rules.CalculateScore(board));
        }

        [Fact]
        public void CalculateScore_DoubleInRow_ReturnsMultipliedScore()
        {
            // Row 0 has two 5s in Col 0 and Col 1
            int[][] board = new int[3][] { 
                new int[3] { 5, 0, 0 }, 
                new int[3] { 5, 0, 0 }, 
                new int[3] { 0, 0, 0 } 
            };
            
            // Row 0: two 5s = (5*2)*2 = 20
            // Col 0: one 5 = 5
            // Col 1: one 5 = 5
            // Total: 20 + 5 + 5 = 30
            Assert.Equal(30, Rules.CalculateScore(board));
        }

        [Fact]
        public void CalculateScore_TripleInRow_ReturnsMultipliedScore()
        {
            // Row 0 has three 6s
            int[][] board = new int[3][] { 
                new int[3] { 6, 0, 0 }, 
                new int[3] { 6, 0, 0 }, 
                new int[3] { 6, 0, 0 } 
            };
            
            // Row 0: three 6s = (6*3)*3 = 54
            // Col 0, 1, 2: one 6 each = 6*3 = 18
            // Total: 54 + 18 = 72
            Assert.Equal(72, Rules.CalculateScore(board));
        }

        [Fact]
        public void CalculateScore_UserExample_Returns129()
        {
            // Board setup ([col][row]):
            // Col 0: 5, 3, 3 -> Score: 5 + (3*2)*2 = 17
            // Col 1: 1, 1, 6 -> Score: (1*2)*2 + 6 = 10
            // Col 2: 5, 6, 6 -> Score: 5 + (6*2)*2 = 29
            // Row 0: 5, 1, 5 -> Score: (5*2)*2 + 1 = 21
            // Row 1: 3, 1, 6 -> Score: 3+1+6 = 10
            // Row 2: 3, 6, 6 -> Score: 3 + (6*2)*2 = 27
            // Diversity Bonuses:
            // Col 0: no (not full)
            // Col 1: yes (1, 1, 6 is not 3 distinct) -> wait, (1, 1, 6) has only 2 distinct.
            // Col 2: no
            // Row 1: (3, 1, 6) is full and has 3 distinct -> +15 bonus
            // Total: (17+10+29) + (21+10+27) + 15 = 56 + 58 + 15 = 129
            int[][] board = new int[3][] { 
                new int[3] { 5, 3, 3 }, 
                new int[3] { 1, 1, 6 }, 
                new int[3] { 5, 6, 6 } 
            };
            
            Assert.Equal(129, Rules.CalculateScore(board));
        }

        [Fact]
        public void HandleDestruction_RemovesMatchingOpponentDiceInSameRowAndCol()
        {
            // opponentBoard is [col][row]
            int[][] opponentBoard = new int[3][] { 
                new int[3] { 5, 5, 0 }, // Col 0: Row0=5, Row1=5
                new int[3] { 5, 0, 0 }, // Col 1: Row0=5
                new int[3] { 1, 0, 0 }  // Col 2: Row0=1
            };
            
            // Player places a 5 in Col 0, Row 0
            Rules.HandleDestruction(0, 0, 5, opponentBoard);
            
            Assert.Equal(0, opponentBoard[0][0]); // Same cell
            Assert.Equal(0, opponentBoard[1][0]); // Same row (Row 0, Col 1)
            Assert.Equal(0, opponentBoard[0][1]); // Same col (Col 0, Row 1)
            Assert.Equal(1, opponentBoard[2][0]); // Same row, different value (unchanged)
        }
    }
}
