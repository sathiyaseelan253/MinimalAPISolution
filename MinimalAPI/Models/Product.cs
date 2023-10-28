namespace MinimalAPI.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }

        public override string ToString()
        {
            return $"Product ID: {ProductID}, Product Name: {ProductName}";
        }
    }
}
