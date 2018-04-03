using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using Dapper;
using MyFirstWebApi.Models;
using MyFirstWebApi.Services;

namespace MyFirstWebApi.Controllers
{
    [RoutePrefix("orderapi/pizza")]
    public class PizzaController : ApiController
    {
        [HttpPost,Route("placeorder")]
        public HttpResponseMessage PlaceOrder(OrderDto newOrder)
        {
            var order = new Order
            {
                NumberOfPizzas = newOrder.NumberOfPizzas,
                TypeOfPizza = newOrder.TypeOfPizza,
                AddressForDelivery = newOrder.AddressForDelivery,
                NameOfCustomer = newOrder.NameOfCustomer,
                Cost = 10 * newOrder.NumberOfPizzas
            };

            var orderService = new OrderPizzaService();
            var success = orderService.OrderPizza(order);

            if (success)
                return Request.CreateResponse(HttpStatusCode.Created);
                
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Could not process your order, try again later...");       
        }

        [HttpGet,Route("")]
        public HttpResponseMessage GetAllOrders()
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["PizzaTime"].ConnectionString))
            {
                db.Open();

                var orders = db.Query<Order>("select * from Orders");

                return Request.CreateResponse(HttpStatusCode.OK, orders);
            }
        }

        [HttpGet, Route("{id}")]
        public HttpResponseMessage GetOrder(int id)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["PizzaTime"].ConnectionString))
            {
                db.Open();

                var orders = db.QueryFirst<Order>("select * from Orders where Id = @id", new {id});

                return Request.CreateResponse(HttpStatusCode.OK, orders);
            }
        }

        [HttpGet, Route("")]
        public HttpResponseMessage GetOrder(string firstLetter)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["PizzaTime"].ConnectionString))
            {
                db.Open();

                var orders = db.QueryFirst<Order>("select * " +
                                                   "from Orders " +
                                                   "where TypeOfPizza = @firstLetter", new {firstLetter});

                return Request.CreateResponse(HttpStatusCode.OK, orders);
            }
        }

    }
}
