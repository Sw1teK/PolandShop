﻿namespace LearningCode.Products
{
    public class Clothes : Product
    {
        public Clothes(string name, int id, int price) : base(name, id, price)
        {
            ProductType = ProductType.Clothes;
        }
        
        
    }
}