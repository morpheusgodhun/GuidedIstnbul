using Core;
using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.ReservationDtos;
using Dto.ApiPanelDtos.UserDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Discount;
using Dto.ApiWebDtos.ApiToWebDtos.Payment;
using Dto.ApiWebDtos.ApiToWebDtos.Reservation;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Core.Enums;

namespace Data.Repository
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        Context _context;
        private readonly ITourPriceService _tourPriceService;
        private readonly ISystemOptionRepository _systemOptionsRepository;
        private readonly IReservationPaymentService _reservationPaymentService;

        public ReservationRepository(Context context, ITourPriceService tourPriceService, ISystemOptionRepository systemOptionsRepository, IReservationPaymentService reservationPaymentService) : base(context)
        {
            _context = context;
            _tourPriceService = tourPriceService;
            _systemOptionsRepository = systemOptionsRepository;
            _reservationPaymentService = reservationPaymentService;
        }
        public ReservationBillingInformationDto GetReservationBillingInformationDto(Guid reservationId, int languageId)
        {
            Reservation reservation = _context.Reservations.Find(reservationId);
            if (reservation.ReservationBillingInfoId == null)
            {
                _context.Reservations.Update(reservation);

                ReservationBillingInformationDto dto = new ReservationBillingInformationDto()
                {
                    ReservationId = reservationId,
                    IsIndividual = true
                };
                return dto;
            }
            else
            {
                ReservationBillingInfo billingInfo = _context.ReservationBillingInfos.Find(reservation.ReservationBillingInfoId);
                ReservationBillingInformationDto dto;
                if (billingInfo.IsIndividual)
                {
                    dto = new ReservationBillingInformationDto()
                    {
                        ReservationId = reservationId,
                        IsIndividual = billingInfo.IsIndividual,
                        Fullname = billingInfo.BillingFullname,
                        TcOrPassportNo = billingInfo.TcOrPassportNo,
                        EmailIndividual = billingInfo.BillingEmail,
                        PhoneIndividual = billingInfo.BillingPhoneNumber,
                        AddressIndividual = billingInfo.BillingAddress,

                    };

                }
                else
                {
                    dto = new ReservationBillingInformationDto()
                    {
                        ReservationId = reservationId,
                        IsIndividual = billingInfo.IsIndividual,
                        EmailCorporate = billingInfo.BillingEmail,
                        PhoneCorporate = billingInfo.BillingPhoneNumber,
                        AddressCorporate = billingInfo.BillingAddress,
                        TaxAdministration = billingInfo.BillingTaxAdministration,
                        TaxNumber = billingInfo.BillingTaxNumber,
                    };
                }

                return dto;
            }

        }
        public ReservationParticipantInformationDto GetReservationParticipantInformationDto(Guid reservationId, int languageId)
        {
            ////Reservation reservation = _context.Reservations.Where(x=>x.Id== reservationId).FirstOrDefault();
            //string eren1 = "";
            //var eren = new Guid(eren1);
            //var sds = _context.Reservations.Where(x => x.Id == eren); //.Find(reservationId);

            //var reservation = sds.FirstOrDefault();
            var reservation = _context.Reservations.Where(x => x.Id == reservationId).FirstOrDefault();
            ReservationParticipantInformationDto value = new ReservationParticipantInformationDto()
            {
                ReservationId = reservationId,
                PickUpPoint = NullableToString(reservation.PickUpPoint),
                ReservationNote = NullableToString(reservation.ReservationNote),
                Participants = (from participant in _context.ReservationParticipants.ToList()
                                where participant.ReservationFormId == reservationId
                                select new ParticipantDto()
                                {
                                    ParticipantId = participant.Id,
                                    Name = NullableToString(participant.Name),
                                    Surname = NullableToString(participant.Surname),
                                    BirthDate = participant.BirthDay == null ? "" : Convert.ToDateTime(participant.BirthDay).ToString("yyyy-MM-dd"),
                                }).ToList(),

            };

            //ilk değer boşsa rezervasyon yapan kişiyi ilk kişi olarak arayalımm
            if (value.Participants.First().Name == String.Empty)
            {
                var fullName = reservation.Fullname;

                if (fullName.Contains(' '))
                {
                    string[] nameParts = fullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    value.Participants[0].Name = nameParts[0];
                    value.Participants[0].Surname = string.Join(" ", nameParts.Skip(1));
                }
                else
                {
                    value.Participants[0].Name = fullName;
                    value.Participants[0].Surname = "Guest Surname 1";
                }
            }

            return value;
        }
        //Revize
        public ReservationPaymentInformationDto GetReservationPaymentInformationDto(Guid reservationId, int languageId)
        {

            Reservation reservation = _context.Reservations.Find(reservationId);

            ReservationPaymentInformationDto dto = new ReservationPaymentInformationDto()
            {
                TotalPrice = reservation.TotalPrice,
                ReservationId = reservationId,
                DepositoPrice = reservation.DepositoPrice,

            };

            return dto;
        }

        public PaymentInquiryDto GetPaymentInquiryDto(Guid reservationId)
        {

            Reservation reservation = _context.Reservations.Find(reservationId);

            PaymentInquiryDto dto = new PaymentInquiryDto()
            {
                Amount = reservation.TotalPrice,
                ReservationId = reservationId,
            };

            return dto;
        }
        public Guid ReservationBillingInformation(ReservationBillingInformationDto dto)
        {
            Reservation reservation = _context.Reservations.Find(dto.ReservationId);
            if (reservation.BookingStatus == (int)Bookingstatus.No.NotCompletedReservationParticipantInformations)
            {
                reservation.BookingStatus = (int)Bookingstatus.No.NotCompletedReservationBillingInformation;
                _context.Reservations.Update(reservation);
            }
            ReservationBillingInfo billingInfo = _context.ReservationBillingInfos.Find(reservation.ReservationBillingInfoId);
            if (billingInfo == null)
            {
                ReservationBillingInfo reservationBillingInfo;
                if (dto.IsIndividual)
                {
                    reservationBillingInfo = new ReservationBillingInfo()
                    {
                        ReservationId = reservation.Id,
                        IsIndividual = dto.IsIndividual,
                        BillingFullname = dto.Fullname,
                        TcOrPassportNo = dto.TcOrPassportNo,
                        BillingEmail = dto.EmailIndividual,
                        BillingPhoneNumber = dto.PhoneIndividual,
                        BillingAddress = dto.AddressIndividual,

                    };
                }
                else
                {
                    reservationBillingInfo = new ReservationBillingInfo()
                    {
                        ReservationId = reservation.Id,
                        IsIndividual = dto.IsIndividual,
                        BillingEmail = dto.EmailCorporate,
                        BillingPhoneNumber = dto.PhoneCorporate,
                        BillingAddress = dto.AddressCorporate,
                        BillingTaxAdministration = dto.TaxAdministration,
                        BillingTaxNumber = dto.TaxNumber
                    };
                }

                _context.ReservationBillingInfos.Add(reservationBillingInfo);
                reservation.ReservationBillingInfoId = reservationBillingInfo.Id;
                _context.Reservations.Update(reservation);
            }
            else
            {
                if (dto.IsIndividual)
                {
                    billingInfo.IsIndividual = dto.IsIndividual;
                    billingInfo.BillingFullname = dto.Fullname;
                    billingInfo.TcOrPassportNo = dto.TcOrPassportNo;
                    billingInfo.BillingEmail = dto.EmailIndividual;
                    billingInfo.BillingPhoneNumber = dto.PhoneIndividual;
                    billingInfo.BillingAddress = dto.AddressIndividual;
                }
                else
                {
                    billingInfo.IsIndividual = dto.IsIndividual;
                    billingInfo.BillingEmail = dto.EmailCorporate;
                    billingInfo.BillingPhoneNumber = dto.PhoneCorporate;
                    billingInfo.BillingAddress = dto.AddressCorporate;
                    billingInfo.BillingTaxAdministration = dto.TaxAdministration;
                    billingInfo.BillingTaxNumber = dto.TaxNumber;
                }


                _context.ReservationBillingInfos.Update(billingInfo);
            }



            return reservation.Id;
        }
        public Guid ReservationGeneralInfo(ReservationFormMainInformationDto dto)
        {
            if (dto.FindUsId is null)
            {
                //dto.FindUsId = new Guid(); //TODO: Bu niye guid - statik bişi verelim
                dto.FindUsId = Guid.Parse("6D16DC9B-3155-4B01-99DF-CDA577F1138B"); // Other
            }


            var product = _context.Products.Where(x => x.Id == dto.ProductId).FirstOrDefault();
            Tour? tour = _context.Tours.Where(x => x.ProductID == dto.ProductId).FirstOrDefault();

            #region default fiyat hesaplama
            /*_tourPriceService.TourPriceForDays(tour.Id,tour.Duration)*/ //todo: Sonradan tekrar hesapla burada. Dtodan gelen totalprice yanlış olabilir.
            var toplamOdeme = (decimal)dto.TotalPrice;

            // eğer işlemi yapan kişi agent ise ona göre member ise ona göre deposit fiyat hesaplaması

            var CalcDeposito = product.CustomerDeposito < 1 ? 1 : product.CustomerDeposito;
            var minPayPercent = product.MinimumPayoutPercent < 1 ? 1 : product.MinimumPayoutPercent;

            //userın rolü bir agent rolü ise
            var user = _context.Users.Where(x => x.Id == dto.UserId && x.Status && !x.IsDeleted).FirstOrDefault();

            if (ConstantRoles.AgentRoleTemplates.Contains(user.RoleTemplateId)) // user agent rollerinden birimi
            {
                CalcDeposito = product.AgentDeposito < 1 ? 1 : product.AgentDeposito;
            }

            //eğer deposito min pay oranının altında altında kalırsa min pay oranına göre hesap yap
            CalcDeposito = Math.Max(toplamOdeme * minPayPercent / 100, CalcDeposito);
            #endregion


            //eğer product price 0 geldiyse askfor pricea düşecek panelde yarı bir sayfada listelenecek
            var bookingStatus = dto.ProductPrice == 0 ? (int)Bookingstatus.No.AskForPrice : (int)Bookingstatus.No.NotCompletedReservationMainInformations;

            Reservation reservation = new Reservation()
            {
                DepositoPrice = CalcDeposito, //product.CustomerDeposito,
                TotalPrice = toplamOdeme,
                BookingStatus = bookingStatus,
                ReservationCode = ReservationCode(),
                ProductId = (Guid)dto.ProductId,
                StartDate = (DateTime)dto.StartDate,
                StartTimeId = (int)dto.StartTimeId,
                ParticipantNumber = (int)dto.ParticipantNumber,
                FindUsId = (Guid)dto.FindUsId,
                Fullname = dto.Fullname,
                Email = dto.Email,
                Phone = dto.Phone,
                Hotel = dto.Hotel,
                AgentId = dto.AgentId != null ? (Guid)dto.AgentId : null,
                AlsoInterestedNote = dto.AlsoInterestedNote,
                UserId = dto.UserId,
                SelectedDayCount = dto.DurationSelect
            };
            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            #region AdditionalServices
            decimal AdditionalTotalPrice = 0; // eğer alan kişi agent ise ve indirim yapılacaksa ona göre komisyon indirimi yapacağız IsComissionValid
            decimal AdditionalTotalComissionablePrice = 0; // additionallarda indirim yapılabilceklerin yoplamını alıp onlardan indirim yapıcam
            foreach (var additonalServiceOption in dto.AdditionalServiceOptions)
            {

                AdditionalTotalPrice += additonalServiceOption.CalcPrice;
                AdditionalTotalComissionablePrice += additonalServiceOption.IsComissionValid ? additonalServiceOption.CalcPrice : 0;

                Many_Reservation_AdditionalServiceOption many = new Many_Reservation_AdditionalServiceOption()
                {
                    AdditionalServiceOptionId = additonalServiceOption.OptionId,
                    ReservationId = reservation.Id,
                    Price = (decimal)additonalServiceOption.Price,
                    AdditionalServiceOptionPriceId = additonalServiceOption.AdditionalServiceOptionPricesId,
                    ParticipantNumber = additonalServiceOption.ParticipantNumber,
                    CalcPrice = additonalServiceOption.CalcPrice
                };
                _context.Many_Reservation_AdditionalServiceOptions.Add(many);
                if (additonalServiceOption.Inputs != null)
                {
                    foreach (var input in additonalServiceOption.Inputs)
                    {
                        ReservationOptionInput reservationOptionInput = new ReservationOptionInput()
                        {
                            Answer = input.Value,
                            Many_Reservation_AdditionalServiceOptionId = many.Id,
                            AdditionalServiceInputId = input.InputId
                        };
                        _context.ReservationOptionInputs.Add(reservationOptionInput);
                    }
                }

            }
            #endregion

            #region Participants
            for (int i = 0; i < dto.ParticipantNumber; i++)
            {
                ReservationParticipant participant = new ReservationParticipant()
                {
                    ReservationFormId = reservation.Id,

                };
                _context.ReservationParticipants.Add(participant);
            }
            #endregion

            #region Also İnterested
            foreach (var alsoInterestedId in dto.AlsoInterestedIDs)
            {
                Many_ReservationForm_AlsoInterested many = new Many_ReservationForm_AlsoInterested()
                {
                    SystemOptionId = alsoInterestedId,
                    ReservationFormId = reservation.Id
                };
                _context.Many_ReservationForm_AlsoInteresteds.Add(many);
            }
            #endregion

            #region Payments

            //hesaplanan turun fiyatı
            ReservationPayment debtProductPrice = new ReservationPayment()
            {
                ReservationId = reservation.Id,
                DebtTypeId = (int)PaymentDebtTypeList.No.Product,
                Price = (decimal)dto.ProductPrice,
                IsDebt = true,
            };
            _context.ReservationPayments.Add(debtProductPrice);

            //Tüm ekstra servislerin fiyatları
            ReservationPayment debtAdditionalsPrice = new ReservationPayment()
            {
                ReservationId = reservation.Id,
                DebtTypeId = (int)PaymentDebtTypeList.No.Additional,
                Price = AdditionalTotalPrice,
                IsDebt = true,
            };
            _context.ReservationPayments.Add(debtAdditionalsPrice);


            //agent ise ona göre comisyon indirimi yapalımm
            if (ConstantRoles.AgentRoleTemplates.Contains(user.RoleTemplateId)) // user agent rollerinden birimi
            {

                var tourDiscountRate = 0;
                var additionalDiscountRate = 0;

                //agentin discuntunu veya bu tura air özel discountunu çekelim
                //TODO: Kesinlikle düzeltilmeli tek seferde çekilsin
                var agentDefaults = (from agent in _context.Agents where agent.Id == user.AgentId && agent.Status && !agent.IsDeleted select agent).FirstOrDefault();
                var agentProductDiscount = (from agentpd in _context.Many_Agent_Products where agentpd.AgentId == user.AgentId && agentpd.ProductId == dto.ProductId && agentpd.Status && !agentpd.IsDeleted select agentpd).FirstOrDefault();

                //default indirimi ekleyelim
                tourDiscountRate = agentDefaults.DefaultTourDiscount;
                additionalDiscountRate = agentDefaults.ServicesDiscountRate;

                if (agentProductDiscount is not null)
                {
                    tourDiscountRate = agentProductDiscount.DiscountRate;
                }

                if (tourDiscountRate > 0 && dto.ProductPrice > 0)
                {
                    ReservationPayment comissionTourPrice = new ReservationPayment()
                    {
                        ReservationId = reservation.Id,
                        IsDebt = false,
                        Price = ((decimal)dto.ProductPrice * tourDiscountRate / 100),
                        DiscountRate = tourDiscountRate,
                        PaymentTypeId = (int)PaymentTypeList.No.Indirim,
                        PaymentMethodId = (int)PaymentMethodList.No.AcenteTurIndirimi,
                    };
                    _context.ReservationPayments.Add(comissionTourPrice);
                }


                if (additionalDiscountRate > 0 && AdditionalTotalComissionablePrice > 0)
                {
                    ReservationPayment comissionTourPrice = new ReservationPayment()
                    {
                        ReservationId = reservation.Id,
                        IsDebt = false,
                        Price = (AdditionalTotalComissionablePrice * additionalDiscountRate / 100),
                        DiscountRate = additionalDiscountRate,
                        PaymentTypeId = (int)PaymentTypeList.No.Indirim,
                        PaymentMethodId = (int)PaymentMethodList.No.AcenteServisIndirimi,
                    };
                    _context.ReservationPayments.Add(comissionTourPrice);
                }

            }

            #endregion

            return reservation.Id;
        }
        public Guid ReservationParticipantInformation(ReservationParticipantInformationDto dto)
        {
            foreach (var participant in dto.Participants)
            {

                ReservationParticipant reservationParticipant = _context.ReservationParticipants.Find(participant.ParticipantId);
                reservationParticipant.Name = participant.Name;
                reservationParticipant.Surname = participant.Surname;
                reservationParticipant.BirthDay = Convert.ToDateTime(participant.BirthDate);
                _context.ReservationParticipants.Update(reservationParticipant);
            }

            Reservation reservation = _context.Reservations.Find(dto.ReservationId);
            reservation.PickUpPoint = dto.PickUpPoint;
            reservation.ReservationNote = dto.ReservationNote;

            // ilk kayıtta 1 gelir durum main bilgiler girilmiş durumda -> tekrar geldiğinde durum değişmesin diye yapıyoruz
            // o sebeple kayıt tamamlanınca participentlar girilmiş durumdaya alıyoruz
            if (reservation.BookingStatus == (int)Bookingstatus.No.NotCompletedReservationMainInformations)
            {
                reservation.BookingStatus = (int)Bookingstatus.No.NotCompletedReservationParticipantInformations;
            }

            _context.Reservations.Update(reservation);

            return reservation.Id;
        }
        public Guid ReservationPaymentInformation(ReservationPaymentInformationDto dto)
        {
            Reservation reservation = _context.Reservations.Find(dto.ReservationId);

            decimal reservationPrice = (decimal)reservation.TotalPrice * 80 / 100;
            decimal kdvPrice = (decimal)reservation.TotalPrice * 20 / 100;

            ReservationPayment debtReservationPrice = new ReservationPayment()
            {
                ReservationId = reservation.Id,
                DebtTypeId = 1,
                Price = reservationPrice,
                IsDebt = true,
            };
            _context.ReservationPayments.Add(debtReservationPrice);

            ReservationPayment debtKdvPrice = new ReservationPayment()
            {
                ReservationId = reservation.Id,
                DebtTypeId = 2,
                Price = kdvPrice,
                IsDebt = true,
            };
            _context.ReservationPayments.Add(debtKdvPrice);

            if (dto.IsDeposit)
            {
                ReservationPayment depositPayment = new ReservationPayment()
                {
                    ReservationId = reservation.Id,
                    Price = reservation.DepositoPrice,
                    IsDebt = false,
                    PaymentMethodId = 1,
                    PaymentTypeId = 2,
                    CardHolderName = StringConvertCardHolderName(dto.CardHolderName),
                    CardNumber = StringConvertCardNumber(dto.CardNumber),
                };
                _context.ReservationPayments.Add(depositPayment);
            }
            else
            {
                ReservationPayment reservationPayment = new ReservationPayment()
                {
                    ReservationId = reservation.Id,
                    Price = reservation.TotalPrice,
                    IsDebt = false,
                    PaymentMethodId = 1,
                    PaymentTypeId = 1,
                    CardHolderName = StringConvertCardHolderName(dto.CardHolderName),
                    CardNumber = StringConvertCardNumber(dto.CardNumber),
                };
                _context.ReservationPayments.Add(reservationPayment);
            }

            reservation.BookingStatus = 4;
            _context.Reservations.Update(reservation);

            return reservation.Id;
        }
        public Guid CompleteWithoutPayment(CompleteWithoutPaymentDto dto)
        {
            Reservation reservation = _context.Reservations.Find(dto.ReservationId);

            decimal reservationPrice = (decimal)reservation.TotalPrice * 80 / 100;
            decimal kdvPrice = (decimal)reservation.TotalPrice * 20 / 100;

            ReservationPayment debtReservationPrice = new ReservationPayment()
            {
                ReservationId = reservation.Id,
                DebtTypeId = 1,
                Price = reservationPrice,
                IsDebt = true,
            };
            _context.ReservationPayments.Add(debtReservationPrice);

            ReservationPayment debtKdvPrice = new ReservationPayment()
            {
                ReservationId = reservation.Id,
                DebtTypeId = 2,
                Price = kdvPrice,
                IsDebt = true,
            };
            _context.ReservationPayments.Add(debtKdvPrice);



            reservation.BookingStatus = 13;
            _context.Reservations.Update(reservation);

            return reservation.Id;
        }
        public ReservationSuccessDto GetReservationSuccessDto(Guid reservationId, int languageId)
        {

            Reservation reservation = _context.Reservations.Find(reservationId);
            Product product = _context.Products.Find(reservation.ProductId);
            ProductLanguageItem productLanguageItem = _context.ProductLanguageItems.Where(x => x.ProductID == product.Id && x.LanguageID == languageId).FirstOrDefault();
            ReservationSuccessDto dto = new ReservationSuccessDto()
            {
                ProductName = productLanguageItem.DisplayName,
                ReservationCode = reservation.ReservationCode,
                ConstantValues = new List<ConstantValueDto>(),
                TotalPrice = reservation.TotalPrice,
                TourPrice = 0, //TODO tur fiyatı 
                StartDate = reservation.StartDate.ToString("dd-MM-yyyy"),
                //AlsoInterestedNote = reservation.AlsoInterestedNote,
                ReservationNote = NullableToString(reservation.ReservationNote),
                PickUpPoint = NullableToString(reservation.PickUpPoint),
                Participants = (from participant in _context.ReservationParticipants.ToList()
                                where participant.ReservationFormId == reservationId
                                select new ParticipantDto()
                                {
                                    Name = participant.Name,
                                    Surname = participant.Surname,
                                    BirthDate = Convert.ToDateTime(participant.BirthDay).ToString("dd-MM-yyyy")
                                }).ToList(),
                ParticipantNumber = reservation.ParticipantNumber,
                Plan = product.IsTour ? (from tour in _context.Tours.ToList()
                                         where tour.ProductID == reservation.ProductId
                                         join tourProgram in _context.TourPrograms.ToList()
                                         on tour.Id equals tourProgram.TourID
                                         orderby tourProgram.Day
                                         join tourProgramLanguageItem in _context.TourProgramLanguageItems.ToList()
                                         on tourProgram.Id equals tourProgramLanguageItem.TourProgramID
                                         where tourProgramLanguageItem.LanguageID == languageId
                                         select new TourDetailPlan()
                                         {
                                             Day = tourProgram.Day,
                                             DayTitle = tourProgramLanguageItem.Title,
                                             DayContent = tourProgramLanguageItem.Content
                                         }).ToList() : null,
                IsTour = product.IsTour,
                AdditionalServices = (from many in _context.Many_Reservation_AdditionalServiceOptions.ToList()
                                      where many.ReservationId == reservationId
                                      join option in _context.AdditionalServiceOptions.ToList()
                                      on many.AdditionalServiceOptionId equals option.Id
                                      join optionLanguageItem in _context.AdditionalServiceOptionLanguageItems.ToList()
                                      on option.Id equals optionLanguageItem.AdditionalServiceOptionID
                                      where optionLanguageItem.LanguageID == languageId
                                      join service in _context.AdditionalServices.ToList()
                                      on option.AdditionalServiceID equals service.Id
                                      join serviceLanguageItem in _context.AdditionalServiceLanguageItems.ToList()
                                      on service.Id equals serviceLanguageItem.AdditionalServiceID
                                      where serviceLanguageItem.LanguageID == languageId
                                      select new ReservationSuccessAdditionalServiceDto()
                                      {
                                          AdditionalServiceName = serviceLanguageItem.DisplayName,
                                          OptionName = optionLanguageItem.DisplayName,
                                          Price = many.Price,
                                          ParticipantNumber = many.ParticipantNumber
                                      }).ToList(),

            };
            var toura = _context.Tours.FirstOrDefault(x => x.Id == product.TourID);
            var tourDayCount = toura.SelectableDurations.Split(',', StringSplitOptions.RemoveEmptyEntries).Length;
            dto.IsDaysCanChange = tourDayCount > reservation.SelectedDayCount;


            return dto;
        }
        public string StringConvertCardHolderName(string holderName)
        {

            string holder = string.Empty;
            try
            {
                string[] names = holderName.Split(' ');

                for (int i = 0; i < names.Length; i++)
                {
                    holder += names[i].Substring(0, 2);
                    holder += "****";
                }
            }
            catch (Exception)
            {
                holder += "*** ****";
            }
            return holder;
        }
        public string StringConvertCardNumber(string cardNumber)
        {
            string number = string.Empty;
            try
            {
                number += cardNumber.Substring(0, 4);
                number += " **** **** ";
                number += cardNumber.Substring(12, 4);
            }
            catch (Exception)
            {
                number = "**** **** **** ****";

            }

            return number;

        }
        public string NullableToString(string value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {
                return value;
            }
        }
        public string ReservationCode()
        {
            string reservationCode = "RV";
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                reservationCode += rnd.Next(10);
            }


            return reservationCode;
        }
        public List<ReservationListDto> UncomplatedReservationList()
        {
            List<Dto.ApiPanelDtos.ReservationDtos.ReservationListDto> reservationList =
                (from reservation in _context.Reservations.ToList()
                 where !reservation.IsDeleted && reservation.BookingStatus != (int)Bookingstatus.No.AskForPrice && reservation.BookingStatus < 4
                 join product in _context.Products.ToList()
                 on reservation.ProductId equals product.Id
                 select new Dto.ApiPanelDtos.ReservationDtos.ReservationListDto()
                 {
                     ReservationId = reservation.Id,
                     ReservationCode = reservation.ReservationCode,
                     Booker = reservation.Fullname,
                     ProductName = product.ProductName,
                     StartDateTime = reservation.StartDate.ToString("dd-MM-yyyy") + " " + StartTimeList.StartTimes.FirstOrDefault(x => x.ID == reservation.StartTimeId).Value,
                     BookingStatus = new Bookingstatus().GetValue(reservation.BookingStatus),
                     BookingTime = reservation.CreateDate
                 }).OrderByDescending(x => x.BookingTime).ToList<Dto.ApiPanelDtos.ReservationDtos.ReservationListDto>();



            return reservationList;
        }
        public List<ReservationListDto> AskForPriceReservationList()
        {
            List<Dto.ApiPanelDtos.ReservationDtos.ReservationListDto> reservationList =
                (from reservation in _context.Reservations.ToList()
                 where !reservation.IsDeleted && reservation.BookingStatus == (int)Bookingstatus.No.AskForPrice
                 join product in _context.Products.ToList()
                 on reservation.ProductId equals product.Id
                 select new Dto.ApiPanelDtos.ReservationDtos.ReservationListDto()
                 {
                     ReservationId = reservation.Id,
                     ReservationCode = reservation.ReservationCode,
                     Booker = reservation.Fullname,
                     ProductName = product.ProductName,
                     StartDateTime = reservation.StartDate.ToString("dd-MM-yyyy") + " " + StartTimeList.StartTimes.FirstOrDefault(x => x.ID == reservation.StartTimeId).Value,
                     BookingStatus = new Bookingstatus().GetValue(reservation.BookingStatus),
                     BookingTime = reservation.CreateDate
                 }).OrderByDescending(x => x.BookingTime).ToList<Dto.ApiPanelDtos.ReservationDtos.ReservationListDto>();



            return reservationList;
        }

        public List<ReservationListDto> ReservationList()
        {
            List<Dto.ApiPanelDtos.ReservationDtos.ReservationListDto> reservationList =
                (from reservation in _context.Reservations.ToList()
                 where !reservation.IsDeleted /*&& reservation.BookingStatus == (int)Bookingstatus.No.AskForPrice TODO: Buna nereden geliyor bakalım ve açalım gerekiyorsa. heryere koydum çünkü bi burası kaldı*/ && reservation.BookingStatus >= 4 && reservation.BookingStatus != 11

                 join product in _context.Products.ToList()
                 on reservation.ProductId equals product.Id
                 select new Dto.ApiPanelDtos.ReservationDtos.ReservationListDto()
                 {
                     ReservationId = reservation.Id,
                     ReservationCode = reservation.ReservationCode,
                     Booker = reservation.Fullname,
                     ProductName = product.ProductName,
                     StartDateTime = reservation.StartDate.ToString("dd-MM-yyyy") + " " + StartTimeList.StartTimes.FirstOrDefault(x => x.ID == reservation.StartTimeId).Value,
                     BookingStatus = new Bookingstatus().GetValue(reservation.BookingStatus),
                     BookingTime = reservation.CreateDate
                 }).OrderByDescending(x => x.BookingTime).ToList<Dto.ApiPanelDtos.ReservationDtos.ReservationListDto>();



            return reservationList;
        }
        public List<ReservationPanelListDto> ReservationPanelList()
        {
            List<Dto.ApiPanelDtos.ReservationDtos.ReservationPanelListDto> reservationPanelList =
                (from reservation in _context.Reservations.ToList()
                 where !reservation.IsDeleted && reservation.BookingStatus != (int)Bookingstatus.No.AskForPrice && reservation.BookingStatus >= 4 && reservation.BookingStatus != 11
                 join product in _context.Products.ToList()
                 on reservation.ProductId equals product.Id
                 join requests in _context.ReservationEditRequests on reservation.Id equals requests.ReservationId into requestGroup
                 select new Dto.ApiPanelDtos.ReservationDtos.ReservationPanelListDto()
                 {
                     ReservationId = reservation.Id,
                     ReservationCode = reservation.ReservationCode,
                     Booker = reservation.Fullname,
                     ProductName = product.ProductName,
                     StartDateTime = reservation.StartDate.ToString("dd-MM-yyyy") + " " + StartTimeList.StartTimes.FirstOrDefault(x => x.ID == reservation.StartTimeId).Value,
                     BookingStatus = new Bookingstatus().GetValue(reservation.BookingStatus),
                     BookingTime = reservation.CreateDate,
                     HasActiveCancelRequest = requestGroup.Any(x => x.RequestType == (int)Enums.ReservationEditRequestType.Cancel && x.RequestStatus == (int)ReservationEditRequestStatus.ReservationEditRequestStatusEnum.Beklemede),
                     HasActiveUpdateRequest = requestGroup.Any(x => x.RequestType == (int)Enums.ReservationEditRequestType.Update && x.RequestStatus == (int)ReservationEditRequestStatus.ReservationEditRequestStatusEnum.Beklemede)

                 }).OrderByDescending(x => x.BookingTime).ToList<Dto.ApiPanelDtos.ReservationDtos.ReservationPanelListDto>();

            /*
             * 
             * CancelRequestDtos = requestGroup.Where(req => req.RequestType == (int)Enums.ReservationEditRequestType.Cancel).Select(x => new ReservationCancelRequestDto
                     {
                         AdminAnswer = x.AdminAnswer,
                         Reason = _systemOptionsRepository.GetSystemOptionById((Guid)x.ReasonId, 1).SystemOptionLanguageItems.FirstOrDefault(x => x.LanguageID == 1).Name,
                         ReasonText = x.Reason,
                         RequestStatus = ReservationEditRequestStatus.GetValue(x.RequestStatus),
                         ReservationCode = reservation.ReservationCode
                     }).ToList(),
                     UpdateRequestDtos = requestGroup.Where(req => req.RequestType == (int)Enums.ReservationEditRequestType.Update).Select(x => new ReservationUpdateRequestDto
                     {
                         AdminAnswer = x.AdminAnswer,
                         Reason = x.Reason,
                         RequestStatus = ReservationEditRequestStatus.GetValue(x.RequestStatus),
                         ReservationCode = reservation.ReservationCode

                     }).ToList()
            */

            return reservationPanelList;
        }
        public List<ReservationListDto> ComplatedReservationList()
        {
            List<Dto.ApiPanelDtos.ReservationDtos.ReservationListDto> reservationList = (from reservation in _context.Reservations.ToList()
                                                                                         where !reservation.IsDeleted && reservation.BookingStatus == 11
                                                                                         join product in _context.Products.ToList()
                                                                                         on reservation.ProductId equals product.Id
                                                                                         select new Dto.ApiPanelDtos.ReservationDtos.ReservationListDto()
                                                                                         {
                                                                                             ReservationId = reservation.Id,
                                                                                             ReservationCode = reservation.ReservationCode,
                                                                                             Booker = reservation.Fullname,
                                                                                             ProductName = product.ProductName,
                                                                                             StartDateTime = reservation.StartDate.ToString("dd-MM-yyyy") + " " + StartTimeList.StartTimes.FirstOrDefault(x => x.ID == reservation.StartTimeId).Value,
                                                                                             BookingStatus = new Bookingstatus().GetValue(reservation.BookingStatus),
                                                                                             BookingTime = reservation.CreateDate
                                                                                         }).ToList<Dto.ApiPanelDtos.ReservationDtos.ReservationListDto>();



            return reservationList;
        }
        public void SetReservationBookingStatus(Guid reservationId, int bookingStatus)
        {
            Reservation reservation = _context.Reservations.Find(reservationId);
            reservation.BookingStatus = bookingStatus;
            _context.Reservations.Update(reservation);
        }
        public ReservationFormDetailDto GetReservationFormDetailDto(Guid reservationId)
        {
            ReservationFormDetailDto reservationFormDetail = (from reservation in _context.Reservations.ToList()
                                                              where reservation.Id == reservationId
                                                              join product in _context.Products.ToList()
                                                              on reservation.ProductId equals product.Id
                                                              select new ReservationFormDetailDto()
                                                              {
                                                                  ReservationId = reservation.Id,
                                                                  ReservationCode = reservation.ReservationCode,
                                                                  BookingStatus = reservation.BookingStatus,
                                                                  ProductName = product.ProductName,
                                                                  StartDateTime = reservation.StartDate.ToString("dd-MM-yyyy") + " " + StartTimeList.StartTimes.FirstOrDefault(x => x.ID == reservation.StartTimeId).Value,
                                                                  ParticipantNumber = reservation.ParticipantNumber,
                                                                  FindUs = (from systemOptionLanguageItem in _context.SystemOptionLanguageItems.ToList()
                                                                            where systemOptionLanguageItem.SystemOptionId == reservation.FindUsId
                                                                            where systemOptionLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                                                                            select systemOptionLanguageItem.Name).FirstOrDefault(),
                                                                  ContactFullname = reservation.Fullname,
                                                                  ContactEmail = reservation.Email,
                                                                  ContactPhone = reservation.Phone,
                                                                  Hotel = reservation.Hotel,
                                                                  AlsoInterestedNote = reservation.AlsoInterestedNote,
                                                                  ReservationNote = NullableToString(reservation.ReservationNote),
                                                                  PickUpPoint = NullableToString(reservation.PickUpPoint),
                                                                  AlsoInterested = (from many in _context.Many_ReservationForm_AlsoInteresteds.ToList()
                                                                                    where many.ReservationFormId == reservation.Id
                                                                                    join systemOptionLanguage in _context.SystemOptionLanguageItems.ToList()
                                                                                    on many.SystemOptionId equals systemOptionLanguage.SystemOptionId
                                                                                    where systemOptionLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                                                    select systemOptionLanguage.Name).ToList(),
                                                                  Participants = (from participant in _context.ReservationParticipants.ToList()
                                                                                  where participant.ReservationFormId == reservation.Id
                                                                                  select new ParticipantDto()
                                                                                  {
                                                                                      ParticipantId = participant.Id,
                                                                                      Name = NullableToString(participant.Name),
                                                                                      Surname = NullableToString(participant.Surname),
                                                                                      BirthDate = participant.BirthDay == null ? "" : Convert.ToDateTime(participant.BirthDay).ToString("yyyy-MM-dd"),
                                                                                  }).ToList(),
                                                                  AdditionalServices = (from many in _context.Many_Reservation_AdditionalServiceOptions.ToList()
                                                                                        where many.ReservationId == reservationId
                                                                                        join option in _context.AdditionalServiceOptions.ToList()
                                                                                        on many.AdditionalServiceOptionId equals option.Id
                                                                                        join optionLanguageItem in _context.AdditionalServiceOptionLanguageItems.ToList()
                                                                                        on option.Id equals optionLanguageItem.AdditionalServiceOptionID
                                                                                        where optionLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                                                                                        join service in _context.AdditionalServices.ToList()
                                                                                        on option.AdditionalServiceID equals service.Id
                                                                                        join serviceLanguageItem in _context.AdditionalServiceLanguageItems.ToList()
                                                                                        on service.Id equals serviceLanguageItem.AdditionalServiceID
                                                                                        where serviceLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                                                                                        select new ReservationFormDetailAdditionalServiceDto()
                                                                                        {
                                                                                            AdditionalServiceName = serviceLanguageItem.DisplayName,
                                                                                            AdditionalServiceOptionName = optionLanguageItem.DisplayName,
                                                                                            ParcitipantNumber = many.ParticipantNumber,
                                                                                            Price = many.Price
                                                                                        }).ToList(),
                                                                  BillingInfo = (from billingInfo in _context.ReservationBillingInfos.ToList()
                                                                                 where billingInfo.ReservationId == reservation.Id
                                                                                 select new ReservationFormDetailBillingInfoDto()
                                                                                 {

                                                                                     IsIndividual = billingInfo.IsIndividual,
                                                                                     Address = billingInfo.BillingAddress,
                                                                                     Email = billingInfo.BillingEmail,
                                                                                     Phone = billingInfo.BillingPhoneNumber,
                                                                                     Fullname = billingInfo.BillingFullname,
                                                                                     TaxAdministration = billingInfo.BillingTaxAdministration,
                                                                                     TaxNumber = billingInfo.BillingTaxNumber,
                                                                                     TcOrPassportNo = billingInfo.TcOrPassportNo,
                                                                                 }).FirstOrDefault(),
                                                                  ReservationPayments = _reservationPaymentService.PaymentListByReservationId(reservationId)
                                                              }).FirstOrDefault();

            return reservationFormDetail;
        }
        public PaymentReservationDetailDto GetPaymentReservationDetailDto(Guid reservationId, int languageId)
        {
            PaymentReservationDetailDto value = (from reservation in _context.Reservations.ToList()
                                                 where reservation.Id == reservationId
                                                 select new PaymentReservationDetailDto()
                                                 {
                                                     ReservationId = reservation.Id,
                                                     StartDate = reservation.StartDate.ToString("dd-MM-yyyy"),
                                                     StartTime = StartTimeList.GetValue(reservation.StartTimeId),
                                                     ParticipantNumber = reservation.ParticipantNumber,
                                                     DayNumber = (int)reservation.SelectedDayCount,
                                                     TotalPrice = reservation.TotalPrice,
                                                     DepositoPrice = reservation.DepositoPrice,
                                                     CancellationPolicy = (from product in _context.Products.ToList()
                                                                           where product.Id == reservation.ProductId
                                                                           join cancellationLanguage in _context.CancellationPolicyLanguageItems.ToList()
                                                                           on product.CancellationPolicyID equals cancellationLanguage.CancellationPolicyID
                                                                           where cancellationLanguage.LangaugeID == languageId
                                                                           select cancellationLanguage.Content).FirstOrDefault(),
                                                     Products = (from many in _context.Many_Reservation_AdditionalServiceOptions.ToList()
                                                                 where many.ReservationId == reservationId
                                                                 join option in _context.AdditionalServiceOptions.ToList()
                                                                 on many.AdditionalServiceOptionId equals option.Id
                                                                 join optionLanguageItem in _context.AdditionalServiceOptionLanguageItems.ToList()
                                                                 on option.Id equals optionLanguageItem.AdditionalServiceOptionID
                                                                 where optionLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                                                                 join service in _context.AdditionalServices.ToList()
                                                                 on option.AdditionalServiceID equals service.Id
                                                                 join serviceLanguageItem in _context.AdditionalServiceLanguageItems.ToList()
                                                                 on service.Id equals serviceLanguageItem.AdditionalServiceID
                                                                 where serviceLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                                                                 select new ReservationFormDetailAdditionalServiceDto()
                                                                 {
                                                                     AdditionalServiceName = serviceLanguageItem.DisplayName,
                                                                     AdditionalServiceOptionName = optionLanguageItem.DisplayName,
                                                                     Price = many.CalcPrice,
                                                                     ParcitipantNumber = many.ParticipantNumber
                                                                 }).ToList(),
                                                     PaymentDiscountDto = (from discount in _context.Discounts.ToList()
                                                                           where reservation.DiscountId == discount.Id
                                                                           select new PaymentDiscountDto
                                                                           {
                                                                               CouponCode = discount.CouponCode,
                                                                               DiscountAmount = discount.DiscountAmount,
                                                                               DiscountType = (int)Enum.Parse(typeof(DiscountType), discount.DiscountType, true)
                                                                           }).FirstOrDefault()
                                                 }).FirstOrDefault();
            return value;
        }
        public PaymentAgentInfoDto GetPaymentAgentInfo(Guid productId, Guid agentId)
        {
            //TODO: Kesinlikle düzeltilmeli
            var agentDefaults = (from agent in _context.Agents
                                 where agent.Id == agentId
                                 select agent).FirstOrDefault();

            var agentProductDiscount = (from agentpd in _context.Many_Agent_Products
                                        where agentpd.AgentId == agentId && agentpd.ProductId == productId
                                        select agentpd).FirstOrDefault();

            var special = -1;
            if (agentProductDiscount is not null)
            {
                special = agentProductDiscount?.DiscountRate ?? -1;
            }

            PaymentAgentInfoDto value = (from product in _context.Products
                                         where product.Id == productId
                                         select new PaymentAgentInfoDto()
                                         {
                                             DefaultTourDiscount = agentDefaults.DefaultTourDiscount,
                                             ServicesDiscountRate = agentDefaults.ServicesDiscountRate,
                                             WithoutPay = agentDefaults.WithoutPay,

                                             SpecialDiscount = special,

                                             SpecialServicesDiscount = -1,
                                             SpecialTourDiscount = -1
                                         }
                                           ).FirstOrDefault();
            return value;
        }
        public WebReservationListDto ReservationInquiry(string code, int languageId)
        {
            WebReservationListDto dto = (from reservation in _context.Reservations.ToList()
                                         where reservation.ReservationCode == code
                                         select new WebReservationListDto()
                                         {
                                             ReservationId = reservation.Id,
                                             ReservationCode = reservation.ReservationCode,
                                             StartDate = reservation.StartDate.ToString("dd-MM-yyyy"),
                                             CommentSendable = reservation.BookingStatus == 11,
                                             Status = reservation.BookingStatus == 11 ? MyReservationStatus.GetValue(3) : (reservation.BookingStatus < 4 ? MyReservationStatus.GetValue(1) : MyReservationStatus.GetValue(2)),
                                             Price = reservation.TotalPrice,
                                             Cancellable = (from product in _context.Products.ToList()
                                                            where reservation.ProductId == product.Id
                                                            join cancellationPolicy in _context.CancellationPolicies.ToList()
                                                            on product.CancellationPolicyID equals cancellationPolicy.Id
                                                            select ((reservation.StartDate - DateTime.Now).TotalMinutes <= (cancellationPolicy.UncancellableHours * 60))).FirstOrDefault(),
                                             Updateable = (from product in _context.Products.ToList()
                                                           where reservation.ProductId == product.Id
                                                           join cancellationPolicy in _context.CancellationPolicies.ToList()
                                                           on product.CancellationPolicyID equals cancellationPolicy.Id
                                                           select ((reservation.StartDate - DateTime.Now).TotalMinutes <= (cancellationPolicy.UncancellableHours * 60))).FirstOrDefault(),
                                             Type = (from product in _context.Products.ToList()
                                                     where reservation.ProductId == product.Id
                                                     select product.IsTour ? "Tour" : "Service").FirstOrDefault(),
                                             ProductName = (from product in _context.Products.ToList()
                                                            where reservation.ProductId == product.Id
                                                            join productLanguageItem in _context.ProductLanguageItems.ToList()
                                                            on product.Id equals productLanguageItem.ProductID
                                                            where productLanguageItem.LanguageID == languageId
                                                            select productLanguageItem.DisplayName).FirstOrDefault()
                                         }).FirstOrDefault();


            return dto;
        }
        public ReservationDetailDto ReservationDetail(Guid reservationId, int languageId)
        {
            Reservation reservation = _context.Reservations.Find(reservationId);
            Product product = _context.Products.Find(reservation.ProductId);
            ProductLanguageItem productLanguageItem = _context.ProductLanguageItems.Where(x => x.ProductID == product.Id && x.LanguageID == languageId).FirstOrDefault();
            ReservationDetailDto dto = new ReservationDetailDto()
            {
                ProductName = productLanguageItem.DisplayName,
                ReservationCode = reservation.ReservationCode,
                ConstantValues = new List<ConstantValueDto>(),
                TotalPrice = reservation.TotalPrice,
                StartDate = reservation.StartDate.ToString("dd-MM-yyyy"),
                //AlsoInterestedNote = reservation.AlsoInterestedNote,
                ReservationNote = NullableToString(reservation.ReservationNote),
                PickUpPoint = NullableToString(reservation.PickUpPoint),
                Participants = (from participant in _context.ReservationParticipants.ToList()
                                where participant.ReservationFormId == reservationId
                                select new ParticipantDto()
                                {
                                    Name = participant.Name,
                                    Surname = participant.Surname,
                                    BirthDate = Convert.ToDateTime(participant.BirthDay).ToString("dd-MM-yyyy")
                                }).ToList(),

                Plan = product.IsTour ? (from tour in _context.Tours.ToList()
                                         where tour.ProductID == reservation.ProductId
                                         join tourProgram in _context.TourPrograms.ToList()
                                         on tour.Id equals tourProgram.TourID
                                         orderby tourProgram.Day
                                         join tourProgramLanguageItem in _context.TourProgramLanguageItems.ToList()
                                         on tourProgram.Id equals tourProgramLanguageItem.TourProgramID
                                         where tourProgramLanguageItem.LanguageID == languageId
                                         select new TourDetailPlan()
                                         {
                                             Day = tourProgram.Day,
                                             DayTitle = tourProgramLanguageItem.Title,
                                             DayContent = tourProgramLanguageItem.Content
                                         }).ToList() : null,
                IsTour = product.IsTour,
                AdditionalServices = (from many in _context.Many_Reservation_AdditionalServiceOptions.ToList()
                                      where many.ReservationId == reservationId
                                      join option in _context.AdditionalServiceOptions.ToList()
                                      on many.AdditionalServiceOptionId equals option.Id
                                      join optionLanguageItem in _context.AdditionalServiceOptionLanguageItems.ToList()
                                      on option.Id equals optionLanguageItem.AdditionalServiceOptionID
                                      where optionLanguageItem.LanguageID == languageId
                                      join service in _context.AdditionalServices.ToList()
                                      on option.AdditionalServiceID equals service.Id
                                      join serviceLanguageItem in _context.AdditionalServiceLanguageItems.ToList()
                                      on service.Id equals serviceLanguageItem.AdditionalServiceID
                                      where serviceLanguageItem.LanguageID == languageId
                                      select new ReservationSuccessAdditionalServiceDto()
                                      {
                                          AdditionalServiceName = serviceLanguageItem.DisplayName,
                                          OptionName = optionLanguageItem.DisplayName,
                                          Price = many.Price
                                      }).ToList(),
                BookingStatus = reservation.BookingStatus,
                User = (from user in _context.Users
                        where user.Id == reservation.UserId
                        select new UserDto
                        {
                            UserID = user.Id.ToString(),
                            Name = user.Name,
                            Surname = user.Surname,
                            Email = user.Email,
                            Roles = new(){
                                user.RoleTemplateId.ToString()
                            }
                        }).FirstOrDefault()

            };

            return dto;
        }
        public Reservation GetReservationById(Guid reservationId)
        {
            var reservation = _context.Reservations.Include(x => x.User).Include(x => x.Product).ThenInclude(p => p.Tour).ThenInclude(x => x.Many_Tour_TourCategories).Include(x => x.Many_Reservation_AdditionalServiceOptions).
                ThenInclude(x => x.AdditionalServiceOption).ThenInclude(o => o.AdditionalService).
                FirstOrDefault(x => x.Id == reservationId);

            return reservation;
        }
        public async Task<List<Customer_ReservationListItemDto>> MyReservationAsync(Guid userId, int languageId)
        {
            MyReservationStatus status = new MyReservationStatus();
            List<Customer_ReservationListItemDto> dto = await (from reservation in _context.Reservations
                                                                   //join editRequest in _context.ReservationEditRequests on reservation.Id equals editRequest.ReservationId
                                                                   //into requestGroup
                                                               where reservation.UserId == userId
                                                               select new Customer_ReservationListItemDto()
                                                               {
                                                                   ReservationID = reservation.Id.ToString(),
                                                                   IsCommentSendable = reservation.BookingStatus == (int)Bookingstatus.No.Completed,
                                                                   IsPayable = false,//todo:
                                                                   IsCancelable = (from product in _context.Products
                                                                                   where reservation.ProductId == product.Id
                                                                                   join cancellationPolicy in _context.CancellationPolicies
                                                                                   on product.CancellationPolicyID equals cancellationPolicy.Id
                                                                                   select ((reservation.StartDate - DateTime.Now).TotalMinutes <= (cancellationPolicy.UncancellableHours * 60))).FirstOrDefault(),
                                                                   IsUpdatable = (from product in _context.Products
                                                                                  where reservation.ProductId == product.Id
                                                                                  join cancellationPolicy in _context.CancellationPolicies
                                                                                  on product.CancellationPolicyID equals cancellationPolicy.Id
                                                                                  select ((reservation.StartDate - DateTime.Now).TotalMinutes <= (cancellationPolicy.UncancellableHours * 60))).FirstOrDefault(),
                                                                   //TODO: type alanı ReservationInquiry gibi deği başka birşeyi temsil ediyor
                                                                   ReservationType = 1,/*(from product in _context.Products.ToList()
                                                                   where reservation.ProductId == product.Id
                                                                   select product.IsTour ? "Tour" : "Service").FirstOrDefault(),*/
                                                                   ReservationStatus = reservation.BookingStatus,
                                                                   ReservationNumber = reservation.ReservationCode,
                                                                   ReservationPrice = reservation.TotalPrice,
                                                                   Date = reservation.StartDate,
                                                                   //EditRequests = requestGroup.Select(x => new ReservationEditRequestDto
                                                                   //{
                                                                   //    ReservationId = x.ReservationId,
                                                                   //    AdminAnswer = x.AdminAnswer,
                                                                   //    ReasonId = x.ReasonId,
                                                                   //    ReasonText = x.Reason,
                                                                   //    RequestStatus = x.RequestStatus,
                                                                   //    RequestType = x.RequestType
                                                                   //}).ToList()
                                                                   /*
                                                                    ProductName = (from product in _context.Products.ToList()
                                                                                   where reservation.ProductId == product.Id
                                                                                   join productLanguageItem in _context.ProductLanguageItems.ToList()
                                                                                   on product.Id equals productLanguageItem.ProductID
                                                                                   where productLanguageItem.LanguageID == languageId
                                                                                   select productLanguageItem.DisplayName).FirstOrDefault()*/
                                                               }).ToListAsync();


            return dto;
        }
        public List<Agent_ReservationListItemDto> AgentReservations(Guid agentId, int languageId)
        {

            List<Agent_ReservationListItemDto> dto = (from reservation in _context.Reservations
                                                      join user in _context.Users on reservation.UserId equals user.Id into userGroup
                                                      from user in userGroup.DefaultIfEmpty()
                                                      where reservation.AgentId == agentId || (user != null && user.AgentId == agentId)
                                                      select new Agent_ReservationListItemDto()
                                                      {
                                                          ReservationID = reservation.Id.ToString(),
                                                          ReservationType = 1,
                                                          ReservationStatus = MyReservationStatus.GetValue(reservation.BookingStatus),
                                                          ReservationCode = reservation.ReservationCode,
                                                          ReservationPrice = reservation.TotalPrice,
                                                          Date = reservation.StartDate,
                                                          Username = $"{user.Name} {user.Surname}"
                                                      }).ToList();

            //return dto;


            return dto;
        }
    }
}
