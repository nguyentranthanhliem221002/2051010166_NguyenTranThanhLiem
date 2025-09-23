using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<User>> GetEmployeesAsync()
        {
            return await _context.Users
                .Where(x => x.Status >= 0 && !string.IsNullOrEmpty(x.Position) && !x.IsResident)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<User?> GetEmployeeByIdAsync(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsResident && !string.IsNullOrEmpty(x.Position));
        }

        public async Task<User> AddEmployeeAsync(User employee)
        {
            if (await _userManager.FindByEmailAsync(employee.Email) != null)
                throw new Exception("Email đã tồn tại.");

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

            await _userManager.AddToRoleAsync(employee, "User");

            return employee;
        }


        public async Task UpdateEmployeeAsync(User employee)
        {
            var existing = await _context.Users.FirstOrDefaultAsync(x => x.Id == employee.Id && !x.IsResident);
            if (existing != null)
            {
                existing.FullName = employee.FullName;
                existing.DocumentNumber = employee.DocumentNumber;
                existing.Address = employee.Address;
                existing.Email = employee.Email;
                existing.PhoneNumber = employee.PhoneNumber;
                existing.Sex = employee.Sex;
                existing.Status = employee.Status;
                existing.Position = "Employee";
                existing.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var employee = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && !x.IsResident);
            if (employee != null)
            {
                employee.Status = -1;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
