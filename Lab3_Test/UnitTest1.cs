using Lab3ClassLib;
using Lab3;

namespace Lab3_Test
{
    public class UnitTest1
    {
        [Fact]
        public void PossitionIsValid_False()
        {
            Board board = new Board(8, 8);
            Horse horse = new Horse(0, 0, board);

            bool isValid = horse.PossitionIsValid(0, 0, -1, 0);

            Assert.False(isValid);
        }


        [Fact]
        public void PossitionIsValid_True()
        {
            Board board = new Board(8, 8);

            int currentX = 3;
            int currentY = 3;
            int valueX = -2;
            int valueY = 1;
            Horse horse = new Horse(currentX, currentY, board);

            bool isValid = horse.PossitionIsValid(currentX, currentY, valueX, valueY);

            Assert.True(isValid);
        }

        [Fact]
        public void ParseUserInput_ValidInput()
        {
            string[] input = { "a1", "h8" };
            int boardSizeX = 8;
            int boardSizeY = 8;

            List<Tuple<int, int>> result = Program.ParseUserInput(input, boardSizeX, boardSizeY);

            Assert.Equal(new Tuple<int, int>(7, 0), result[0]);
            Assert.Equal(new Tuple<int, int>(0, 7), result[1]);
        }

        [Fact]
        public void CalculateBoardCoeff_Valid()
        {
            Board board = new Board(8, 8);
            Horse horse = new Horse(7, 0, board);

            horse.CalculateBoardCoeff();
            int[,] expectedResult = 
            {
                { 5, 4, 5, 4, 5, 4, 5, 6},
                { 4, 3, 4, 3, 4, 5, 4, 5 },
                { 3, 4, 3, 4, 3, 4, 5, 4 },
                { 2, 3, 2, 3, 4, 3, 4, 5 },
                { 3, 2, 3, 2, 3, 4, 3, 4 },
                { 2, 1, 4, 3, 2, 3, 4, 5 },
                { 3, 4, 1, 2, 3, 4, 3, 4 },
                { 0, 3, 2, 3, 2, 3, 4, 5}
            };

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Assert.Equal(expectedResult[i, j], board.board[i, j].minNumberOfSteps);
                }
            }
        }

        [Fact]
        public void ParseUserInput_InvalidCoordinates()
        {
            string[] invalidInput = { "h9", "a0" };
            int boardSizeX = 8;
            int boardSizeY = 8;

            var exception = Assert.Throws<Exception>(() => Program.ParseUserInput(invalidInput, boardSizeX, boardSizeY));
            Assert.Equal("1 or 2 fields are outside the board", exception.Message);
        }
    }
}