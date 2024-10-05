using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Horse
    {
        int startPosX;
        int startPosY;
        public Board boardCoeff;
        public Horse(int startPosX, int startPosY, Board boardCoeff)
        {
            this.startPosX = startPosX;
            this.startPosY = startPosY;
            this.boardCoeff = boardCoeff;
        }

        public void CalculateBoardCoeff()
        {
            boardCoeff.board[startPosX, startPosY].minNumberOfSteps = 0;
            CalculateBoardCoeffHelper(startPosX, startPosY);
        }

        private void CalculateBoardCoeffHelper(int currentX, int currentY)
        {
            int[,] horseMove = { {-2, 1}, {-2, -1}, {2, 1}, {2, -1}, {1, 2}, {1, -2}, {-1, 2}, {-1, -2} };
            List<Tuple<int, int>> listToProcess = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(currentX, currentY)
            };
            List<Tuple<int, int>> newList = new List<Tuple<int, int>>();
            while (listToProcess.Count > 0)
            {
                for (int j = 0; j < listToProcess.Count; ++j)
                {
                    for (int i = 0; i < horseMove.GetLength(0); ++i)
                    {
                        if (PossitionIsValid(listToProcess[j].Item1, listToProcess[j].Item2, horseMove[i, 0], horseMove[i, 1]))
                        {
                            int oldX = listToProcess[j].Item1;
                            int oldY = listToProcess[j].Item2;
                            int newX = listToProcess[j].Item1 + horseMove[i, 0];
                            int newY = listToProcess[j].Item2 + horseMove[i, 1];
                            if (boardCoeff.board[newX, newY].minNumberOfSteps > boardCoeff.board[oldX, oldY].minNumberOfSteps + 1)
                            {
                                boardCoeff.board[newX, newY].minNumberOfSteps = boardCoeff.board[oldX, oldY].minNumberOfSteps + 1;
                                if (!newList.Exists(item => item.Item1 == newX && item.Item2 == newY))
                                {
                                    newList.Add(new Tuple<int, int>(newX, newY));
                                }
                            }
                        }
                    }
                }
                listToProcess.Clear();
                listToProcess = newList.Select(item => new Tuple<int, int>(item.Item1, item.Item2)).ToList();
                newList.Clear();
            }
        }

        private bool PossitionIsValid(int currentX, int currentY, int valueX, int valueY) 
        {
            if(currentX + valueX < 0 ||
                currentY + valueY < 0 ||
                currentX + valueX >= boardCoeff.sizeX ||
                currentY + valueY >= boardCoeff.sizeY)
            {
                return false;
            }
            return true;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < boardCoeff.board.GetLength(0); i++) 
            {
                for(int j = 0; j < boardCoeff.board.GetLength(1); j++)
                {
                    Console.Write(boardCoeff.board[i, j].minNumberOfSteps + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
