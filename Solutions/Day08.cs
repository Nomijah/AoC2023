using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.Solutions
{
    internal class Day08
    {
        public static string testData = "LLR\r\n\r\nAAA = (BBB, BBB)\r\nBBB = (AAA, ZZZ)\r\nZZZ = (ZZZ, ZZZ)";
        //public static string[] input = testData.Split('\n', StringSplitOptions.TrimEntries);
        public static string[] input = TextFormatter.ToLines("C:\\Users\\pette\\source\\repos\\AoC2023\\Data\\Day8.txt");

        public static void SolutionA()
        {
            string instructions = input[0];
            //List<Node> nodes = new List<Node>();
            string[,] nodes = new string[input.Length - 2, 3];
            Dictionary<string, int> nodeNames = new();
            for (int i = 2; i < input.Length; i++)
            {
                string[] temp = input[i].Split(new char[] { ' ', '=', ',', '(', ')' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                nodes[i - 2, 0] = temp[0];
                nodes[i - 2, 1] = temp[1];
                nodes[i-2,2] = temp[2];
                nodeNames.Add(temp[0],i - 2);
            }

            Console.WriteLine(instructions.Length);

            bool finished = false;
            int presentNode = 0;
            long counter = 0;

            while (!finished)
            {
                for (int i = 0; i < instructions.Length; i++)
                {
                    //Console.WriteLine(counter);
                    if (instructions[i] == 'L')
                    {
                        presentNode = nodeNames.GetValueOrDefault(nodes[presentNode, 1], -1);
                        counter++;
                    }
                    else
                    {
                        presentNode = nodeNames.GetValueOrDefault(nodes[presentNode, 2], -1);
                        counter++;
                    }
                    if (presentNode == nodeNames.Count - 1)
                    {
                        finished = true;
                        Console.WriteLine("Nu är det färdigt:");
                        Console.WriteLine(counter);
                        break;
                    }
                    if (i == instructions.Length - 1)
                    {
                        i = -1;
                    }
                    if (presentNode == 381 || presentNode == 598)
                    {
                        Console.WriteLine("hej från " + presentNode);
                    }
                    if (presentNode == 0 && i == 0)
                    {
                        Console.WriteLine("Nu är det 0 - " + i);
                    }
                    if (presentNode == -1)
                    {
                        Console.WriteLine("Error");
                        break;
                    }
                    //if (counter % 9999999 == 0)
                    //{
                    //    Console.WriteLine(presentNode);
                    //    Console.WriteLine(counter);
                    //}
                }
            }

            Console.WriteLine(counter);
        }
    }

    public class Node
    {
        public string Name { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }
    }
}
