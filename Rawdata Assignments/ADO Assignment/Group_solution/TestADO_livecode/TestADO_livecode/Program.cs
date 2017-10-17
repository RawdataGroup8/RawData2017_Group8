using System;
using System.Linq;

namespace TestADO_livecode
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NorthwindContext())
            {
                var categories = db.Categories.Where(x => x.Id < 5);
                foreach (var category in categories)
                {
                    Console.WriteLine(category.Name);
                }

                //var category = db.Categories.FirstOrDefault(x => x.Id == 11);
                /*if (category != null)
                {
                    category.Name = "Retesting";
                }*/


                //db.Categories.Remove(category);


                /*var category = new Category
                {
                    Name = "New Obj",
                    Description = "a description"
                };
                db.Add(category);
                db.SaveChanges();*/
            }
        }
    }
}
