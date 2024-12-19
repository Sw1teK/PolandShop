using LearningCode.Products;

namespace LearningCode;

public class StockInitialization
{
    public static readonly Dictionary<ProductType, Dictionary<string, int>> PredefinedStock = new()
    {
        [ProductType.Vegetables] = new()
        {
            { "Яблоко", 40 },
            { "Дынька", 20 }
        },
        [ProductType.Phone] = new()
        {
            { "Iphone 14PRO", 10 },
            { "Iphone 13MINI", 1 }
        },
        [ProductType.Clothes] = new()
        {
            { "Джинсовые шорты по колено", 8 }
        }
    };
}