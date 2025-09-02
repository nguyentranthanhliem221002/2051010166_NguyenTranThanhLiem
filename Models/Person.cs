using System.ComponentModel.DataAnnotations;

namespace _2051010166_NguyenTranThanhLiem.Models
{
    public class Person : Base
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsResident { get; set; } = false;
    }
}
