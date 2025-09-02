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

        public ICollection<Person> GetResidents()
        {
            return _context.Persons
                           .Where(x => x.Status >= 0 && x.IsResident)
                           .ToList(); 
        }

        public Person GetResidentById(Guid id)
        {
            return _context.Persons.FirstOrDefault(x => x.Id == id && x.IsResident);
        }


        public void AddResident(Person resident)
        {
            resident.IsResident = true; // chắc chắn là cư dân
            _context.Persons.Add(resident);
            _context.SaveChanges();
        }

        public void UpdateResident(Person resident)
        {
            var existing = _context.Persons.FirstOrDefault(x => x.Id == resident.Id && x.IsResident);
            if (existing != null)
            {
                existing.FullName = resident.FullName;
                existing.DocumentNumber = resident.DocumentNumber;
                existing.Address = resident.Address;
                existing.Email = resident.Email;
                existing.PhoneNumber = resident.PhoneNumber;
                existing.Sex = resident.Sex;
                existing.Status = resident.Status;
                _context.SaveChanges();
            }
        }

        public void DeleteResident(Guid id)
        {
            var resident = _context.Persons.FirstOrDefault(x => x.Id == id && x.IsResident);
            if (resident != null)
            {
                resident.Status = -1;
                _context.SaveChanges();
            }
        }

    }
}
