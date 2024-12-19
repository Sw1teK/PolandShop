using LearningCode.Products;

namespace LearningCode
{
    
    
    public abstract class Product
    {
        public  ProductType ProductType { get; protected set; }
        
        public string Name { get; private set; }
        
        public int Id{ get; private set; }
        
        public int Price { get; private set; }
        
        public Product(string name,int id, int price)
        {
            Id = id;
            Price = price;
            Name = name;
        }
    }
}