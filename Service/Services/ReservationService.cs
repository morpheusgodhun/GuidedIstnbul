using Core;
using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Core.StaticValues;
using Data.Migrations;
using Dto.ApiPanelDtos.ReservationDtos;
using Dto.ApiPanelDtos.ReservationPaymentDtos;
using Dto.ApiPanelDtos.SendMailDtos;
using Dto.ApiPanelDtos.UserDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Payment;
using Dto.ApiWebDtos.ApiToWebDtos.Reservation;
using Dto.ApiWebDtos.ApiToWebDtos.Service;
using Dto.ApiWebDtos.WebToApiDtos;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Mail;

namespace Service.Services
{
    public class ReservationService : GenericService<Reservation>, IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ISendMailTemplateService _sendMailTemplateService;
        private readonly ISendMailService _sendMailService;
        private readonly IMailSenderService _mailSenderService;
        private readonly IUserService _userService;
        private readonly IAdditionalServiceOptionPriceService _additionalServiceOptionPriceService;
        private readonly IReservationEditRequestRepository _reservationEditRequestRepository;
        private readonly ISystemOptionRepository _systemOptionRepository;
        private readonly IOperationService _operationService;
        private readonly IConfiguration _configuration;
        private readonly ITourRepository _tourRepository;
        private readonly IReservationPaymentService _reservationPaymentService;

        private readonly ITourService _tourService;
        private readonly IAdditionalServiceService _additionalServiceService;
        private readonly IMany_Product_AdditionalServiceService _manyProductAdditionalServiceService;
        private readonly IUserExtensionMailService _userExtensionMailService;

        public ReservationService(IGenericRepository<Reservation> repository, IUnitOfWork unitOfWork, IReservationRepository reservationRepository, ISendMailTemplateService sendMailTemplateService, IUserService userService, ISendMailService sendMailService, IMailSenderService mailSenderService, IAdditionalServiceOptionPriceService additionalServiceOptionPriceService, IUserRepository userRepository, IReservationEditRequestRepository reservationCancellationRequestRepository, IOperationService operationService, IConfiguration configuration, ITourRepository tourRepository, ISystemOptionRepository systemOptionRepository, ITourService tourService, IAdditionalServiceService additionalServiceService, IMany_Product_AdditionalServiceService manyProductAdditionalServiceService, IReservationPaymentService reservationPaymentService, IUserExtensionMailService userExtensionMailService) : base(repository, unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _sendMailTemplateService = sendMailTemplateService;
            _userService = userService;
            _sendMailService = sendMailService;
            _mailSenderService = mailSenderService;
            _additionalServiceOptionPriceService = additionalServiceOptionPriceService;
            _reservationEditRequestRepository = reservationCancellationRequestRepository;
            _operationService = operationService;
            _configuration = configuration;
            _tourRepository = tourRepository;
            _systemOptionRepository = systemOptionRepository;
            _tourService = tourService;
            _additionalServiceService = additionalServiceService;
            _manyProductAdditionalServiceService = manyProductAdditionalServiceService;
            _reservationPaymentService = reservationPaymentService;
            _userExtensionMailService = userExtensionMailService;
        }

        public List<ReservationListDto> ComplatedReservationList()
        {
            return _reservationRepository.ComplatedReservationList();
        }

        public PaymentReservationDetailDto GetPaymentReservationDetailDto(Guid reservationId, int languageId)
        {
            var vl = _reservationRepository.GetPaymentReservationDetailDto(reservationId, languageId);
            vl.ReservationPayments = _reservationPaymentService.PaymentListByReservationId(reservationId);
            return vl;
        }

        public PaymentAgentInfoDto GetPaymentAgentInfo(Guid productId, Guid agentId)
        {
            return _reservationRepository.GetPaymentAgentInfo(productId, agentId);
        }



