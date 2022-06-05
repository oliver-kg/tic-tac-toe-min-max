using System;
using System.Collections.Generic;

namespace MinMaxTree
{

    public class Program
    {
        public static int Testings = 0;
        public static int WinningScore = 0;
        public static int MinMax(int[,] CurrentBoard, int depth, bool maximizing, int scores)
        {
            // checks if has won
            int result = checkIfWon(CurrentBoard);
            int Testingg;
            // if the winner is AI
            if(result == 2 && result != 0)
            {
                WinningScore = 1 + WinningScore;
                scores = 1 + scores;
                if(Testings > scores)
                {
                }else
                {
                    Testings = scores;
                }
                
                return WinningScore;
            }

            // if winner is Human
            if (result == 1 && result != 0)
            {
                WinningScore = WinningScore - 1;
                scores = scores - 1;
                if(Testings > scores)
                {
                }else
                {
                    Testings = scores;
                }
                return WinningScore;
            }
            
            // if its X / AI go
            if(maximizing == true)
            {
                // all this is to maximise X's chances
                
                int bestScore = -100;
                for (int r = 0; r < 3; r++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        //Is the spot available
                        if (CurrentBoard[r,c] == 0)
                        {
                            //set that square to ai go
                            CurrentBoard[r,c] = 2;
                            // recursive function to call min max and advance into the tree and see if the score has been beaten
                            int score = MinMax(CurrentBoard, depth+1, false, scores);
                            // reverse ai's move after
                            CurrentBoard[r,c] = 0;
                            if (score > bestScore)
                            {
                                bestScore = score;
                            }
                        }
                    }
                }
            Testingg = scores;
            return bestScore;
            
            // if O / Humans go
            }else
            {
                // all the same as above, but for O's go and assuming they are going to be playing its best.
                
                int bestScore = 100;
                for (int r = 0; r < 3; r++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        //Is the spot available
                        if (CurrentBoard[r,c] == 0)
                        {
                            CurrentBoard[r,c] = 1;
                            int score = MinMax(CurrentBoard, depth+1, true, scores);
                            CurrentBoard[r,c] = 0;

                            if (score < bestScore)
                            {
                                bestScore = score;
                            }
                        }
                    }
                }
            Testingg = scores;
            return bestScore;
            
            }
        
        }
    
        public static void bestMove(int[,] CurrentBoard, string WhoGo, int scores)
        {
            int bestScore = -100000;
            int move = 0;
            int i = 0;
            int score;
            int Testing = scores;
            
            for (int r = 0; r <= 2; r++)
            {
                for (int c = 0; c <= 2; c++)
                {
                    // if the spot is available
                    if(CurrentBoard[r,c] == 0)
                    {
                        //move there
                        CurrentBoard[r,c] = 2;
                        //call minmax alg to find scorw

                        score = MinMax(CurrentBoard, 0, false, scores);
                        
                        CurrentBoard[r,c] = 0;
                        //if the score has been beaten
                        if (WinningScore > bestScore)
                        {
                            //change the previous best score to the current best score
                            bestScore = WinningScore;
                            //this is the square that is the best to move
                            move = i+1;
                        }
                        WinningScore = 0;
                    }
                    i += 1;
                }
            }

            Console.WriteLine("Move: "+move);
            
            int IntToSpace = 0;
            int availableSpaces = 0;
            
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    IntToSpace++;
                    //moves the ai's go to the board
                    if (IntToSpace == move)
                    {
                        CurrentBoard[r,c] = 2;
                    }

                    if (CurrentBoard[r,c] == 0)
                    {
                        availableSpaces += 1;
                    }
                }
            }

            int winner = checkIfWon(CurrentBoard);
            
            if (availableSpaces != 0 && winner == 0)
            {
            }else
            {
                if (winner == 0)
                {
                    Console.WriteLine("Tie");
                }

                if (winner == 1)
                {
                    Console.WriteLine("O's Won");
                }
                
                if (winner == 2)
                {
                    Console.WriteLine("X's Won");
                }
                
                Console.WriteLine("Game Over");
                return;
            }
            
            
            Console.WriteLine("Your move: ");
            int playersMove = 0;
            playersMove =  Int32.Parse(Console.ReadLine());
            
            IntToSpace = 0;
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                        IntToSpace++;
                    //moves the players's go to the board
                        if (IntToSpace == playersMove)
                        {
                            CurrentBoard[r,c] = 1;
                        }
                }
            }
            bestMove(CurrentBoard, WhoGo, scores);
        }
        
        public static bool equals3(int a, int b, int c)
        {   
            return a == b && b == c && a != 0;
        }
        public static bool Equal(int a, int b, int c, int d)
        {   
            return a == b && b == c && c == d && a != 0;
        }
        public static bool Equal(int a, int b, int c, int d, int e)
        {   
            return a == b && b == c && c == d && d == e && a != 0;
        }
        public static bool Equal(int a, int b, int c, int d, int e, int f)
        {   
            return a == b && b == c && c == d && d == e && e == f && a != 0;
        }
        public static bool Equal(int a, int b, int c, int d, int e, int f, int g)
        {   
            return a == b && b == c && c == d && d == e && e == f && f == g && a != 0;
        }
        
        public static int checkIfWon(int[,] board)
        {
            int winner = 0;

        // Checks if anyone has won

            // Horizontal
            for (int i = 0; i < 3; i++)
            {
                if (equals3(board[i,0], board[i,1], board[i,2]))
                {
                    winner = board[i,0];
                }
            }

            // Vertical
            for (int i = 0; i < 3; i++)
            {
                if (equals3(board[0,i], board[1,i], board[2,i]))
                {
                    winner = board[0,i];
                }
            }

            // Diagonal
            if (equals3(board[0,0], board[1,1], board[2,2]))
            {
                winner = board[0,0];
            }
            
            if (equals3(board[2,0], board[1,1], board[0,2]))
            {
                winner = board[2,0];
            }
            int openSpots = 0;
            //Looks if any open spots are there
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if(board[r,c] == 0)
                    {
                        openSpots++;
                    }
                }
            }

            // checks if the game is at a tie
            if (winner == 0 && openSpots == 0)
            {
                // returns a tie
                //Console.WriteLine("Tie!");
                return 0;
            } else
            {
                // returns the winner
                //Console.WriteLine(winner +" Has won!!!");
                return winner;
            }
        }
        
        static void Main()    
        {
            int boardSize = 3;
            
            int[,] EmptyBoard = new int[boardSize,boardSize];
            
            Console.WriteLine("Who start: ");
            string WhoGo = Console.ReadLine();

            if (WhoGo == "x")
            {
                bestMove(EmptyBoard, "x", 0);
            }else
            {
                Console.WriteLine("Your move: ");

                int playersMove = Int32.Parse(Console.ReadLine());
                
                int InSpace = 0;
                for (int r = 0; r < 3; r++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                            InSpace++;
                        //moves the players's go to the board
                            if (InSpace == playersMove)
                            {
                                EmptyBoard[r,c] = 1;
                            }
                    }
                }
                bestMove(EmptyBoard, "o", 0);
            }
        }
    }
}