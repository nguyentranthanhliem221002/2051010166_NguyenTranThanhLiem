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
        public ICollection<User> GetEmployees()
        {
            return _context.Users
                           .Where(x => x.Status >= 0 && x.Position == "Manager")
                           .OrderByDescending(x => x.CreatedDate)
                           .ToList();
        }

        // Lấy nhân viên theo Id
        public User GetEmployeeById(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id && x.Position != null);
        }

        // Thêm nhân viên mới
        public void AddEmployee(User employee)
        {
            employee.Status = 0;        // mặc định trạng thái active
            employee.CreatedDate = DateTime.Now;
            _context.Users.Add(employee);
            _context.SaveChanges();
        }

        // Cập nhật thông tin nhân viên
        public void UpdateEmployee(User employee)
        {
            var existing = _context.Users.FirstOrDefault(x => x.Id == employee.Id && x.Position != null);
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

        public bool DeleteEmployee(Guid id)
        {
            var employee = _context.Users.FirstOrDefault(x => x.Id == id && x.Position != null);
            if (employee != null)
            {
                employee.Status = -1; 
                _context.SaveChanges();
                return true; 
            }
            return false; 
        }

    }
}
