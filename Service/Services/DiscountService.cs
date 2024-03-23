using Core;
using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticValues;
using Data.Migrations;
using Dto.ApiPanelDtos.Discount;
using Dto.ApiWebDtos.ApiToWebDtos.Payment;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums;

namespace Service.Services
{
    public class DiscountService : GenericService<Discount>, IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMany_Discount_TourCategoryRepository _discountTourCategoryRepository;
        private readonly IMany_Discount_ProductRepository _discountProductRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationPaymentRepository _reservationPaymentRepository;

        private readonly IReservationPaymentService _reservationPaymentService;

        public DiscountService(IGenericRepository<Discount> repository, IUnitOfWork unitOfWork, IDiscountRepository discountRepository, IMany_Discount_TourCategoryRepository discountTourCategoryRepository, IMany_Discount_ProductRepository discountProductRepository, IReservationRepository reservationRepository, IReservationPaymentRepository reservationPaymentRepository, IReservationPaymentService reservationPaymentService) : base(repository, unitOfWork)
        {
            _discountRepository = discountRepository;
            _discountTourCategoryRepository = discountTourCategoryRepository;
            _discountProductRepository = discountProductRepository;
            _reservationRepository = reservationRepository;
            _reservationPaymentRepository = reservationPaymentRepository;
            _reservationPaymentService = reservationPaymentService;
        }

        public void AddDiscount(AddDiscountDto discountDto)
        {
            DiscountTypeSelectList discountTypeSelectList = new();

            if (discountDto.UsingStartDate is null && discountDto.UsingEndDate is null)
            {
                discountDto.UsingStartDate = DateTime.Now;
                discountDto.UsingEndDate = DateTime.Now.AddYears(100);
            }

            Discount discount = new()
            {
                CouponCode = discountDto.CouponCode,
                DiscountAmount = discountDto.DiscountAmount,
                DiscountName = discountDto.DiscountName,
                DiscountType = discountTypeSelectList.GetValue(discountDto.DiscountType),
                Explanation = discountDto.Explanation,
                StartDate = discountDto.StartDate,
                EndDate = discountDto.EndDate,
                ImagePath = discountDto.ImagePath ?? "",
                MaxUsageCount = discountDto.MaxUsageCount,
                UsageLeft = discountDto.MaxUsageCount,
                UsingStartDate = discountDto.UsingStartDate,
                UsingEndDate = discountDto.UsingEndDate,
            };
            _discountRepository.Add(discount);
            _unitOfWork.saveChanges();
            foreach (Guid discountProductItemId in discountDto.ProductToBenefitIds)
            {
                Many_Discount_Product discountProduct = new()
                {
                    ProductId = discountProductItemId,
                    DiscountId = discount.Id
                };
                _discountProductRepository.Add(discountProduct);
            }
            foreach (Guid discountTourCategoryId in discountDto.TourCategoryIds)
            {
                Many_Discount_TourCategory discountTourCategory = new()
                {
                    TourCategoryID = discountTourCategoryId,
                    DiscountId = discount.Id
                };
                _discountTourCategoryRepository.Add(discountTourCategory);
            }
            _unitOfWork.saveChanges();
        }

