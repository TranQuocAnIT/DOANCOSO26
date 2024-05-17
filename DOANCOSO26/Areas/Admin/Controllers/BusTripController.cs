using DOANCOSO26.Data;
using DOANCOSO26.Models;
using DOANCOSO26.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace DOANCOSO26.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BusTripController : Controller
    {
        
      
            private readonly IBusTripRepository _busTripRepository;
            private readonly IBusRepository _busRepository;
            private readonly ApplicationDbContext _context;

            public BusTripController(IBusTripRepository busTripRepository, IBusRepository BusRepository, ApplicationDbContext context)
            {
                _busTripRepository = busTripRepository;
                _busRepository = BusRepository;
                _context = context;
            }


            // Hiển thị danh sách sản phẩm
            public async Task<IActionResult> Index()
            {
                var busTrips = await _busTripRepository.GetAllAsync();
                return View(busTrips);
            }
            // Hiển thị form thêm sản phẩm mới
            public async Task<IActionResult> Add()
            {
                var buses = await _busRepository.GetAllAsync();
                ViewBag.Buses = new SelectList(buses, "Id", "BusNumber");
                return View();
            }


            // Xử lý thêm sản phẩm mới
            [HttpPost]
        public async Task<IActionResult> Add(BusTrip bustrip, IFormFile imageUrl, List<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    // Lưu hình ảnh đại diện tham khảo bài 02 hàm SaveImage
                    bustrip.ImageUrl = await SaveImage(imageUrl);
                }
                if (images != null)
                {
                    bustrip.Images = new List<BusTripImage>();
                    foreach (var item in images)
                    {
                        ProductImage image = new ProductImage()
                        {
                            ProductId = product.Id,
                            Url = await SaveImage(item)
                        };
                        product.Images.Add(image);
                    }
                }
                await _productRepository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }


        // Viết thêm hàm SaveImage (tham khảo bào 02)
        private async Task<string> SaveImage(IFormFile imageUrl)
        {
            var savePath = Path.Combine("wwwroot/images", imageUrl.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await imageUrl.CopyToAsync(fileStream);
            }
            return "/images/" + imageUrl.FileName;
        }


        // Viết thêm hàm SaveImage (tham khảo bào 02)
        



        // Hiển thị thông tin chi tiết sản phẩm
        public async Task<IActionResult> Display(int id)
            {
            var buses = await _busRepository.GetAllAsync();
                var busTrip = await _busTripRepository.GetByIdAsync(id);
                if (busTrip == null)
                {
                    return NotFound();
                }
                ViewBag.lsImage = _context.BusTripImages.Where(x => x.BusTripId == id).ToList();
                ViewBag.Categories = new SelectList(buses, "Id", "BusNumber", busTrip.BusId);
            return View(busTrip);
            }
            // Hiển thị form cập nhật sản phẩm
            public async Task<IActionResult> Update(int id)
            {
                var busTrip = await _busTripRepository.GetByIdAsync(id);
                if (busTrip == null)
                {
                    return NotFound();
                }
                var buses = await _busRepository.GetAllAsync();
                ViewBag.Categories = new SelectList(buses, "Id", "BusNumber", busTrip.BusId);
                return View(busTrip);
            }
            // Xử lý cập nhật sản phẩm
            [HttpPost]
            public async Task<IActionResult> Update(int id, BusTrip busTrip, IFormFile imageUrl)
            {
                ModelState.Remove("ImageUrl"); // Loại bỏ xác thực ModelState cho ImageUrl
                if (id != busTrip.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var existingbusTrip = await _busTripRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync


                    // Giữ nguyên thông tin hình ảnh nếu không có hình mới được tải lên
                    if (imageUrl == null)
                    {
                        busTrip.ImageUrl = existingbusTrip.ImageUrl;
                    }
                    else
                    {
                        // Lưu hình ảnh mới
                        busTrip.ImageUrl = await SaveImage(imageUrl);
                    }
                // Cập nhật các thông tin khác của sản phẩm
                existingbusTrip.Name = busTrip.Name;
                existingbusTrip.StartLocation = busTrip.StartLocation;
                    existingbusTrip.EndLocation = busTrip.EndLocation;
                    existingbusTrip.DepartureTime = busTrip.DepartureTime;
                    existingbusTrip.DepartureDate = busTrip.DepartureDate;
                    existingbusTrip.ImageUrl = busTrip.ImageUrl;
                   


                await _busTripRepository.UpdateAsync(existingbusTrip);
                    return RedirectToAction(nameof(Index));
                }
                var categories = await _busRepository.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(busTrip);
            }


            // Hiển thị form xác nhận xóa sản phẩm
            public async Task<IActionResult> Delete(int id)
            {
                var busTrip = await _busTripRepository.GetByIdAsync(id);
                if (busTrip == null)
                {
                    return NotFound();
                }
                return View(busTrip);
            }


            // Xử lý xóa sản phẩm
            [HttpPost, ActionName("Delete")]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _busTripRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
        }
}
