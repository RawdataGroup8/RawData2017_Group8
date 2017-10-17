using System;
using System.Collections.Generic;

namespace DBMapper.DBObjects
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } //DateTime? //Just use strings (like in db) for dates and convert them only if needed on the server?
        public DateTime Required { get; set; }
        public DateTime Shipped { get; set; }
        public float Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipCity { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
    }
}
