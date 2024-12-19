using LearningCode.Products;

namespace LearningCode;

public class RefactoringCartMenu
{
    private readonly Warehouse _warehouse;
    private readonly Cart.Cart _cart;
    public RefactoringCartMenu(Warehouse warehouse, Cart.Cart cart)
    {
        _warehouse = warehouse;
        _cart = cart;
        
    }
    
     public int CartMenu()
    {
        Console.WriteLine("1 - Добавить товар в корзину.\n2 - Удалить товар из корзину.\n3 - Товары в корзине");
        string cartChoice = Console.ReadLine();
        int.TryParse(cartChoice, out int cartNumber);
        if (cartNumber <=0 && cartNumber > 3)
        {
            Console.WriteLine("Некорректные данные, попробуйте еще раз ");
        }

        return cartNumber;
    }

    public int AddProductToCartMenu()
    {
         Console.WriteLine("Выберите тип товара:");
         foreach (var type in Enum.GetValues(typeof(ProductType)))
         {
             Console.WriteLine($"{(int) type} - {type}");
         }

         ProductType productType = (ProductType) 0;
         int? typeInput = Convert.ToInt16(Console.ReadLine());
         if (Enum.IsDefined(typeof(ProductType), typeInput))
         { 
             productType = (ProductType)typeInput;
         }
         else
         {
             Console.WriteLine("Неверный тип товара!");
             return 0;
         }
         Console.Write("Введите название товара: ");
         string? name = Console.ReadLine();
                               
         if (string.IsNullOrWhiteSpace(name))
         {
             Console.WriteLine("Название товара не может быть пустым.");
             Console.ReadLine();
             Console.Clear();
             return 0; 
         }

         var addProduct = _warehouse.FindProductByName(name);
         if (addProduct == null)
         {
             Console.WriteLine("Такой товар не найден!");
             Console.ReadLine();
             Console.Clear();
             return 0;
         }
         Console.Write("Введите количество товара: ");
         if (int.TryParse(Console.ReadLine(), out int num)&& num > 0)
         {
             _cart.AddToCart(addProduct, num, productType);
         }
         else
         {
             Console.WriteLine("Некорректное количество.");
             Console.ReadLine();
             Console.Clear();
         }
         Console.ReadLine();
         Console.Clear();
         return 0;
    }

    public int RemoveProductFromCartMenu()
    {
         Console.WriteLine("Выберите тип товара:");
         foreach (var type in Enum.GetValues(typeof(ProductType)))
         {
             Console.WriteLine($"- {type}");
         }
         string? removeTypeInput = Console.ReadLine();

         if (!Enum.TryParse(removeTypeInput, out ProductType removeProductType))
         {
             Console.WriteLine("Введен не верный тип товара");
             Console.ReadLine();
             Console.Clear();
         }
         Console.Write("Введите название товара: ");
         string? productName = Console.ReadLine();
                               
         if (string.IsNullOrWhiteSpace(productName))
         {
             Console.WriteLine("Название товара не может быть пустым.");
             Console.ReadLine();
             Console.Clear();
             return 0; 
         }

         var product = _warehouse.FindProductByName(productName);
         if (product == null)
         {
             Console.WriteLine("Такой товар не найден!");
             Console.ReadLine();
             Console.Clear();
             return 0;
         }
         Console.Write("Введите количество товара: ");
         if (int.TryParse(Console.ReadLine(), out int quantity)&& quantity > 0)
         {
             _cart.RemoveFromCart(product,removeProductType , quantity);
         }
         else
         {
             Console.WriteLine("Некорректное количество.");
             Console.ReadLine();
             Console.Clear();
         }
         Console.ReadLine();
         Console.Clear();
         return 0;
    }
}