using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _2051010166_NguyenTranThanhLiem.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ManagerController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Resident()
        {
            return View();
        }
        public IActionResult Apartment()
        {
            return View();
        }
        public IActionResult Employee()
        {
            return View();
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
