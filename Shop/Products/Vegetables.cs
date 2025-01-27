namespace LearningCode.Products
{
    public class Vegetables : Product
    {
        public Vegetables(string name, int id, int price) : base(name, price, id)
        {
            ProductType = ProductType.Vegetables;
        }
        
    }
}