using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace GuideWeb.Validations.ReservationValidations
{
    public class CancelReservationValidator : AbstractValidator<CancelReservationRequestPostDto>
    {
        public CancelReservationValidator()
        {
            RuleFor(x => x.ReservationId).NotEmpty();
            RuleFor(x => x.CancellationReasonText).NotEmpty().WithMessage("Provide a reason!");
            RuleFor(x => x.CancellationReasonId).NotEmpty().WithMessage("Select cancellation reason");
            RuleFor(x => x.ReadCancellationPolicy).Equal(true);
        }
    }
}
