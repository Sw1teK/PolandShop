using LearningCode.Products;

namespace LearningCode;

public class RefactoringMainMenu
{
    private readonly Warehouse _warehouse;
    private readonly Cart.Cart _cart;

    public RefactoringMainMenu(Warehouse warehouse, Cart.Cart cart)
    {
        _warehouse = warehouse;
        _cart = cart;
    }
    public int MainMenu()
    {
        Console.WriteLine("Добро пожаловать в наш магазин!");
        Console.SetCursorPosition(0, 4);
        Console.WriteLine("1 - Просмотр каталога товаров.\n2 - Управление каталогом.");
        Console.WriteLine("3 - Работа с корзиной.\n4 - Оформление покупки.");
        Console.WriteLine("5 - Просмотр истории заказов.\n6 - Выйти из программы.");
        Console.SetCursorPosition(0,1);
        Console.Write("Выберите желаемое действие: ");
        string menuChoice = Console.ReadLine();
        int.TryParse(menuChoice, out int menuNumber);
        if (menuNumber <= 0 && menuNumber > 6)
        {
            Console.WriteLine("Некорректные данные, попробуйте еще раз ");
            return 0;
        }
        return menuNumber;
    }
    
   
}