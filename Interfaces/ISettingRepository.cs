using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Interfaces
{
    public interface ISettingRepository
    {
        ApartmentInfo GetApartment();
        SystemSetting GetSystemSetting();
    }
}
