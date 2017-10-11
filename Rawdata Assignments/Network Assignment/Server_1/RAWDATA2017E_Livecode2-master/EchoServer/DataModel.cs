using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Server1
{
    class DataModel
    {
        public Dictionary<string, Category> DataStructure;

        //Create datamodel with hardcoded data
        public DataModel()
        {
            DataStructure = new Dictionary<string, Category>();
            Console.Write("Creating datamodel ... ");
            DataStructure.Add("/api/categories/1", new Category(1,"Beverages"));
            DataStructure.Add("/api/categories/2", new Category(2, "Condiments"));
            DataStructure.Add("/api/categories/3", new Category(3, "Confections"));

            Console.WriteLine("Done:");
            foreach (var c in DataStructure)
            {
                Console.Write(c.Key + " returns --> ");
                c.Value.Print();
            }
        }

        public Category Read(string path)
        {
            return DataStructure[path];
        }

        public Category[] ReadAll() {
            var entries = new Category[DataStructure.Count];
            var count = 0;
            foreach (var c in DataStructure)
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
