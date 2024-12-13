﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using LearningCode.HistoryOrder;
using LearningCode.Products;

namespace LearningCode.Cart
{
    public class Cart : ICart
    {
        public List<(Product product, int quantity)> cartProducts = new List<(Product product, int quantity)>();
        Warehouse warehouse = new Warehouse();
        public List<Order> orderHistory = new List<Order>();

        public void AddToCart(Product product, int quantity, ProductType productType)
        {
            var availableQuantity = warehouse.GetAvailebleQuantity(product, productType);
            if (availableQuantity < quantity)
            {
                Console.WriteLine("Ты долбаёб, посмотри сколько товара доступно.");
                return;
            }

            var availableProduct = cartProducts.FirstOrDefault(c => c.product == product);
            if (availableProduct != default)
            {
                int index = cartProducts.FindIndex(c => c.product == product);
                if (index >= 0)
                {
                    var (existingProduct, existingQuantity) =
                        cartProducts
                            [index]; // в cartProduct  два параметра, присваеваем один параметр existingProduct, а второй existingQauntity
                    cartProducts[index] =
                        (product,
                            existingQuantity +
                            quantity); // Если продукт уже есть в корзине добавляем к нему новое число продуктов
                }
            }
            else
            {
                cartProducts.Add((product, quantity)); // Если нет такого продукта в корзине
                warehouse.DecreaseStock(product, productType, quantity);
            }
        }

        public void RemoveFromCart(Product product, ProductType productType, int quantity)
        {
            var productInCart = cartProducts.FirstOrDefault(c => c.product == product);
            if (productInCart == default)
            {
                Console.WriteLine("Товара нет!");
                return;
            }

            int quantityToRemove = 0; // Сколько товара хочет удалить клиент
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
                cartProducts.Remove((product, quantity));
            }

            warehouse.IncreaseStock(product, productType, quantity);
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

        public void Checkout(Product product)
        {
            if (!cartProducts.Any())
            {
                Console.WriteLine("Корзина пуста.");
                return;
            }

            int totalCost = 0;

            foreach (var (product1, existingQuantity) in cartProducts)
            {
                totalCost += product.Price * existingQuantity;
            }

            orderHistory.Add(new Order
            {
                TotalCost = totalCost,
                Products = new List<(Product product, int quantity)>(cartProducts)
            });
        }

        public void ViewOrderHistory()
        {
            if (!orderHistory.Any())
            {
                Console.WriteLine("Купи что-нибудь, Чмо(Скризя)!");
                return;
            }

            foreach (var history in orderHistory)
            {
                foreach (var (product, quantity) in history.Products)
                {
                    Console.WriteLine(product.Name, quantity);
                }

                Console.WriteLine(history.TotalCost);
            }

        }
    }
}    