using System;
using System.Linq;

namespace DBMapper
{
    internal class Runner
    {
        private static void Main(string[] args)
        {
            //DataLayer.Listingcategories();

            var dataService = new DataService();
            dataService.Listingcategories();

            /*var categories = db.Categories.Where(x => x.Id < 5);
            foreach (var category in categories)
            {
                Console.WriteLine(category.Name);
            }*/

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

