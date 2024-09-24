using Xunit.Sdk;
using Lab1;

namespace Lab1_Test
{
    public class UnitTest1
    {
        [Fact]
        public void ParseUserInput_ValidInput()
        {
            string input = "100";
            int result = Program.ParseUserInput(input);
            Assert.Equal(100, result);
        }

        [Fact]
        public void ParseUserInput_InputLowerRange()
        {
            string input = "2";
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Program.ParseUserInput(input));
            Assert.Equal("Input number must be in the range from 3 to 2*10^9", exception.ParamName);
        }

        [Fact]
        public void ParseUserInput_InputAboveMaxLimit()
        {
            string input = "2000000001";
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Program.ParseUserInput(input));
            Assert.Equal("Input number must be in the range from 3 to 2*10^9", exception.ParamName);
        }

        [Fact]
        public void FindLargestFraction_ValidInput()
        {
            int inputNumber = 10;
            var (numerator, denominator) = Program.FindLargestFraction(inputNumber);
            Assert.Equal(3, numerator);
            Assert.Equal(7, denominator);
        }

        [Fact]
        public void GreatestCommonDivisior_ValidInput_ShouldReturnCorrectGCD()
        {
            int numerator = 56;
            int denominator = 98;
            int gcd = Program.GreatestCommonDivisior(numerator, denominator);

            Assert.Equal(14, gcd);
        }
    }
}