        public void DeleteDiscount(string discountId)
        {
            var discount = _discountRepository.GetById(Guid.Parse(discountId));
            discount.IsDeleted = true;
            //discount.Status
            _unitOfWork.saveChanges();
        }
        public void EditDiscount(EditDiscountDto editDiscountDto)
        {
            var discount = _discountRepository.GetDiscountById(Guid.Parse(editDiscountDto.Id));

            if (discount != null)
            {
                discount.DiscountName = editDiscountDto.DiscountName;
                discount.CouponCode = editDiscountDto.CouponCode;
                discount.DiscountAmount = editDiscountDto.DiscountAmount;
                discount.DiscountType = new DiscountTypeSelectList().GetValue(editDiscountDto.DiscountType);
                discount.StartDate = editDiscountDto.StartDate;
                discount.EndDate = editDiscountDto.EndDate;
                discount.Explanation = editDiscountDto.Explanation;
                discount.ImagePath = editDiscountDto.ImagePath;
                discount.MaxUsageCount = editDiscountDto.MaxUsageCount;
                discount.UsingStartDate = editDiscountDto.UsingStartDate;
                discount.UsingEndDate = editDiscountDto.UsingEndDate;


                var addedTourCategoryIds = editDiscountDto.TourCategoryIds.Except(discount.Many_Discount_TourCategories.Select(x => x.TourCategoryID)).ToList();

                var removedTourCategoryIds = discount.Many_Discount_TourCategories.Select(x => x.TourCategoryID).Except(editDiscountDto.TourCategoryIds).ToList();

                foreach (var addedTourCategoryId in addedTourCategoryIds)
                {
                    var newManyDiscountTourCategory = new Many_Discount_TourCategory
                    {
                        DiscountId = discount.Id,
                        TourCategoryID = addedTourCategoryId
                    };
                    _discountTourCategoryRepository.Add(newManyDiscountTourCategory);
                }

                foreach (var removedTourCategoryId in removedTourCategoryIds)
                {
                    var removedManyDiscountTourCategory = discount.Many_Discount_TourCategories
                        .FirstOrDefault(x => x.TourCategoryID == removedTourCategoryId);
                    if (removedManyDiscountTourCategory is not null)
                        _discountTourCategoryRepository.Remove(removedManyDiscountTourCategory);
                }

                var addedProductIds = editDiscountDto.ProductToBenefitIds.Except(discount.Many_Discount_Products.Select(x => x.ProductId)).ToList();

                var removedProductIds = discount.Many_Discount_Products.Select(x => x.ProductId).Except(editDiscountDto.ProductToBenefitIds).ToList();

                foreach (var addedProductId in addedProductIds)
                {
                    var newManyDiscountProduct = new Many_Discount_Product
                    {
                        DiscountId = discount.Id,
                        ProductId = addedProductId
                    };
                    _discountProductRepository.Add(newManyDiscountProduct);
                }

                foreach (var removedProductId in removedProductIds)
                {
                    var removedManyDiscountProduct = discount.Many_Discount_Products
                        .FirstOrDefault(x => x.ProductId == removedProductId);
                    if (removedManyDiscountProduct is not null)
                        _discountProductRepository.Remove(removedManyDiscountProduct);
                }

                _discountRepository.Update(discount);
                _unitOfWork.saveChanges();
            }
        }
        public List<DiscountListDto> GetDiscounts()
        {
            return _discountRepository.GetDiscounts().GetAwaiter().GetResult();
        }
        public EditDiscountDto GetForEdit(string id)
        {
            var discount = _discountRepository.GetDiscountById(Guid.Parse(id));
            var editDiscountDto = new EditDiscountDto
            {
                Id = discount.Id.ToString(),
                DiscountName = discount.DiscountName,
                TourCategoryIds = discount.Many_Discount_TourCategories.Select(t => t.TourCategoryID).ToList(),
                ProductToBenefitIds = discount.Many_Discount_Products.Select(p => p.ProductId).ToList(),
                MaxUsageCount = discount.MaxUsageCount,
                DiscountType = new DiscountTypeSelectList().GetValue(discount.DiscountType),
                CouponCode = discount.CouponCode,
                DiscountAmount = discount.DiscountAmount,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                UsingStartDate = discount.UsingStartDate,
                UsingEndDate = discount.UsingEndDate,
                Explanation = discount.Explanation,
                ImagePath = discount.ImagePath
            };
            return editDiscountDto;
        }
        public bool IsCouponCodeExist(string couponCode)
        {
            var discount = _discountRepository.GetDiscountByCouponCode(couponCode);
            return discount is not null;
        }
        //kullanıcıya hangi sebeple kupon kodunun geçersiz olduğunu bildirmiyoruz. bildirmek istersek buradan CustomResponseDto dönüp içerisine errorMessage yerleştirebiliriz.
        private bool IsCouponCodeValidForDiscount(Discount? discount, Reservation reservation)
        {
            if (discount is null)
                return false;

            //discount usingStart ve end ile kontrol et
            //rezervasyon oluşturma tarihi discountun startDate ve endDate arasında mı diye kontrol edecez.
            //bugünün tarihi ile kontrol etmiyoruz çünkü rezervasyon oluşturma ile ödeme tarihi farklı olabilir. 
            bool isReservationDateValid = discount.StartDate < reservation.CreateDate && reservation.CreateDate < discount.EndDate;

            if (!isReservationDateValid)
                return false;

            List<Guid> discountTourCategoryIds = discount.Many_Discount_TourCategories.Select(x => x.TourCategoryID).ToList();
            List<Guid> discountProductTourIds = discount.Many_Discount_Products.Select(x => x.ProductId).ToList();
            List<Guid> reservationProductTourCategoryIds = reservation.Product.Tour.Many_Tour_TourCategories.Select(x => x.TourCategoryID).ToList();

            //todo --> daha okunaklı yaz 
            bool doesDiscountTourCategoriesIncludeReservationProductTourCategory = discountTourCategoryIds.Intersect(reservationProductTourCategoryIds).Any();
            bool doesDiscountProductToursIncludeReservationProductTour = discountProductTourIds.Any(x => x == reservation.ProductId);

            if (!(doesDiscountTourCategoriesIncludeReservationProductTourCategory || doesDiscountProductToursIncludeReservationProductTour))
                return false;

            bool isCouponReservationDatesAreValid = reservation.StartDate > discount.UsingStartDate && reservation.StartDate < discount.UsingEndDate;

            if (!isCouponReservationDatesAreValid)
                return false;

            if (discount.UsageLeft == 0)
                return false;

            return true;
        }
        public CustomResponseDto<GetCouponDiscountResponseDto> GetCouponDiscountForReservation(GetCouponDiscountDto dto)
        {
            var tupleData = GetReservationDiscountInfo(dto.Reservation, dto.CouponCode);

            var reservation = tupleData.reservation;
            var discount = tupleData.discount;

            if (!IsCouponCodeValidForDiscount(discount, reservation))
                return CustomResponseDto<GetCouponDiscountResponseDto>.Fail(400, "Coupon code is not valid !");

            GetCouponDiscountResponseDto responseObj = new()
            {
                DiscountAmount = discount.DiscountAmount,
                DiscountType = (int)Enum.Parse(typeof(DiscountType), discount.DiscountType, true)
            };

            reservation.DiscountId = discount.Id;
            _reservationRepository.Update(reservation);


            var reservationPayments = _reservationPaymentService.PaymentListByReservationId(reservation.Id);
            var d = reservationPayments.Where(x => x.IsDebt).Sum(x => x.Price);
            var f = reservationPayments.Where(x => !x.IsDebt).Sum(x => x.Price);
            var totalPrice = d - f;


            //var totalPrice = reservation.TotalPrice;




            bool isDiscountTypeAmount = (int)Enum.Parse(typeof(DiscountType), discount.DiscountType, true) == (int)DiscountType.Amount;
            bool isDiscountTypePercentage = (int)Enum.Parse(typeof(DiscountType), discount.DiscountType, true) == (int)DiscountType.Percentage;

            decimal ci = 0;

            if (isDiscountTypeAmount)
                ci = discount.DiscountAmount;

            if (isDiscountTypePercentage)
                ci = (totalPrice * discount.DiscountAmount / 100);

            ReservationPayment reservationPayment = new()
            {
                ReservationId = reservation.Id,
                IsDebt = false,
                DiscountRate = isDiscountTypePercentage ? Convert.ToInt32(responseObj.DiscountAmount) : null,
                Price = ci,
                PaymentTypeId = (int)PaymentTypeList.No.Indirim,
                PaymentMethodId = (int)PaymentMethodList.No.KuponKodu,
            };
            _reservationPaymentRepository.Add(reservationPayment);
            _unitOfWork.saveChanges();

            discount.UsageLeft--;
            Update(discount);



            //donus için default geri donme yontemine ek yapalım - buradan aşağıgısı kopyala yapıştır yaptım 
            #region OdemeDetail 
                reservationPayments = _reservationPaymentService.PaymentListByReservationId(reservation.Id);

                PaymentShowDetailDto paymentDetail = new PaymentShowDetailDto()
                {
                    TotalPrice = reservation.TotalPrice,
                    DepositoPrice = reservation.DepositoPrice,
                    Discounts = new List<PaymentShowDetailDiscountsDto>() { }
                };

                //TODO: Buradaki statik alanları constant value alanına ekleme yaacağız sonra
                // şuanda yüzde göstermeyeceğim sadece indirim oranını göstereceğim
                //eğer yüzdeli göstermek istenirse guide veya acentanın tur için ve servis için olan indirimlerini ayrı ayrı göstermek gerekecektir 
                var agentDiscountPrice = reservationPayments.Where(p => !p.IsDebt && p.PaymentTypeId == (int)PaymentTypeList.No.Indirim && (p.PaymentMethodId == (int)PaymentMethodList.No.AcenteTurIndirimi || p.PaymentMethodId == (int)PaymentMethodList.No.AcenteServisIndirimi)).Sum(x => x.Price);
                if (agentDiscountPrice > 0)
                {
                    paymentDetail.Discounts.Add(new PaymentShowDetailDiscountsDto()
                    {
                        Name = "Agent Discount",
                        Percentage = 0,
                        Amount = agentDiscountPrice
                    });
                }

                var guideDiscountPrice = reservationPayments.Where(p => !p.IsDebt && p.PaymentTypeId == (int)PaymentTypeList.No.Indirim && (p.PaymentMethodId == (int)PaymentMethodList.No.RehberTurIndirimi || p.PaymentMethodId == (int)PaymentMethodList.No.RehberServisIndirimi)).Sum(x => x.Price);
                if (guideDiscountPrice > 0)
                {
                    paymentDetail.Discounts.Add(new PaymentShowDetailDiscountsDto()
                    {
                        Name = "Guide Discount",
                        Percentage = 0,
                        Amount = guideDiscountPrice
                    });
                }

                var couponDiscountPrice = reservationPayments.Where(p => !p.IsDebt && p.PaymentTypeId == (int)PaymentTypeList.No.Indirim && (p.PaymentMethodId == (int)PaymentMethodList.No.KuponKodu)).Sum(x => x.Price);
                if (couponDiscountPrice > 0)
                {
                    paymentDetail.Discounts.Add(new PaymentShowDetailDiscountsDto()
                    {
                        Name = "Coupon Discount",
                        Percentage = 0,
                        Amount = couponDiscountPrice
                    });
                }

                paymentDetail.LastTotalPrice = d - f;
            #endregion


            responseObj.PaymentDetail = paymentDetail;



            return CustomResponseDto<GetCouponDiscountResponseDto>.Success(200, responseObj);
        }

        private (Discount discount, Reservation reservation) GetReservationDiscountInfo(Guid reservationId, string couponCode)
        {
            var reservation = _reservationRepository.GetReservationById(reservationId);
            var discount = _discountRepository.GetDiscountByCouponCode(couponCode);

            return (discount, reservation);
        }

        public void RemoveCouponDiscountFromReservation(RemoveCouponDiscountDto dto)
        {
            var tupleData = GetReservationDiscountInfo(dto.ReservationId, dto.CouponCode);
            Reservation reservation = tupleData.reservation;
            Discount discount = tupleData.discount;

            var reservationPayment = _reservationPaymentRepository.Where(x => x.ReservationId == reservation.Id && !x.IsDeleted && x.PaymentMethodId == (int)PaymentMethodList.No.KuponKodu).FirstOrDefault();

            reservationPayment.Status = false;
            reservationPayment.IsDeleted = true;
            _reservationPaymentRepository.Update(reservationPayment);

            reservation.DiscountId = null;
            _reservationRepository.Update(reservation);

            discount.UsageLeft++;
            _discountRepository.Update(discount);

            _unitOfWork.saveChanges();

        }
    }
}
