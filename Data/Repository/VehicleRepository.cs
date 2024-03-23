using Core.Entities;
using Core.IRepository;
using Dto.ApiPanelDtos.VehicleDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        Context _dbContext { get; set; }
        public VehicleRepository(Context context) : base(context)
        {
            _dbContext = context;
            IncludedQueryable = _dbContext.Vehicles.Include(x => x.Supplier).Include(x => x.VehicleType).Include(x => x.User);
        }
        private readonly IQueryable<Vehicle> IncludedQueryable;
        public List<VehicleListDto> GetVehiclesList()
        {
            var vehicles = IncludedQueryable.Select(x => new VehicleListDto()
            {
                VehicleID = x.Id.ToString(),
                VehicleName = x.Name,
                Capacity = x.Capacity,
                VehicleType = x.VehicleType.Type,
                PlateNumber = x.PlateNumber,
                Status = x.Status,
                Supplier = x.Supplier.Name,
                TursabPlateNumber = x.TursabPlateNumber,
                Driver = $"{x.User.Name} {x.User.Surname}"
            }).ToList();
            return vehicles;
        }

        public List<VehicleListDto> GetVehiclesList(Guid supplierId)
        {
            var vehicles = IncludedQueryable.Where(x => x.SupplierId == supplierId).Select(x => new VehicleListDto()
            {
                VehicleID = x.Id.ToString(),
                Capacity = x.Capacity,
                VehicleType = x.VehicleType.Type,
                PlateNumber = x.PlateNumber,
                Status = x.Status,
                Supplier = x.Supplier.Name,
                TursabPlateNumber = x.TursabPlateNumber,
                VehicleName = x.Name,
                Driver = $"{x.User.Name} {x.User.Surname}"
            }).ToList();
            return vehicles;
        }

        public Vehicle GetVehicleById(Guid vehicleId)
        {
            return IncludedQueryable.FirstOrDefault(x => x.Id == vehicleId);
        }
    }
}
