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
        public static string testData2 = "LR\r\n\r\n11A = (11B, XXX)\r\n11B = (XXX, 11Z)\r\n11Z = (11B, XXX)\r\n22A = (22B, XXX)\r\n22B = (22C, 22C)\r\n22C = (22Z, 22Z)\r\n22Z = (22B, 22B)\r\nXXX = (XXX, XXX)";
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
                nodes[i - 2, 2] = temp[2];
                nodeNames.Add(temp[0], i - 2);
            }

            bool finished = false;
            int presentNode = nodeNames.GetValueOrDefault("AAA");
            long counter = 0;

            while (!finished)
            {
                for (int i = 0; i < instructions.Length; i++)
                {
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
                    if (presentNode == nodeNames.GetValueOrDefault("ZZZ"))
                    {
                        finished = true;
                        break;
                    }
                    if (i == instructions.Length - 1)
                    {
                        i = -1;
                    }
                }
            }

            Console.WriteLine(counter);
        }

        public static void SolutionB()
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
                nodes[i - 2, 2] = temp[2];
                nodeNames.Add(temp[0], i - 2);
            }

            bool finished = false;
            long counter = 0;

            List<int> endWithAIndexes = new List<int>();
            List<int> endWithZIndexes = new();
            foreach (KeyValuePair<string, int> node in nodeNames)
            {
                if (node.Key[2] == 'A')
                {
                    endWithAIndexes.Add(node.Value);
                }
            }

            foreach (KeyValuePair<string, int> node in nodeNames)
            {
                if (node.Key[2] == 'Z')
                {
                    endWithZIndexes.Add(node.Value);
                }
            }

            int[] currentIndex = endWithAIndexes.ToArray();
            int[] zIndex = endWithZIndexes.ToArray();

            while (!finished)
            {
                for (int i = 0; i < instructions.Length; i++)
                {
                    for (int j = 0; j < currentIndex.Length; j++)
                    {
                        if (instructions[i] == 'L')
                        {
                            currentIndex[j] = nodeNames.GetValueOrDefault(nodes[currentIndex[j], 1]);
                        }
                        else
                        {
                            currentIndex[j] = nodeNames.GetValueOrDefault(nodes[currentIndex[j], 2]);
                        }
                    }
                    int endCounter = 0;
                    foreach (var num in currentIndex)
                    {
                        for (int k = 0; k < zIndex.Length; k++)
                        {
                            if (endWithZIndexes[k] == num)
                            {
                                endCounter++;
                            }
                        }
                        if (endCounter == 0)
                        {
                            break;
                        }
                    }
                    counter++;
                    if (endCounter == 6)
                    {
                        finished = true;
                        break;
                    }
                    if (i == instructions.Length - 1)
                    {
                        i = -1;
                    }
                    //Console.WriteLine(counter);
                }
            }
            Console.WriteLine("svar");
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
