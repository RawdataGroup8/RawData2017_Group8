using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server1
{
    static class Helpers
    {
        public static bool IsUnixTimestamp(string str)
        {
            //only works for the next many years
            return str.Length <= 10 && str.All(c => c >= '0' && c <= '9');// && int.Parse(str) > 1490000000;
        }
    }
}
