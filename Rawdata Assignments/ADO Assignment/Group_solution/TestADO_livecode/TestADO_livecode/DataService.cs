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
        #region Categories  
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
        #endregion

        //---------------------------------------------------------- Products
        #region Products
        public List<ProductDTO> GetProducts()
        {
            return _db.Products.ToList().Select(p => new ProductDTO(p.Name, p.UnitPrice, p.Category)).ToList();
        }

        public ProductDTO GetProduct(int id)
        {
            var p = _db.Products.FirstOrDefault(x => x.Id == id);
            p.Category = GetCategory(p.CategoryId);
            return new ProductDTO(p.Name, p.UnitPrice, p.Category);
        }

        public List<ProductDTO> GetProductByName(string name)
        {
            var products = _db.Products.Where(x => x.Name.Contains(name)).ToList();
            return products.Select(p => new ProductDTO(p.Name, p.UnitPrice, p.Category)).ToList();
            //return _db.Products.Where(x => x.Name.Contains(name)).ToList();
        }

        public List<ProductDTO> GetProductByCategory(int id)
        {
            var products = _db.Products.Where(x => x.CategoryId == id).ToList();
            foreach (var p in products)           
                p.Category = GetCategory(p.CategoryId);            
            return products.Select(p => new ProductDTO(p.Name, p.UnitPrice, p.Category)).ToList();
        }
        #endregion

        //---------------------------------------------------------- Orders
        #region Orders
        public Order GetSingleOrder(int id) => _db.Orders.FirstOrDefault(x => x.Id == id);

        public List<Order> GetOrders() => _db.Orders.ToList();

        public Order GetOrder(int id) 
        {
            var order = _db.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null) return null;

            order.OrderDetails = GetOrderDetailsByOrderId(id);

            return order;
        }
        #endregion

        //---------------------------------------------------------- Order Details
        #region Order Details
        public List<OrderDetails> GetOrderDetailsByOrderId(int id)
        {
            var orderDetails =_db.OrderDetails.Where(z => z.OrderId1 == id).ToList();
            return FillOrderDetails(orderDetails);
        }

        public List<OrderDetails> GetOrderDetailsByProductId(int id)
        {
            var orderDetails = _db.OrderDetails.Where(z => z.ProductId == id).ToList();
            return FillOrderDetails(orderDetails);
        }
        private List<OrderDetails> FillOrderDetails(List<OrderDetails> orderDetails)
        {
            foreach (var od in orderDetails)
            {
                od.Product = FillProduct(od.ProductId);
                od.Order = GetSingleOrder(od.OrderId1);
            }
            return orderDetails;
        }
        private Product FillProduct(int id)
        {
            var p = _db.Products.FirstOrDefault(x => x.Id == id);
            p.Category = GetCategory(p.CategoryId);
            return p;
        }
        #endregion

    }

}
