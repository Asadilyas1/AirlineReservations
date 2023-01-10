#nullable disable
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineReservations.Models
{
    public class BookingMaster
    {
        public int Id { get; set; }
        //Forgien key relationship
        public int BookindDetailID { get; set; }
        [ForeignKey("BookindDetailID")]
        
        public BookingDetails bookingDetails { get; set; }

        public string UserID { get; set; }
        
    }
}
