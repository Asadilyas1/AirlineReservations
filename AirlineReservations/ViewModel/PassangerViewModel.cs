#nullable disable
namespace AirlineReservations.ViewModel
{
    public class PassangerViewModel
    {
        public string  AirlineName { get; set; }
        public string AirlineClassType { get; set; }
        public string AirlineCityRouteFrom { get; set; }

        public string AirlineCityRouteTo { get; set; }

        public int TotalSeat { get; set; }

        public string AirlineCode { get; set; }

        public int TicketPrice { get; set; }

        public string TicketFrom { get; set; }

        public string TicketTo { get; set; }

        public DateTime TicketTimeFrom { get; set; }

        public DateTime TicketTimeTo { get; set; }

        public int TicketID { get; set; }

       
    }
}
