namespace LearningCode.Products
{
    public class Phone : Product
    {
        public Phone(string name, int id, int price) : base(name, price, id)
        {
            ProductType = ProductType.Phone;
        }
        
    }
}