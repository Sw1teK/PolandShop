using System;
using System.Net.Http.Headers;
using System.Threading.Channels;
using LearningCode.Products;

namespace LearningCode
{
    public class Menu
    {
        private readonly Warehouse _warehouse;
        private readonly Cart.Cart _cart;

        public Menu(Warehouse warehouse, Cart.Cart cart)
        {
            _warehouse = warehouse;
            _cart = cart;
        }
        
        
        public static void  DecorMenu()
        {

        }

        public void CaseMenu()
        {
            bool menu = true;
            while (menu)
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
                }
                Console.Clear();
                switch (menuNumber)
                {
                    case 1:
                        _warehouse.ShowProducts();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 2:
                        Console.WriteLine("1 - Добавить товар в каталог.\n2 - Удалить товар из каталога.");
                        string catalogChoice = Console.ReadLine();
                        int.TryParse(catalogChoice, out int catalogNumber);
                        if (catalogNumber != 1 && catalogNumber != 2)
                        {
                            Console.WriteLine("Некорректные данные, попробуйте еще раз ");
                        }
                        switch (catalogNumber)
                        {
                            case 1:
                                Console.Write("Введите ID товара: ");
                                if (!int.TryParse(Console.ReadLine(), out int id))
                                {
                                    Console.WriteLine("Некорректный ID.");
                                    break;
                                }

                                Console.Write("Введите название товара: ");
                                string? name = Console.ReadLine();
                                
                                if (string.IsNullOrWhiteSpace(name))
                                {
                                    Console.WriteLine("Название товара не может быть пустым.");
                                    break; 
                                }

                                Console.Write("Введите стоимость товара: ");
                                if (!int.TryParse(Console.ReadLine(), out int price))
                                {
                                    Console.WriteLine("Некорректная стоимость.");
                                    break;
                                }
                                Console.WriteLine("Выберите тип товара:");
                                foreach (var type in Enum.GetValues(typeof(ProductType)))
                                {
                                    Console.WriteLine($"{type as int?} - {type}");
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
                                                return;
                                        }
                                        
                                        _warehouse.AddProduct(product);
                                        _warehouse.UpdateStock(productType, product, quantity);
                                        Console.WriteLine("Товар добавлен.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Некорректное количество.");
                                    }
                                }
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            case 2 :
                                Console.Write("Введите товар, который хотите удалить");
                                string? productName = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(productName))
                                {
                                    Console.WriteLine("Введите название товара, поле не может быть пустым.");
                                    break;
                                }
                                _warehouse.RemoveProduct(productName);
                                Console.ReadLine();
                                Console.Clear();
                                break;
                        }
                        break;
                    case 3:
                        Console.WriteLine("1 - Добавить товар в корзину.\n2 - Удалить товар из корзину.\n3 - Товары в корзине");
                        string cartChoice = Console.ReadLine();
                        int.TryParse(cartChoice, out int cartNumber);
                        if (cartNumber <=0 && cartNumber > 3)
                        {
                            Console.WriteLine("Некорректные данные, попробуйте еще раз ");
                        }
                        switch (cartNumber)
                        {
                            case 1:
                                
                                Console.WriteLine("Выберите тип товара:");
                                foreach (var type in Enum.GetValues(typeof(ProductType)))
                                {
                                    Console.WriteLine($"{type as int?} - {type}");
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
                                    break;
                                }
                                Console.Write("Введите название товара: ");
                                string? name = Console.ReadLine();
                               
                                if (string.IsNullOrWhiteSpace(name))
                                {
                                    Console.WriteLine("Название товара не может быть пустым.");
                                    break; 
                                }

                                var addProduct = _warehouse.FindProductByName(name);
                                if (addProduct == null)
                                {
                                    Console.WriteLine("Такой товар не найден!");
                                    break;
                                }
                                Console.Write("Введите количество товара: ");
                                if (int.TryParse(Console.ReadLine(), out int num)&& num > 0)
                                {
                                    _cart.AddToCart(addProduct, num, productType);
                                }
                                else
                                {
                                    Console.WriteLine("Некорректное количество.");
                                }
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            case 2:
                                Console.WriteLine("Выберите тип товара:");
                                foreach (var type in Enum.GetValues(typeof(ProductType)))
                                {
                                    Console.WriteLine($"- {type}");
                                }
                                string? removeTypeInput = Console.ReadLine();

                                if (!Enum.TryParse(removeTypeInput, out ProductType removeProductType))
                                {
                                    Console.WriteLine("Введен не верный тип товара");
                                }
                                Console.Write("Введите название товара: ");
                                string? productName = Console.ReadLine();
                               
                                if (string.IsNullOrWhiteSpace(productName))
                                {
                                    Console.WriteLine("Название товара не может быть пустым.");
                                    break; 
                                }

                                var product = _warehouse.FindProductByName(productName);
                                if (product == null)
                                {
                                    Console.WriteLine("Такой товар не найден!");
                                    break;
                                }
                                Console.Write("Введите количество товара: ");
                                if (int.TryParse(Console.ReadLine(), out int quantity)&& quantity > 0)
                                {
                                    _cart.RemoveFromCart(product,removeProductType , quantity);
                                }
                                else
                                {
                                    Console.WriteLine("Некорректное количество.");
                                }
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            case 3:
                                _cart.ViewCart();
                                Console.ReadLine();
                                Console.Clear();
                                break;
                        }
                        break;
                    case 4:
                        _cart.Checkout();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 5:
                        _cart.ViewOrderHistory();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 6:
                        menu = false;
                        break;
                }
            }
        }
        
    }
}