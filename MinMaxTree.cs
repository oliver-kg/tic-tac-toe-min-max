using System;
using static TicTacToe.TicTacToeAI;

namespace TicTacToe
{
    class Program
    {
        static int[,] board = new int[3, 3];

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
                int winner = CheckWinner(board);
                if (winner != 0 || !IsMovesLeft(board))
                    break;

                if (aiTurn)
                {
                    var move = BestMove(board);
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
            int finalWinner = CheckWinner(board);
            if (finalWinner == AI)
                Console.WriteLine("X wins!");
            else if (finalWinner == HUMAN)
                Console.WriteLine("O wins!");
            else
                Console.WriteLine("It's a tie.");
        }
    }
}
