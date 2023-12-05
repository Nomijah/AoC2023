using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.Solutions
{
    internal class Day04
    {
        public static string testData = "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53\r\nCard 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19\r\nCard 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1\r\nCard 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83\r\nCard 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36\r\nCard 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";
        //public static string[] input = testData.Split("\n");
        public static string[] input = TextFormatter.ToLines("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day4.txt");

        public static int SolutionA()
        {
            int sum = 0;
            foreach (var item in input)
            {
                string[] strings = item.Split(new char[] { ':', '|' }, StringSplitOptions.TrimEntries);
                //Console.WriteLine(strings[0]);
                //Console.WriteLine(strings[1]);
                //Console.WriteLine(strings[2]);
                List<int> winningNumbers = strings[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => Convert.ToInt32(x)).ToList();
                List<int> myNumbers = strings[2].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => Convert.ToInt32(x)).ToList();
                int temp = 1;

                foreach (int num in winningNumbers)
                {
                    foreach (int num2 in myNumbers)
                    {
                        if (num == num2)
                        {
                            temp *= 2;
                        }
                    }
                }
                sum += temp / 2;
            }
            return sum;
        }

        public static int SolutionB()
        {
            List<int> scratchCards = new();
            input.ToList().ForEach(x => { scratchCards.Add(1); });
            int currentListIndex = 0;

            foreach (var item in input)
            {
                string[] strings = item.Split(new char[] { ':', '|' }, StringSplitOptions.TrimEntries);
                List<int> winningNumbers = strings[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => Convert.ToInt32(x)).ToList();
                List<int> myNumbers = strings[2].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => Convert.ToInt32(x)).ToList();
                int matchingNums = 0;
                foreach (int num in winningNumbers)
                {
                    foreach (int num2 in myNumbers)
                    {
                        if (num == num2)
                        {
                            matchingNums++;
                        }
                    }
                }

                for (int i = 1; i <= matchingNums; i++)
                {
                    scratchCards[currentListIndex + i] += scratchCards[currentListIndex];
                }

                currentListIndex++;
            }
            
            return scratchCards.Sum();
        }
    }
}
