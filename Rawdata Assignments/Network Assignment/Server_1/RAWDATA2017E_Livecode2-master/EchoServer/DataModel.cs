using System;
using System.Collections.Generic;
using System.Linq;
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
            Data.Add("/api/categories/1", new Category(1, "Beverages"));
            Data.Add("/api/categories/2", new Category(2, "Condiments"));
            Data.Add("/api/categories/3", new Category(3, "Confections"));

            Console.WriteLine("Done:");
            PrintModel();
        }

        public Category Create(string path, string name)
        {
            var c = new Category(Data.Count+1,name);
            Data.Add(path +"/"+c.Id, c);
            PrintModel();
            return c;
        }

        public void Delete(string path)
        {
            Data.Remove(path);
        }

        public Category UpdateName(string path, string name)
        {
            Data[path].Name = name;
            PrintModel();
            return Data[path];
        }

        public Category Retrieve(string path) => path != null && Data.ContainsKey(path) ? Data[path] : null;

        public List<object> ReadAll() => Data.Select(c => c.Value).Cast<object>().ToList();

        public void PrintModel()
        {
            foreach (var c in Data)
            {
                Console.Write(c.Key + " returns --> ");
                c.Value.Print();
            }
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
