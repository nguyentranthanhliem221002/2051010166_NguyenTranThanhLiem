using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace _2051010166_NguyenTranThanhLiem.Models
{
    public class Apartment : Base
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Floor { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}
