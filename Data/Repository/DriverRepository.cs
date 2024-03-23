using Core.Entities;
using Core.IRepository;
using Dto.ApiPanelDtos.DriverDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(Context context) : base(context)
        {
        }

        public List<Driver> GetDrivers()
        {
            //var query = from driver in _context.Drivers
            //            join vehicleType in _context.VehicleTypes on driver.VehicleTypeId equals vehicleType.Id into driverVehicleType
            //            where driver.IsDeleted == false
            //            select new Driver
            //            {
            //                Id = driver.Id,
            //                VehicleTypeId = driver.VehicleTypeId,
            //                VehicleType = driverVehicleType.FirstOrDefault(),
            //                CreateDate = driver.CreateDate,
            //                Email = driver.Email,
            //                Name = driver.Name,
            //                PhoneNumber = driver.PhoneNumber,
            //                Status = driver.Status,
            //                Surname = driver.Surname,

            //            };
            //var list = query.ToList();

            //var drivers = _context.Drivers.Include(x => x.VehicleType).ToList();
            return new();
        }

    }
}
