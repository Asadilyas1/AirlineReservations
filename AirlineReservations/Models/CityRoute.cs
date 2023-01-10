#nullable disable
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace AirlineReservations.Models
{
    public class CityRoute
    {
        [Key]
       public int RouteId { get; set; }

        [Display(Name = "Traseu din oraș")]
        public string RouteFromCity { get; set; }


        [Display(Name = "Traseu spre oraș")]
        public string RouteToCity { get; set; }

        [Display(Name = "Numele companiei aeriene")]
        public int AirlineId { get; set; }

        [ForeignKey("AirlineId")]

        public Airline Airline { get; set; }
    }
}
