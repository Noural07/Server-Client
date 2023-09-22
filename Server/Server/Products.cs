using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Products
    {
        public string Name { get; set; }
        public decimal MinPrice { get; set; }

        public decimal Price { get; set; }
        public int ActionTimeInSeconds { get; set; } 
        public DateTime EndTime { get; set; }

        public Products(string name, decimal minPrice, decimal price, int actionTimeInSeconds, DateTime endTime)
        {
            Name = name;
            MinPrice = minPrice;
            Price = price;
            ActionTimeInSeconds = actionTimeInSeconds;
            EndTime = endTime;
        }


    }
}
