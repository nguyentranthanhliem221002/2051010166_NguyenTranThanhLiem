using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.EntityFrameworkCore;

namespace _2051010166_NguyenTranThanhLiem.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public ApartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Apartment>> GetApartmentsAsync()
        {
            return await _context.Apartments
                .Where(x => x.Status >= 0)
                .OrderByDescending(x => x.CreatedDate)
                .Include(x => x.Person)
                .Include(x => x.Vehicles)
                .ToListAsync();
        }


        // Lấy căn hộ theo Id
        public async Task<Apartment?> GetApartmentByIdAsync(int id)
        {
            return await _context.Apartments
                .Where(x => x.Status >= 0)
                .Include(x => x.Person)
                .Include(x => x.Vehicles)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Thêm căn hộ mới
        public async Task<Apartment> AddApartmentAsync(Apartment apartment)
        {
            apartment.CreatedDate = DateTime.Now;
            apartment.UpdatedDate = DateTime.Now;
            apartment.Status = 1; // active mặc định

            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();
            return apartment;
        }

        // Cập nhật thông tin căn hộ
        public async Task UpdateApartmentAsync(Apartment apartment)
        {
            var existing = await _context.Apartments
                .FirstOrDefaultAsync(x => x.Id == apartment.Id && x.Status >= 0);

            if (existing != null)
            {
                existing.Code = apartment.Code;
                existing.Floor = apartment.Floor;
                existing.PersonId = apartment.PersonId;
                existing.Status = apartment.Status;
                existing.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }

        // Xoá căn hộ (soft delete)
        public async Task<bool> DeleteApartmentAsync(int id)
        {
            var apartment = await _context.Apartments
                .FirstOrDefaultAsync(x => x.Id == id && x.Status >= 0);

            if (apartment != null)
            {
                apartment.Status = -1;
                apartment.UpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
