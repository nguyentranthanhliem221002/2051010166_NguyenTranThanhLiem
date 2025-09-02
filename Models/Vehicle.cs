using System.ComponentModel.DataAnnotations;

namespace _2051010166_NguyenTranThanhLiem.Models
{
    public class Vehicle : Base
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }  // Biển số
        public string Type { get; set; }          // Xe máy / Ô tô
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}
