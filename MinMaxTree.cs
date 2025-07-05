using System;

namespace TicTacToe
{
    class Program
    {
        const int HUMAN = 1; // O
        const int AI = 2;    // X

        static int[,] board = new int[3, 3];

        static bool IsMovesLeft()
        {
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    if (board[r, c] == 0)
                        return true;
            return false;
        }

        static int CheckWinner()
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

        static int Minimax(bool isMax, int depth)
        {
            int winner = CheckWinner();
            if (winner == AI)
                return 10 - depth; // prefer faster win
            if (winner == HUMAN)
                return depth - 10; // prefer slower loss
            if (!IsMovesLeft())
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
                            best = Math.Max(best, Minimax(false, depth + 1));
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
                            best = Math.Min(best, Minimax(true, depth + 1));
                            board[r, c] = 0;
                        }
                    }
                }
                return best;
            }
        }

        static (int row, int col) BestMove()
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
                        int moveVal = Minimax(false, 0);
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

        static void PrintBoard()
        {
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    char ch = board[r, c] switch
                    {
                        HUMAN => 'O',
                        AI => 'X',
                        _ => '.'
                    };
                    Console.Write(ch);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Main()
        {
            Console.WriteLine("Who starts? (x/o)");
            string starter = Console.ReadLine()?.Trim().ToLower();
            bool aiTurn = starter == "x";
            while (true)
            {
                PrintBoard();
                int winner = CheckWinner();
                if (winner != 0 || !IsMovesLeft())
                    break;

                if (aiTurn)
                {
                    var move = BestMove();
                    board[move.row, move.col] = AI;
                    aiTurn = false;
                }
                else
                {
                    Console.WriteLine("Your move (1-9):");
                    if (!int.TryParse(Console.ReadLine(), out int moveIdx) || moveIdx < 1 || moveIdx > 9)
                        continue;
                    int row = (moveIdx - 1) / 3;
                    int col = (moveIdx - 1) % 3;
                    if (board[row, col] != 0)
                        continue;
                    board[row, col] = HUMAN;
                    aiTurn = true;
                }
            }

            PrintBoard();
            int finalWinner = CheckWinner();
            if (finalWinner == AI)
                Console.WriteLine("X wins!");
            else if (finalWinner == HUMAN)
                Console.WriteLine("O wins!");
            else
                Console.WriteLine("It's a tie.");
        }
    }
}
