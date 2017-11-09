using System.ComponentModel.DataAnnotations;

namespace DBMapper.DBObjects
{
    public class OrderDetails
    {
        [Key]
        public int OrderId1 { get; set; }
        public Order Order { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
