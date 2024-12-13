using System;
using System.Net.Http.Headers;
using System.Threading.Channels;
using LearningCode.Products;

namespace LearningCode
{
    public class Menu
    {
        public static void  DecorMenu()
        {

        }

        public void CaseMenu()
        {
            Console.SetCursorPosition(0, 6);
            Console.WriteLine("1 - Просмотр каталога товаров.\n2 - Управление каталогом.");
            Console.WriteLine("3 - Работа с корзиной.\n4 - Оформление покупки.");
            Console.WriteLine("5 - Просмотр истории заказов.\n6 - Выйти из программы.");
            Console.SetCursorPosition(0,0);
            Console.WriteLine("Добро пожаловать в наш магазин!");
            while (true)
            {
                Console.Write("Выберите желаемое действие: ");
                string menuChoice = Console.ReadLine();
                int.TryParse(menuChoice, out int menuNumber);
                if (menuNumber <= 0 && menuNumber > 6)
                {
                    Console.WriteLine("Некорректные данные, попробуйте еще раз ");
                }
                Warehouse warehouse = new Warehouse();
                
                
                switch (menuNumber)
                {
                    case 1:
                        warehouse.ShowProducts();
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
                                    Console.WriteLine($"- {type}");
                                }
                                string? typeInput = Console.ReadLine();

                                if (Enum.TryParse(typeInput, out ProductType productType))
                                {
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

                                        warehouse.UpdateStock(productType, product, quantity);
                                        warehouse.AddProduct(product);
                                        Console.WriteLine("Товар добавлен.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Некорректное количество.");
                                    }
                                }
                                break;
                            case 2 :
                                Console.Write("Введите товар, который хотите удалить");
                                string? productName = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(productName))
                                {
                                    Console.WriteLine("Введите название товара, поле не может быть пустым.");
                                    break;
                                }
                                warehouse.RemoveProduct(productName);
                                break;;
                        }
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                }
            }
        }
        
    }
}