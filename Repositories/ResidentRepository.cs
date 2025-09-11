using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.AspNetCore.Identity;

namespace _2051010166_NguyenTranThanhLiem.Repositories
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ResidentRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ICollection<User> GetResidents()
        {
            return _context.Users
                .Where(x => x.Status >= 0 && x.IsResident && x.Position == "")
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
        }

        public User GetResidentById(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id && x.IsResident);
        }

        public async Task AddResidentAsync(User resident)
        {
            // Kiểm tra user trùng
            if (await _userManager.FindByEmailAsync(resident.Email) != null)
                throw new Exception("Email đã tồn tại.");

            // Khởi tạo các trường bắt buộc
            resident.Id = Guid.NewGuid();
            resident.IsResident = true;
            resident.CreatedDate = DateTime.Now;
            resident.UpdatedDate = DateTime.Now;
            resident.UserName = resident.Email;
            resident.Position ??= "";
            resident.CreatedBy = Guid.NewGuid();  
            resident.UpdatedBy = Guid.NewGuid();
            resident.SecurityStamp = Guid.NewGuid().ToString();
            resident.Status = 1;

            string defaultPassword = "Resident@123";

            var result = await _userManager.CreateAsync(resident, defaultPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Tạo resident thất bại: " +
                    string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            // Thêm role
            await _userManager.AddToRoleAsync(resident, "User");
        }

        public void UpdateResident(User resident)
        {
            var existing = _context.Users.FirstOrDefault(x => x.Id == resident.Id && x.IsResident);
            if (existing != null)
            {
                existing.FullName = resident.FullName;
                existing.DocumentNumber = resident.DocumentNumber;
                existing.Address = resident.Address;
                existing.Email = resident.Email;
                existing.PhoneNumber = resident.PhoneNumber;
                existing.Sex = resident.Sex;
                existing.Position = resident.Position;
                existing.Status = resident.Status;
                existing.UpdatedDate = resident.UpdatedDate;
                _context.SaveChanges();
            }
        }

        public bool DeleteResident(Guid id)
        {
            var resident = _context.Users.FirstOrDefault(x => x.Id == id && x.IsResident);
            if (resident != null)
            {
                resident.Status = -1;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Guid GetPersonIdByUserId(Guid userId)
        {
            var person = _context.Users.FirstOrDefault(p => p.Id == userId);
            return person?.Id ?? Guid.Empty;
        }
    }
}
