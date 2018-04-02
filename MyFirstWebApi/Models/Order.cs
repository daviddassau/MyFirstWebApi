namespace MyFirstWebApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string TypeOfPizza { get; set; }
        public int NumberOfPizzas { get; set; }
        public double Cost { get; set; }
        public string AddressForDelivery { get; set; }
        public string NameOfCustomer { get; set; }
    }
}