using _2051010166_NguyenTranThanhLiem.Models;
using System.ComponentModel.DataAnnotations;

namespace _2051010166_NguyenTranThanhLiem.ViewModels
{
    public class PaymentViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public IEnumerable<Service> Services { get; set; }

        public decimal TotalFee { get; set; }
    }

}
