using Lab2;

namespace Lab2_Test
{
    public class UnitTest1
    {
        [Fact]
        public void Calculate_MinValues_ReturnsExpectedResult()
        {
            int n = 2;
            double baseValue = 2;
            double expected = 2;

            double result = Program.Calculate(n, baseValue);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Calculate_MaxValues_ReturnsExpectedResult()
        {
            int n = 12;
            double baseValue = 6;
            double expected = 1413765625; 

            double result = Program.Calculate(n, baseValue);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Calculate_SumExceeds18_ThrowsException()
        {
            int n = 9;
            double baseValue = 10;

            Assert.Throws<Exception>(() => Program.Calculate(n, baseValue));
        }

        [Fact]
        public void Calculate_SumLessThan4_ThrowsException()
        {
            int n = 1;
            double baseValue = 2;

            Assert.Throws<Exception>(() => Program.Calculate(n, baseValue));
        }

        [Fact]
        public void Calculate_OutOfRange_ThrowsException()
        {
            int n = 3;
            double baseValue = 11; 

            Assert.Throws<Exception>(() => Program.Calculate(n, baseValue));
        }
    }
}