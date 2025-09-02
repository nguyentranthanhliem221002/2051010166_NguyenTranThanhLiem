using System.ComponentModel.DataAnnotations;

namespace _2051010166_NguyenTranThanhLiem.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
