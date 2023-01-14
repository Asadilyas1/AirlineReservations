#nullable disable
namespace AirlineReservations.ViewModel
{
    public class UserOrderViewModel
    {
        public string UserName { get; set; }
        public string CityFrom { get; set; }

        public string cityTo { get; set; }

        public string phoneNumber { get; set; }

        public string AirlineName { get; set; }

        public string Status { get; set; }
        public int TicketPrice { get; set; }

        public string TicketFrom { get; set; }

        public string TicketTo { get; set; }
        public DateTime TicketTimeTo { get; set; }
        public int OrderId { get; set; }

    }
}