        public ReservationBillingInformationDto GetReservationBillingInformationDto(Guid reservationId, int languageId)
        {
            return _reservationRepository.GetReservationBillingInformationDto(reservationId, languageId);
        }

        public ReservationFormDetailDto GetReservationFormDetailDto(Guid reservationId)
        {
            return _reservationRepository.GetReservationFormDetailDto(reservationId);
        }

        public WebReservationListDto GetReservationListDto(string code, int languageId)
        {
            throw new NotImplementedException();
        }

        public ReservationParticipantInformationDto GetReservationParticipantInformationDto(Guid reservationId, int languageId)
        {
            return _reservationRepository.GetReservationParticipantInformationDto(reservationId, languageId);
        }

        public ReservationPaymentInformationDto GetReservationPaymentInformationDto(Guid reservationId, int languageId)
        {
            return _reservationRepository.GetReservationPaymentInformationDto(reservationId, languageId);
        }

        public ReservationSuccessDto GetReservationSuccessDto(Guid reservationId, int languageId)
        {
            return _reservationRepository.GetReservationSuccessDto(reservationId, languageId);
        }

        public Guid ReservationBillingInformation(ReservationBillingInformationDto dto)
        {
            Guid reservationId = _reservationRepository.ReservationBillingInformation(dto);
            _unitOfWork.saveChanges();
            return reservationId;
        }

        public ReservationDetailDto ReservationDetail(Guid reservationId, int languageId)
        {
            return _reservationRepository.ReservationDetail(reservationId, languageId);
        }

