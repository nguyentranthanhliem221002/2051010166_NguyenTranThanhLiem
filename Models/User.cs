using Microsoft.AspNetCore.Identity;

namespace _2051010166_NguyenTranThanhLiem.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
        public string Address { get; set; }
        public int? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsResident { get; set; } = false;
        public string Position { get; set; }
        public int Status { get; set; } = 0;

        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } // 1 User có nhiều xe
        public ICollection<Apartment> Apartments { get; set; } // 1 User có nhiều căn hộ
    }
}
