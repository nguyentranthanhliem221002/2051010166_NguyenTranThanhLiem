using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Interfaces
{
    public interface IApartmentRepository
    {
        IEnumerable<Apartment> GetApartments();
        Apartment GetApartmentById(int id);
    }
}