        public Guid ReservationGeneralInfo(ReservationFormMainInformationDto dto)
        {
            //TODO: Güvenlik eklenmeli ilerde
            //gelen optionlar gerçekten o additonalınmı altında
            //selected duration 0 olmasın o turun min günü olsun eğer 0 sa 


            //boş gelenleri temizleyelim.
            dto.AdditionalServiceOptions.RemoveAll(option => option.OptionId == Guid.Empty);

            //kalan additionalarda perperson veya perday özelliğine ulşamamız lazım AdditionalServiceId
            var selectedAdditionals = _additionalServiceService.Where(ads => dto.AdditionalServiceOptions.Select(x => x.AdditionalServiceId).Contains(ads.Id)).ToList();

            var selectedAdditionalsComission = _manyProductAdditionalServiceService.Where(x => x.ProductID == dto.ProductId && dto.AdditionalServiceOptions.Select(x => x.AdditionalServiceId).Contains(x.AdditionalServiceID)).ToList();


            foreach (var option in dto.AdditionalServiceOptions)
            {
                // tüm fiyatlarını çektim.
                // şimdi bu fiyatlar arasında seçilen tarih ve kişi sayısına göre en uygun fiyatı hesaplayacağız
                var allPrices = _additionalServiceOptionPriceService.OptionAllPriceList(option.OptionId);

                //tüm fiyatlar içinde en uygun o tarihteki kişi sayısına göre en uygun fiyatı çekelim
                var optionPrice = FindBestPrice(allPrices, (DateTime)dto.StartDate, (int)dto.ParticipantNumber);

                //her ihtimale karşı fiyatı hesaplanamayan bir servis çekilmişse aşağıda remove yapalımm
                if (optionPrice is not null)
                {
                    //çekilen fiaytları dbye kaydetmek için atama yapalım
                    option.Price = optionPrice.Price;
                    option.AdditionalServiceOptionPricesId = optionPrice.Id; // bu fiyatın direk idsi

                    var se = selectedAdditionals.Where(sad => sad.Id == option.AdditionalServiceId).First();
                    option.CalcPrice = optionPrice.Price * (se.IsPerPerson ? option.ParticipantNumber : 1) * (se.IsPerDay ? dto.DurationSelect : 1);


                    option.IsComissionValid = selectedAdditionalsComission.Where(x => x.ProductID == dto.ProductId && x.AdditionalServiceID == option.AdditionalServiceId).Select(x => x.ComissionRateAbility).First();
                }
            }

            //fiyatı olmayan addionallarıda ekleyelim dediler bunu kapattım o sebeple
            //dto.AdditionalServiceOptions.RemoveAll(option => option.Price == 0);



            //Rezervasyonile ilgili tüm borçlar burada oluşturulacak payment tablosuna
            //turun fiyatı kişi sayısı hesabı
            //additionallar ve kişi sayı hesabı yukarıda yapıldı zaten sadece paymente yazacağız
            //agent guide indirimi varsa onu ekleyeceğiz - additionallarda comission valid seçili ise one göre agent indirimi yapacağız
            //kupon için bir sonraki sayfada işlemler yapılacak - eğer agent veya guide ise onlara kupon gösterimi yapmayacağım.

            // tur bilgisi çekip fiyat hesabı yapalım
            //var tourDetail = _tourService.Where(tour => tour.ProductID == dto.ProductId);
            //şimdilik önyüzden ne geldiyse o fiyatır yazdırıyorum hesap doğrulama ve karşılaştırma aktif edilecek

            //Repoda ödeme kayıt işlemi yapacağım



            Guid reservationId = _reservationRepository.ReservationGeneralInfo(dto);
            _unitOfWork.saveChanges();

            var reservation = _repository.GetById(reservationId);

            var user = new User();
            if (reservation.UserId != null)
            {
                user = _userService.GetById((Guid)reservation.UserId);
            }


            //TODO: mail açılacak

            if (true)
            {
                SendMailTemplateDto template;
                string reservationLink = $"{_configuration.GetValue<string?>("ReservationCreatedAccessLink")}{reservation.ReservationCode}";
                reservationLink = reservationLink.Replace("{language}", dto.LanguagePrefix);
                if (user is not null)
                {
                    if (false && dto.ProductPrice == 0)
                    {
                        //TODO: ask for price açılacak
                        /*template = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.ReservationCreatedAskForPriceCustomer);
                        MailPlaceholderUtil.ReplaceMailContent(template, new(name: user.Name, surname: user.Surname)); //,  link: reservationLink*/
                    }
                    else
                    {
                        template = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonOlusturulduMember);
                        MailPlaceholderUtil.ReplaceMailContent(template, new(name: user.Name, surname: user.Surname, link: reservationLink));
                    }
                }
                else
                {
                    //buraya hiç girmeyecek ki
                    template = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonOlusturulduCustomer);
                    MailPlaceholderUtil.ReplaceMailContent(template, new(name: user.Name, surname: user.Surname, link: reservationLink));
                }


