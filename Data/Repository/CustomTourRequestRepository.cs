using Azure.Identity;
using Core.Entities;
using Core.IRepository;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.CustomTourDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Home;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.WebToApiDtos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Core.StaticValues.CustomTourOfferStatus;

namespace Data.Repository
{
    public class CustomTourRequestRepository : GenericRepository<CustomTourRequest>, ICustomTourRequestRepository
    {
        Context _context;

        public CustomTourRequestRepository(Context context) : base(context)
        {
            _context = context;
        }

        public CustomMadeTourPostDto AddRequest(CustomMadeTourPostDto customTour)
        {

            CustomTourRequest request = new CustomTourRequest()
            {
                ArrivalDate = customTour.ArrivalDate,
                DepartureDate = customTour.DeperatureDate,
                ParticipantNumber = customTour.NumberOfAdult,
                Fullname = customTour.CustomerName,
                Email = customTour.CustomerEmail,
                Phone = customTour.CustomerPhone,
                CountryId = customTour.CountryID,
                LanguageId = customTour.LanguageID,
                OfferStatusId = (int)CustomTourOfferStatus.OfferStatus.WaitingAnswer,
                FindUsId = customTour.HowFindUsID,
                RequestNote = customTour.CustomerNote,
                UserId = customTour.MemberId,
            };

            //MemberId = customTour.MemberId,

            _context.CustomTourRequests.Add(request);
            _context.SaveChanges();
            customTour.RequestId = request.Id; // geri dönerken lazım olabilir

            foreach (var destinationId in customTour.DestinationIDs)
            {
                Many_CustomTourRequest_Destination many = new Many_CustomTourRequest_Destination()
                {
                    CustomTourRequestId = request.Id,
                    DestinationId = destinationId
                };

                _context.Many_CustomTourRequest_Destinations.Add(many);
            }

            foreach (var alsoInterestedId in customTour.AlsoInterestedIDs)
            {
                Many_CustomTourRequest_AlsoInterested many = new Many_CustomTourRequest_AlsoInterested()
                {
                    CustomTourRequestId = request.Id,
                    SystemOptionId = alsoInterestedId
                };

                _context.Many_CustomTourRequest_AlsoInteresteds.Add(many);
            }

            return customTour;
        }

        public List<CustomTourRequestListItemDto> CustomTourRequestList()
        {

            List<CustomTourRequestListItemDto> requestList = (from request in _context.CustomTourRequests
                                                              where !request.IsDeleted && request.Status
                                                              select new CustomTourRequestListItemDto
                                                              {
                                                                  RequestId = request.Id,
                                                                  //MemberId = request.UserId,
                                                                  MailContent = request.MailContent,
                                                                  SenderName = request.Fullname,
                                                                  SenderEmail = request.Email,
                                                                  Price = (from offer in _context.Offers
                                                                           where offer.CustomTourRequestId == request.Id
                                                                           orderby offer.CreateDate
                                                                           select offer.Price).FirstOrDefault(),
                                                                  OfferStatus = new CustomTourOfferStatus().GetValue(request.OfferStatusId),
                                                                  Program = (from offer in _context.Offers
                                                                             where offer.CustomTourRequestId == request.Id
                                                                             orderby offer.CreateDate
                                                                             select offer.TourProgram).FirstOrDefault(),
                                                                  RequestDetail = new CustomTourRequestDetail()
                                                                  {
                                                                      RequestId = request.Id,
                                                                      Destinations = (from many in _context.Many_CustomTourRequest_Destinations
                                                                                      where many.CustomTourRequestId == request.Id
                                                                                      join destinationLanguageItem in _context.DestinationLanguageItems
                                                                                      on many.DestinationId equals destinationLanguageItem.DestinationID
                                                                                      where destinationLanguageItem.LangaugeID == LanguageList.BaseLanguage.Id
                                                                                      select destinationLanguageItem.DisplayName).ToList(),
                                                                      AlsoInteresteds = (from many in _context.Many_CustomTourRequest_AlsoInteresteds
                                                                                         where many.CustomTourRequestId == request.Id
                                                                                         join systemOptionLanguage in _context.SystemOptionLanguageItems
                                                                                         on many.SystemOptionId equals systemOptionLanguage.SystemOptionId
                                                                                         where systemOptionLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                                                         select systemOptionLanguage.Name).ToList(),
                                                                      SenderPhone = request.Phone,
                                                                      Country = CountryList.GetValue(request.CountryId),
                                                                      HowFindUs = (from systemOptionLanguage in _context.SystemOptionLanguageItems
                                                                                   where systemOptionLanguage.SystemOptionId == request.FindUsId
                                                                                   where systemOptionLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                                                   select systemOptionLanguage.Name).FirstOrDefault(),
                                                                      CustomerNote = request.RequestNote,
                                                                      Language = new LanguageSelectList().GetValue(request.LanguageId),
                                                                      SenderName = request.Fullname,
                                                                      SenderEmail = request.Email,
                                                                      ArrivalDate = request.ArrivalDate.ToString("dd-MM-yyyy"),
                                                                      DeperatureDate = request.DepartureDate.ToString("dd-MM-yyyy"),
                                                                      NumberOfAldult = request.ParticipantNumber,
                                                                      RequestStatus = request.OfferStatusId,
                                                                  }
                                                              }).ToList();

            return requestList;
        }

