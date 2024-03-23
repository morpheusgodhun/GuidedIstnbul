using Core.Entities.Operation;
using Core.IRepository;
using Core.StaticValues.Operation;
using Dto.ApiPanelDtos.OperationDtos;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class OperationRepository : GenericRepository<Operation>, IOperationRepository
    {
        public OperationRepository(Context context) : base(context)
        {
        }
        public List<OperationListDto> GetOperationList()
        {

            var query = from operation in _context.Operations.ToList()
                        join reservation in _context.Reservations.ToList() on operation.ReservationId equals reservation.Id
                        join user in _context.Users.ToList() on reservation.UserId equals user.Id
                        join product in _context.Products.ToList() on reservation.ProductId equals product.Id
                        select new OperationListDto
                        {
                            CustomerName = user.Name,
                            ReservationId = reservation.Id.ToString(),
                            OperationStartDate = reservation.StartDate,
                            NumberOfParticipants = reservation.ParticipantNumber,
                            ProductName = product.ProductName,
                            OperationNote = operation.Note,
                            Hotel = reservation.Hotel,
                            PickUpPoint = reservation.PickUpPoint,
                            OperationId = operation.Id.ToString(),
                            ReservationCode = reservation.ReservationCode,
                            ProductImagePath = (from productImage in _context.ProductImages.ToList()
                                                where productImage.ProductID == product.Id
                                                select productImage.ImagePath).ToString(),
                            OperationGuides = (from operationGuide in _context.OperationGuides
                                               join guide in _context.Guides on operationGuide.GuideId equals guide.Id into guides
                                               from leftGuide in guides.DefaultIfEmpty()
                                               where operationGuide.OperationId == operation.Id
                                               select new OperationGuideDto
                                               {
                                                   Date = operationGuide.Date,
                                                   Day = operationGuide.Day,
                                                   OperationGuideId = operationGuide.Id.ToString(),
                                                   Guide = leftGuide.Name,
                                                   GuideStatus = OperationGuideStatus.GetValue(operationGuide.GuideStatus)
                                               }).ToList(),
                            OperationAdditionalServices = (from operationAdditionals in _context.OperationAdditionalServices.ToList()
                                                           join additionalService in _context.AdditionalServiceLanguageItems.ToList() on operationAdditionals.AdditionalServiceId equals additionalService.AdditionalServiceID
                                                           where operationAdditionals.OperationId == operation.Id
                                                           select new OperationAdditionalServiceDto
                                                           {
                                                               AdditionalService = additionalService.DisplayName,
                                                               Date = operationAdditionals.Date,
                                                               Day = operationAdditionals.Day,
                                                               AdditionalServiceStatus = OperationAdditionalServiceStatus.GetValue(operationAdditionals.AdditionalServiceStatus)
                                                           }).ToList(),
                            //OperationVehicles = (from operationVehicle in _context.OperationVehicles
                            //                     join vehicle in _context.Vehicles on operationVehicle.VehicleId equals vehicle.Id into vehicles
                            //                     from leftVehicle in vehicles.DefaultIfEmpty()
                            //                     join user in _context.Users on leftVehicle.UserId equals user.Id into users
                            //                     from leftUser in users.DefaultIfEmpty()
                            //                     where operationVehicle.OperationId == operation.Id
                            //                     select new OperationVehicleDto
                            //                     {
                            //                         OperationVehicleId = operationVehicle.Id.ToString(),
                            //                         Date = operationVehicle.Date,
                            //                         Day = operationVehicle.Day,
                            //                         //Status =,
                            //                         Vehicle = $"{leftUser.Name} {leftUser.Surname} {leftVehicle.Name}"
                            //                     }
                            //                     ).ToList(),
                            OperationVehicles = _context.OperationVehicles.Where(x => x.OperationId == operation.Id).Include(x => x.Vehicle).ThenInclude(x => x.User).Select(x => new OperationVehicleDto
                            {
                                Date = x.Date,
                                Day = x.Day,
                                OperationVehicleId = x.Id.ToString(),
                                Vehicle = x.Vehicle.Name + " " + x.Vehicle.User.Name,
                                VehicleStatus = OperationVehicleStatus.GetValue(x.VehicleStatus)
                            }).ToList(),
                            OperationNotes = (from operationNote in _context.OperationNotes.ToList()
                                              where operationNote.OperationId == operation.Id
                                              select new OperationNoteDto
                                              {
                                                  Day = operationNote.Day,
                                                  Date = operationNote.Date,
                                                  Note = operationNote.Note,
                                                  OperationNoteId = operationNote.Id.ToString(),
                                                  Hour = operationNote.Hour,
                                              }).ToList(),
                            OperationShops = (from operationShop in _context.OperationShops.ToList()
                                              where operationShop.OperationId == operation.Id
                                              select new OperationShopDto
                                              {
                                                  Day = operationShop.Day,
                                                  OperationShopId = operationShop.Id.ToString(),
                                                  Date = operationShop.Date,
                                                  ShopStatus = OperationShopStatus.GetValue(operationShop.OperationShopStatus)
                                              }).ToList(),
                            OperationTickets = (from operationTicket in _context.OperationTickets.ToList()
                                                where operationTicket.OperationId == operation.Id
                                                select new OperationTicketDto
                                                {
                                                    Date = operationTicket.Date,
                                                    Price = operationTicket.Price.ToString(),
                                                    Day = operationTicket.Day,
                                                    OperationTicketId = operationTicket.Id.ToString(),
                                                }).ToList(),

                        };
            var data = query.ToList();
            return data;
        }



    }
}

