using _2051010166_NguyenTranThanhLiem.Models;
using System.ComponentModel.DataAnnotations;

namespace _2051010166_NguyenTranThanhLiem.ViewModels
{
    public class SettingPageViewModel
    {
        public ApartmentInfo Apartment { get; set; }
        public ICollection<UserRoleViewModel> Users { get; set; }
        public SystemSetting Setting { get; set; }
    }

}
