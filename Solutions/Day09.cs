using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.Solutions
{
    internal class Day09
    {
        public static string testData = "0 3 6 9 12 15\r\n1 3 6 10 15 21\r\n10 13 16 21 30 45";
        //public static string[] input = testData.Split("\r\n", StringSplitOptions.TrimEntries);
        public static string[] input = TextFormatter.ToLines("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day9.txt");


        public static void SolutionA()
        {
            
            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                List<int> endNums = new List<int>();
                bool done = false;
                int[] temp = input[i].Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
                endNums.Add(temp[temp.Length - 1]);
                while (!done)
                {
                    int[] diff = Diff(temp);
                    bool allZeros = true;
                    for (int j = 0; j < diff.Length; j++)
                    {
                        if (diff[j] != 0)
                        {
                            allZeros = false;
                            endNums.Add(diff[diff.Length - 1]);
                            break;
                        }
                        if (j == diff.Length - 1 && diff[j] == 0)
                        {
                            done = true;
                        }
                    }
                    temp = diff;
                }
                sum += endNums.Sum();
            }

            Console.WriteLine(sum);
        }
        public static void SolutionB()
        {

            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                List<int> startNums = new List<int>();
                bool done = false;
                int[] temp = input[i].Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
                startNums.Add(temp[0]);
                while (!done)
                {
                    int[] diff = Diff(temp);
                    bool allZeros = true;
                    for (int j = 0; j < diff.Length; j++)
                    {
                        if (diff[j] != 0)
                        {
                            allZeros = false;
                            startNums.Add(diff[0]);
                            break;
                        }
                        if (j == diff.Length - 1 && diff[j] == 0)
                        {
                            done = true;
                        }
                    }
                    temp = diff;
                }
                int tempNum = startNums[startNums.Count - 1];
                for (int j = startNums.Count -2; j >= 0; j--)
                {
                    tempNum = startNums[j] - tempNum;
                }
                sum += tempNum;
            }

            Console.WriteLine(sum);
        }

        public static int[] Diff(int[] a)
        {
            int[] result = new int[a.Length - 1];
            for (int j = 0; j < result.Length; j++)
            {
                result[j] = a[j+1] - a[j];
            }
            return result;
        }

    }
}
