#nullable disable
namespace AirlineReservations.ViewModel
{
    public class ConfirmViewModel
    {
        public string TicketFrom { get; set; }

        public string TicketTo { get; set; }

        public int singlePrice { get; set; }

        public int TwoWayPrice { get; set; }
        
        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public string PassangerClass { get; set; }

        public int ticketid { get; set; }

        public string RouteFromCity { get; set; }
        public string RoutetoCity { get; set; }
       
    }
}
