using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstWebApi.Models
{
    public class OrderDto
    {
        public string TypeOfPizza { get; set; }
        public int NumberOfPizzas { get; set; }
        public string AddressForDelivery { get; set; }
        public string NameOfCustomer { get; set; }
    }
}