using System;

namespace TicTacToe;

public static class TicTacToeAI
{
    public const int HUMAN = 1; // O
    public const int AI = 2;    // X

    public static bool IsMovesLeft(int[,] board)
    {
        for (int r = 0; r < 3; r++)
            for (int c = 0; c < 3; c++)
                if (board[r, c] == 0)
                    return true;
        return false;
    }

    public static int CheckWinner(int[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i,0] != 0 && board[i,0] == board[i,1] && board[i,1] == board[i,2])
                return board[i,0];
            if (board[0,i] != 0 && board[0,i] == board[1,i] && board[1,i] == board[2,i])
                return board[0,i];
        }
        if (board[0,0] != 0 && board[0,0] == board[1,1] && board[1,1] == board[2,2])
            return board[0,0];
        if (board[0,2] != 0 && board[0,2] == board[1,1] && board[1,1] == board[2,0])
            return board[0,2];
        return 0;
    }

    static int Minimax(int[,] board, bool isMax, int depth)
    {
        int winner = CheckWinner(board);
        if (winner == AI)
            return 10 - depth; // prefer faster win
        if (winner == HUMAN)
            return depth - 10; // prefer slower loss
        if (!IsMovesLeft(board))
            return 0;

        if (isMax)
        {
            int best = int.MinValue;
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (board[r, c] == 0)
                    {
                        board[r, c] = AI;
                        best = Math.Max(best, Minimax(board, false, depth + 1));
                        board[r, c] = 0;
                    }
                }
            }
            return best;
        }
        else
        {
            int best = int.MaxValue;
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (board[r, c] == 0)
                    {
                        board[r, c] = HUMAN;
                        best = Math.Min(best, Minimax(board, true, depth + 1));
                        board[r, c] = 0;
                    }
                }
            }
            return best;
        }
    }

    public static (int row, int col) BestMove(int[,] board)
    {
        int bestVal = int.MinValue;
        int bestRow = -1, bestCol = -1;
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (board[r, c] == 0)
                {
                    board[r, c] = AI;
                    int moveVal = Minimax(board, false, 0);
                    board[r, c] = 0;
                    if (moveVal > bestVal)
                    {
                        bestVal = moveVal;
                        bestRow = r;
                        bestCol = c;
                    }
                }
            }
        }
        return (bestRow, bestCol);
    }
}
