using System;
using System.Collections.Generic;
using System.Linq;
using DBMapper.DBObjects;

namespace DBMapper
{
    public class DataService
    {
        private readonly NorthwindContext _db;
        public DataService() => _db = new NorthwindContext();

        //---------------------------------------------------------- Categories
        // This method returns all the categories
        //rune comment: Are you guys up for dropping prints in dataservice and doing that in runner? that way the code in this library class becomes a lot simpler. this method could be a one liner :)
        public List<Category> Listingcategories() 
        {
            foreach (var category in _db.Categories)
            {
                Console.WriteLine((category.Id, category.Name, category.Description));
            }
            return _db.Categories.ToList();
        }

        public void AddCategory(string name, string description)
        {
            var category = new Category
            {
                Name = name,
                Description = description
            };
            _db.Add(category);
            _db.SaveChanges();
        }

        public bool UpdateCategory(int id, string name, string description)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null) return false;
            category.Name = name;
            category.Description = description;
            _db.SaveChanges();
            return true;
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

        // not working as intended
        public Order GetOrder(int id) 
        {
            var order = _db.Orders.FirstOrDefault(x => x.Id == id);
            //var od = new List<OrderDetails>();
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
