using System;
using System.Linq;

namespace DBMapper
{
    internal class Runner
    {
        private static void Main(string[] args)
        {
            var ds = new DataService();
            //Delete the "Testing, Testing" categories
            for (var i = 11; i < 14; i++)            
                Console.WriteLine(ds.DeleteCategory(i));
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

