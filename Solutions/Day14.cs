using System.Text.Json;

namespace AoC2023.Solutions
{
    internal class Day14
    {
        public static string testData = "O....#....\r\nO.OO#....#\r\n.....##...\r\nOO.#O....O\r\n.O.....O#.\r\nO.#..O.#.#\r\n..O..#O..O\r\n.......O..\r\n#....###..\r\n#OO..#....";
        //public static string[] input = testData.Split("\r\n");
        public static string[] input = TextFormatter.ToLines("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day14.txt");
        public static void SolutionA()
        {
            char[,] data = new char[input.Length, input[0].Length];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    data[i, j] = input[i][j];
                }
            }

            for (int i = 0; i < data.GetLength(1); i++)
            {
                for (int j = 1; j < data.GetLength(0); j++)
                {
                    if (data[j, i] == 'O')
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (data[k, i] == 'O' || data[k, i] == '#')
                            {
                                if (k + 1 != j)
                                {
                                    char temp = data[j, i];
                                    data[j, i] = data[k + 1, i];
                                    data[k + 1, i] = temp;
                                }
                                break;
                            }
                            else if (k == 0)
                            {
                                char temp = data[j, i];
                                data[j, i] = data[k, i];
                                data[k, i] = temp;
                            }
                        }
                    }
                }
            }
            int sum = 0;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (data[i, j] == 'O')
                    {
                        sum += data.GetLength(0) - i;
                    }
                }
            }
            Console.WriteLine(sum);
        }

        public static void SolutionB()
        {
            char[,] data = new char[input.Length, input[0].Length];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    data[i, j] = input[i][j];
                }
            }

            char[,] dataBackup = new char[input.Length, input[0].Length];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    dataBackup[i, j] = input[i][j];
                }
            }


            int hits = 0;
            int firstHit = 0;
            int secondHit = 0;
            int highest = 0;
            for (int i = 1; i <= 2000; i++)
            {
                data = TiltNorth(data);
                data = TiltWest(data);
                data = TiltSouth(data);
                data = TiltEast(data);
                if (i >500 &&  i < 1000)
                if (Sum(data) > highest)
                    highest = Sum(data);
                if (i > 1000)
                {
                    if (hits == 0 && highest == Sum(data))
                    {
                        hits++;
                        firstHit = i;
                    }
                    else if (hits == 1 && highest == Sum(data) && i > firstHit + 2)
                    {
                        hits++;
                        secondHit = i;
                    }
                }
            }

            int loopLength = secondHit - firstHit;

            int stepsAfterSecondHit = 1000000000 % loopLength;

            for (int i = 1; i <= (loopLength * 300) + stepsAfterSecondHit;  i++)
            {
                dataBackup = TiltNorth(dataBackup);
                dataBackup = TiltWest(dataBackup);
                dataBackup = TiltSouth(dataBackup);
                dataBackup = TiltEast(dataBackup);
            }
            Console.WriteLine(Sum(dataBackup));



        }

        public static int Sum(char[,] data)
        {
            int sum = 0;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (data[i, j] == 'O')
                    {
                        sum += data.GetLength(0) - i;
                    }
                }
            }
            return sum;
        }

        public static void Print(char[,] data)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Console.Write(data[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static char[,] TiltNorth(char[,] data)
        {
            for (int i = 0; i < data.GetLength(1); i++)
            {
                for (int j = 1; j < data.GetLength(0); j++)
                {
                    if (data[j, i] == 'O')
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (data[k, i] == 'O' || data[k, i] == '#')
                            {
                                if (k + 1 != j)
                                {
                                    char temp = data[j, i];
                                    data[j, i] = data[k + 1, i];
                                    data[k + 1, i] = temp;
                                }
                                break;
                            }
                            else if (k == 0)
                            {
                                char temp = data[j, i];
                                data[j, i] = data[k, i];
                                data[k, i] = temp;
                            }
                        }
                    }
                }
            }
            return data;
        }

        public static char[,] TiltWest(char[,] data)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 1; j < data.GetLength(1); j++)
                {
                    if (data[i, j] == 'O')
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (data[i, k] == 'O' || data[i, k] == '#')
                            {
                                if (k + 1 != j)
                                {
                                    char temp = data[i, j];
                                    data[i, j] = data[i, k + 1];
                                    data[i, k + 1] = temp;
                                }
                                break;
                            }
                            else if (k == 0)
                            {
                                char temp = data[i, j];
                                data[i, j] = data[i, k];
                                data[i, k] = temp;
                            }
                        }
                    }
                }
            }
            return data;
        }

        public static char[,] TiltSouth(char[,] data)
        {
            for (int i = 0; i < data.GetLength(1); i++)
            {
                for (int j = data.GetLength(0) - 2; j >= 0; j--)
                {
                    if (data[j, i] == 'O')
                    {
                        for (int k = j + 1; k < data.GetLength(0); k++)
                        {
                            if (data[k, i] == 'O' || data[k, i] == '#')
                            {
                                if (k - 1 != j)
                                {
                                    char temp = data[j, i];
                                    data[j, i] = data[k - 1, i];
                                    data[k - 1, i] = temp;
                                }
                                break;
                            }
                            else if (k == data.GetLength(0) - 1)
                            {
                                char temp = data[j, i];
                                data[j, i] = data[k, i];
                                data[k, i] = temp;
                            }
                        }
                    }
                }
            }
            return data;
        }

        public static char[,] TiltEast(char[,] data)
        {
            for (int i = data.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = data.GetLength(1) - 2; j >= 0; j--)
                {
                    if (data[i, j] == 'O')
                    {
                        for (int k = j + 1; k < data.GetLength(1); k++)
                        {
                            if (data[i, k] == 'O' || data[i, k] == '#')
                            {
                                if (k + 1 != k)
                                {
                                    char temp = data[i, j];
                                    data[i, j] = data[i, k - 1];
                                    data[i, k - 1] = temp;
                                }
                                break;
                            }
                            else if (k == data.GetLength(1) - 1)
                            {
                                char temp = data[i, j];
                                data[i, j] = data[i, k];
                                data[i, k] = temp;
                            }
                        }
                    }
                }
            }
            return data;
        }
    }
}
