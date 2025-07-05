namespace TicTacToe.Tests;

public class AITests
{
    [Fact]
    public void PicksWinningMove()
    {
        int[,] board =
        {
            {2,2,0},
            {1,1,0},
            {0,0,0}
        };
        var move = TicTacToeAI.BestMove(board);
        Assert.Equal((0,2), move);
    }

    [Fact]
    public void BlocksOpponentWin()
    {
        int[,] board =
        {
            {1,1,0},
            {2,0,0},
            {0,0,2}
        };
        var move = TicTacToeAI.BestMove(board);
        Assert.Equal((0,2), move);
    }

    [Fact]
    public void NoMoveLeftReturnsMinusOne()
    {
        int[,] board =
        {
            {2,1,2},
            {2,1,1},
            {1,2,2}
        };
        var move = TicTacToeAI.BestMove(board);
        Assert.Equal((-1,-1), move);
        Assert.False(TicTacToeAI.IsMovesLeft(board));
        Assert.Equal(0, TicTacToeAI.CheckWinner(board));
    }
}
