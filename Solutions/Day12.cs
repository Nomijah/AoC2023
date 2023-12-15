using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.Solutions
{
    internal class Day12
    {
        public static string testData = "???.### 1,1,3\r\n.??..??...?##. 1,1,3\r\n?#?#?#?#?#?#?#? 1,3,1,6\r\n????.#...#... 4,1,1\r\n????.######..#####. 1,6,5\r\n?###???????? 3,2,1";
        public static string[] input = testData.Split("\r\n");

        public static void SolutionA()
        {


            string[] temp = input[0].Split(' ');
            List<int> groups = temp[1].Split(',').Select(x => Convert.ToInt32(x)).ToList();
            int stringLength = temp[0].Length;
            int wiggleRoom = stringLength - (groups.Sum() + groups.Count - 1);
            string possibleOrder = "";

            foreach (int group in groups)
            {
                for (int i = 0; i < group; i++)
                {
                    possibleOrder += '#';
                }
                possibleOrder += '.';
            }



        }
    }
}
