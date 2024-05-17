using DOANCOSO26.Models;
using DOANCOSO26.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOANCOSO26.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BusController : Controller
    {
        private readonly IBusTripRepository _bustripRepository;
        private readonly IBusRepository _busRepository;
        public BusController (IBusTripRepository bustripRepository, IBusRepository busRepository)
        {
            _bustripRepository = bustripRepository;
            _busRepository = busRepository;
        }
        public async Task<IActionResult> Index()
        {
            var buses = await _busRepository.GetAllAsync();
            return View(buses);
        }
        // Hiển thị form thêm sản phẩm mới
        public IActionResult Add()
        {
            return View();
        }

        // Xử lý thêm loại mới
        [HttpPost]
        public async Task<IActionResult> Add(Bus bus)
        {
            if (ModelState.IsValid)
            {
                await _busRepository.AddAsync(bus);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập

            return View(bus);
        }
        // Hiển thị thông tin chi tiết loại
        public async Task<IActionResult> Display(int id)
        {
            var category = await _busRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _busRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Bus bus)
        {
            ModelState.Remove("ImageUrl"); // Loại bỏ xác thực ModelState cho ImageUrl
            if (id != bus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingCategory = await _busRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync


                // Giữ nguyên thông tin hình ảnh nếu không có hình mới được tải lên
                existingCategory.Company = bus.Company;
                // Cập nhật các thông tin khác của sản phẩm
                existingCategory.BusNumber = bus.BusNumber;
                existingCategory.Capacity = bus.Capacity;
              

                await _busRepository.UpdateAsync(existingCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(bus);
        }

        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _busRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _busRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
