using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.Solutions
{
    internal class Day15
    {
        public static string testData = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
        //public static string[] input = testData.Split(',');
        public static string[] input = File.ReadAllText("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day15.txt").Split(',');


        public static void SolutionA()
        {
            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                int temp = 0;
                for (int j = 0; j < input[i].Length; j++)
                {
                    temp += input[i][j];
                    temp = (temp * 17) % 256;
                }
                sum += temp;
            }
            Console.WriteLine(sum);
        }

        public static void SolutionB()
        {
            Box[] boxes = new Box[256];
            for (int i = 0; i < 256; i++)
            {
                boxes[i] = (new Box(i));
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Contains('='))
                {
                    string[] split = input[i].Split('=');
                    int boxNum = GetBoxNum(split[0]);
                    if (boxes[boxNum].Lenses.Any(l => l.name == split[0]))
                    {
                        var temp = boxes[boxNum].Lenses.FirstOrDefault(l => l.name == split[0]);
                        temp.focalLength = Convert.ToInt32(split[1]);
                    }
                    else
                    {
                        boxes[boxNum].Lenses.Add(new Lens(split[0], Convert.ToInt32(split[1])));
                    }
                }
                else
                {
                    string label = input[i].Trim('-');
                    int boxNum = GetBoxNum(label);
                    if (boxes[boxNum].Lenses.Any(l => l.name == label))
                    {
                        boxes[boxNum].Lenses.Remove(boxes[boxNum].Lenses.FirstOrDefault(l => l.name == label));
                    }
                }
            }

            int sum = 0;
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i].Lenses.Count != 0)
                {
                    int counter = 1;
                    foreach (Lens lens in boxes[i].Lenses)
                    {
                        sum += lens.focalLength * counter *(i + 1);
                        counter++;
                    }
                }
            }
            Console.WriteLine(sum);
        }

        public static int GetBoxNum(string s)
        {
            int temp = 0;
            for (int j = 0; j < s.Length; j++)
            {
                temp += s[j];
                temp = (temp * 17) % 256;
            }
            return temp;
        }
    }

    public class Box
    {
        public int number;
        public List<Lens> Lenses;
        public Box(int number)
        {
            this.number = number;
            Lenses = new List<Lens>();
        }
    }

    public class Lens
    {
        public string name;
        public int focalLength;
        public Lens(string name, int focalLength)
        {
            this.name = name;
            this.focalLength = focalLength;
        }
    }
}
