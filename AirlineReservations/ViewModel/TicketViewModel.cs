#nullable disable
namespace AirlineReservations.ViewModel
{
    public class TicketViewModel
    {

        public string TicketFrom { get; set; }

        public string TicketTo { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public int TotalTicket { get; set; }

        public int SingleWayPrice { get; set; } 

        public int TwoWayPrice { get; set; }

        public int TicketId { get; set; }

        public string airlineName { get; set; }

        public string AirlineCode { get; set; }

        public string PassangerClass { get; set; }

    }
}
