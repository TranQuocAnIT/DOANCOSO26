using System.ComponentModel.DataAnnotations;

namespace DOANCOSO26.Models
{
    public class BusTrip
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên chuyến đi")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa điểm xuất phát")]
        public string StartLocation { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa điểm đến")]
        public string EndLocation { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn xe")]
        public int BusId { get; set; }
        public Bus Bus { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thời gian xuất phát")]
        public string DepartureTime { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày xuất phát")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        public ICollection<Seat> Seats { get; set; }
        public ICollection<Stop> Stops { get; set; }
        public List <BusTripImage>? Images { get; set; }
    }
}