using Core.Entities;
using Core.Entities.Operation;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticValues.Operation;
using Dto.ApiPanelDtos.OperationDtos;
using Service.Mail;

namespace Service.Services
{
    public class OperationService : GenericService<Operation>, IOperationService
    {
        readonly IOperationGuideRepository _operationGuideRepository;
        readonly IOperationVehicleRepository _operationVehicleRepository;
        readonly IVehicleRepository _vehicleRepository;
        readonly IOperationAdditionalServiceRepository _operationAdditionalServiceRepository;
        readonly IOperationRepository _operationRepository;
        readonly IMailSenderService _mailSenderService;
        readonly ISendMailTemplateService _mailSenderTemplateService;
        readonly IGuideService _guideService;
        readonly IOperationNoteRepository _operationNoteRepository;
        readonly IOperationTicketRepository _operationTicketRepository;
        readonly IOperationShopRepository _operationShopRepository;

        public OperationService(IGenericRepository<Operation> repository, IUnitOfWork unitOfWork, IOperationGuideRepository operationGuideRepository, IOperationVehicleRepository operationVehicleRepository, IOperationAdditionalServiceRepository operationAdditionalServiceRepository, IOperationRepository operationRepository, IVehicleRepository vehicleRepository, IMailSenderService mailSenderService, ISendMailTemplateService mailSenderTemplateService, IGuideService guideService, IOperationNoteRepository operationNoteRepository, IOperationTicketRepository operationTicketRepository, IOperationShopRepository operationShopRepository) : base(repository, unitOfWork)
        {
            _operationGuideRepository = operationGuideRepository;
            _operationVehicleRepository = operationVehicleRepository;
            _operationAdditionalServiceRepository = operationAdditionalServiceRepository;
            _operationRepository = operationRepository;
            _vehicleRepository = vehicleRepository;
            _mailSenderService = mailSenderService;
            _mailSenderTemplateService = mailSenderTemplateService;
            _guideService = guideService;
            _operationNoteRepository = operationNoteRepository;
            _operationTicketRepository = operationTicketRepository;
            _operationShopRepository = operationShopRepository;
        }
        public void CreateOperation(Reservation reservation)
        {
            Operation operation = new()
            {
                ReservationId = reservation.Id,
                OperationStatus = (int)MyOperationStatus.No.Waiting,
            };
            _repository.Add(operation);

            var product = reservation.Product;
            if (product.IsTour)
            {
                var reservationStartDate = reservation.StartDate;
                var tour = product.Tour;

                List<AdditionalService> perDayAdditionals = new List<AdditionalService>();
                if (reservation.Many_Reservation_AdditionalServiceOptions.Count > 0)
                {
                    foreach (var additionalService in reservation.Many_Reservation_AdditionalServiceOptions)
                    {
                        var additional = additionalService.AdditionalServiceOption.AdditionalService;
                        if (additional.IsPerDay)
                            perDayAdditionals.Add(additional);

                        else
                        {
                            OperationAdditionalService operationAdditionalService = new()
                            {
                                AdditionalServiceId = additionalService.Id,
                                OperationId = operation.Id,
                                AdditionalServiceStatus = 1, //TODO additionalServiceStatus ekle
                                Date = reservationStartDate,
                            };
                            _operationAdditionalServiceRepository.Add(operationAdditionalService);
                        }
                    }

                }
                for (int i = 0; i < tour?.Duration; i++)
                {
                    OperationGuide operationGuide = new()
                    {
                        GuideId = null,
                        Day = i + 1,
                        Date = reservationStartDate.AddDays(i),
                        OperationId = operation.Id,
                        GuideStatus = (int)OperationGuideStatus.No.Waiting,
                    };
                    _operationGuideRepository.Add(operationGuide);

                    OperationVehicle operationVehicle = new()
                    {
                        OperationId = operation.Id,
                        VehicleId = null,
                        Date = reservationStartDate.AddDays(i),
                        Day = i + 1,
                        VehicleStatus = (int)OperationVehicleStatus.No.Waiting,
                    };
                    _operationVehicleRepository.Add(operationVehicle);

                    foreach (var additionalService in perDayAdditionals)
                    {
                        OperationAdditionalService operationAdditionalService = new()
                        {
                            AdditionalServiceId = additionalService.Id,
                            OperationId = operation.Id,
                            Day = i + 1,
                            Date = reservationStartDate.AddDays(i),
                            AdditionalServiceStatus = (int)OperationAdditionalServiceStatus.No.Waiting
                        };
                        _operationAdditionalServiceRepository.Add(operationAdditionalService);

                    }
                    OperationNote operationNote = new()
                    {
                        OperationId = operation.Id,
                        Date = reservationStartDate.AddDays(i),
                        NoteStatus = 1,
                        Day = i + 1
                    };
                    _operationNoteRepository.Add(operationNote);
                    OperationTicket operationTicket = new()
                    {
                        OperationId = operation.Id,
                        Date = reservationStartDate.AddDays(i),
                        Day = i + 1,
                    };
                    _operationTicketRepository.Add(operationTicket);

                    OperationShop operationShop = new()
                    {
                        Date = reservationStartDate.AddDays(i),
                        Day = i + 1,
                        OperationShopStatus = (int)OperationShopStatus.No.None,
                        OperationId = operation.Id
                    };
                    _operationShopRepository.Add(operationShop);
                }
                _unitOfWork.saveChanges();
            }

        }

