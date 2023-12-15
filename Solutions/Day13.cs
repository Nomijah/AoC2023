using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.Solutions
{
    internal class Day13
    {
        public static string testData = "#.##..##.\r\n..#.##.#.\r\n##......#\r\n##......#\r\n..#.##.#.\r\n..##..##.\r\n#.#.##.#.\r\n\r\n#...##..#\r\n#....#..#\r\n..##..###\r\n#####.##.\r\n#####.##.\r\n..##..###\r\n#....#..#";
        //public static string[] input = testData.Split("\r\n\r\n");
        public static string[] input = File.ReadAllText("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day12.txt").Split("\r\n\r\n");

        public static void SolutionA()
        {
            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                string[] pattern = input[i].Split("\r\n");
                int result = FindLine(pattern);
                if (result != 0)
                {
                    sum += result * 100;
                }
                else
                {
                    string[] patternConverted = new string[pattern[0].Length];
                    for (int j = 0; j < pattern[0].Length; j++)
                    {
                        string temp = "";
                        for (int k = 0; k < pattern.Length; k++)
                        {
                            temp += pattern[k][j];
                        }
                        patternConverted[j] = temp;
                    }
                    int secondResult = FindLine(patternConverted);
                    sum += secondResult;
                }
            }
            Console.WriteLine(sum);
        }

        public static void SolutionB()
        {
            int sum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                string[] pattern = input[i].Split("\r\n");
                int result = FindLine(pattern);
                if (result != 0)
                {
                    result = FindLineB(pattern, result);
                    sum += result * 100;
                }
                else
                {
                    result = FindLineB(pattern, result - 1);
                    if (result != 0)
                    {
                        sum += result * 100;
                    }
                    else
                    {

                    string[] patternConverted = new string[pattern[0].Length];
                    for (int j = 0; j < pattern[0].Length; j++)
                    {
                        string temp = "";
                        for (int k = 0; k < pattern.Length; k++)
                        {
                            temp += pattern[k][j];
                        }
                        patternConverted[j] = temp;
                    }
                    int secondResult = FindLine(patternConverted);
                    secondResult = FindLineB(patternConverted, secondResult);
                    sum += secondResult;
                    }
                }
            }

            //for (int i = 0; i < input.Length; i++)
            //{
            //    string[] pattern = input[i].Split("\r\n");
            //    int result = FindLineB(pattern, listOfOldPoints[i]);
            //    if (result != 0)
            //    {
            //        sum += result * 100;
            //    }
            //    else
            //    {
            //        string[] patternConverted = new string[pattern[0].Length];
            //        for (int j = 0; j < pattern[0].Length; j++)
            //        {
            //            string temp = "";
            //            for (int k = 0; k < pattern.Length; k++)
            //            {
            //                temp += pattern[k][j];
            //            }
            //            patternConverted[j] = temp;
            //        }
            //        int secondResult = FindLineB(patternConverted, listOfOldPoints[i]);
            //        sum += secondResult;
            //    }
            //}
            Console.WriteLine(sum);
        }

        public static int FindLine(string[] pattern)
        {
            int match = 0;
            int currentHighest = 0;
            int currentIndex = -1;
            for (int j = 0; j < pattern.Length - 1; j++)
            {
                for (int i = 0; i < pattern.Length; i++)
                {
                    if (j >= i && j < pattern.Length - (i + 1))
                    {
                        if (pattern[j - i] == pattern[j + i + 1])
                        {
                            match++;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (match > currentHighest && (j + 1 - match == 0 || j + match + 1 == pattern.Length))
                {
                    currentIndex = j;
                    currentHighest = match;
                }
                match = 0;
            }
            return currentIndex + 1;
        }

        public static int FindLineB(string[] pattern, int oldPoint)
        {
            int match = 0;
            int currentHighest = 0;
            int currentIndex = -1;
            bool smudgeFound = false;
            for (int j = 0; j < pattern.Length - 1; j++)
            {
                for (int i = 0; i < pattern.Length; i++)
                {
                    if (j >= i && j < pattern.Length - (i + 1))
                    {
                        if (pattern[j - i] == pattern[j + i + 1])
                        {
                            match++;
                        }
                        else if (stringComparer(pattern[j - i], pattern[j + i + 1]) && !smudgeFound)
                        {
                            match++;
                            smudgeFound = true;
                            pattern[j-i] = pattern[j+i+1];
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (match > currentHighest && (j + 1 - match == 0 || j + match + 1 == pattern.Length) && j +1 != oldPoint)
                {
                    currentIndex = j;
                    currentHighest = match;
                }
                match = 0;
                smudgeFound = false;
            }
            return currentIndex + 1;
        }

        public static bool stringComparer(string one, string two)
        {
            int diffs = 0;
            for (int i = 0; i < one.Length; i++)
            {
                if (one[i] != two[i]) diffs++;
            }
            if (diffs == 1) return true;
            return false;
        }
    }
}
