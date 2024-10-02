using System;
using System.Collections.Generic;
using System.IO;

namespace Lab2
{
    public class Program
    {

        public static void Main()
        {
            try
            {
                string inputFile = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "INPUT.txt");
                string outputFile = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "OUTPUT.txt");

                string[] input = File.ReadAllText(inputFile).Split(' ');
                int n = int.Parse(input[0]);
                double baseValue = double.Parse(input[1]);

                double result = Calculate(n, baseValue);
                Console.WriteLine("Input: " + n.ToString() + " " + baseValue.ToString());
                Console.WriteLine("Result: " + result);

                File.WriteAllText(outputFile, result.ToString());
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("It seems like your input is less than 2 numbers, it should only be 2 numbers");
            }
            catch (FormatException)
            {
                Console.WriteLine("Input must be in int format, two numbers");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Here is exception message: " + ex.Message);
            }
        }

        public static double Calculate(int n, double baseValue)
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