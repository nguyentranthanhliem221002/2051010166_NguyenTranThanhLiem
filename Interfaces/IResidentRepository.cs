using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Interfaces
{
    public interface IResidentRepository
    {
        ICollection<User> GetResidents();
        User GetResidentById(Guid id);
        Task AddResidentAsync(User resident);
        void UpdateResident(User resident);
        bool DeleteResident(Guid id);
        Guid GetPersonIdByUserId(Guid userId);
    }
}
