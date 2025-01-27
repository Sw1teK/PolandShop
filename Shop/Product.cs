using LearningCode.Products;

namespace LearningCode
{
    
    
    public class Product
    {
        public  ProductType ProductType { get; protected set; }
        
        public string Name { get; private set; }
        
        public int Id{ get; private set; }
        
        public int Price { get; private set; }
        
        public Product(string name, int price, int id)
        {
            Name = name;
            Price = price;
            Id = id;
        }

        public Product()
        {
            
        }
    }
}