using System.ComponentModel.DataAnnotations.Schema;

namespace _2051010166_NguyenTranThanhLiem.Models
{
    public class Invoice : Base
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        [Column(TypeName = "decimal(20,3)")] 
        public decimal Total {  get; set; }
        public bool IsPaid {  get; set; }
    }
}
