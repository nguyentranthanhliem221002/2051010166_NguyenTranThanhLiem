using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace _2051010166_NguyenTranThanhLiem.Models
{
    public class Apartment : Base
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
    }
}
