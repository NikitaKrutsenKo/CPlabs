using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForLab4
{
    public class Lab2Lib
    {
        public static List<double> RunLab2(string userInput)
        {
            try
            {
                string[] input = userInput.Split(' ');
                int n = int.Parse(input[0]);
                double baseValue = double.Parse(input[1]);

                double result = Calculate(n, baseValue);
                return new List<double> { result };
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("It seems like your input is less than 2 numbers, it should only be 2 numbers");
            }
            catch (FormatException)
            {
                throw new Exception("Input must be in int format, two numbers");
            }
            catch (Exception ex)
            {
                throw new Exception("Here is exception message: " + ex.Message);
            }
        }

        private static double Calculate(int n, double baseValue)
        {
            if (n + baseValue < 4 || n + baseValue > 18 || baseValue < 2 || baseValue > 10)
            {
                throw new Exception("Your values should be like that: 2 <= K <= 10; 2 <= N; 4 <= N+K <= 18");
            }

            List<double> count = new List<double>(new double[n + 1]);

            count[1] = baseValue - 1;
            if (n >= 2)
            {
                count[2] = (baseValue - 1) * baseValue;
            }

            for (int i = 3; i <= n; i++)
            {
                count[i] = (baseValue - 1) * (count[i - 1] + count[i - 2]);
            }

            return count[n];
        }
    }
}
