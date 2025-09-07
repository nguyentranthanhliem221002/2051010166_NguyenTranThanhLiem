using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.EntityFrameworkCore;

namespace _2051010166_NguyenTranThanhLiem.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        // Ở đây mình giả lập dữ liệu, bạn có thể thay bằng EF Core truy vấn DB
        public ApartmentInfo GetApartment()
        {
            return new ApartmentInfo
            {
                Id = 1,
                Name = "Chung cư Happy",
                Address = "123 Đường ABC, Quận 1",
                Phone = "0901234567",
                Email = "contact@happyapartment.com",
                LogoUrl = "/images/logo.png"
            };
        }

        public SystemSetting GetSystemSetting()
        {
            return new SystemSetting
            {
                EmailNotification = true,
                SmsNotification = false
            };
        }
    }
}
