
using System.Collections.Generic;

namespace fss.ServerlessEats
{

     public class OpenHour
    {
        public string Day { get; set; }
        public string open { get; set; }
        public string close { get; set; }
    }

    public class RestaurantDetail
    {
        public string name { get; set; }
        public string Cuisine { get; set; }
        public List<OpenHour> openHours { get; set; }
        public List<Menu> menu { get; set; }
    }

    public class FoodItem
    {
        public string Name { get; set; }
        public string Desciption { get; set; }
        public double price { get; set; }
    }

    public class Menu
    {
        public string name { get; set; }
        public string description { get; set; }
        public List<FoodItem> items { get; set; }
    }

    public class Restaurant
    {
        public string id { get; set; }
        public RestaurantDetail restaurant { get; set; }
    }


}