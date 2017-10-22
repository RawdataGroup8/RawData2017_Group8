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
        public string CategoryName { get; set; } // Needed to pass webservice tests
        public Category Category { get; set; } //Needed to pass db tests

        public ProductDTO(string pName, double pUnitPrice, Category pCategory)
        {
            Name = pName;
            UnitPrice = pUnitPrice;
            Category = pCategory;
            CategoryName = Category.Name;
        }
    }
}