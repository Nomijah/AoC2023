using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.Solutions
{
    internal class Day05
    {
        public static string testData = "seeds: 79 14 55 13\r\n\r\nseed-to-soil map:\r\n50 98 2\r\n52 50 48\r\n\r\nsoil-to-fertilizer map:\r\n0 15 37\r\n37 52 2\r\n39 0 15\r\n\r\nfertilizer-to-water map:\r\n49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4\r\n\r\nwater-to-light map:\r\n88 18 7\r\n18 25 70\r\n\r\nlight-to-temperature map:\r\n45 77 23\r\n81 45 19\r\n68 64 13\r\n\r\ntemperature-to-humidity map:\r\n0 69 1\r\n1 0 69\r\n\r\nhumidity-to-location map:\r\n60 56 37\r\n56 93 4";
        //public static string[] input = testData.Split('\n', StringSplitOptions.TrimEntries);
        public static string[] input = TextFormatter.ToLines("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day5.txt");

        //public static long SolutionA()
        //{
        //    int first = input.ToList().IndexOf("seed-to-soil map:");
        //    int second = input.ToList().IndexOf("soil-to-fertilizer map:");
        //    int third = input.ToList().IndexOf("fertilizer-to-water map:");
        //    int fourth = input.ToList().IndexOf("water-to-light map:");
        //    int fifth = input.ToList().IndexOf("light-to-temperature map:");
        //    int sixth = input.ToList().IndexOf("temperature-to-humidity map:");
        //    int seventh = input.ToList().IndexOf("humidity-to-location map:");

        //    List<string> seedSoil = input[(first + 1)..(second - 1)].ToList();
        //    List<string> soilFert = input[(second + 1)..(third - 1)].ToList();
        //    List<string> fertWater = input[(third + 1)..(fourth - 1)].ToList();
        //    List<string> waterLight = input[(fourth + 1)..(fifth - 1)].ToList();
        //    List<string> lightTemp = input[(fifth + 1)..(sixth - 1)].ToList();
        //    List<string> tempHum = input[(sixth + 1)..(seventh - 1)].ToList();
        //    List<string> humLoc = input[(seventh + 1)..(input.Length)].ToList();



        //    string[] seedData = input[0].Split(' ');
        //    List<long> seeds = seedData[1..].ToList().Select(x => Convert.ToInt64(x)).ToList();

        //    for (int i = 0; i < seeds.Count; i++)
        //    {
        //        seeds[i] = Converter(
        //            Converter(
        //                Converter(
        //                    Converter(
        //                        Converter(
        //                            Converter(
        //                                Converter(seeds[i], seedSoil), soilFert), fertWater), waterLight), lightTemp), tempHum), humLoc);

        //    }

        //    return seeds.Min();
        //}

        public static long SolutionB()
        {
            int first = input.ToList().IndexOf("seed-to-soil map:");
            int second = input.ToList().IndexOf("soil-to-fertilizer map:");
            int third = input.ToList().IndexOf("fertilizer-to-water map:");
            int fourth = input.ToList().IndexOf("water-to-light map:");
            int fifth = input.ToList().IndexOf("light-to-temperature map:");
            int sixth = input.ToList().IndexOf("temperature-to-humidity map:");
            int seventh = input.ToList().IndexOf("humidity-to-location map:");

            string[] seedSoil1 = input[(first + 1)..(second - 1)].ToArray();
            string[] soilFert1 = input[(second + 1)..(third - 1)].ToArray();
            string[] fertWater1 = input[(third + 1)..(fourth - 1)].ToArray();
            string[] waterLight1 = input[(fourth + 1)..(fifth - 1)].ToArray();
            string[] lightTemp1 = input[(fifth + 1)..(sixth - 1)].ToArray();
            string[] tempHum1 = input[(sixth + 1)..(seventh - 1)].ToArray();
            string[] humLoc1 = input[(seventh + 1)..(input.Length)].ToArray();

            long[,] seedSoil = RuleConverter(seedSoil1);
            long[,] soilFert = RuleConverter(soilFert1);
            long[,] fertWater = RuleConverter(fertWater1);
            long[,] waterLight = RuleConverter(waterLight1);
            long[,] lightTemp = RuleConverter(lightTemp1);
            long[,] tempHum = RuleConverter(tempHum1);
            long[,] humLoc = RuleConverter(humLoc1);

            string[] seedData = input[0].Split(' ');
            long[] seedData2 = seedData[1..].Select(x => Convert.ToInt64(x)).ToArray();

            //for (int i = 0; i < seedData2.Length; i++)
            //{
            //    Console.WriteLine(seedData2[i]);
            //}

            //WriteMatchingIndex(seedData2, seedSoil);
            //Console.WriteLine();
            //WriteMatchingIndex(seedData2, soilFert);
            //Console.WriteLine();
            //WriteMatchingIndex(seedData2, fertWater);
            //Console.WriteLine();
            //WriteMatchingIndex(seedData2, waterLight);
            //Console.WriteLine();
            //WriteMatchingIndex(seedData2, lightTemp);
            //Console.WriteLine();
            //WriteMatchingIndex(seedData2, tempHum);
            //Console.WriteLine();
            //WriteMatchingIndex(seedData2, humLoc);

            long[] p1 = [seedData2[0], seedData2[1]];
            List<long> p1b = new();
            for (long j = p1[0]; j <= p1[0] + p1[1]; j++)
            {
                long temp = Converter(j, seedSoil);
                if (!p1b.Contains(temp))
                {
                    p1b.Add(temp);
                }
            }
            Console.WriteLine(p1[1]);
            Console.WriteLine(p1b.Count);
            //long currentLowest = 0;
            //bool firstSet = false;

            //for (int i = 0; i < seedData2.Length; i += 2)
            //{
            //    Console.WriteLine("ny runda");
            //    for (long j = seedData2[i]; j < seedData2[i] + seedData2[i + 1]; j++)
            //    {
            //        long temp = Converter(
            //            Converter(
            //                Converter(
            //                    Converter(
            //                        Converter(
            //                            Converter(
            //                                Converter(j, seedSoil), soilFert), fertWater), waterLight), lightTemp), tempHum), humLoc);
            //        if (!firstSet)
            //        {
            //            currentLowest = temp;
            //            firstSet = true;
            //        }
            //        else if (temp < currentLowest)
            //        {
            //            currentLowest = temp;

            //        }
            //        //Console.WriteLine(j);
            //        //Console.WriteLine(currentLowest);
            //    }
            //}

            return 0;
        }

        public static long Converter(long item, long[,] rules)
        {
            for (long i = 0; i < rules.GetLength(0); i++)
            {
                if (item >= rules[i, 1] && item <= rules[i, 1] + rules[i, 2])
                {
                    return item += rules[i, 0] - rules[i, 1];
                }
            }
            return item;
        }

        public static void WriteMatchingIndex(long[] seedData2, long[,] rules)
        {
            for (int i = 0; i < seedData2.Length; i += 2)
            {
                var p1 = FindMatch(seedData2[i], rules);
                var p2 = FindMatch(seedData2[i] + seedData2[i + 1], rules);
                Console.WriteLine(p1);
                Console.WriteLine(p2);
            }
        }
        public static long FindMatch(long item, long[,] rules)
        {
            for (long i = 0; i < rules.GetLength(0); i++)
            {
                if (item >= rules[i, 1] && item <= rules[i, 1] + rules[i, 2])
                {
                    return i;
                }
            }
            return -1;
        }

        public static long[,] RuleConverter(string[] rules)
        {
            long[,] result = new long[rules.Length, 3];
            for (int i = 0; i < rules.GetLength(0); i++)
            {
                long[] data = rules[i].Split(' ').Select(x => Convert.ToInt64(x)).ToArray();
                result[i, 0] = data[0];
                result[i, 1] = data[1];
                result[i, 2] = data[2];
            }
            return result;
        }
    }
}
