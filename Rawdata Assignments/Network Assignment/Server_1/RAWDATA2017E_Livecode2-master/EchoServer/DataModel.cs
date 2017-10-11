using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Server1
{
    internal class DataModel
    {
        public Dictionary<string, Category> Data;

        //Create datamodel with very hardcoded data
        public DataModel()
        {
            Data = new Dictionary<string, Category>();
            Console.Write("Creating datamodel ... ");
            Data.Add("/api/categories/1", new Category(1,"Beverages"));
            Data.Add("/api/categories/2", new Category(2, "Condiments"));
            Data.Add("/api/categories/3", new Category(3, "Confections"));

            Console.WriteLine("Done:");
            PrintModel();
        }

        private void PrintModel()
        {
            foreach (var c in Data)
            {
                Console.Write(c.Key + " returns --> ");
                c.Value.Print();
            }
        }

        public Category Retrieve(string path)
        {
            
            return path != null && Data.ContainsKey(path) ? Data[path] : null;
        }

        public Category[] ReadAll() {
            var entries = new Category[Data.Count];
            var count = 0;
            foreach (var c in Data)
            {
                entries[count] = c.Value;
                count++;
            }
            return entries;
        }
    }


    public class Category
    {
        [JsonProperty("cid")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        public Category(){}

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void Print() {
            Console.WriteLine("[ID: "+Id +", Name: "+Name+"]");
        }
    }
}
