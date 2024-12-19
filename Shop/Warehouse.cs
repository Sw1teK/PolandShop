using System;
using System.Collections.Generic;
using System.Threading.Channels;
using LearningCode.Products;

namespace LearningCode
{
    
    public class Warehouse
    {
        private List<Product> _products = new List<Product>()
        {
            new Vegetables("Яблоко", 77, 5),
            new Vegetables("Дынька", 8, 8),
            new Phone("Iphone 14PRO", 999, 2100),
            new Phone("Iphone 13MINI", 666, 1666),
            new Clothes("Джинсовые шорты по колено",111,  140)
        };

        private readonly Dictionary<ProductType, Dictionary<string, int>> _stock;

        public Warehouse()
        {
            _stock = StockInitialization.PredefinedStock;
        }
        
        public void ShowProducts()
        {
            if (_products.Count == 0)
            {
                Console.WriteLine("Список товаров пуст.");
                return;
            }
            foreach (var product in _products)
            {
                Console.WriteLine($"{product.Name} стоимость товара - {product.Price}");
            }
        }
        
        public void AddProduct(Product product)
        {
            _products.Add(product);
        }
        
        public void RemoveProduct(string productName)
        {
            var productToRemove = _products.Find(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
            if (productToRemove != null)
            {
                _products.Remove(productToRemove);
                Console.WriteLine($"{productName} удален из каталога");
            }
            else
            {
                Console.WriteLine("Товар не найден");
            }
        }
        
        public int GetAvailebleQuantity(Product product, ProductType productType)
        {
            if (_stock.ContainsKey(productType))
            {
                if (_stock[productType].ContainsKey(product.Name))
                {
                    return _stock[productType][product.Name];
                }
            }
            return 0;
        }
        
        public int DecreaseStock(Product product, ProductType productType, int quantity)
        {
            if (_stock.ContainsKey(productType))
            {
                if (_stock[productType].ContainsKey(product.Name))
                {
                    int currentStock = _stock[productType][product.Name] - quantity;
                    return currentStock;
                }
            }
            return 0;
        }
        
        public int IncreaseStock(Product product, ProductType productType, int quantityToRemove)
        {
            if (_stock.ContainsKey(productType))
            {
                if (_stock[productType].ContainsKey(product.Name))
                {
                    int currentStock = _stock[productType][product.Name] + quantityToRemove;
                    return currentStock;
                }
            }
            return 0;
        }
        
        public void UpdateStock(ProductType productType, Product product, int quantity)
        {
            _stock[productType].Add(product.Name,quantity);
        }
        
        public Product? FindProductByName(string name)
        {
            return _products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}