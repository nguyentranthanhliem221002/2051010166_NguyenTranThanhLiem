using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2051010166_NguyenTranThanhLiem.Models
{
    public class Service : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(20,3)")]
        public decimal Price {  get; set; }
    }
}
