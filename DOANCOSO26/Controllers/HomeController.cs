using DOANCOSO26.Data;
using DOANCOSO26.Models;
using DOANCOSO26.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DOANCOSO26.Controllers
{

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ISeatRepository _seatRepository;
        private readonly IBusRouteRepository _routeRepository;
        private readonly IStopRepository _stopRepository;
        private readonly IBusTripRepository _bustripRepository;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IBusTripRepository bustripRepository, ISeatRepository seatRepository, IStopRepository stopRepository, ApplicationDbContext context ,IBusRouteRepository routeRepository, UserManager<ApplicationUser> userManager
            )
        {
            _seatRepository = seatRepository;
            _routeRepository = routeRepository;
            _stopRepository = stopRepository;
            _bustripRepository = bustripRepository;
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }

        public async Task<IActionResult> Search(string Start, string End, DateTime? DepartureDate, int? SeatsRequired)
        {
            ViewBag.StartLocations = await _routeRepository.GetAllStartLocationsAsync();
            ViewBag.EndLocations = await _routeRepository.GetAllEndLocationsAsync();

            var trips = await _bustripRepository.GetAllAsync();
            trips = trips.Where(t => t.GetAvailableSeats() > 0 && t.TripStatus == StatusTrip.NotYetDeparted).ToList();

            if (!string.IsNullOrEmpty(Start))
            {
                trips = trips.Where(t => t.BusRoute.Start.Contains(Start, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(End))
            {
                trips = trips.Where(t => t.BusRoute.End.Contains(End, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (DepartureDate.HasValue)
            {
                trips = trips.Where(t => t.DepartureDate.Date == DepartureDate.Value.Date).ToList();
            }

            if (SeatsRequired.HasValue)
            {
                trips = trips.Where(t => t.GetAvailableSeats() >= SeatsRequired.Value).ToList();
            }

            // Sorting trips by DepartureDate in ascending order
            trips = trips.OrderBy(t => t.DepartureTime).ToList();

            foreach (var trip in trips)
            {
                trip.Seats = _seatRepository.GetSeatsByBusTripId(trip.Id).ToList();
            }

            ViewBag.SearchResults = trips;
            return View("SearchAndResult");
        }


        public async Task<IActionResult> SearchAndResult()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {

     
            var route = await _routeRepository.GetAllAsync();

            ViewBag.StartLocations = await _routeRepository.GetAllStartLocationsAsync();
            ViewBag.EndLocations = await _routeRepository.GetAllEndLocationsAsync();

            return View(route);
        }
        
        public  IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Privacy()
        {
            return View();
        }

    }
}
