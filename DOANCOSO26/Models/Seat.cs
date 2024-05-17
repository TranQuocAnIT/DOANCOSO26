namespace DOANCOSO26.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public int BusTripId { get; set; }
        // Navigation property
        public BusTrip BusTrip { get; set; }
        public ICollection<Booking> Bookings { get; set; } // Dan
    }
}
