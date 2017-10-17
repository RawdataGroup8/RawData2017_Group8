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
                foreach (var category1 in categories)
                {
                    Console.WriteLine(category1.Name);
                }

                var category2 = db.Categories.FirstOrDefault(x => x.Id == 11);
                if (category2 != null)
                {
                    category2.Name = "Retesting";
                }


                //db.Categories.Remove(category);


                var category3 = new Category
                {
                    Name = "New Obj",
                    Description = "a description"
                };
                db.Add(category3);
                db.SaveChanges();
            }
        }
    }
}
