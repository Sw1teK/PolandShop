namespace LearningCode.Products
{
    public class Vegetables : Product
    {
        public Vegetables(string name, int id, int price) : base(name,  id, price){}
        
        public  ProductType ProductType = ProductType.Vegetables;
    }
}