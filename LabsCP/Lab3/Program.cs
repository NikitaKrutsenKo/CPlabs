namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            Horse horse = new Horse(7, 0, board);
            horse.CalculateBoardCoeff();
            horse.PrintBoard();
        }
    }
}