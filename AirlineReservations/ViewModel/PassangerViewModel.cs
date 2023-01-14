#nullable disable
namespace AirlineReservations.ViewModel
{
    public class PassangerViewModel
    {

        public string PassengerName { get; set; }

        public string PassengerPhone { get; set; }

        public string PassengerAdress { get; set; }

        public string Status { get; set; }

        public string  AirlineName { get; set; }
        public string AirlineClassType { get; set; }
      
        public int TicketPrice { get; set; }

        public string TicketFrom { get; set; }

        public string TicketTo { get; set; }
        public DateTime TicketTimeTo { get; set; }

        public int BookingDetialsId { get; set; }

       
    }
}