        public void AssignGuide(AssignGuideToOperationDto assignGuideDto)
        {
            OperationGuide operationGuide = _operationGuideRepository.GetById(Guid.Parse(assignGuideDto.OperationGuideId));
            operationGuide.GuideId = Guid.Parse(assignGuideDto.GuideId);

            Guide assignedGuide = _guideService.GetById(Guid.Parse(assignGuideDto.GuideId));
            SendMail mail = new()
            {
                Content = "Şu tur için rehber olarak atandınız", //
                Email = assignedGuide.Email,
                SendTime = DateTime.Now.AddMinutes(5),
                Subject = "Rehber ataması"
            };
            _mailSenderService.ScheduleMailForSent(mail);

            _operationGuideRepository.Update(operationGuide);
            _unitOfWork.saveChanges();
        }

        public void AssignVehicle(AssignVehicleToOperationDto assignVehicleDto)
        {
            OperationVehicle operationVehicle = _operationVehicleRepository.GetById(Guid.Parse(assignVehicleDto.OperationVehicleId));
            operationVehicle.VehicleId = Guid.Parse(assignVehicleDto.VehicleId);

            Vehicle assignedVehicle = _vehicleRepository.GetVehicleById(Guid.Parse(assignVehicleDto.VehicleId));
            User assignedDriverUser = assignedVehicle.User;

            SendMail mail = new()
            {
                Content = "Şu tur için araç kullanacaksınız", //
                Email = assignedDriverUser.Email,
                SendTime = DateTime.Now.AddMinutes(5),
                Subject = "Rehber ataması"
            };
            _mailSenderService.ScheduleMailForSent(mail);

            _operationVehicleRepository.Update(operationVehicle);
            _unitOfWork.saveChanges();
        }



        public List<OperationListDto> GetOperations()
        {
            var data = _operationRepository.GetOperationList();
            return data;

        }
        public AssignGuideToOperationDto GetOperationGuideForEdit(string operationGuideId)
        {
            var operationGuide = _operationGuideRepository.GetById(Guid.Parse(operationGuideId));
            return new()
            {
                GuideId = operationGuide.GuideId.ToString(),
                OperationGuideId = operationGuideId,
            };
        }

        public AssignVehicleToOperationDto GetOperationVehicleForEdit(string operationVehicleId)
        {
            var operationVehicle = _operationVehicleRepository.GetForEdit(Guid.Parse(operationVehicleId)); //supplierId joinlemek için repoya yazdım
            return operationVehicle;
        }

        public void SaveDailyOperationNote(SaveDailyOperationNoteDto dto)
        {
            var dailyOperationNote = _operationNoteRepository.GetById(Guid.Parse(dto.OperationNoteId));
            dailyOperationNote.Note = dto.OperationNote;
            _operationNoteRepository.Update(dailyOperationNote);
            _unitOfWork.saveChanges();
        }

        public void SaveTicketInfo(SaveTicketInfoDto dto)
        {
            var operationTicket = _operationTicketRepository.GetById(Guid.Parse(dto.OperationTicketId));
            operationTicket.Price = dto.Price;
            _operationTicketRepository.Update(operationTicket);
            _unitOfWork.saveChanges();
        }

        public void SaveHourInfo(SaveHourInfoDto dto)
        {
            var operationNote = _operationNoteRepository.GetById(Guid.Parse(dto.OperationNoteId));
            operationNote.Hour = dto.Hour;
            _operationNoteRepository.Update(operationNote);
            _unitOfWork.saveChanges();
        }

        public void ChangeShopStatus(ChangeShopStatusDto dto)
        {
            var operationShop = _operationShopRepository.GetById(Guid.Parse(dto.OperationShopId));
            operationShop.OperationShopStatus = dto.ShopStatus;
            _operationShopRepository.Update(operationShop);
            _unitOfWork.saveChanges();
        }
    }
}
