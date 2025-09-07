using _2051010166_NguyenTranThanhLiem.Models;

namespace _2051010166_NguyenTranThanhLiem.Interfaces
{
    public interface IVehicleRepository
    {
        IEnumerable<Vehicle> GetVehicles();          // Lấy tất cả xe
        Vehicle GetVehicleById(int id);             // Lấy xe theo Id
        void AddVehicle(Vehicle vehicle);           // Thêm xe
        void UpdateVehicle(Vehicle vehicle);        // Cập nhật xe
        bool DeleteVehicle(int id);                 // Xóa xe (nếu muốn)
        void VehicleEdit(int id, bool isApproved);

        IEnumerable<Vehicle> GetApprovedVehiclesByUser(Guid personId);
        IEnumerable<VehicleType> GetVehicleTypes();
    }
}
