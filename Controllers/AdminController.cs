using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _2051010166_NguyenTranThanhLiem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IResidentRepository _residentRepo;
        private readonly IEmployeeRepository _employeeRepo;

        public AdminController(ILogger<AdminController> logger, IResidentRepository residentRepo, IEmployeeRepository employeeRepo)
        {
            _logger = logger;
            _residentRepo = residentRepo;
            _employeeRepo = employeeRepo;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Resident()
        {
            var residents = _residentRepo.GetResidents(); 
            return View(residents); 
        }
        public IActionResult Apartment()
        {
            return View();
        }
        public IActionResult Employee()
        {
            var employees = _employeeRepo.GetEmployees();
            return View(employees);
        }
        public IActionResult Setting()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
