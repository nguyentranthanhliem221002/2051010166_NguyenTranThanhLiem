using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Repositories
{
    public class ResidentRepository : IResidentRepository
    {

        private readonly ApplicationDbContext _context;

        public ResidentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<User> GetResidents()
        {
            return _context.Users
                .Where(x => x.Status >= 0 && x.IsResident && x.Position == "")
                .OrderByDescending(x => x.CreatedDate) // hoặc x.Id
                .ToList();

        }

        public User GetResidentById(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id && x.IsResident);
        }


        public void AddResident(User resident)
        {
            resident.IsResident = true; // chắc chắn là cư dân
            resident.CreatedDate = DateTime.Now;
            _context.Users.Add(resident);
            _context.SaveChanges();
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
            // Tìm personId dựa vào UserId
            var person = _context.Users.FirstOrDefault(p => p.Id == userId);
            if (person == null) return Guid.Empty;

            return person.Id; // Id chính là PersonId
        }

    }
}
