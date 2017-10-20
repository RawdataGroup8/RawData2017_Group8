namespace DBMapper.DBObjects
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string QuantityPerUnit { get; set; }
        public int UnitsInStock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class ProductDTO
    {
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public Category Category { get; set; }

        public ProductDTO(string pName, double pUnitPrice, Category pCategory)
        {
            Name = pName;
            UnitPrice = pUnitPrice;
            Category = pCategory;
        }
    }
}