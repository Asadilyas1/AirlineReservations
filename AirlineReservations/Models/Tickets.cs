#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineReservations.Models
{
    //Ticket table
    public class Tickets
    {
        [Key]
        public int Id { get; set; }
        //from
        [Display(Name = "Bilet de la")]
        public string TicketFrom { get; set; }
        //to
        [Display(Name = "Bilet la")]
        public string TicletTo { get; set; }
        //time from
        [Display(Name = "timp de la")]
        public DateTime TimeFrom { get; set; }
        [Display(Name = "Timp pentru")]
        //time to
        public DateTime TimeTo { get; set; }
        //seats
        [Display(Name = "Scaune")]
        public int TotalSeata { get; set; }


        [Display(Name = "Prețul biletului dus dus")]
        public int SinglePrice { get; set; }
        [Display(Name = "Prețul biletului dus-întors")]
        public int TwoWaysPrice { get; set; }

        //forigen key relation with airline
        [Display(Name = "Selectați Companie aeriană")]
        public int AirlineID { get; set; }

        [ForeignKey("AirlineID")]
        public Airline Airline { get; set; }

        [Display(Name = "selectați tipul biletului")]
        public int ClassTypeID { get; set; }

        [ForeignKey("ClassTypeID")]
        public BookingClass bookingClass { get; set; }

        
    }
}