        public List<OfferListDto> OfferListByRequestId(Guid requestId)
        {
            List<OfferListDto> offerList = (from offer in _context.Offers
                                            where offer.CustomTourRequestId == requestId
                                            join request in _context.CustomTourRequests
                                            on offer.CustomTourRequestId equals request.Id
                                            select new OfferListDto()
                                            {
                                                OfferId = offer.Id,
                                                Answer = offer.CustomerAnswer,
                                                OfferStatus = offer.Answered, //"deneme",
                                                Price = offer.Price,
                                                Program = offer.TourProgram,
                                                Date = offer.CreateDate,
                                                AdminAnswer = offer.AdminAnswer,
                                                CustomerAnswer = offer.CustomerAnswer,
                                                RequestDetail = new CustomTourRequestDetail()
                                                {
                                                    RequestId = requestId,
                                                    Destinations = (from many in _context.Many_CustomTourRequest_Destinations
                                                                    where many.CustomTourRequestId == request.Id
                                                                    join destinationLanguageItem in _context.DestinationLanguageItems
                                                                    on many.DestinationId equals destinationLanguageItem.DestinationID
                                                                    where destinationLanguageItem.LangaugeID == LanguageList.BaseLanguage.Id
                                                                    select destinationLanguageItem.DisplayName).ToList(),
                                                    AlsoInteresteds = (from many in _context.Many_CustomTourRequest_AlsoInteresteds
                                                                       where many.CustomTourRequestId == request.Id
                                                                       join systemOptionLanguage in _context.SystemOptionLanguageItems
                                                                       on many.SystemOptionId equals systemOptionLanguage.SystemOptionId
                                                                       where systemOptionLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                                       select systemOptionLanguage.Name).ToList(),
                                                    SenderPhone = request.Phone,
                                                    Country = CountryList.GetValue(request.CountryId),
                                                    HowFindUs = (from systemOptionLanguage in _context.SystemOptionLanguageItems
                                                                 where systemOptionLanguage.SystemOptionId == request.FindUsId
                                                                 where systemOptionLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                                 select systemOptionLanguage.Name).FirstOrDefault(),
                                                    CustomerNote = request.RequestNote,
                                                    Language = new Bookingstatus().GetValue(request.LanguageId),
                                                    SenderName = request.Fullname,
                                                    SenderEmail = request.Email,
                                                    ArrivalDate = request.ArrivalDate.ToString("dd-MM-yyyy"),
                                                    DeperatureDate = request.DepartureDate.ToString("dd-MM-yyyy"),
                                                    NumberOfAldult = request.ParticipantNumber,
                                                    RequestStatus = request.OfferStatusId
                                                }
                                            }).ToList();
            return offerList;
        }

        public void AddOffer(AddOfferDto addOfferDto)
        {
            Offer offer = new Offer()
            {
                CustomTourRequestId = addOfferDto.CustomTourRequestId,
                Price = addOfferDto.Price,
                TourProgram = addOfferDto.Program,
                AdminAnswer = addOfferDto.AdminAnswer,
                Answered = new CustomTourOfferStatus().GetValue((int)CustomTourOfferStatus.OfferStatus.WaitingAnswer), //WaitingAnswer
            };

            CustomTourRequest request = _context.CustomTourRequests.Find(addOfferDto.CustomTourRequestId);

            request.OfferStatusId = (int)CustomTourOfferStatus.OfferStatus.WaitingAnswer; //1;
            _context.CustomTourRequests.Update(request);

            _context.Offers.Add(offer);
        }

