using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả nhân viên
        public ICollection<Employee> GetEmployees()
        {
            return _context.Employees
                           .Where(x => x.Status >= 0 && x.Position != null)
                           .ToList();
        }

        // Lấy nhân viên theo Id
        public Employee GetEmployeeById(Guid id)
        {
            return _context.Employees.FirstOrDefault(x => x.Id == id && x.Position != null);
        }

        // Thêm nhân viên mới
        public void AddEmployee(Employee employee)
        {
            employee.Status = 0;        // mặc định trạng thái active
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        // Cập nhật thông tin nhân viên
        public void UpdateEmployee(Employee employee)
        {
            var existing = _context.Employees.FirstOrDefault(x => x.Id == employee.Id && x.Position != null);
            if (existing != null)
            {
                existing.FullName = employee.FullName;
                existing.DocumentNumber = employee.DocumentNumber;
                existing.Address = employee.Address;
                existing.Email = employee.Email;
                existing.PhoneNumber = employee.PhoneNumber;
                existing.Sex = employee.Sex;
                existing.Status = employee.Status;
                existing.Position = employee.Position;
                _context.SaveChanges();
            }
        }

        // Xóa nhân viên (chỉ đánh dấu Status = -1)
        public void DeleteEmployee(Guid id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == id && x.Position != null);
            if (employee != null)
            {
                employee.Status = -1; // đánh dấu đã xóa
                _context.SaveChanges();
            }
        }
    }
}
