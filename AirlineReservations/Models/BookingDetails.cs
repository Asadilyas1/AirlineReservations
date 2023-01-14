#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineReservations.Models
{
    public class BookingDetails
    {
        [Key]
        public int BookingID { get; set; }

        [Display(Name = "din")]
        public string From { get; set; }
        [Display(Name = "la")]
        public string To { get; set; }
        [Display(Name = "Timp de plecare")]
        public DateTime DepartureTime { get; set; }
        [Display(Name = "Ora de întoarcere")]
        public DateTime ReturnTime { get; set; }
        [Display(Name = "scaune")]
        public int Seats { get; set; }
      


        [Display(Name = "Prețul biletului")]
        public int TicketsId { get; set; }
        [ForeignKey("TicketsId")]
        public Tickets Tickets { get; set; }


        public string userId { get; set; }

        public int TicketPrice { get; set; }

        public string TicketStatus { get; set; }

        public int RouteId { get; set; }
    }
}
