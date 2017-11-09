using System;
using System.Collections.Generic;
using System.Text;
using DBMapper.DBObjects;
using DBMapper.DTOs;

namespace DBMapper
{
    public interface IDataService
    {
        bool UpdateCategory(int id, string name, string description);
        bool DeleteCategory(int id);
        Category CreateCategory(string name, string description);
        Category GetCategory(int id);
        List<Category> GetCategories();

        Order GetSingleOrder(int id);
        Order GetOrder(int id);
        List<Order> GetOrders();
        ProductDTO GetProduct(int id);

        List<OrderDetails> GetOrderDetailsByOrderId(int id);
        List<OrderDetails> GetOrderDetailsByProductId(int id);

        List<ProductDTO> GetProducts(); 
        List<ProductDTO> GetProductByCategory(int i);
        List<ProductDTO> GetProductByName(string ant); 
    }
}
