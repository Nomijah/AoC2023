using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023
{
    internal class TextFormatter
    {
        public static string[] ToLines(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}
