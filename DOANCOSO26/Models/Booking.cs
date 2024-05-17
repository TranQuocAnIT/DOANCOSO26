using System.ComponentModel.DataAnnotations.Schema;

namespace DOANCOSO26.Models
{
    public class Booking
    {
        public int Id { get; set; }

        // Foreign key
        // Other properties
        public string UserId { get; set; }
        public DateTime Timebooking { get; set; }
        public ApplicationUser User { get; set; }
        public int SeatId { get; set; }
        public Seat Seats { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
