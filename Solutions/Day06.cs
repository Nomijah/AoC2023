using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC2023.Solutions
{
    internal class Day06
    {
        public static string testData = "Time:      7  15   30\r\nDistance:  9  40  200";
        public static string inputData = "Time:        35     93     73     66\r\nDistance:   212   2060   1201   1044";
        public static string[] input = inputData.Split(new char[] { '\n', ' ' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        public static void SolutionA()
        {
            int[] data = [.. (input[1..(input.Length / 2)].Select(x => Convert.ToInt32(x)).ToArray()), .. (input[(input.Length / 2 + 1)..].Select(x => Convert.ToInt32(x)).ToArray())];
            int sum = 1;
            for (int i = 0; i < data.Length / 2; i++)
            {
                int possibleWins = 0;
                for (int j = 1; j <= data[i]; j++)
                {
                    int temp = (data[i] - j) * j;
                    if (temp > data[i + data.Length / 2])
                    {
                        possibleWins++;
                    }
                }
                sum *= possibleWins;
            }
            Console.WriteLine(sum);
        }

        public static void SolutionB()
        {
            int possibleWins = 0;
            long time = 35937366;
            long distance = 212206012011044;
            for (long j = 1; j <= time; j++)
            {
                long temp = (time - j) * j;
                if (temp > distance)
                {
                    possibleWins++;
                }
            }
            Console.WriteLine(possibleWins);
        }
    }
}
