using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.AspNetCore.Identity;

namespace _2051010166_NguyenTranThanhLiem.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public EmployeeRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Lấy tất cả nhân viên
        public ICollection<User> GetEmployees()
        {
            return _context.Users
                .Where(x => x.Status >= 0 && !string.IsNullOrEmpty(x.Position) && !x.IsResident)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
        }

        // Lấy nhân viên theo Id
        public User GetEmployeeById(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id && !x.IsResident && !string.IsNullOrEmpty(x.Position));
        }

        // Thêm nhân viên mới
        public async Task AddEmployeeAsync(User employee)
        {
            // Kiểm tra email trùng
            if (await _userManager.FindByEmailAsync(employee.Email) != null)
                throw new Exception("Email đã tồn tại.");

            // Khởi tạo các trường bắt buộc
            employee.Id = Guid.NewGuid();
            employee.IsResident = false;
            employee.CreatedDate = DateTime.Now;
            employee.UpdatedDate = DateTime.Now;
            employee.UserName = employee.Email;
            employee.Position ??= "Employee";
            employee.CreatedBy = Guid.NewGuid();
            employee.UpdatedBy = Guid.NewGuid();
            employee.SecurityStamp = Guid.NewGuid().ToString();
            employee.Status = 1;

            string defaultPassword = "Employee@123";

            var result = await _userManager.CreateAsync(employee, defaultPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Tạo employee thất bại: " +
                    string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            // Thêm role
            await _userManager.AddToRoleAsync(employee, "User");
        }

        // Cập nhật thông tin nhân viên
        public void UpdateEmployee(User employee)
        {
            var existing = _context.Users.FirstOrDefault(x => x.Id == employee.Id && !x.IsResident);
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
                existing.UpdatedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public bool DeleteEmployee(Guid id)
        {
            var employee = _context.Users.FirstOrDefault(x => x.Id == id && !x.IsResident);
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
