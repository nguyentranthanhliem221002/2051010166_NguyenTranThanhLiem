using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Interfaces
{
    public interface IResidentRepository
    {
        Task<IEnumerable<User>> GetResidentsAsync();
        Task<User?> GetResidentByIdAsync(Guid id);
        Task<User> AddResidentAsync(User resident);
        Task UpdateResidentAsync(User resident);
        Task<bool> DeleteResidentAsync(Guid id);
        Task<Guid> GetPersonIdByUserIdAsync(Guid userId);
    }
}
