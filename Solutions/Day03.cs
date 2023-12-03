using System.Text.RegularExpressions;

namespace AoC2023.Solutions
{
    internal class Day03
    {
        public static string testData = "467..114..\r\n...*......\r\n..35..633.\r\n......#...\r\n617*......\r\n.....+.58.\r\n..592.....\r\n......755.\r\n...$.*....\r\n.664.598..";
        public static string testData2 = ".....*......204......*..........*.............226...........663.......*......555.992.....502...........631..49.#....366........677..........\r\n..958.350.....*......44...694...449..-39........*...........-...786.........*......*............................140.........................\r\n...............477.........*...................815..............*..........815.................521....................&...273........103....";
        public static string testData3 = "..889................695........654..750.....*.............637........./...............................780....*726....233...*...............\r\n..................../.................*.....453.....642....*.........828......@...94...........152/...*....790.......*.....445......../.....\r\n...........................51.......681........................271..........719.......................964......399..426...............456...";
        public static string testData4 = ".975....399.......*...-.......*814...663..................*....*...........381../...............*.............@.........284............*....\r\n...*............367....198..........@.....992.....716*529....96.................729.329.688...%.322.-......67.79......-.......335....997....\r\n488............................632........*...../......................387.............*....225......491.............391..750...%...........";
        //public static string[] input = testData4.Split('\n', StringSplitOptions.TrimEntries);
        public static string[] input = TextFormatter.ToLines("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day3.txt");

        public static int SolutionA()
        {
            var partList = GetParts();

            partList = SetPartsValidity(partList, input.ToList());

            int sum = 0;
            foreach (var part in partList)
            {
                if (part.isPartNr)
                {
                    sum += part.Value;
                }
            }

            // Makes a list of all coordinates that have actual parts
            var redList = new List<Coordinate>();
            foreach (var part in partList)
            {
                if (part.isPartNr)
                {
                    redList.Add(part.Start);
                    if (part.Start.x != part.End.x)
                    {
                        if (part.Start.x + 2 == part.End.x)
                        {
                            redList.Add(new Coordinate { y = part.Start.y, x = part.Start.x + 1 });
                        }
                        redList.Add(part.End);
                    }
                }
            }

            // Prints out the map with colors, parts with adjacent symbols are red, symbols are green and the rest is white.
            Regex symbolsMatch = new Regex("[^0-9.\\s]");
            char[][] result = input.Select(item => item.ToArray()).ToArray();
            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < result.Length; j++)
                {
                    foreach (var c in redList)
                    {
                        if (c.y == i && c.x == j)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                    }
                    if (symbolsMatch.IsMatch(result[i][j].ToString()))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(result[i][j]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("\n");
            }

            return sum;
        }

        public static int SolutionB()
        {
            var partList = GetParts();
            var starList = GetStars();

            int sum = 0;
            var redList = new List<Coordinate>();
            foreach (var star in starList)
            {
                    var coordinatesToCheck = GenerateNumbers(star);
                    var adjacentParts = new List<Part>();
                    foreach (var part in partList)
                    {
                        // Added this to save time when debugging
                        if (!(star.x - 2 > part.End.x || star.x + 2 < part.Start.x))
                        {
                            bool added = false;
                            foreach (var c in coordinatesToCheck)
                            {
                                if (c.y == part.Start.y)
                                {
                                    if (!added)
                                    {
                                        if (c.x == part.Start.x)
                                        {
                                            adjacentParts.Add(part);
                                            added = true;
                                            break;
                                        }
                                        else if (c.x == part.End.x)
                                        {
                                            adjacentParts.Add(part);
                                            added = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (adjacentParts.Count == 2)
                    {
                        sum += adjacentParts[0].Value * adjacentParts[1].Value;
                        redList.Add(star);
                    }
            }

            // Prints out the map with colors, red stars are gears with two adjacent parts and green are not.
            Regex symbolsMatch = new Regex("[^0-9.\\s]");
            char[][] result = input.Select(item => item.ToArray()).ToArray();
            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < result[0].Length; j++)
                {
                    bool red = false;
                    foreach (var c in redList)
                    {
                        if (c.y == i && c.x == j)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            red = true;
                        }
                    }
                    if (!red)
                    {
                        foreach (var c in starList)
                        {
                            if (c.y == i && c.x == j)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                        }
                    }
                    Console.Write(result[i][j]);
                    Console.ForegroundColor = ConsoleColor.White;
                    red = false;
                }
                Console.Write("\n");
            }

            return sum;
        }

        public static List<Coordinate> GetStars()
        {
            var starList = new List<Coordinate>();
            Regex starMatch = new("\\*{1}");
            int lineCounter = 0;

            foreach (string line in input)
            {
                string[] parts = starMatch.Matches(line).Cast<Match>().Select(m => m.Value).ToArray();
                var starPoint = 0;
                foreach (var item in parts)
                {
                    starPoint = line.IndexOf(item, starPoint + 1);
                    var temp = new Coordinate { y = lineCounter, x = starPoint, };
                    starList.Add(temp);
                }
                lineCounter++;
            }
            return starList;
        }

        public static List<Part> GetParts()
        {
            Regex digitMatch = new("\\d{1,3}");
            int lineCounter = 0;
            List<Part> partList = new List<Part>();

            foreach (string line in input)
            {
                string[] parts = digitMatch.Matches(line).Cast<Match>().Select(m => m.Value).ToArray();
                var startPoint = 0;
                foreach (var item in parts)
                {
                    startPoint = line.IndexOf(item, startPoint);
                    var endPoint = startPoint + item.Length - 1;
                    var temp = new Part
                    {
                        Start = new Coordinate { y = lineCounter, x = startPoint, },
                        End = new Coordinate { y = lineCounter, x = endPoint, },
                        Value = Convert.ToInt32(item),
                        isPartNr = false
                    };
                    partList.Add(temp);
                    // Add one to startpoint so the same part does not get registered twice if they have the same number.
                    startPoint++; 
                }
                lineCounter++;
            }
            return partList;
        }

        public static List<Coordinate> GenerateNumbers(Coordinate c)
        {
            var cList = new List<Coordinate>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (!(j == 0 && i == 0))
                        cList.Add(new Coordinate { y = c.y + i, x = c.x + j });
                }
            }
            return cList;
        }

        public static bool CheckSymbol(Coordinate c, int yMax, int xMax, List<string> data)
        {
            Regex symbolsMatch = new Regex("[^0-9.\\s]");
            var coordinatesToCheck = GenerateNumbers(c);
            foreach (var coordinate in coordinatesToCheck)
            {
                if (coordinate.y != -1 && coordinate.x != -1 && coordinate.y != yMax && coordinate.x != xMax)
                    if (symbolsMatch.IsMatch(Convert.ToString(data[coordinate.y][coordinate.x])))
                    {
                        return true;
                    }
            }
            return false;
        }

        public static List<Part> SetPartsValidity(List<Part> parts, List<string> data)
        {
            foreach (var part in parts)
            {
                if (CheckSymbol(part.Start, data.Count, data[0].Length, data))
                {
                    part.isPartNr = true;
                }
                else if (CheckSymbol(part.End, data.Count, data[0].Length, data))
                {
                    part.isPartNr = true;
                }
            }
            return parts;
        }
    }

    internal class Part
    {
        public Coordinate Start { get; set; }
        public Coordinate End { get; set; }
        public int Value { get; set; }
        public bool isPartNr { get; set; }
    }
    internal class Coordinate
    {
        public int y { get; set; }
        public int x { get; set; }
    }
}
