using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<User>> GetResidentsAsync()
        {
            return await _context.Users
                .Where(x => x.Status >= 0 && x.IsResident && x.Position == "")
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<User?> GetResidentByIdAsync(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id && x.IsResident);
        }

        public async Task<User> AddResidentAsync(User resident)
        {

            //if (await _userManager.FindByEmailAsync(resident.Email) != null)
            //    throw new Exception("Email đã tồn tại.");

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

            await _userManager.AddToRoleAsync(resident, "User");

            return resident;
        }

        public async Task UpdateResidentAsync(User resident)
        {
            var existing = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == resident.Id && x.IsResident);

            if (existing != null)
            {
                existing.FullName = resident.FullName;
                existing.DocumentNumber = resident.DocumentNumber;
                existing.Address = resident.Address;
                existing.Email = resident.Email;
                existing.PhoneNumber = resident.PhoneNumber;
                existing.Sex = resident.Sex;
                existing.Position = "";
                existing.Status = resident.Status;
                existing.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteResidentAsync(Guid id)
        {
            var resident = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id && x.IsResident);

            if (resident != null)
            {
                resident.Status = -1;
                resident.UpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<Guid> GetPersonIdByUserIdAsync(Guid userId)
        {
            var person = await _context.Users.FirstOrDefaultAsync(p => p.Id == userId);
            return person?.Id ?? Guid.Empty;
        }
    }
}
