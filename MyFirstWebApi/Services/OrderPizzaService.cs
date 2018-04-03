using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFirstWebApi.Models;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace MyFirstWebApi.Services
{
    public class OrderPizzaService
    {
        public bool OrderPizza(Order order)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["PizzaTime"].ConnectionString))
            {
                db.Open();

                var numberAffected = db.Execute(@"Insert into Orders (TypeOfPizza, NumberOfPizzas, Cost, AddressForDelivery, NameOfCustomer)
                             Values(@TypeOfPizza, @NumberOfPizza, @Cost, @AddressForDelivery, @NameOfCustomer)",
                    order);

                return numberAffected == 1;
            }
        }
    }
}