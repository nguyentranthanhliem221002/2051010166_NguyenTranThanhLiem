using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.EntityFrameworkCore;

namespace _2051010166_NguyenTranThanhLiem.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Vehicle> GetVehicles()
        {
            return _context.Vehicles.Where(v => v.Status >= 0).Include(x => x.VehicleType).ToList();
        }

        public Vehicle GetVehicleById(int id)
        {
            return _context.Vehicles.Where(x => x.Status >= 0).Include(v => v.Person)
                                    .FirstOrDefault(v => v.Id == id);
        }

        public void AddVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
        }

        public bool DeleteVehicle(int id)
        {
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle == null) return false;

            _context.Vehicles.Remove(vehicle);
            _context.SaveChanges();
            return true;
        }

        //public void VehicleEdit(int id, bool isApproved)
        //{
        //    var vehicle = _context.Vehicles.Find(id);
        //    if (vehicle != null)
        //    {
        //        vehicle.TrangThaiDuyet = isApproved
        //            ? Vehicle.ApprovalStatus.Done
        //            : Vehicle.ApprovalStatus.Cancel;

        //        // Cập nhật Status số nếu vẫn cần (nếu dùng cho logic khác)
        //        vehicle.Status = isApproved ? 2 : 1;

        //        _context.Vehicles.Update(vehicle);
        //        _context.SaveChanges();
        //    }
        //}
        public void VehicleEdit(int id, bool isApproved)
        {
            var vehicle = _context.Vehicles.Include(v => v.Apartment).FirstOrDefault(v => v.Id == id);
            if (vehicle != null)
            {
                vehicle.TrangThaiDuyet = isApproved
                    ? Vehicle.ApprovalStatus.Done
                    : Vehicle.ApprovalStatus.Cancel;

                vehicle.Status = isApproved ? 2 : 1;

                _context.Vehicles.Update(vehicle);
                _context.SaveChanges();

                if (isApproved)
                {
                    var invoice = new Invoice
                    {
                        ApartmentId = vehicle.ApartmentId.Value,
                        Total = CalculateTotal(vehicle.PackageMonths, vehicle.VehicleTypeId),
                        IsPaid = false,
                        CreatedBy = vehicle.PersonId,
                        UpdatedBy = vehicle.PersonId,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    };

                    _context.Invoices.Add(invoice);
                    _context.SaveChanges();
                }
            }
        }

        // Hàm tính tiền
        private decimal CalculateTotal(int packageMonths, int vehicleTypeId)
        {
            decimal pricePerMonth = 0;
            switch (vehicleTypeId)
            {
                case 1: pricePerMonth = 500_000; break; // xe máy
                case 2: pricePerMonth = 1_000_000; break; // ô tô
                default: pricePerMonth = 300_000; break;
            }
            return pricePerMonth * packageMonths;
        }

        public IEnumerable<Vehicle> GetApprovedVehiclesByUser(Guid personId)
        {
            return _context.Vehicles
                .Include(v => v.VehicleType) // Load luôn VehicleType
                .Where(v => v.PersonId == personId && v.TrangThaiDuyet == Vehicle.ApprovalStatus.Done)
                .ToList();
        }

        public IEnumerable<VehicleType> GetVehicleTypes()
        {
            return _context.VehicleTypes.Where(x => x.Status >= 0).ToList();
        }


    }
}
