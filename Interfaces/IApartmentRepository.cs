using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Interfaces
{
    public interface IApartmentRepository
    {
        Task<IEnumerable<Apartment>> GetApartmentsAsync();
        Task<Apartment?> GetApartmentByIdAsync(int id);
        Task<Apartment> AddApartmentAsync(Apartment apartment);
        Task UpdateApartmentAsync(Apartment apartment);
        Task<bool> DeleteApartmentAsync(int id);
    }

}
