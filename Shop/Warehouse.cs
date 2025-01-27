using System;
using System.Collections.Generic;
using System.Threading.Channels;
using Dapper;
using LearningCode.Products;
using Microsoft.Data.SqlClient;

namespace LearningCode
{
    public class Warehouse
    {
        private readonly string _connectionString;
        private readonly List<Product> _products;
        private readonly Dictionary<int, int> _stock;
        private readonly Dictionary<int, string> _productTypes;

        public Warehouse(string connectionString)
        {
            _connectionString = connectionString;
            _products = LoadProducts();
            _productTypes = LoadProductTypes();
            _stock = LoadStock();
        }

        private List<Product> LoadProducts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                SELECT p.ProductID AS Id, p.ProductTypeID AS ProductType, p.Name, p.Price
                FROM Product p";

                return connection.Query<Product>(query).ToList();
            }
        }

        private Dictionary<int, string> LoadProductTypes()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                SELECT ProductTypeID, Name FROM ProductType";
             

                return connection.Query(query).ToDictionary(row => (int)row.ProductTypeID, row => (string)row.Name);
            }
        }

        private Dictionary<int, int> LoadStock()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                SELECT ProductID, Quantity FROM Stock";
             

                return connection.Query(query).ToDictionary(row => (int)row.ProductID, row => (int)row.Quantity);
            }
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

        public void AddProduct(Product product, int quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string productQuery = @"
                INSERT INTO Product (ProductTypeID, Name, Price)
                VALUES (@ProductTypeID, @Name, @Price)
                SELECT CAST(SCOPE_IDENTITY() as int)";

                int productId = connection.QuerySingle<int>(productQuery, new 
                {
                    ProductTypeID = (int)product.ProductType,
                    product.Name,
                    product.Price
                });

                string stockQuery = @"
                INSERT INTO Stock (ProductID, Quantity)
                VALUES (@ProductID, @Quantity)";
            
                connection.Execute(stockQuery, new { ProductID = productId, Quantity = quantity });
                
                _products.Add(product);
            
                _stock[productId] = quantity;
            
            }
            
        }

        public void RemoveProduct(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                DELETE FROM Stock WHERE ProductID = @ProductID;
                DELETE FROM Product WHERE ProductID = @ProductID";
                
                connection.Execute(query, new { ProductID = productId });
            }
            
            var productToRemove = _products.FirstOrDefault(p => p.Id == productId);
            if (productToRemove != null)
            {
                _products.Remove(productToRemove);
                Console.WriteLine($"{productToRemove} удален из каталога");
                _stock.Remove(productId);
            }
            else
            {
                Console.WriteLine("Товар не найден");
            }
        }

        public int GetAvailebleQuantity(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                SELECT Quantity FROM Stock WHERE ProductID = @ProductID";

                int availebleQuantity = connection.QuerySingleOrDefault<int>(query, new { ProductId = productId });
                return availebleQuantity > 0 ? availebleQuantity : 0;
            }
        }

        public void DecreaseStock(int productId, int quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                UPDATE Stock SET Quantity = Quantity - @Quantity WHERE ProductID = @ProductID";
                connection.Execute(query, new { ProductID = productId, Quantity = quantity });
            }

            if (_stock.ContainsKey(productId))
            {
                _stock[productId] = Math.Max(0, _stock[productId] - quantity);
            }
        }

        public void IncreaseStock(int productId, int quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                UPDATE Stock SET Quantity = Quantity + @Quantity WHERE ProductID = @ProductID";
                connection.Execute(query, new { ProductID = productId, Quantity = quantity });
            }

            if (_stock.ContainsKey(productId))
            {
                _stock[productId] += quantity;
            }
        }

        public void UpdateStock(Product product, int quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                SELECT COUNT(*) FROM Stock WHERE ProductID = @ProductID ";
                int count = connection.QuerySingleOrDefault<int>(query, new { ProductID = product.Id });
                if (count > 0)
                {
                    string updateQuery = @"UPDATE Stock SET Quantity = @Quantity WHERE ProductID = @ProductID";
                    connection.Execute(updateQuery, new { ProductID = product.Id, Quantity = quantity });
                }
                else
                {
                    string insertQuery = @"INSERT INTO Stock (ProductID, Quantity) VALUES (@ProductID, @Quantity)";
                    connection.Execute(insertQuery, new { ProductID = product.Id, Quantity = quantity });
                }
            }
            _stock[product.Id] = quantity;
        }

        public Product? FindProductByName(string name)
        {
            return _products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}