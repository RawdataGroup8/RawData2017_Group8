using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server1
{
    internal static class Helpers
    {   //only works for the next many many years
        public static bool IsUnixTimestamp(string str) =>
            str.Length <= 10 && str.Length >= 7 && str.All(c => c >= '0' && c <= '9');// && int.Parse(str) > 1490000000;
    }
}
