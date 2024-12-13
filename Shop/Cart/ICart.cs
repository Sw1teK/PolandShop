using System;
using LearningCode.Products;

namespace LearningCode.Cart
{
    public interface ICart
    {
        void AddToCart(Product product, int quantity, ProductType productType);

        void RemoveFromCart(Product product,ProductType productType, int quantity);

        void ViewCart();

        void Checkout(Product product);
    }
}