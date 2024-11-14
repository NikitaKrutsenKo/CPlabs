using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForLab4
{
    public class Lab3Lib
    {
        public static List<double> RunLab3(string userInput)
        {
            try
            {
                int boardSizeX = 8;
                int boardSizeY = 8;
                Board boardRed = new Board(boardSizeX, boardSizeY);
                Board boardGreen = new Board(boardSizeX, boardSizeY);

                string[] input = GetUserInput(userInput);
                List<Tuple<int, int>> inputParsed = ParseUserInput(input, boardSizeX, boardSizeY);

                Horse red = new Horse(inputParsed[0].Item1, inputParsed[0].Item2, boardRed);
                Horse green = new Horse(inputParsed[1].Item1, inputParsed[1].Item2, boardGreen);

                red.CalculateBoardCoeff();
                green.CalculateBoardCoeff();
                red.PrintBoard();
                green.PrintBoard();

                int result = CalculateStepsToMeet(red.boardCoeff, green.boardCoeff, boardSizeX, boardSizeY);
                return new List<double> { result };
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured, here is message: " + ex.Message);
            }
        }

        public static string[] GetUserInput(string userInput)
        {
            string[] input = File.ReadAllText(userInput).Trim().Split(" ");
            int i = 0;
            foreach (string line in input)
            {
                ++i;
                if (line.Length != 2 || i > 2)
                {
                    throw new Exception("Invalid input, it must be a pair of coordianates. Example: a1 a3");
                }
            }
            return input;
        }

        public static List<Tuple<int, int>> ParseUserInput(string[] input, int boardSizeX, int boardSizeY)
        {
            char[] alphabet = Enumerable.Range('a', boardSizeX).Select(x => (char)x).ToArray();
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();
            foreach (string line in input)
            {
                int letterIndx = Array.IndexOf(alphabet, line[0]);
                int number = boardSizeY - int.Parse(line[1].ToString());
                if (letterIndx < 0 || letterIndx >= boardSizeX || number < 0 || number > boardSizeY)
                {
                    throw new Exception("1 or 2 fields are outside the board");
                }
                result.Add(new Tuple<int, int>(number, letterIndx));
            }
            return result;
        }

        public static int CalculateStepsToMeet(Board boardRed, Board boardGreen, int boardSizeX, int boardSizeY)
        {
            int result = -1;

            for (int i = 0; i < boardSizeX; ++i)
            {
                for (int j = 0; j < boardSizeY; ++j)
                {
                    if (boardRed.board[i, j].minNumberOfSteps % 2 == boardGreen.board[i, j].minNumberOfSteps % 2)
                    {
                        int possibleRes = boardRed.board[i, j].minNumberOfSteps > boardGreen.board[i, j].minNumberOfSteps ?
                                          boardRed.board[i, j].minNumberOfSteps : boardGreen.board[i, j].minNumberOfSteps;
                        if (result == -1 || result > possibleRes)
                        {
                            result = possibleRes;
                        }
                    }
                }
            }
            return result;
        }
    }

    public class Board
    {
        public int sizeX;
        public int sizeY;
        public Field[,] board;
        public Board(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            board = new Field[sizeX, sizeY];
            InitFields(sizeX, sizeY);
        }

        private void InitFields(int sizeX, int sizeY)
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    board[i, j] = new Field();
                }
            }
        }
    }

    public class Field
    {
        public int minNumberOfSteps;
        public Field()
        {
            minNumberOfSteps = int.MaxValue;
        }
    }

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

        private Board CalculateBoardCoeffHelper(int currentX, int currentY)
        {
            int[,] horseMove = { { -2, 1 }, { -2, -1 }, { 2, 1 }, { 2, -1 }, { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 } };
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
            return boardCoeff;
        }

        public bool PossitionIsValid(int currentX, int currentY, int valueX, int valueY)
        {
            if (currentX + valueX < 0 ||
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
                for (int j = 0; j < boardCoeff.board.GetLength(1); j++)
                {
                    Console.Write(boardCoeff.board[i, j].minNumberOfSteps + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
