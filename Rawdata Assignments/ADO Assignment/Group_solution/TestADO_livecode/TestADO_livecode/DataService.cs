using System;
using System.Collections.Generic;
using System.Linq;
using DBMapper.DBObjects;

namespace DBMapper
{
    public class DataService
    {
        private readonly NorthwindContext _db;
        public DataService()
        {
            _db = new NorthwindContext(); 
        }

        //---------------------------------------------------------- Categories
        // This method returns all the categories
        public List<Category> Listingcategories() //Made it return a list as well :)
        {
            //var categories = _db.Categories; // no need ;) 
            foreach (var category in _db.Categories)
            {
                Console.WriteLine((category.Id, category.Name, category.Description));
            }
            return _db.Categories.ToList();
        }

        public void AddCategory(string name, string description)
        {
            int maxId  =  0;
            foreach(var category in _db.Categories)
            {
                if (category.Id > maxId)
                {
                    maxId = category.Id;
                }
            }
            var category2 = new Category
            {
                Id = maxId + 1,
                Name = name,
                Description = description
            };
            _db.Add(category2);
            _db.SaveChanges();
        }

        public bool UpdateCategory(int id, string name, string description) //id of the category you want to modify, followed by new name and description
        {
            var selectedCategory = _db.Categories.FirstOrDefault(x => x.Id == id);
            if (_db.Categories..Contains(selectedCategory))
            {
                selectedCategory.Name = name;
                selectedCategory.Description = description;
                return true;
            } else
            {
                return false;
            }
                            
        }

        public void DeleteCategory(int id)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null) // need null check          
                _db.Categories.Remove(category);          
        }

        public Category GetCategory(int id) => _db.Categories.FirstOrDefault(x => x.Id == id);

        //---------------------------------------------------------- Products
        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------- Orders
        public Order GetSingleOrder(int id) => _db.Orders.FirstOrDefault(x => x.Id == id);

        public List<Order> GetOrders() => _db.Orders.ToList();

        public Order GetOrder(int id) 
        {
            var order = _db.Orders.FirstOrDefault(x => x.Id == id);
            if (order != null)           
                order.OrderDetails = _db.OrderDetails.Where(z => z.OrderId == id).ToList();           
            return order;
        }

        //---------------------------------------------------------- Order Details
        public List<OrderDetails> GetOrderDetailsByOrderId(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetails> GetOrderDetailsByProductId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
