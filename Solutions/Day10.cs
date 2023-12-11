namespace AoC2023.Solutions
{
    internal class Day10
    {
        public static string testData = "7-F7-\r\n.FJ|7\r\nSJLL7\r\n|F--J\r\nLJ.LJ";
        //public static string[] input = testData.Split("\r\n", StringSplitOptions.TrimEntries);
        public static string[] input = TextFormatter.ToLines("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day10.txt");


        public static void SolutionA()
        {
            char[,] map = new char[input.GetLength(0), input[0].Length];
            Coordinate startingPoint = new(0, 0);

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = input[i][j];
                    if (map[i, j] == 'S')
                    {
                        startingPoint.y = i;
                        startingPoint.x = j;
                    }
                }
            }

            /* | is a vertical pipe connecting north and south.
                - is a horizontal pipe connecting east and west.
                L is a 90-degree bend connecting north and east.
                J is a 90-degree bend connecting north and west.
                7 is a 90-degree bend connecting south and west.
                F is a 90-degree bend connecting south and east.
                . is ground; there is no pipe in this tile. */

            char northOfStart = map[startingPoint.y - 1, startingPoint.x];
            char southOfStart = map[startingPoint.y + 1, startingPoint.x];
            char eastOfStart = map[startingPoint.y, startingPoint.x + 1];

            Coordinate firstPart = startingPoint;

            bool connectionFound = false;
            int counter = 1;
            while (!connectionFound)
            {
                switch (counter)
                {
                    case 1:
                        if (northOfStart == '|' || northOfStart == '7' || northOfStart == 'F')
                        {
                            connectionFound = true;
                            firstPart.y--;
                            break;
                        }
                        counter++;
                        break;
                    case 2:
                        if (eastOfStart == '-' || eastOfStart == '7' || eastOfStart == 'J')
                        {
                            connectionFound = true;
                            firstPart.x++;
                            break;
                        }
                        counter++;
                        break;
                    case 3:
                        if (southOfStart == '|' || southOfStart == 'J' || southOfStart == 'L')
                        {
                            connectionFound = true;
                            firstPart.y++;
                            break;
                        }
                        counter++;
                        break;
                }
            }

            Coordinate current = firstPart;
            Coordinate previous = startingPoint;
            bool backToStart = false;
            int steps = 1;

            while (!backToStart)
            {
                if (current.y < previous.y)
                {
                    previous = current;
                    switch (map[current.y, current.x])
                    {
                        case '|':
                            current.y--;
                            break;
                        case '7':
                            current.x--;
                            break;
                        case 'F':
                            current.x++;
                            break;
                    }
                    steps++;
                }
                else if (current.y > previous.y)
                {
                    previous = current;
                    switch (map[current.y, current.x])
                    {
                        case '|':
                            current.y++;
                            break;
                        case 'J':
                            current.x--;
                            break;
                        case 'L':
                            current.x++;
                            break;
                    }
                    steps++;
                }
                else if (current.x > previous.x)
                {
                    previous = current;
                    switch (map[current.y, current.x])
                    {
                        case '-':
                            current.x++;
                            break;
                        case 'J':
                            current.y--;
                            break;
                        case '7':
                            current.y++;
                            break;
                    }
                    steps++;
                }
                else if (current.x < previous.x)
                {
                    previous = current;
                    switch (map[current.y, current.x])
                    {
                        case '-':
                            current.x--;
                            break;
                        case 'L':
                            current.y--;
                            break;
                        case 'F':
                            current.y++;
                            break;
                    }
                    steps++;
                }

                if (current.x == startingPoint.x && current.y == startingPoint.y)
                {
                    backToStart = true;
                }
            }
            Console.WriteLine(steps / 2);
        }

        public static void SolutionB()
        {
            char[,] map = new char[input.GetLength(0), input[0].Length];
            Coordinate startingPoint = new(0, 0);

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = input[i][j];
                    if (map[i, j] == 'S')
                    {
                        startingPoint.y = i;
                        startingPoint.x = j;
                    }
                }
            }

            /* | is a vertical pipe connecting north and south.
                - is a horizontal pipe connecting east and west.
                L is a 90-degree bend connecting north and east.
                J is a 90-degree bend connecting north and west.
                7 is a 90-degree bend connecting south and west.
                F is a 90-degree bend connecting south and east.
                . is ground; there is no pipe in this tile. */

            char northOfStart = map[startingPoint.y - 1, startingPoint.x];
            char southOfStart = map[startingPoint.y + 1, startingPoint.x];
            char eastOfStart = map[startingPoint.y, startingPoint.x + 1];

            Coordinate firstPart = startingPoint;

            bool connectionFound = false;
            int counter = 1;
            while (!connectionFound)
            {
                switch (counter)
                {
                    case 1:
                        if (northOfStart == '|' || northOfStart == '7' || northOfStart == 'F')
                        {
                            connectionFound = true;
                            firstPart.y--;
                            break;
                        }
                        counter++;
                        break;
                    case 2:
                        if (eastOfStart == '-' || eastOfStart == '7' || eastOfStart == 'J')
                        {
                            connectionFound = true;
                            firstPart.x++;
                            break;
                        }
                        counter++;
                        break;
                    case 3:
                        if (southOfStart == '|' || southOfStart == 'J' || southOfStart == 'L')
                        {
                            connectionFound = true;
                            firstPart.y++;
                            break;
                        }
                        counter++;
                        break;
                }
            }

            List<Coordinate> pipeParts = new List<Coordinate>();

            Coordinate current = firstPart;
            Coordinate previous = startingPoint;
            bool backToStart = false;
            int steps = 1;

            while (!backToStart)
            {
                    pipeParts.Add(current);
                if (current.y < previous.y)
                {
                    previous = current;
                    switch (map[current.y, current.x])
                    {
                        case '|':
                            current.y--;
                            break;
                        case '7':
                            current.x--;
                            break;
                        case 'F':
                            current.x++;
                            break;
                    }
                    steps++;
                }
                else if (current.y > previous.y)
                {
                    previous = current;
                    switch (map[current.y, current.x])
                    {
                        case '|':
                            current.y++;
                            break;
                        case 'J':
                            current.x--;
                            break;
                        case 'L':
                            current.x++;
                            break;
                    }
                    steps++;
                }
                else if (current.x > previous.x)
                {
                    previous = current;
                    switch (map[current.y, current.x])
                    {
                        case '-':
                            current.x++;
                            break;
                        case 'J':
                            current.y--;
                            break;
                        case '7':
                            current.y++;
                            break;
                    }
                    steps++;
                }
                else if (current.x < previous.x)
                {
                    previous = current;
                    switch (map[current.y, current.x])
                    {
                        case '-':
                            current.x--;
                            break;
                        case 'L':
                            current.y--;
                            break;
                        case 'F':
                            current.y++;
                            break;
                    }
                    steps++;
                }

                if (current.x == startingPoint.x && current.y == startingPoint.y)
                {
                    backToStart = true;
                }
            }

            PrintMap(map, pipeParts);

            
        }

        public static void PrintMap(char[,] map, List<Coordinate> pipes)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (pipes.Contains(new Coordinate(i, j)))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(map[i, j]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(map[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        public struct Coordinate
        {
            public int y;
            public int x;

            public Coordinate(int y, int x)
            {
                this.y = y;
                this.x = x;
            }
        }
    }
}
