using System;
using System.Collections.Generic;
using System.Text;
using DBMapper.DBObjects;

namespace DBMapper
{
    public interface IDataService
    {
        //List<Category> ListAllCategories();
        Category CreateCategory(string name, string description);
        bool UpdateCategory(int id, string name, string description);
        bool DeleteCategory(int id);
        Category GetCategory(int id);
        Product GetProduct(int id);
        Order GetSingleOrder(int id);
        List<Order> GetOrders();
        Order GetOrder(int id);
        List<OrderDetails> GetOrderDetailsByOrderId(int id);
        List<OrderDetails> GetOrderDetailsByProductId(int id);
        List<Category> GetCategories();
        List<Product> GetProductByCategory(int i);
        List<Product> GetProductByName(string ant);
    }
}
