using System;
using System.Linq;

namespace DBMapper
{
    internal class Runner
    {
        private static void Main(string[] args)
        {
            var ds = new DataService();

            /*ds.Listingcategories();
            ds.AddCategory("Testagory", "Its just a test");
            ds.Listingcategories();*/

            //foreach (var o in ds.GetOrders())
            //    Console.WriteLine(o.Freight);

            Console.WriteLine(ds.GetOrder(10248));
           


        }
    }    
}

