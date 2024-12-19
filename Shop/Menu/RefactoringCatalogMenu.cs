using LearningCode.Products;

namespace LearningCode;

public class RefactoringCatalogMenu
{
    private readonly Warehouse _warehouse;
    public RefactoringCatalogMenu(Warehouse warehouse)
    {
        _warehouse = warehouse;
    }
    public int CatalogMenu()
    {
        Console.WriteLine("1 - Добавить товар в каталог.\n2 - Удалить товар из каталога.");
        string catalogChoice = Console.ReadLine();
        int.TryParse(catalogChoice, out int catalogNumber);
        if (catalogNumber != 1 && catalogNumber != 2)
        {
            Console.WriteLine("Некорректные данные, попробуйте еще раз ");
            return 0;
        }
        return catalogNumber;
    }

    public int AddProdutToCatalogMenu()
    {
         Console.Write("Введите ID товара: ");
         if (!int.TryParse(Console.ReadLine(), out int id))
         {
             Console.WriteLine("Некорректный ID.");
             Console.ReadLine();
             Console.Clear();
             return 0;
         }

         Console.Write("Введите название товара: ");
         string? name = Console.ReadLine();
                                
         if (string.IsNullOrWhiteSpace(name))
         {
             Console.WriteLine("Название товара не может быть пустым.");
             Console.ReadLine();
             Console.Clear();
             return 0 ;
         }

         Console.Write("Введите стоимость товара: ");
         if (!int.TryParse(Console.ReadLine(), out int price))
         {
             Console.WriteLine("Некорректная стоимость.");
             Console.ReadLine();
             Console.Clear();
             return 0;
         }
         Console.WriteLine("Выберите тип товара:");
         foreach (var type in Enum.GetValues(typeof(ProductType)))
         {
             Console.WriteLine($"{(int)type} - {type}");
         }

         ProductType productType = (ProductType) 0;
         int? typeInput = Convert.ToInt16(Console.ReadLine());
         if (Enum.IsDefined(typeof(ProductType), typeInput))
         { 
             productType = (ProductType)typeInput;
             Console.Write("Введите количество товара: ");
             if (int.TryParse(Console.ReadLine(), out int quantity))
             {
                 Product product;

                 switch (productType)
                 {
                     case ProductType.Vegetables:
                         product = new Vegetables(name,id , price);
                         break;
                     case ProductType.Phone:
                         product = new Phone(name, id, price);
                         break;
                     case ProductType.Clothes:
                         product = new Clothes(name, id, price);
                         break;
                     default:
                         Console.WriteLine("Неподдерживаемый тип товара.");
                         Console.ReadLine();
                         Console.Clear();
                         return 0;
                 }
                 _warehouse.AddProduct(product);
                 _warehouse.UpdateStock(productType, product, quantity);
                 Console.WriteLine("Товар добавлен.");
             }
             else
             {
                 Console.WriteLine("Некорректное количество.");
                 Console.ReadLine();
                 Console.Clear();
             }
         }
         Console.ReadLine();
         Console.Clear();
         return 0;
    }

    public int RemoveProductFromCatalogMenu()
    {
        Console.Write("Введите товар, который хотите удалить: ");
        string? productName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(productName))
        {
            Console.WriteLine("Введите название товара, поле не может быть пустым.");
            Console.ReadLine();
            Console.Clear();
            return 0;
        }
        _warehouse.RemoveProduct(productName);
        Console.ReadLine();
        Console.Clear();
        return 0;
    }
}