        //panelden buna istek yapılsın
        public void AnswerOffer(AnswerOfferDto dto)
        {
            Offer offer = _context.Offers.Find(dto.OfferId);
            CustomTourRequest request = _context.CustomTourRequests.Find(offer.CustomTourRequestId);
            if (dto.Confirmed)
            {
                offer.Answered = new CustomTourOfferStatus().GetValue((int)CustomTourOfferStatus.OfferStatus.Confirmed); //"Confirmed";
                request.OfferStatusId = (int)CustomTourOfferStatus.OfferStatus.Confirmed; // 3;
            }
            else
            {
                offer.Answered = new CustomTourOfferStatus().GetValue((int)CustomTourOfferStatus.OfferStatus.Rejected);  //"Rejected";
                request.OfferStatusId = (int)CustomTourOfferStatus.OfferStatus.Rejected; // 2;
            }
            offer.CustomerAnswer = dto.Answer;
            _context.Offers.Update(offer);

            _context.CustomTourRequests.Update(request);
        }


        public CustomTourOfferCustomerAnswer AddCustomerAnswer(CustomTourOfferCustomerAnswer dto)
        {
            Offer offer = _context.Offers.Find(dto.OfferId);
            CustomTourRequest request = _context.CustomTourRequests.Find(offer.CustomTourRequestId);

            offer.CustomerAnswer = dto.CustomerAnswer;
            offer.Answered = dto.Answer;

            _context.Offers.Update(offer);

            //eğer confirmlediyse bunuda confirmleyelim
            if (dto.Answer == new CustomTourOfferStatus().GetValue((int)CustomTourOfferStatus.OfferStatus.Confirmed))
            {
                request.OfferStatusId = 3;
                _context.CustomTourRequests.Update(request);
            }

            return dto;
        }



        public GetCustomTourRequestDetailDto CustomTourRequestDetail(Guid requestId, int languageId)
        {

            var requestDetail = (from request in _context.CustomTourRequests
                                 where request.Id == requestId
                                 /* join offers in _context.Offers
                                   on request.Id equals offers.CustomTourRequestId*/
                                 select new GetCustomTourRequestDetailDto()
                                 {
                                     RequestDetail = new CustomTourRequestDetail()
                                     {
                                         RequestId = requestId,
                                         Destinations = (from many in _context.Many_CustomTourRequest_Destinations
                                                         where many.CustomTourRequestId == request.Id
                                                         join destinationLanguageItem in _context.DestinationLanguageItems
                                                         on many.DestinationId equals destinationLanguageItem.DestinationID
                                                         where destinationLanguageItem.LangaugeID == LanguageList.BaseLanguage.Id
                                                         select destinationLanguageItem.DisplayName).ToList(),
                                         AlsoInteresteds = (from many in _context.Many_CustomTourRequest_AlsoInteresteds
                                                            where many.CustomTourRequestId == request.Id
                                                            join systemOptionLanguage in _context.SystemOptionLanguageItems
                                                            on many.SystemOptionId equals systemOptionLanguage.SystemOptionId
                                                            where systemOptionLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                            select systemOptionLanguage.Name).ToList(),
                                         SenderPhone = request.Phone,
                                         Country = CountryList.GetValue(request.CountryId),
                                         HowFindUs = (from systemOptionLanguage in _context.SystemOptionLanguageItems
                                                      where systemOptionLanguage.SystemOptionId == request.FindUsId
                                                      where systemOptionLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                      select systemOptionLanguage.Name).FirstOrDefault(),
                                         CustomerNote = request.RequestNote,
                                         Language = new Bookingstatus().GetValue(request.LanguageId),
                                         SenderName = request.Fullname,
                                         SenderEmail = request.Email,
                                         ArrivalDate = request.ArrivalDate.ToString("dd-MM-yyyy"),
                                         DeperatureDate = request.DepartureDate.ToString("dd-MM-yyyy"),
                                         NumberOfAldult = request.ParticipantNumber,
                                         RequestStatus = request.OfferStatusId
                                     },
                                     OfferList = (from offers in _context.Offers
                                                  where offers.CustomTourRequestId == request.Id

                                                  select new CustomTourOfferListDto()
                                                  {
                                                      requestId = requestId,
                                                      OfferId = offers.Id,
                                                      Answer = offers.Answered,
                                                      Date = offers.CreateDate,
                                                      Program = offers.TourProgram,
                                                      TourName = "",
                                                      Price = offers.Price,
                                                      OfferStatus = request.OfferStatusId.ToString(),
                                                      AdminAnswer = offers.AdminAnswer,
                                                      CustomerAnswer = offers.CustomerAnswer,
                                                  }).ToList(),
                                 }).FirstOrDefault();


            return requestDetail;
        }


    }
}
