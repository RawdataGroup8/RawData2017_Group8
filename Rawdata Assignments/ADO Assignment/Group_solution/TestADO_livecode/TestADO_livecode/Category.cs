namespace DBMapper
{
    public class Category
    {
        //[Column("categoryid")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class Product
    {
        //[Column("categoryid")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SupplierId { get; set; }
        public string CategoryId { get; set; }
        public string QuantityUnit { get; set; }
        public string UnitPrice { get; set; }
        public string UnitsInStock { get; set; }

    }
}
