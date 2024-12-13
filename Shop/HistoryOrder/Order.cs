using System.Collections.Generic;

namespace LearningCode.HistoryOrder
{
    public class Order
    {
        public int TotalCost { get; set; }
        public List<(Product product,int quantity)> Products { get; set; }
    }
}