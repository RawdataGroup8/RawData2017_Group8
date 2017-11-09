using System;
using System.Collections.Generic;

namespace DBMapper.DBObjects
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } 
        public DateTime Required { get; set; }
        public DateTime? Shipped { get; set; }
        public double Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipCity { get; set; }
        
        public List<OrderDetails> OrderDetails { get; set; }        
    }
}
