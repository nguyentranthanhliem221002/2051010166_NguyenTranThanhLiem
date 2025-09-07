using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Interfaces
{
    public interface IResidentRepository
    {
        ICollection<User> GetResidents();
        User GetResidentById(Guid id);
        void AddResident(User resident);
        void UpdateResident(User resident);
        bool DeleteResident(Guid id);
        Guid GetPersonIdByUserId(Guid userId);
    }
}
