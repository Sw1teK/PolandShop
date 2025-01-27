using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using LearningCode.HistoryOrder;
using LearningCode.Products;

namespace LearningCode.Cart
{
    public class Cart : ICart
    {
        private List<(Product product, int quantity)> cartProducts = new List<(Product product, int quantity)>();
        
        private List<Order> orderHistory = new List<Order>();
        
        private readonly Warehouse _warehouse;

        public Cart(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }
        public void AddToCart(Product product, int quantity, ProductType productType)
        {
            var availableQuantity = _warehouse.GetAvailebleQuantity(product.Id);
            if (availableQuantity < quantity)
            {
                Console.WriteLine("Ты долбаёб, посмотри сколько товара доступно.");
                return;
            }

            var availableProduct = cartProducts.FirstOrDefault(c => c.product == product);
            if (availableProduct != default)
            {
                var index = cartProducts.FindIndex(c => c.product == product);
                if (index >= 0)
                {
                    var (existingProduct, existingQuantity) =
                        cartProducts
                            [index]; // в cartProduct  два параметра, присваеваем один параметр existingProduct, а второй existingQauntity
                    cartProducts[index] =
                        (product, existingQuantity + quantity); // Если продукт уже есть в корзине добавляем к нему новое число продуктов
                }
            }
            else
            {
                cartProducts.Add((product, quantity)); // Если нет такого продукта в корзине
            }
            _warehouse.DecreaseStock(product.Id, quantity);
        }
        
        public void RemoveFromCart(Product product, ProductType productType, int quantityToRemove)
        {
            var productInCart = cartProducts.FirstOrDefault(c => c.product == product);
            if (productInCart == default)
            {
                Console.WriteLine("Товара нет!");
                return;
            }
            
            int quantityInCart = productInCart.quantity; // Сколько товара в корзине
            if (quantityInCart < quantityToRemove) // Проверка: Есть ли столько товара в корзине чтобы его удалить
            {
                Console.WriteLine($"у вас в корзине только {quantityInCart} товаров!");
                return;
            }

            int totalQuantity = quantityInCart - quantityToRemove;
            if (totalQuantity > 0)
            {
                var index = cartProducts.FindIndex(c => c.product == product);
                cartProducts[index] = (product, totalQuantity);
            }
            else
            {
                cartProducts.Remove((product, quantityToRemove));
            }

            _warehouse.IncreaseStock(product.Id, quantityToRemove);
        }

        public void ViewCart()
        {
            if (!cartProducts.Any())
            {
                Console.WriteLine("Корзина пуста.");
                return;
            }

            foreach (var (product, quantity) in cartProducts)
            {
                Console.WriteLine($"{product.Name}, {quantity} - шт");
            }
        }

        public void Checkout()
        {
            if (!cartProducts.Any())
            {
                Console.WriteLine("Корзина пуста.");
                return;
            }

            int totalCost = 0;
            Console.WriteLine("Вы выбрали такие товары.");
            foreach (var (productCheckout, existingQuantity) in cartProducts)
            {
                Console.WriteLine($"{productCheckout.Name} - {existingQuantity} штук.");
            }

            foreach (var (productCheckout, existingQuantity) in cartProducts)
            {
                totalCost += productCheckout.Price * existingQuantity;
            }

            Console.WriteLine($"Итоговая стоимость составила: {totalCost} руб.");
            orderHistory.Add(new Order
            {
                TotalCost = totalCost,
                Products = new List<(Product product, int quantity)>(cartProducts)
            });
            cartProducts.Clear();
        }

        public void ViewOrderHistory()
        {
            if (!orderHistory.Any())
            {
                Console.WriteLine("Купи что-нибудь, Чмо(Скризя)!");
                return;
            }
            int a = 1;
            foreach (var history in orderHistory)
            {
                Console.WriteLine($"Покупка {a}");
                a++;
                foreach (var (product, quantity) in history.Products)
                {
                    Console.WriteLine(product.Name, quantity);
                }
                Console.WriteLine(history.TotalCost);
            }
        }
    }
}    