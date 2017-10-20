using System;
using System.Linq;

namespace DBMapper
{
    internal class Runner
    {
        private static void Main(string[] args)
        {
            var ds = new DataService();
            Console.WriteLine(ds.GetProductByCategory(1).Count);
            Console.ReadKey();
        }
    }    
}

