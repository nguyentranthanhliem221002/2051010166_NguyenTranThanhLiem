using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<User>> GetEmployeesAsync();
        Task<User?> GetEmployeeByIdAsync(Guid id);
        Task<User> AddEmployeeAsync(User employee);
        Task UpdateEmployeeAsync(User employee);
        Task<bool> DeleteEmployeeAsync(Guid id);
    }
}
