using Core.Entities;
using Data.Migrations;
using Dto.ApiPanelDtos.ReservationEditRequestDtos;
using Dto.ApiPanelDtos.SendMailDtos;
using Dto.ApiPanelDtos.UserDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mail
{
    public static class MailReplace
    {
        public static SendMailTemplateDto ReplaceContentForReturnRequest(SendMailTemplateDto templateDto, User user, CustomMadeTourPostDto request)
        {
            string ad = user != null ? user.Name : "";
            string soyad = user != null ? user.Surname : "";
            templateDto.Content = templateDto.Content
        .Replace("{ad}", ad)
        .Replace("{soyad}", soyad)
        .Replace("{link}", $"https://localhost:7115/Reservation/ReservationInquiryResponse?ReservationCode={request.RequestId}");
            return templateDto;
        }

        //todo --> gerekliyse subjectleri de doldur


        /// <summary>
        /// admine atılan mail replace
        /// </summary>
        public static void ReplaceContentForReservationUpdateRequest(SendMailTemplateDto template, Reservation reservation, UpdateReservationRequestPostDto dto, UserListDto adminUser, User reservationUser)
        {
            template.Content
              .Replace("{ad}", adminUser.FullName.Split(' ')[0])
              .Replace("{soyad}", adminUser.FullName.Split(' ')[1])
              .Replace("{username}", $"{reservationUser.Name} {reservationUser.Surname}")
              .Replace("{reservationCode}", reservation.ReservationCode)
              .Replace("{updateRequestReason}", dto.UpdateReasonText)
              .Replace("{updateRequestDate}", DateTime.Now.ToString());
        }


    }
}
