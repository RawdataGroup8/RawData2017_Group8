using System;
using System.Collections.Generic;
using System.Text;
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
        public void Listingcategories()
        {
            var categories = _db.Categories;
            foreach (var category in categories)
            {
                Console.WriteLine((category.Name));
            }
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------- Products
        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------- Orders
        public void GetFullOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrders()
        {
            throw new NotImplementedException();
        }
        //---------------------------------------------------------- Order Details

        public List<OrderDetails> GetOrderDetailsByOrderId(int i)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetails> GetOrderDetailsByProductId(int i)
        {
            throw new NotImplementedException();
        }



    }
}