                SendMail mailForMember = new()
                {
                    CreateDate = DateTime.Now,
                    Content = template.Content,
                    Subject = template.Subject,
                    Email = user.Email,
                    IsDeleted = false,
                    Status = true,
                    SendTime = DateTime.Now.AddMinutes(1),
                };
                _mailSenderService.SendInstantMail(mailForMember); //mail gonderildi
            }
            return reservation.Id; //todo return reservation.Id
        }

        public RegisterReservationUser RegisterReservationUser(RegisterReservationUser dto)
        {

            var createUser = false;
            var returnUser = _userService.Where(u => u.Email == dto.Email).FirstOrDefault();

            // kullanıcı yoksa yeni customer oluştur
            // kullanıcı varsa ve rolü customerden farklıysa, membersa yada agentse filan hep yeni customer oluşturmak lazım.
            if (returnUser is not null)
            {
                if (returnUser.RoleTemplateId != ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Customer).OptionID)
                {
                    createUser = true;
                }
            }
            else
            {
                createUser = true;
            }


            if (createUser)
            {
                string fullName = dto.NameSurname;
                string ad = fullName;
                string soyad = "-";

                if (fullName.Contains(" "))
                {
                    string[] nameParts = fullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    ad = nameParts[0];
                    soyad = string.Join(" ", nameParts.Skip(1));
                }

                var newuserId = _userService.AddUsers(new RegisterPostDto
                {
                    Email = dto.Email,
                    Name = ad,
                    Surname = soyad,
                    Password = "00123456",
                    PasswordAgain = "00123456",
                    RoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Customer).OptionID
                });

                dto.UserId = newuserId;
            }
            else
            {
                dto.UserId = returnUser.Id;
            }

            return dto;
        }

        public WebReservationListDto ReservationInquiry(string code, int languageId)
        {
            return _reservationRepository.ReservationInquiry(code, languageId);
        }

        public List<ReservationListDto> ReservationList()
        {
            return _reservationRepository.ReservationList();
        }
        public List<ReservationPanelListDto> ReservationPanelList()
        {
            return _reservationRepository.ReservationPanelList();
        }
        public Guid ReservationParticipantInformation(ReservationParticipantInformationDto dto)
        {
            Guid reservationId = _reservationRepository.ReservationParticipantInformation(dto);
            _unitOfWork.saveChanges();
            return reservationId;
        }

        public Guid ReservationPaymentInformation(ReservationPaymentInformationDto dto)
        {
            Guid reservationId = _reservationRepository.ReservationPaymentInformation(dto);
            _unitOfWork.saveChanges();
            return reservationId;
        }

        public Guid CompleteWithoutPayment(CompleteWithoutPaymentDto dto)
        {
            Guid reservationId = _reservationRepository.CompleteWithoutPayment(dto);
            _unitOfWork.saveChanges();
            return reservationId;
        }

        public string ReservationSuccessAddPayment(string id, decimal amount)
        {

            _reservationPaymentService.Add(new ReservationPayment()
            {
                ReservationId = new Guid(id),
                DiscountRate = null,
                Price = (decimal)amount,
                IsDebt = false,
                CardHolderName = null,
                CardNumber = null,
                DebtTypeId = null,
                PaymentMethodId = 1,
                PaymentTypeId = 1,
            });

            _unitOfWork.saveChanges();
            return id;

        }

        public void SetReservationBookingStatus(Guid reservationId, int bookingStatus)
        {
            _reservationRepository.SetReservationBookingStatus(reservationId, bookingStatus);
            _unitOfWork.saveChanges();
        }

        public List<ReservationListDto> UncomplatedReservationList()
        {
            return _reservationRepository.UncomplatedReservationList();
        }
        public List<ReservationListDto> AskForPriceReservationList()
        {
            return _reservationRepository.AskForPriceReservationList();
        }

        public async Task<List<Customer_ReservationListItemDto>> MyReservationAsync(Guid userId, int languageId)
        {
            var myReservationList = await _reservationRepository.MyReservationAsync(userId, languageId);

            User user = await _userService.GetByIdAsync(userId);


            //TODO --> koşulları öğren
            myReservationList.ForEach(x =>
            {
                //isUpdatable ve isCancelable propları repodan cancellation time kontrol edilerek set ediliyor, true geldiyse update ve cancel request var mı kontrol et
                ////dbden sadece statusu 1 olan requestler geldiği için requestlerin statuslerini kontrol etmeyeceğiz

                //for update
                if (x.IsUpdatable)
                {
                    //if (x.EditRequests.Exists(x => x.RequestType == (int)Enums.ReservationEditRequestType.Update))
                    //{
                    //    x.IsUpdatable = !x.EditRequests.Any(x => x.RequestStatus == (int)ReservationEditRequestStatus.ReservationEditRequestStatusEnum.Beklemede);
                    //    x.HasUpdateRequest = true;
                    //}
                    //else
                    //{
                    bool isCompletedOrCancelled = x.ReservationStatus == (int)MyReservationStatus.No.Completed || x.ReservationStatus == (int)MyReservationStatus.No.Cancelled;

                    x.IsUpdatable = !isCompletedOrCancelled;
                    //}
                }

                //for cancel
                if (x.IsCancelable)
                {
                    //aktif bir tane update ve cancel request olabilir  
                    //var editRequest = x.EditRequests.Where(x => x.RequestType == (int)Enums.ReservationEditRequestType.Cancel).FirstOrDefault();
                    //if (editRequest is not null)
                    //{
                    //    x.IsCancelable = false;
                    //    x.HasCancelRequest = true;
                    //    var reason = _systemOptionService.GetSystemOptionById(Guid.Parse(editRequest.ReasonId.ToString()),
                    //        1).SystemOptionLanguageItems.FirstOrDefault()?.Name;
                    //    editRequest.Reason = reason;
                    //}
                    //else
                    //{
                    bool smallerThanRejected = x.ReservationStatus <= (int)MyReservationStatus.No.Rejected;

                    bool isNotCancelable = x.ReservationStatus == (int)MyReservationStatus.No.OnOperation || x.ReservationStatus == (int)MyReservationStatus.No.Completed || x.ReservationStatus == (int)MyReservationStatus.No.Cancelled;

                    //bool isCancelable = x.ReservationStatus != (int)MyReservationStatus.No.OnOperation && x.ReservationStatus != (int)MyReservationStatus.No.Completed && x.ReservationStatus != (int)MyReservationStatus.No.Cancelled;

                    x.IsCancelable = !isNotCancelable;
                    //}
                }


                ////for pay
                //x.IsPayable = x.ReservationStatus == (int)MyReservationStatus.No.NotCompletedReservationMainInformations || x.ReservationStatus == (int)MyReservationStatus.No.CompleteWithoutPay;
            });
            return myReservationList;
        }
        public static AdditionalServicePriceDto FindBestPrice(List<AdditionalServicePriceDto> optionPrices, DateTime givenDate, int givenPersonCount)
        {
            if (optionPrices == null || optionPrices.Count == 0)
            {
                return null;
            }
            /*public class PriceOption
 {
     public DateTime FromDate { get; set; }
     public DateTime ToDate { get; set; }
     public int PricePerson { get; set; }
     public int PricePersonEnd { get; set; }
     public int Priority { get; set; }
     public string Price { get; set; }
 }*/
            // Verilen tarihi ve kişi sayısını içeren fiyatları filtrele
            var validPrices = optionPrices
                .Where(price =>
                {
                    var fromDate = price.FromDate;
                    var toDate = price.ToDate;

                    // Eğer pricePerson ve pricePersonEnd değerleri sıfırsa bu şartı işleme sokma
                    if (price.PricePerson != 0 && price.PricePersonEnd != 0)
                    {
                        return fromDate <= givenDate && givenDate <= toDate &&
                               price.PricePerson <= givenPersonCount && givenPersonCount <= price.PricePersonEnd;
                    }
                    else
                    {
                        return fromDate <= givenDate && givenDate <= toDate;
                    }
                })
                .ToList();

            if (validPrices.Count == 0)
            {
                return null;
            }

            validPrices.Sort((a, b) => b.Priorty - a.Priorty
                                         != 0 ? b.Priorty - a.Priorty
                                         : (int)a.Price - (int)b.Price);

            // En uygun fiyat
            var bestPriceObj = validPrices[0];

            return bestPriceObj; //bestPriceObj.Price;
        }
        public void CancelReservationRequest(CancelReservationRequestPostDto dto)
        {
            Reservation reservation = _reservationRepository.GetReservationById(Guid.Parse(dto.ReservationId));
            User reservationUser = reservation.User;

            HandleCancelReservationRequestMails(dto, reservation, reservationUser);

            ReservationEditRequest request = new()
            {
                ReservationId = reservation.Id,
                ReasonId = Guid.Parse(dto.CancellationReasonId),
                Reason = dto.CancellationReasonText,
                RequestType = (int)Enums.ReservationEditRequestType.Cancel,
                RequestStatus = (int)ReservationEditRequestStatus.ReservationEditRequestStatusEnum.Beklemede
            };

            _reservationEditRequestRepository.Add(request);
            _unitOfWork.saveChanges();
        }

        public void UpdateReservationRequest(UpdateReservationRequestPostDto dto)
        {
            Reservation reservation = _reservationRepository.GetReservationById(Guid.Parse(dto.ReservationId));
            var reservationUser = reservation.User;

            HandleUpdateReservationRequestMails(dto, reservation, reservationUser);

            ReservationEditRequest request = new()
            {
                ReservationId = reservation.Id,
                Reason = dto.UpdateReasonText,
                RequestType = (int)Enums.ReservationEditRequestType.Update,
                RequestStatus = (int)ReservationEditRequestStatus.ReservationEditRequestStatusEnum.Beklemede
            };
            _reservationEditRequestRepository.Add(request);
            _unitOfWork.saveChanges();
        }

        public void SendReservationToOperation(string reservationId)
        {
            var reservation = _reservationRepository.GetReservationById(Guid.Parse(reservationId));
            _operationService.CreateOperation(reservation);
            reservation.BookingStatus = (int)MyReservationStatus.No.OnOperation;
            Update(reservation);
        }

        public async Task<List<Agent_ReservationListItemDto>> AgentReservationsAsync(Guid agentId)
        {
            var agentReservations = _reservationRepository.AgentReservations(agentId, 1);

            return agentReservations;
        }
        void HandleCancelReservationRequestMails(CancelReservationRequestPostDto dto, Reservation reservation, User reservationUser)
        {
            string reasonSelectListItem = _systemOptionRepository.GetSystemOptionByCategoryId(10, 1).Where(x => x.OptionID == Guid.Parse(dto.CancellationReasonId))?.FirstOrDefault()?.Option;
            #region adminMail
            Guid adminRoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Admin).OptionID;
            List<UserDto> adminUsers = _userService.GetUserListByRoleTemplate(adminRoleTemplateId.ToString());
            SendMailTemplateDto adminMailTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonIptalTalebiReceivedAdmin);
            foreach (var user in adminUsers)
            {
                MailPlaceholderUtil.ReplaceMailContent(adminMailTemplate, new(name: user.Name, surname: user.Surname, customerName: $"{reservationUser.Name} {reservationUser.Surname}", reservationCode: reservation.ReservationCode, cancellationReasonSelectListItem: reasonSelectListItem, cancellationReasonText: dto.CancellationReasonText, date: DateTime.Now.ToShortDateString()));

                var adminMail = new SendMail()
                {
                    Content = adminMailTemplate.Content,
                    Email = user.Email,
                    SendTime = DateTime.Now.AddMinutes(1),
                    Subject = adminMailTemplate.Subject,
                };
                _mailSenderService.ScheduleMailForSent(adminMail);
            }
            #endregion
            #region sendCustomerMail 

            SendMailTemplateDto customerOrMemberMailTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonIptalTalebiReceivedCustomerMember);
            MailPlaceholderUtil.ReplaceMailContent(customerOrMemberMailTemplate, new(customerName: $"{reservationUser.Name} {reservationUser.Surname}", reservationCode: reservation.ReservationCode, link: "")); //TODO link gömülecek

            SendMail userMail = new()
            {
                Content = customerOrMemberMailTemplate.Content,
                Email = reservationUser.Email,
                SendTime = DateTime.Now.AddMinutes(1),
                Subject = customerOrMemberMailTemplate.Subject,
            };
            _mailSenderService.SendInstantMail(userMail).GetAwaiter().GetResult();

            #endregion
            #region operationMail
            SendMailTemplateDto operationMailTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonIptalTalebiReceivedOperation);

            MailPlaceholderUtil.ReplaceMailContent(operationMailTemplate, new(referenceNumber: reservation.ReservationCode, customerName: $"{reservationUser.Name} {reservationUser.Surname}", tourPackageName: reservation.Product.ProductName, tourDate: reservation.StartDate.ToShortDateString(), numberOfParticipants: reservation.ParticipantNumber.ToString(), reasonForCancellation: $",{reasonSelectListItem} {dto.CancellationReasonText}"));

            SendMail operationMail = new()
            {
                Content = operationMailTemplate.Content,
                Email = SendMailAddressConstants.OperationMail,
                SendTime = DateTime.Now.AddMinutes(1),
                Subject = operationMailTemplate.Subject,
            };
            _mailSenderService.ScheduleMailForSent(operationMail);

            #endregion
            #region managerMail 
            SendMailTemplateDto managerMailtemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonIptalTalebiReceivedManager);
            MailPlaceholderUtil.ReplaceMailContent(managerMailtemplate, new(reservationCode: reservation.ReservationCode, customerName: $"{reservationUser.Name} {reservationUser.Surname}", tourPackageName: reservation.Product.ProductName, tourDate: reservation.StartDate.ToShortDateString(), numberOfParticipants: reservation.ParticipantNumber.ToString(), reasonForCancellation: $",{reasonSelectListItem} {dto.CancellationReasonText}"));
            SendMail managerMail = new()
            {
                Content = managerMailtemplate.Content,
                Email = SendMailAddressConstants.OperationMail,
                SendTime = DateTime.Now.AddMinutes(1),
                Subject = managerMailtemplate.Subject,
            };
            _mailSenderService.ScheduleMailForSent(managerMail);
            #endregion
            #region AgentMailler şimdilik kapalı

            #endregion

        }
        void HandleUpdateReservationRequestMails(UpdateReservationRequestPostDto dto, Reservation reservation, User reservationUser)
        {
            #region AdminMails
            SendMailTemplateDto adminMailTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonGuncellemeTalebiReceivedAdmin);
            Guid adminRoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Admin).OptionID;
            List<UserDto> adminUsers = _userService.GetUserListByRoleTemplate(adminRoleTemplateId.ToString());

            foreach (var user in adminUsers)
            {
                MailPlaceholderUtil.ReplaceMailContent(adminMailTemplate, new(name: user.Name, surname: user.Surname, username: $"{reservationUser.Name} {reservationUser.Surname}", reservationCode: reservation.ReservationCode, requestDate: DateTime.Now.ToShortDateString(), requestReason: dto.UpdateReasonText));

                _mailSenderService.ScheduleMailForSent(new()
                {
                    Content = adminMailTemplate.Content,
                    Email = user.Email,
                    SendTime = DateTime.Now.AddMinutes(1),
                    Subject = adminMailTemplate.Subject,
                });
            }
            #endregion
            #region OperationMail
            SendMailTemplateDto operationMailTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonGuncellemeTalebiReceivedOperation);

            MailPlaceholderUtil.ReplaceMailContent(operationMailTemplate, new(reservationCode: reservation.ReservationCode, username: $"{reservationUser.Name} {reservationUser.Surname}", tourPackageName: reservation.Product.ProductName, tourDate: reservation.StartDate.ToShortDateString(), numberOfParticipants: reservation.ParticipantNumber.ToString(), changes: dto.UpdateReasonText));
            _mailSenderService.ScheduleMailForSent(new()
            {
                Content = operationMailTemplate.Content,
                Email = SendMailAddressConstants.OperationMail,
                SendTime = DateTime.Now.AddMinutes(1),
                Subject = operationMailTemplate.Subject,
            });
            #endregion
            #region Customer/Member Mail
            if (reservationUser.RoleTemplateId == ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Customer).OptionID)
            {
                SendMailTemplateDto customerMailTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonGuncellemeTalebiReceivedCustomer);

                MailPlaceholderUtil.ReplaceMailContent(customerMailTemplate, new(guestName: $"{reservationUser.Name} {reservationUser.Surname}", reservationCode: reservation.ReservationCode, link: ""));
                _mailSenderService.SendInstantMail(new()
                {
                    Content = customerMailTemplate.Content,
                    Email = reservationUser.Email,
                    SendTime = DateTime.Now,
                    Subject = customerMailTemplate.Subject
                }).GetAwaiter().GetResult();
            }
            else if (reservationUser.RoleTemplateId == ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Member).OptionID)
            {
                SendMailTemplateDto memberMailTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonGuncellemeTalebiReceivedMember);

                MailPlaceholderUtil.ReplaceMailContent(memberMailTemplate, new(guestName: $"{reservationUser.Name} {reservationUser.Surname}", reservationCode: reservation.ReservationCode, link: ""));
                _mailSenderService.SendInstantMail(new()
                {
                    Content = memberMailTemplate.Content,
                    Email = reservationUser.Email,
                    SendTime = DateTime.Now,
                    Subject = memberMailTemplate.Subject
                }).GetAwaiter().GetResult();
            }
            #endregion
            #region ManagerMail
            SendMailTemplateDto managerMailTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonGuncellemeTalebiReceivedManager);
            MailPlaceholderUtil.ReplaceMailContent(managerMailTemplate, new(reservationCode: reservation.ReservationCode, username: $"{reservationUser.Name} {reservationUser.Surname}", tourPackageName: reservation.Product.ProductName, tourDate: reservation.StartDate.ToShortDateString(), numberOfParticipants: reservation.ParticipantNumber.ToString(), changes: dto.UpdateReasonText));
            _mailSenderService.ScheduleMailForSent(new()
            {
                Content = managerMailTemplate.Content,
                Email = SendMailAddressConstants.ManagerMail,
                SendTime = DateTime.Now.AddMinutes(1),
                Subject = managerMailTemplate.Subject,
            });
            #endregion
            #region AgentMailler şimdilik kapalı
            //SendMailTemplateDto agentMailTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonGuncellemeTalebiReceivedAgent);
            //MailPlaceholderUtil.ReplaceMailContent(agentMailTemplate, new(agencyName: "", reservationCode: reservation.ReservationCode));
            #endregion
        }

        public void ReservationPaymentSendMail(ReservationPaymentSendMailDto paymentSendMail)
        {
            var reservationPaymentSendMailTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.PaymentLinkMail);

            UserExtensionMail userExtensionMail = new()
            {
                Amount = paymentSendMail.Amount,
                Email = paymentSendMail.Email,
                ReservationId = paymentSendMail.ReservationId,
                UserId = Guid.Empty,
                MailTemplateType = reservationPaymentSendMailTemplate.Template,
                UrlCode = Guid.NewGuid(),
                ExpireDate = DateTime.Now.AddDays(paymentSendMail.ValidatyPeriod),
            };

            _userExtensionMailService.Add(userExtensionMail);

            //TODO : url yapısı değiştirilecek. appsettingste
            string sendPaymentUrl = $"{paymentSendMail.Url}{userExtensionMail.UrlCode}";

            MailPlaceholderUtil.ReplaceMailContent(reservationPaymentSendMailTemplate, new(link: sendPaymentUrl));

            SendMail reservationPaymentSendMail = new()
            {
                CreateDate = DateTime.Now,
                Content = reservationPaymentSendMailTemplate.Content,
                Subject = reservationPaymentSendMailTemplate.Subject,
                Email = paymentSendMail.Email,
                IsDeleted = false,
                Status = true,
                SendTime = DateTime.Now,
            };
            _sendMailService.Add(reservationPaymentSendMail);
            _mailSenderService.SendInstantMail(reservationPaymentSendMail);
        }

        public PaymentInquiryDto GetPaymentInquiryDto(Guid reservationId)
        {
            return _reservationRepository.GetPaymentInquiryDto(reservationId);
        }
    }
}
