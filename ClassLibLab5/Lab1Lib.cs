using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForLab4
{
    public class Lab1Lib
    {
        public static List<double> RunLab1(string userInput)
        {
            try
            {
                int input = CheckUserInput(userInput);

                int numerator, denominator;
                (numerator, denominator) = FindLargestFraction(input);
                return new List<double> { numerator, denominator };
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:" + ex.Message);
            }
            return new List<double>();
        }

        private static int CheckUserInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("Input cannot be null or empty");
            }
            int inputNumber = int.Parse(input);
            if (inputNumber < 3 || inputNumber > 2000000000)
            {
                throw new ArgumentOutOfRangeException("Input number must be in the range from 3 to 2*10^9");
            }
            return inputNumber;
        }


        /// <summary>
        /// Кожним проходом збільшуємо чисельник на 1 і відповідно зменшуємо знаменник на 1.
        /// Робимо так поки не дійдемо до найбільшого нескоротного дробу
        /// </summary>
        /// <param name="inputNumber">Число, яке розкладаємо на чисельник і знаменник дробу</param>
        /// <returns>Кортеж з чисельника і знаменника</returns>
        private static (int, int) FindLargestFraction(int inputNumber)
        {
            int maxNumerator = 0;
            int maxDenominator = 0;
            int a, b;

            for (a = 1; a < inputNumber; a++)
            {
                b = inputNumber - a;
                if (a < b && GreatestCommonDivisior(a, b) == 1)
                {
                    maxNumerator = a;
                    maxDenominator = b;
                }
            }

            return (maxNumerator, maxDenominator);
        }

        /// <summary>
        /// Знаходимо найбільший спільний дільник
        /// </summary>
        /// <param name="a">чисельник</param>
        /// <param name="b">знаменник</param>
        /// <returns>Найбільший спільний дільник</returns>
        private static int GreatestCommonDivisior(int a, int b)
        {
            int temp;
            while (b != 0)
            {
                temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
