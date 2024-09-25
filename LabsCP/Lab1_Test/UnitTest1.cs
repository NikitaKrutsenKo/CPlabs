using Xunit.Sdk;
using Lab1;
using System;

namespace Lab1_Test
{
    public class UnitTest1
    {
        [Fact]
        public void ParseUserInput_ValidInput()
        {
            int expected = 100;
            string input = "100";

            Console.WriteLine("ParseUserInput method : valid input test : parse " + input.ToString());
            Console.WriteLine("Expected output : " + expected.ToString());
            int result = Program.ParseUserInput(input);
            Assert.Equal(100, result);

            if (result == expected ) 
            {
                Console.WriteLine("Test passed");
                Console.WriteLine();
            }
        }

        [Fact]
        public void ParseUserInput_InputLowerRange()
        {
            string expected = "Input number must be in the range from 3 to 2*10^9";
            string input = "2";

            Console.WriteLine("ParseUserInput method : invalid input test : parse " + input.ToString());
            Console.WriteLine("Expected output : " + expected);

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Program.ParseUserInput(input));
            Assert.Equal(expected, exception.ParamName);

            if (exception.ParamName == expected)
            {
                Console.WriteLine("Test passed");
                Console.WriteLine();
            }
        }

        [Fact]
        public void ParseUserInput_InputAboveMaxLimit()
        {
            string expected = "Input number must be in the range from 3 to 2*10^9";
            string input = "2000000001";

            Console.WriteLine("ParseUserInput method : invalid input test : parse " + input.ToString());
            Console.WriteLine("Expected output : " + expected);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Program.ParseUserInput(input));
            Assert.Equal("Input number must be in the range from 3 to 2*10^9", exception.ParamName);

            if (exception.ParamName == expected)
            {
                Console.WriteLine("Test passed");
                Console.WriteLine();
            }
        }

        [Fact]
        public void FindLargestFraction_ValidInput()
        {
            int inputNumber = 10;
            int numeratorExpected = 3;
            int denominatorExpected = 7;

            Console.WriteLine("FindLargestFraction method : input " + inputNumber.ToString());
            Console.WriteLine("Expected fraction : " + numeratorExpected.ToString() + "/" + denominatorExpected.ToString());
            var (numerator, denominator) = Program.FindLargestFraction(inputNumber);
            Assert.Equal(numeratorExpected, numerator);
            Assert.Equal(denominatorExpected, denominator);

            if (numeratorExpected == numerator && denominatorExpected == denominator)
            {
                Console.WriteLine("Test passed");
                Console.WriteLine();
            }
        }

        [Fact]
        public void GreatestCommonDivisior_ValidInput_ShouldReturnCorrectGCD()
        {
            int numerator = 56;
            int denominator = 98;
            int gcdExpected = 14;

            Console.WriteLine("GreatestCommonDivisior method : input " + numerator.ToString() + "/" + denominator.ToString());
            Console.WriteLine("Expected gcd : " + gcdExpected.ToString());
            int gcd = Program.GreatestCommonDivisior(numerator, denominator);

            Assert.Equal(gcdExpected, gcd);

            if (gcd == gcdExpected)
            {
                Console.WriteLine("Test passed");
                Console.WriteLine();
            }
        }
    }
}