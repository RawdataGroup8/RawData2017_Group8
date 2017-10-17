namespace DBMapper.DBObjects
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitPrice{ get; set; }
        public int QuantityPerUnit { get; set; }
        public int UnitsInStock { get; set; }

        public Category Category { get; set; }
    }
}