using System.ComponentModel.DataAnnotations;

namespace DBMapper.DBObjects
{
    public class OrderDetails
    {
        [Key]
        public int OrderId { get; set; }
        public Order Order { get; set; }//both id and order?
        //public string Name { get; set; }
        public float UnitPrice { get; set; }
        public float Discount { get; set; }
        public Product Product { get; set; }//both id and product?
        public int ProductId { get; set; }
        public float Quantity { get; set; }
    }
}
