namespace AoC2023.Solutions
{
    public class Day01
    {
        public static int SolutionA()
        {
            string[] input = TextFormatter.ToLines("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day1.txt");

            List<string> numbers = [];

            foreach (string line in input)
            {
                numbers.Add(new string(line.Where(char.IsDigit).ToArray()));
            }

            List<string> numbersReduced = [];
            foreach (string line in numbers)
            {
                if (line.Length <= 2)
                {
                    numbersReduced.Add(line);
                }
                else
                {
                    numbersReduced.Add(new string(line.Substring(0, 1) + line.Substring(line.Length - 1, 1)));
                }
            }

            int result = 0;
            foreach (string line in numbersReduced)
            {
                if (line.Length == 1)
                {
                    string temp = line + line;
                    result += Convert.ToInt32(temp);
                }
                else
                {
                    result += Convert.ToInt32(line);
                }
            }
            return result;
        }

        public static int SolutionB()
        {
            string[] input = TextFormatter.ToLines("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day1.txt");
            //string[] input = "two1nine\r\neightwothree\r\nabcone2threexyz\r\nxtwone3four\r\n4nineeightseven2\r\nzoneight234\r\n7pqrstsixteen".Split('\n');
            //string[] input = "qzhmmsqfc7\r\n3kbklxmh".Split('\n');
            //string[] input = ["449three45three"];
            //string[] input = ["eightnine2eightnineeight"];

            List<string> numbersText = [.. Enum.GetNames<Numbers>()];
            List<int> numbers = [];
            foreach (string line in input)
            {
                numbers.Add(ConvertStringToNumbers(line, numbersText));
            }

            return numbers.Sum();
        }

        public static int ConvertStringToNumbers(string input, List<string> values)
        {
            int indexOfFirst = input.Length +1;
            int indexOfLast = -1;
            int first = 0, last = 0;

            //Console.WriteLine(input);

            foreach (var value in values)
            {
                if (input.Contains(value))
                {
                    int startPointFirst = input.IndexOf(value);
                    int startPointLast = input.LastIndexOf(value);
                    if (startPointFirst < indexOfFirst)
                    {
                        indexOfFirst = startPointFirst;
                        first = (int) Enum.Parse(typeof(Numbers), value);
                    }
                    if (startPointLast > indexOfLast)
                    {
                        indexOfLast = startPointLast;
                        last = (int)Enum.Parse(typeof(Numbers), value);
                    }
                }
                string digit = Convert.ToString((int)Enum.Parse(typeof(Numbers), value));
                if (input.Contains(digit))
                {
                    int startPointFirst = input.IndexOf(digit);
                    int startPointLast = input.LastIndexOf(digit);
                    if (startPointFirst < indexOfFirst)
                    {
                        indexOfFirst = startPointFirst;
                        first = (int)Enum.Parse(typeof(Numbers), value);
                    }
                    if (startPointLast > indexOfLast)
                    {
                        indexOfLast = startPointLast;
                        last = (int)Enum.Parse(typeof(Numbers), value);
                    }
                }
            }
            string numbersTogether = first.ToString() + last.ToString();

            //Console.WriteLine(numbersTogether);

            return Convert.ToInt32(numbersTogether);
        }

        public enum Numbers
        {
            one = 1,
            two = 2,
            three = 3,
            four = 4,
            five = 5,
            six = 6,
            seven = 7,
            eight = 8,
            nine = 9
        }
    }
}
