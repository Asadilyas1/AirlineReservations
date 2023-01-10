#nullable disable
using System.ComponentModel.DataAnnotations;

namespace AirlineReservations.Models
{
    //Airline class table
    public class BookingClass
    {
        //Id
        [Key]
        public int Id { get; set; }
        //Class type
        [Display(Name = "ClasaTip")]
        public string ClassTpye { get; set; }
    }
}
