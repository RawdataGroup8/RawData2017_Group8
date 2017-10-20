using System;
using System.Collections.Generic;
using System.Linq;
using DBMapper.DBObjects;

namespace DBMapper
{
    public class DataService : IDataService
    {
        private readonly NorthwindContext _db;
        public DataService()
        {
            _db = new NorthwindContext();
        }

        //---------------------------------------------------------- Categories
        // This method returns all the categories
        public List<Category> GetCategories() => _db.Categories.ToList();

        public Category CreateCategory(string name, string description)
        {
            var category = new Category
            {
                Name = name,
                Description = description
            };
            _db.Add(category);
            _db.SaveChanges();
            return category;
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
            if (category == null) return false;
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return true;
        }

        public Category GetCategory(int id) => _db.Categories.FirstOrDefault(x => x.Id == id);

        //---------------------------------------------------------- Products
        public Product GetProduct(int id)
        {
            var p = _db.Products.FirstOrDefault(x => x.Id == id);
            p.Category = GetCategory(p.CategoryId);
            return p;
        }


        public List<Product> GetProductsByCategory(int i)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductByName(string name)
        {
            return _db.Products.Where(x => x.Name.Contains(name)).ToList();
        }

        public List<Product> GetProductsMatching(string input)
        {
            List<Product> productsFound = _db.Products.Where(prod => prod.Name.Contains(input)).ToList();

            return productsFound;  // should we have an else or something?
        }

        public List<Product> GetProductByCategory(int id)
        {
            var products = _db.Products.Where(x => x.CategoryId == id).ToList();
            foreach (var p in products)           
                p.Category = GetCategory(p.CategoryId);            
            return products;
        }

        //---------------------------------------------------------- Orders
        public Order GetSingleOrder(int id) => _db.Orders.FirstOrDefault(x => x.Id == id);

        public List<Order> GetOrders() => _db.Orders.ToList();

        public Order GetOrder(int id) 
        {
            var order = _db.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null) return null;

            order.OrderDetails = GetOrderDetailsByOrderId(id);

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
            var orderDetails = _db.OrderDetails.Where(z => z.ProductId == id).ToList();
            foreach (var od in orderDetails)
            {
                od.Product = GetProduct(od.ProductId);
                od.Order = GetSingleOrder(id);
            }
            return orderDetails;
        }
    }

}
