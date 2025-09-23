using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
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
        private readonly ISettingRepository _settingRepository;

        public AdminController(ILogger<AdminController> logger, ISettingRepository settingRepository)
        {
            _logger = logger;
            _settingRepository = settingRepository;
        }

        #region Dashboard
        public IActionResult Index() => View();
        #endregion

        #region System Setting
        public IActionResult SystemSetting()
        {
            var model = new SettingPageViewModel
            {
                Apartment = _settingRepository.GetApartment(),
                Setting = _settingRepository.GetSystemSetting(),
                Users = new List<UserRoleViewModel>() // tạm để test
            };

            return View(model);
        }
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
