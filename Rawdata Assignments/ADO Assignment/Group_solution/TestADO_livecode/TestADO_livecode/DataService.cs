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
        //rune comment: Are you guys up for dropping prints in dataservice.cs and doing that in runner.cs? that way the code in this library becomes a lot simpler. this method could be a one liner :)
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

        public bool DeleteCategory(int id)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                return true;
            } else { return false;}
        }
            
        

        public Category GetCategory(int id) => _db.Categories.FirstOrDefault(x => x.Id == id);

        //---------------------------------------------------------- Products
        public Product GetProduct(int id)
        {
            var p = _db.Products.FirstOrDefault(x => x.Id == id);
            p.Category = GetCategory(p.CategoryId);
            return p;
        }

        public List<Product> GetProductsMatching(String input)
        {
            List<Product> productsFound = _db.Products.Where(prod => prod.Name.Contains(input)).ToList();

            return productsFound;  // should we have an else or something?
        }

        public List<Product> GetProductsByCategory(int catid) //RETURNS NULL LIST
        {
            List<Product> productslist = _db.Products.Where(prod => prod.CategoryId == catid).ToList();
          
            return productslist; // should we have an else or something?
        }

        //---------------------------------------------------------- Orders
        public Order GetSingleOrder(int id) => _db.Orders.FirstOrDefault(x => x.Id == id);

        public List<Order> GetOrders() => _db.Orders.ToList();

        public Order GetOrder(int id) 
        {
            var order = _db.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null) return null;

            order.OrderDetails = GetOrderDetailsByOrderId(id);
            foreach (var od in order.OrderDetails)
            {
                od.Product = GetProduct(od.ProductId);
                od.Order = order;
            }

            return order;
        }


        //---------------------------------------------------------- Order Details
        public List<OrderDetails> GetOrderDetailsByOrderId(int id)
        {
            var orderDetails =_db.OrderDetails.Where(z => z.OrderId1 == id).ToList();
            foreach (var od in orderDetails)
            {
                od.Product = GetProduct(od.ProductId);
                od.Order = GetSingleOrder(id);
            }
            return orderDetails;
        }

        public List<OrderDetails> GetOrderDetailsByProductId(int id)
        {
            throw new NotImplementedException();
        }
    }
}