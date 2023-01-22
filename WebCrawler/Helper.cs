using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebCrawler
{
    public static class Helper
    {
        public static string GetNumber(this string input)
        {
            return Regex.Match(input, @"\d+").Value;
        }
    }
}
