using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace _2051010166_NguyenTranThanhLiem.ViewModels
{
    //public class VehicleViewModel
    //{
    //    [Required(ErrorMessage = "Vui lòng nhập biển số xe")]
    //    public string LicensePlate { get; set; }

    //    [Required(ErrorMessage = "Vui lòng chọn loại xe")]
    //    public int VehicleTypeId { get; set; }  // Lưu Id loại xe

    //    public IEnumerable<SelectListItem> VehicleTypes { get; set; } // dropdown bind

    //    [Required]
    //    public int PackageMonths { get; set; }

    //    [Required]
    //    [DataType(DataType.Date)]
    //    public DateTime StartDate { get; set; }

    //    public string Color { get; set; }
    //}
    public class VehicleViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập biển số xe")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại xe")]
        public int VehicleTypeId { get; set; }
        public IEnumerable<SelectListItem> VehicleTypes { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn gói tháng")]
        public int PackageMonths { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày bắt đầu gửi")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public string Color { get; set; }

        // 🔹 Liên kết với bảng Apartment
        [Required(ErrorMessage = "Vui lòng chọn căn hộ")]
        public int ApartmentId { get; set; }
        public IEnumerable<SelectListItem> Apartments { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên chủ xe")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại chủ xe")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string OwnerPhone { get; set; }

        //public IFormFile? VehicleImage { get; set; }
        public IFormFile VehicleImage { get; set; }

    }

}
