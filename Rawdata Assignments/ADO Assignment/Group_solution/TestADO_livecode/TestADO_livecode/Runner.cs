﻿using System;
using System.Linq;

namespace DBMapper
{
    internal class Runner
    {
        private static void Main(string[] args)
        {
            var ds = new DataService();
            //ds.AddCategory("Guns", "They go pew pew");
            //ds.Listingcategories();
            /*ds.Listingcategories();
            ds.AddCategory("Testagory", "Its just a test");*/
            //ds.UpdateCategory(14, "test", "le test");

            //foreach (var o in ds.GetOrders())
            //    Console.WriteLine(o.Freight);

            Console.WriteLine(ds.GetOrder(10248).Id);
            //Console.ReadLine(); //Alternatively use ctrl+F5 instead of just F5 to run (debug/nodebug), then ReadLine() is not needed



        }
    }    
}
