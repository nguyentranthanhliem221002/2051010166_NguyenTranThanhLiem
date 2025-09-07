using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2051010166_NguyenTranThanhLiem.Models
{
    //public class Vehicle : Base
    //{
    //    public int Id { get; set; }

    //    public string LicensePlate { get; set; }  // Biển số

    //    public string Color { get; set; }

    //    public Guid PersonId { get; set; }
    //    public User Person { get; set; }

    //    public ApprovalStatus TrangThaiDuyet { get; set; } = ApprovalStatus.Pending;

    //    public DateTime CreatedAt { get; set; } = DateTime.Now;

    //    // Quan hệ với VehicleType
    //    public int VehicleTypeId { get; set; }

    //    public VehicleType VehicleType { get; set; }

    //    // Thêm gói và ngày bắt đầu
    //    public int PackageMonths { get; set; }

    //    public DateTime StartDate { get; set; } = DateTime.Now;

    //    public enum ApprovalStatus { Pending, Done, Cancel }

    //}
    public class Vehicle : Base
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }

        [Required]
        public Guid PersonId { get; set; }
        public User Person { get; set; }

        public int? ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

        [Required]
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

        public int PackageMonths { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;

        public string ImageUrl { get; set; }
        public ApprovalStatus TrangThaiDuyet { get; set; } = ApprovalStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public enum ApprovalStatus { Pending, Done, Cancel }
    }


}
