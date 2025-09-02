using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Interfaces
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetEmployees();
        Employee GetEmployeeById(Guid id);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Guid id);
    }
}
