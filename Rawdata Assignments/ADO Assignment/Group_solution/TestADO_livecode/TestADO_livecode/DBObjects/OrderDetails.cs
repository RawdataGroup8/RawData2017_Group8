using System;
using System.Collections.Generic;
using System.Text;

namespace DBMapper.DBObjects
{
    public class OrderDetails
    {
        public Order Order { get; set; }//both id and order?
        public int OrderId { get; set; }
        //public string Name { get; set; }
        public float UnitPrice { get; set; }
        public float Discount { get; set; }
        public Product Product { get; set; }//both id and product?
        public int ProductId { get; set; }
        public float Quantity { get; set; }
    }
}
