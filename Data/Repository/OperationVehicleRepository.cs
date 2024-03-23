using Core.Entities.Operation;
using Core.IRepository;
using Dto.ApiPanelDtos.OperationDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class OperationVehicleRepository : GenericRepository<OperationVehicle>, IOperationVehicleRepository
    {
        public OperationVehicleRepository(Context context) : base(context)
        {

        }

        public AssignVehicleToOperationDto GetForEdit(Guid id)
        {
            var query = from operationVehicles in _context.OperationVehicles
                        join vehicles in _context.Vehicles on operationVehicles.VehicleId equals vehicles.Id
                        join suppliers in _context.Suppliers on vehicles.SupplierId equals suppliers.Id
                        where operationVehicles.Id == id
                        select new AssignVehicleToOperationDto
                        {
                            OperationVehicleId = operationVehicles.Id.ToString(),
                            SupplierId = suppliers.Id.ToString(),
                            VehicleId = vehicles.Id.ToString()
                        };
            return query.FirstOrDefault();
        }
    }
}
