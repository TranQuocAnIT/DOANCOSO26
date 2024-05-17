namespace DOANCOSO26.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int BusTripId { get; set; }
        public BusTrip BusTrip { get; set; }
    }
}
