using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.Solutions
{
    internal class Day11
    {
        public static string testData = "...#......\r\n.......#..\r\n#.........\r\n..........\r\n......#...\r\n.#........\r\n.........#\r\n..........\r\n.......#..\r\n#...#.....";
        public static string[] input = testData.Split("\r\n");
        //public static string[] input = TextFormatter.ToLines("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day11.txt");


        public static void SolutionA()
        {
            char[,] data = ConvertTo2dArray(input);
            int[] columnsToAdd = CheckColumns(data);
            for (int i = columnsToAdd.Length - 1; i >= 0; i--)
            {
                data = InjectChar(data, columnsToAdd[i]);
            }
            int[] rowsToAdd = CheckRows(data);
            for (int i = rowsToAdd.Length - 1; i >= 0; i--)
            {
                data = InjectRow(data, rowsToAdd[i]);
            }

            Coordinate[] galaxies = GetGalaxyPositions(data);

            int sum = 0;
            for (int i = 0; i < galaxies.Length - 1; i++)
            {
                for (int j = i; j < galaxies.Length; j++)
                {
                    if (galaxies[i].y >= galaxies[j].y)
                        sum += galaxies[i].y - galaxies[j].y;
                    else
                        sum += galaxies[j].y - galaxies[i].y;
                    if (galaxies[i].x >= galaxies[j].x)
                        sum += galaxies[i].x - galaxies[j].x;
                    else
                        sum += galaxies[j].x - galaxies[i].x;
                }
            }
            Console.WriteLine(sum);
        }

        public static void SolutionB()
        {
            char[,] data = ConvertTo2dArray(input);
            int[] columnsToAdd = CheckColumns(data);
            int[] rowsToAdd = CheckRows(data);

            Coordinate[] galaxies = GetGalaxyPositions(data);

            long sum = 0;
            long extra = 9;
            for (int i = 0; i < galaxies.Length - 1; i++)
            {
                for (int j = i; j < galaxies.Length; j++)
                {
                    if (galaxies[i].y >= galaxies[j].y)
                    {
                        sum += galaxies[i].y - galaxies[j].y;
                        for (int k = 0; k < columnsToAdd.Length; k++)
                        {
                            if (columnsToAdd[k] < galaxies[i].y && columnsToAdd[k] > galaxies[j].y)
                                sum += extra;
                        }
                    }
                    else
                    {
                        sum += galaxies[j].y - galaxies[i].y;
                        for (int k = 0; k < columnsToAdd.Length; k++)
                        {
                            if (columnsToAdd[k] > galaxies[i].y && columnsToAdd[k] < galaxies[j].y)
                                sum += extra;
                        }
                    }
                    if (galaxies[i].x >= galaxies[j].x)
                    {
                        sum += galaxies[i].x - galaxies[j].x;
                        for (int k = 0; k < columnsToAdd.Length; k++)
                        {
                            if (columnsToAdd[k] < galaxies[i].x && columnsToAdd[k] > galaxies[j].x)
                                sum += extra;
                        }
                    }
                    else
                    {
                        sum += galaxies[j].x - galaxies[i].x;
                        sum += galaxies[i].x - galaxies[j].x;
                        for (int k = 0; k < columnsToAdd.Length; k++)
                        {
                            if (columnsToAdd[k] > galaxies[i].x && columnsToAdd[k] < galaxies[j].x)
                                sum += extra;
                        }
                    }
                }
            }
            Console.WriteLine(sum);
        }

        public static char[,] ConvertTo2dArray(string[] input)
        {
            char[,] temp = new char[input.Length, input[0].Length];
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    temp[i, j] = input[i][j];
                }
            }
            return temp;
        }

        public static void Print(char[,] input)
        {
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    Console.Write(input[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static char[,] InjectChar(char[,] input, int pos)
        {
            char[,] temp = new char[input.GetLength(0), input.GetLength(1) + 1];
            for (int i = 0; i < temp.GetLength(1); i++)
            {
                for (int j = 0; j < temp.GetLength(0); j++)
                {
                    if (i < pos)
                        temp[j, i] = input[j, i];
                    else if (i == pos)
                        temp[j, i] = '.';
                    else if (i > pos)
                        temp[j, i] = input[j, i - 1];
                }
            }
            return temp;
        }

        public static char[,] InjectRow(char[,] input, int pos)
        {
            char[,] temp = new char[input.GetLength(0) + 1, input.GetLength(1)];
            char[] rowToInject = new char[input.GetLength(1)];
            for (int i = 0; i < rowToInject.Length; i++)
            {
                rowToInject[i] = '.';
            }
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    if (i < pos)
                        temp[i, j] = input[i, j];
                    if (i == pos)
                        temp[pos, j] = rowToInject[j];
                    if (i > pos)
                        temp[i, j] = input[i - 1, j];
                }
            }
            return temp;
        }

        public static int[] CheckRows(char[,] input)
        {
            List<int> temp = new();
            for (int i = 0; i < input.GetLength(0); i++)
            {
                bool found = false;
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == '#')
                        found = true;
                }
                if (!found)
                    temp.Add(i);
            }
            return temp.ToArray();
        }

        public static int[] CheckColumns(char[,] input)
        {
            List<int> temp = new();
            for (int i = 0; i < input.GetLength(1); i++)
            {
                bool found = false;
                for (int j = 0; j < input.GetLength(0); j++)
                {
                    if (input[j, i] == '#')
                        found = true;
                }
                if (!found)
                    temp.Add(i);
            }
            return temp.ToArray();
        }

        public static Coordinate[] GetGalaxyPositions(char[,] input)
        {
            List<Coordinate> temp = new();
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == '#')
                        temp.Add(new Coordinate(i, j));
                }
            }
            return temp.ToArray();
        }

        public struct Coordinate
        {
            public int y, x;

            public Coordinate(int y, int x)
            {
                this.y = y;
                this.x = x;
            }
        }
    }
}
