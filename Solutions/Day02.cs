namespace AoC2023.Solutions
{
    internal class Day02
    {
        public static string[] input = TextFormatter.ToLines("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day2.txt");

        public static int SolutionA()
        {
            var games = GetGamesList();
            foreach (var game in games)
            {
                game.IsPossible = true;
                foreach (var round in game.Rounds)
                {
                    if(round.Red > 12)
                        game.IsPossible = false;
                    if(round.Green > 13)
                        game.IsPossible = false;
                    if(round.Blue > 14)
                        game.IsPossible = false;
                }
            }

            int result = 0;
            games.Where(i => i.IsPossible == true).ToList().ForEach((g) => { result += g.Id; });
            return result;
        }

        public static int SolutionB()
        {
            var games = GetGamesList();
            int sum = 0;
            foreach (var game in games)
            {
                int red = 0;
                int green = 0;
                int blue = 0;
                foreach (var round in game.Rounds)
                {
                    if (round.Red > red)
                        red = round.Red;
                    if (round.Green > green)
                        green = round.Green;
                    if (round.Blue > blue)
                        blue = round.Blue;
                }
                sum += red * green * blue;
            }
            return sum;
        }

        public static List<Game> GetGamesList()
        {
            List<Game> games = new List<Game>();
            foreach (var line in input)
            {
                string[] strings = line.Split([':', ';']);
                Game game = new Game() { Rounds = new List<Round>() };
                game.Id = Convert.ToInt32(strings[0].Split(' ')[1]);
                // Loop through each round of current game
                foreach (string round in strings.Skip(1))
                {
                    // Create new round to fill with data
                    Round tempRound = new Round() { Green = 0, Blue = 0, Red = 0 };
                    var roundParts = round.Split(',');
                    foreach (string roundPart in roundParts)
                    {
                        int amount = Convert.ToInt32(roundPart.TrimStart().Split(' ')[0]);
                        string color = Convert.ToString(roundPart.TrimStart().Split(' ')[1]);
                        switch (color)
                        {
                            case "green":
                                tempRound.Green = amount;
                                break;
                            case "blue":
                                tempRound.Blue = amount;
                                break;
                            case "red":
                                tempRound.Red = amount;
                                break;
                        }
                    }
                    game.Rounds.Add(tempRound);
                }
                games.Add(game);
            }
            return games;
        } 
    }

    public class Game
    {
        public int Id { get; set; }
        public List<Round> Rounds { get; set; }
        public bool IsPossible { get; set; }

    }

    public class Round
    {
        public int Green { get; set; }
        public int Blue { get; set; }
        public int Red { get; set; }
    }
}
