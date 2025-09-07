using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2051010166_NguyenTranThanhLiem.Models
{
    public class VehicleType : Base
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "decimal(20,3)")]
        public decimal Fee { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }

}
