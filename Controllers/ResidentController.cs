using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
using _2051010166_NguyenTranThanhLiem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Claims;

namespace _2051010166_NguyenTranThanhLiem.Controllers
{
    [Authorize(Roles = "User")]
    public class ResidentController : Controller
    {
        private readonly ILogger<ResidentController> _logger;
        private readonly IResidentRepository _residentRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly UserManager<User> _userManager;

        public ResidentController(
            ILogger<ResidentController> logger,
            IResidentRepository residentRepository,
            IVehicleRepository vehicleRepository,
            IApartmentRepository apartmentRepository,
            UserManager<User> userManager)
        {
            _logger = logger;
            _residentRepository = residentRepository;
            _vehicleRepository = vehicleRepository;
            _apartmentRepository = apartmentRepository;
            _userManager = userManager;
        }

        #region Dashboard
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return RedirectToAction("Login", "Account");

            var resident = await _residentRepository.GetResidentByIdAsync(Guid.Parse(userId));
            return View(resident);
        }
        #endregion

        #region MyVehicle đăng ký
        public async Task<IActionResult> Vehicles()
        {
            var vehicleTypes = _vehicleRepository.GetVehicleTypes()
                .Select(v => new SelectListItem { Value = v.Id.ToString(), Text = v.Name });

            var apartments = (await _apartmentRepository.GetApartmentsAsync())
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Code });

            return View(new VehicleViewModel { VehicleTypes = vehicleTypes, Apartments = apartments });
        }

        [HttpPost]
        public async Task<IActionResult> Vehicles(VehicleViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return RedirectToAction("Login", "Account");

            //if (!ModelState.IsValid)
            //{
            //    // Load dropdown nếu cần
            //    model.VehicleTypes = new SelectList(_vehicleRepository.GetVehicleTypes(), "Id", "Name");
            //    model.Apartments = new SelectList(await _apartmentRepository.GetApartmentsAsync(), "Id", "Name");
            //    return View(model);
            //}

            string imageUrl = null;

            if (model.VehicleImage != null && model.VehicleImage.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/vehicles");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.VehicleImage.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.VehicleImage.CopyToAsync(stream);
                }

                imageUrl = "/images/vehicles/" + fileName;
            }

            var vehicle = new Vehicle
            {
                LicensePlate = model.LicensePlate,
                Color = model.Color,
                PackageMonths = model.PackageMonths,
                StartDate = model.StartDate,
                PersonId = Guid.Parse(userId),
                VehicleTypeId = model.VehicleTypeId,
                ApartmentId = model.ApartmentId,
                TrangThaiDuyet = Vehicle.ApprovalStatus.Pending,
                CreatedAt = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ImageUrl = imageUrl, // có thể null nếu không upload
            };

            _vehicleRepository.AddVehicle(vehicle);

            TempData["SuccessMessage"] = "Đăng ký xe thành công!";
            return RedirectToAction("Vehicles");
        }

        #endregion

        #region Thanh toán
        public async Task<IActionResult> Payment()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var personId = await _residentRepository.GetPersonIdByUserIdAsync(Guid.Parse(userId));
            if (personId == Guid.Empty) return NotFound("Không tìm thấy PersonId.");

            var approvedVehicles = _vehicleRepository.GetApprovedVehiclesByUser(personId);
            decimal totalFee = approvedVehicles.Sum(v => v.VehicleType.Fee);

            return View(new PaymentViewModel { Vehicles = approvedVehicles, TotalFee = totalFee });
        }
        #endregion

        #region Other pages
        public IActionResult Complain() => View();
        public IActionResult Maintenance() => View();
        public IActionResult Infomation() => View();
        public IActionResult Electricity() => View();
        public IActionResult Water() => View();
        public IActionResult Cleaning() => View();
        public IActionResult Wifi() => View();
        public IActionResult Booking() => View();

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
