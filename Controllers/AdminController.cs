using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
using _2051010166_NguyenTranThanhLiem.Repositories;
using _2051010166_NguyenTranThanhLiem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _2051010166_NguyenTranThanhLiem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IResidentRepository _residentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly ISettingRepository _settingRepository;



        public AdminController(ILogger<AdminController> logger, IResidentRepository residentRepository, IEmployeeRepository employeeRepository, IVehicleRepository vehicleRepository, IApartmentRepository apartmentrepository, ISettingRepository settingRepository)
        {
            _logger = logger;
            _residentRepository = residentRepository;
            _employeeRepository = employeeRepository;
            _vehicleRepository = vehicleRepository;
            _apartmentRepository = apartmentrepository;
            _settingRepository = settingRepository;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Manager_Resident()
        {
            var residents = _residentRepository.GetResidents(); 
            return View(residents); 
        }

        public IActionResult Manager_Apartment()
        {
            var apartments = _apartmentRepository.GetApartments();
            return View(apartments);
        }

        public IActionResult Manager_Employee()
        {
            var employees = _employeeRepository.GetEmployees();
            return View(employees);
        }
        public IActionResult Manager_Maintenance()
        {
            return View();
        }

        public IActionResult Manager_Vehicle()
        {
            var vehicles = _vehicleRepository.GetVehicles();
            return View(vehicles);
        }

        public IActionResult Manager_Service()
        {
            return View();
        }

        public IActionResult Manager_Statistical()
        {
            return View();
        }

        public IActionResult SystemSetting()
        {
            var model = new SettingPageViewModel
            {
                Apartment = _settingRepository.GetApartment(),
                Setting = _settingRepository.GetSystemSetting(),
                Users = new List<UserRoleViewModel>() // tạm thời rỗng để test
            };

            return View(model);
        }
        public IActionResult CreateEmployee() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(User employee)
        {
            try
            {
                await _employeeRepository.AddEmployeeAsync(employee);

                TempData["SuccessMessage"] = "Thêm nhân viên thành công!";
                return RedirectToAction("Manager_Employee"); // quay về danh sách nhân viên
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Manager_Employee", _employeeRepository.GetEmployees());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmployee(Guid id)
        {
            bool success = _employeeRepository.DeleteEmployee(id); 
            if (success)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Xóa thất bại!" });
            }
        }

        public IActionResult CreateResident() => View();

        // Thêm Resident
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateResident(User resident)
        {
            try
            {
      
                await _residentRepository.AddResidentAsync(resident);

                TempData["SuccessMessage"] = "Thêm cư dân thành công!";
                return RedirectToAction("Manager_Resident"); // quay về danh sách cư dân
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Manager_Resident", _residentRepository.GetResidents());
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteResident(Guid id)
        {
            bool success = _residentRepository.DeleteResident(id); 
            if (success)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Xóa thất bại!" });
            }
        }


        public IActionResult VehicleEdit(int id, bool isApproved)
        {
            _vehicleRepository.VehicleEdit(id, isApproved);
            return RedirectToAction("Manager_Vehicle", "Admin");
        }






















        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
