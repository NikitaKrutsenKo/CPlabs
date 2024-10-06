using System.Collections.Generic;

namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string inputFile = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "INPUT.txt");
                string outputFile = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "OUTPUT.txt");
                int boardSizeX = 8;
                int boardSizeY = 8;
                Board boardRed = new Board(boardSizeX, boardSizeY);
                Board boardGreen = new Board(boardSizeX, boardSizeY);

                string[] userInput = GetUserInput(inputFile);
                List<Tuple<int, int>> inputParsed = ParseUserInput(userInput, boardSizeX, boardSizeY);

                Horse red = new Horse(inputParsed[0].Item1, inputParsed[0].Item2, boardRed);
                Horse green = new Horse(inputParsed[1].Item1, inputParsed[1].Item2, boardGreen);

                red.CalculateBoardCoeff();
                green.CalculateBoardCoeff();
                red.PrintBoard();
                green.PrintBoard();

                int result = CalculateStepsToMeet(red.boardCoeff, green.boardCoeff, boardSizeX, boardSizeY);
                Console.WriteLine("Result: " + result);
                File.WriteAllText(outputFile, result.ToString());
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Exception occured, here is message: " + ex.Message);
            }
        }

        public static string[] GetUserInput(string inputFile)
        {
            string[] input = File.ReadAllText(inputFile).Trim().Split(" ");
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
            List <Tuple<int, int>> result = new List <Tuple<int, int>>();
            foreach (string line in input) 
            {
                int letterIndx = Array.IndexOf(alphabet, line[0]);
                int number = boardSizeY - int.Parse(line[1].ToString());
                if (letterIndx < 0 || letterIndx >= boardSizeX || number < 0 || number > boardSizeY)
                {
                    throw new Exception("1 or 2 fields are outside the board");
                }
                result.Add(new Tuple<int, int>(number , letterIndx));
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
}