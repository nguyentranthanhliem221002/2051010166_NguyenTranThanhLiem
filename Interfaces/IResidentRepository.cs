using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Interfaces
{
    public interface IResidentRepository
    {
        ICollection<Person> GetResidents();
        Person GetResidentById(Guid id);
        void AddResident(Person resident);
        void UpdateResident(Person resident);
        void DeleteResident(Guid id);
    }
}
