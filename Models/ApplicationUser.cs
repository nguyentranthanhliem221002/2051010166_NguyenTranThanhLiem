using Microsoft.AspNetCore.Identity;

namespace _2051010166_NguyenTranThanhLiem.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        // Liên kết tới bảng Person (nếu có)
        public int Id { get; set; }
        public Guid? PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
