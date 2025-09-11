using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Interfaces
{
    public interface IEmployeeRepository
    {
        ICollection<User> GetEmployees();
        User GetEmployeeById(Guid id);
        Task AddEmployeeAsync(User employee);
        void UpdateEmployee(User employee);
        bool DeleteEmployee(Guid id);
    }
}
