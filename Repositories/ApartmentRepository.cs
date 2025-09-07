using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {

        private readonly ApplicationDbContext _context;

        public ApartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Apartment> GetApartments()
        {
            return _context.Apartments.Where(x => x.Status >= 0).ToList();
        }

        public Apartment GetApartmentById(int id)
        {
            return _context.Apartments.Where(x => x.Status >= 0).FirstOrDefault(a => a.Id == id);
        }

    }
}
