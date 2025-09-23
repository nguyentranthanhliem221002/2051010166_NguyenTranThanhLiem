using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace _2051010166_NguyenTranThanhLiem.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class ManagerController : Controller
    {
        private readonly ILogger<ManagerController> _logger;
        private readonly IResidentRepository _residentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IApartmentRepository _apartmentRepository;

        public ManagerController(
            ILogger<ManagerController> logger,
            IResidentRepository residentRepository,
            IEmployeeRepository employeeRepository,
            IVehicleRepository vehicleRepository,
            IApartmentRepository apartmentRepository)
        {
            _logger = logger;
            _residentRepository = residentRepository;
            _employeeRepository = employeeRepository;
            _vehicleRepository = vehicleRepository;
            _apartmentRepository = apartmentRepository;
        }

        #region Dashboard
        public IActionResult Index() => View();
        #endregion

        #region Residents
        public async Task<IActionResult> Manager_Resident()
        {
            var residents = await _residentRepository.GetResidentsAsync();
            return View("Manager_Resident", residents);
        }

        public IActionResult CreateResident() => View();

        [HttpPost]
        public async Task<IActionResult> CreateResident(User resident)
        {
            await _residentRepository.AddResidentAsync(resident);
            TempData["SuccessMessage"] = "Thêm cư dân thành công!";
            return RedirectToAction("Manager_Resident");
        }

        public async Task<IActionResult> EditResident(Guid id)
        {
            var resident = await _residentRepository.GetResidentByIdAsync(id);
            return PartialView("_CreateResident", resident);
        }

        [HttpPost]
        public async Task<IActionResult> EditResident(User resident)
        {
            await _residentRepository.UpdateResidentAsync(resident);
            return RedirectToAction("Manager_Resident");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteResident(Guid id)
        {
            var success = await _residentRepository.DeleteResidentAsync(id);
            return Json(new { success });
        }
        #endregion

        #region Apartments
        public async Task<IActionResult> Manager_Apartment()
        {
            var apartments = await _apartmentRepository.GetApartmentsAsync();
            var residents = await _residentRepository.GetResidentsAsync();
            ViewBag.Persons = new SelectList(residents, "Id", "FullName");
            return View("Manager_Apartment", apartments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateApartment(Apartment apartment)
        {
            await _apartmentRepository.AddApartmentAsync(apartment);
            return RedirectToAction("Manager_Apartment");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var success = await _apartmentRepository.DeleteApartmentAsync(id);
            return Json(new { success });
        }
        #endregion

        #region Employees
        public async Task<IActionResult> Manager_Employee()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            return View("Manager_Employee", employees);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(User employee)
        {
            await _employeeRepository.AddEmployeeAsync(employee);
            return RedirectToAction("Manager_Employee");
        }

        public async Task<IActionResult> EditEmployee(Guid id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return PartialView("_CreateEmployee", employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(User employee)
        {
            await _employeeRepository.UpdateEmployeeAsync(employee);
            return RedirectToAction("Manager_Employee");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var success = await _employeeRepository.DeleteEmployeeAsync(id);
            return Json(new { success });
        }
        #endregion

        #region Vehicles
        public IActionResult Manager_Vehicle()
        {
            var vehicles = _vehicleRepository.GetVehicles();
            return View("Manager_Vehicle", vehicles);
        }

        public IActionResult VehicleEdit(int id, bool isApproved)
        {
            _vehicleRepository.VehicleEdit(id, isApproved);
            return RedirectToAction("Manager_Vehicle");
        }


        #endregion

        #region Services / Others
        public IActionResult Manager_Maintenance() => View("Manager_Maintenance");
        public IActionResult Manager_Statistical() => View("Manager_Statistical");

        // placeholder nếu sau này thêm dịch vụ khác
        public IActionResult Manager_Electricity() => View("Manager_Electricity");
        public IActionResult Manager_Water() => View("Manager_Water");
        public IActionResult Manager_Wifi() => View("Manager_Wifi");
        #endregion

        #region Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
