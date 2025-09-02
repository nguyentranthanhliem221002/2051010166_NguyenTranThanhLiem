using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace _2051010166_NguyenTranThanhLiem.Models
{
    public class Person : Base
    {
        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Sex { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
