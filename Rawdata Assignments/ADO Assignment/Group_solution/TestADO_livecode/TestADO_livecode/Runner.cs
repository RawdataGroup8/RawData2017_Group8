using System;
using System.Linq;

namespace DBMapper
{
    internal class Runner
    {
        private static void Main(string[] args)
        {
            var ds = new DataService();
            ds.AddCategory("Guns", "They go pew pew");
            ds.Listingcategories();
            /*ds.Listingcategories();
            ds.AddCategory("Testagory", "Its just a test");*/


            //foreach (var o in ds.GetOrders())
            //    Console.WriteLine(o.Freight);

            Console.WriteLine(ds.GetSingleOrder(10248).Id);
            Console.ReadLine();
           


        }
    }    
}

