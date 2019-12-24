namespace TestProject
{
    public class Product
    {
        public string Name { get; }
        public string Color { get; }
        public double Price { get; }

        public Product(string productName, string productColor, double productPrice)
        {
            Name = productName;
            Color = productColor;
            Price = productPrice;
        }
    }
}