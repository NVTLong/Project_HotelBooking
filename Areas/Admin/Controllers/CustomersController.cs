using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.DTOs.Customer;
using Project_HotelBooking.Application.DTOs.Room;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Application.Services;
using Project_HotelBooking.ViewModels.Customer;
using Project_HotelBooking.ViewModels.Room;
using System.Threading.Tasks;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task<IActionResult> Index()
        {
            var cus = await _customerService.GetAllAsync();
            return View(cus);
        }

        public async Task<IActionResult> GetById(int id)
        {
            var cus = await _customerService.GetByIdAsync(id);
            if (cus == null)
            {
                return NotFound();
            }
            return Json(new
            {
                id = cus.Id,
                fullName = cus.FullName,
                phoneNumber = cus.PhoneNumber,
                email = cus.Email,
                address = cus.Address,
                identityNumber = cus.IdentityNumber,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerVM vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var dto = new CustomerCreateDto
            {
                FullName = vm.FullName,
                PhoneNumber = vm.PhoneNumber,
                Email = vm.Email,
                Address = vm.Address,
                IdentityNumber = vm.IdentityNumber,
            };
            var userIdCreate = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );
            await _customerService.CreateAsync(dto, userIdCreate);

            return Json(new { status = 200, message = "Tạo thành công" });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CustomerVM model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var dto = new CustomerUpdateDto
            {
                Id = model.Id,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Address = model.Address,
                IdentityNumber = model.IdentityNumber,
            };

            var userId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );

            await _customerService.UpdateAsync(dto, userId);

            return Json(new { status = 200, message = "Cập nhật thành công" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var floor = await _customerService.GetByIdAsync(id);

            if (floor == null)
            {
                return NotFound(new { message = "Không tìm thấy dữ liệu" });
            }

            await _customerService.DeleteAsync(id);

            return Ok(new { massage = "Xóa thành công" });
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetAllAsync();

            return Json(new
            {
                status = 200,
                resources = customers.Select(c => new
                {
                    value = c.Id,
                    text = c.FullName
                })
            });
        }

    }
}
