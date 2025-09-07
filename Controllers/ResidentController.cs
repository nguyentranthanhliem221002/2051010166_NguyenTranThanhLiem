using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
using _2051010166_NguyenTranThanhLiem.Repositories;
using _2051010166_NguyenTranThanhLiem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Claims;

namespace _2051010166_NguyenTranThanhLiem.Controllers
{
    public class ResidentController : Controller
    {
        private readonly ILogger<ResidentController> _logger;
        private readonly IResidentRepository _residentRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IApartmentRepository _apartmentRepository;

        private readonly UserManager<User> _userManager;

        public ResidentController(ILogger<ResidentController> logger, IResidentRepository residentRepository, IApartmentRepository apartmentRepository, IVehicleRepository vehicleRepository,  UserManager<User> userManager)
        {
            _residentRepository = residentRepository;
            _vehicleRepository = vehicleRepository;
            _apartmentRepository = apartmentRepository;

            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var resident = _residentRepository.GetResidentById(Guid.Parse(userId));
            return View(resident);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Vehicles()
        {
            var model = new VehicleViewModel
            {
                VehicleTypes = _vehicleRepository.GetVehicleTypes()
                        .Select(v => new SelectListItem
                        {
                            Value = v.Id.ToString(),
                            Text = v.Name
                        }),

                Apartments = _apartmentRepository.GetApartments()
                        .Select(a => new SelectListItem
                        {
                            Value = a.Id.ToString(),
                            Text = a.Code
                        })
            };

            return View(model);
        }
        public IActionResult Booking() => View();
        public IActionResult Complain() => View();
        public IActionResult Maintenance() => View();
        public IActionResult Infomation() => View();

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult RegisterVehicle(VehicleViewModel model)
        //{
        //    var userId = _userManager.GetUserId(User);
        //    if (userId == null)
        //        return RedirectToAction("Login", "Account");

        //        model.VehicleTypes = _vehicleRepository.GetVehicleTypes()
        //            .Select(v => new SelectListItem
        //            {
        //                Value = v.Id.ToString(),
        //                Text = v.Name
        //            });

        //    var vehicle = new Vehicle
        //    {
        //        LicensePlate = model.LicensePlate,
        //        Color = model.Color,
        //        PackageMonths = model.PackageMonths,
        //        StartDate = model.StartDate,
        //        PersonId = Guid.Parse(userId),
        //        TrangThaiDuyet = Vehicle.ApprovalStatus.Pending,
        //        CreatedAt = DateTime.Now,
        //        VehicleTypeId = model.VehicleTypeId
        //    };
        //    _vehicleRepository.AddVehicle(vehicle);

        //    return RedirectToAction("Vehicles","Resident");
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Vehicles(VehicleViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return RedirectToAction("Login", "Account");


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
                ImageUrl = "",
            };

            _vehicleRepository.AddVehicle(vehicle);
            return RedirectToAction("Vehicles", "Resident");
        }

        public IActionResult Payment()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var personId = _residentRepository.GetPersonIdByUserId(Guid.Parse(userId));
            if (personId == Guid.Empty) return NotFound("Không tìm thấy PersonId.");

            var approvedVehicles = _vehicleRepository.GetApprovedVehiclesByUser(personId);
            decimal totalFee = approvedVehicles.Sum(v => v.VehicleType.Fee);

            var model = new PaymentViewModel
            {
                Vehicles = approvedVehicles,
                TotalFee = totalFee
            };

            return View(model);
        }


    }
}
