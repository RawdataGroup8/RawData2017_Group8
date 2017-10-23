using System;
using System.Linq;

namespace DBMapper
{
    internal class Runner
    {
        private static void Main(string[] args)
        {
            var ds = new DataService();
            foreach (var c in ds.GetCategories())
            {
                if (c.Id > 8)
                {
                    Console.WriteLine(ds.DeleteCategory(c.Id));
                }
            }
            Console.ReadKey();
        }
    }    
}

