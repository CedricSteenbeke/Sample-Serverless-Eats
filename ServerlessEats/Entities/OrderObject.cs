using System.Collections.Generic;

namespace fss.ServerlessEats
{
 public class Order
    {
        public string restaurantId { get; set; }
        public string restaurantName { get; set; }
        public float total { get; set; }
        public string deliveryAddress { get; set; }
        public List<OrderItem> items { get; set; }
    }

    public class OrderItem
    {
        public string name { get; set; }
        public double unitPrice { get; set; }
        public int amount { get; set;}
    }
}