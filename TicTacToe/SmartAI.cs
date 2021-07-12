using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace TicTacToe
{
    class SmartAI
    {

        public int x = 0;
        public int y = 0;
        private int[,] board;

        public SmartAI(int[,] gamestate, int pcturn)
        {
            board = gamestate;
            bestMove(pcturn);
        }



        public void bestMove(int pcturn)
        {
            int utilityMax = Int32.MinValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                    {
                        board[i, j] = pcturn;
                        int utility = minimax(pcturn, (pcturn % 2) + 1, false, countEmptySpaces());
                        board[i, j] = 0;
                        if (utility > utilityMax)
                        {
                            utilityMax = utility;
                            x = i;
                            y = j;
                        }
                    }
                }
            }
        }







        public int minimax(int pcTurn, int currentPlayer, Boolean isMax, int remaining)
        {
            
            if (checkWinner())
            {
                if(pcTurn == currentPlayer)
                    return -(remaining + 1);
                else
                    return (remaining + 1);
            }
            else if (checkTie())
            {
                return 0;
            }

            if (isMax)
            {
                int utilityMax = Int32.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == 0)
                        {
                            board[i, j] = currentPlayer;
                            int utility = minimax(pcTurn, (currentPlayer % 2) + 1, false, remaining - 1);
                            board[i, j] = 0;
                            utilityMax = Math.Max(utility, utilityMax);
                        }
                    }
                }
                return utilityMax;
            }
          

            else
            {
                int utilityMin = Int32.MaxValue;
                for (int i = 0; i < 3; i++)
                {   
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == 0)
                        {
                            board[i, j] = currentPlayer;
                            int utility = minimax(pcTurn, (currentPlayer % 2) + 1, true, remaining - 1);
                            board[i, j] = 0;
                            utilityMin = Math.Min(utility, utilityMin);
                        }
                    }
                }
                return utilityMin;
            }
           
        }


        public int countEmptySpaces()
        {
            int[,] array = board;
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (array[i, j] == 0)
                        count++;
                }
            }
            return count;
        }


        public bool checkWinner()
        {
            int[,] array = board;
            if (array[0, 0] == array[0, 1] && array[0, 0] == array[0, 2] && array[0, 0] != 0)
                return true;
            if (array[1, 0] == array[1, 1] && array[1, 0] == array[1, 2] && array[1, 0] != 0)
                return true;
            if (array[2, 0] == array[2, 1] && array[2, 0] == array[2, 2] && array[2, 0] != 0)
                return true;

            if (array[0, 0] == array[1, 0] && array[0, 0] == array[2, 0] && array[0, 0] != 0)
                return true;
            if (array[0, 1] == array[1, 1] && array[0, 1] == array[2, 1] && array[0, 1] != 0)
                return true;
            if (array[0, 2] == array[1, 2] && array[0, 2] == array[2, 2] && array[0, 2] != 0)
                return true;

            if (array[0, 0] == array[1, 1] && array[0, 0] == array[2, 2] && array[0, 0] != 0)
                return true;
            if (array[0, 2] == array[1, 1] && array[1, 1] == array[2, 0] && array[1, 1] != 0)
                return true;

            return false;
        }

        public Boolean checkTie()
        {
            int[,] array = board;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (array[i, j] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}

/*
minimax(isMax){
    if(checkwinner){
        return -1 || 1;
    }
    if(draw())
        return 0;

    if(isMaximizing()){
        int utilityMax = minValue;
        for(i=0-->2)
            for(j=0-->2)
                board[i,j] = currentTurn;
                utility = minimax(false);
                board[i,j] = 0;
                utilityMax = Math.max(utilityMax,utility);

    else
         int utilityMax = maxValue;
        for(i=0-->2)
            for(j=0-->2)
                board[i,j] = currentTurn;
                utility = minimax(true);
                board[i,j] = 0;
                utilityMax = Math.min(utilityMax,utility);

    return utilitymax
    }
    
}
*/
