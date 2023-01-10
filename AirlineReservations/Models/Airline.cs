#nullable disable
using System.ComponentModel.DataAnnotations;

namespace AirlineReservations.Models
{
    //Airline table
    public class Airline
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Aeriana Code")]
        public string Airlinecode { get; set; }
        
        [Display(Name = "Aeriana Nume")]
        public string AirlineName { get; set; }
    }
}
