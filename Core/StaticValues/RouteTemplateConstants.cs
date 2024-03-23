using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues.Route
{
    public class RouteTemplateConstants
    {
        public static Dictionary<No, RouteTemplate> Templates = new()
        {
            { No.Blog, new RouteTemplate("Blog","BlogDetail")},
            { No.BlogCategory, new RouteTemplate("Blog","Index")}, //blogCategorye çevrilecek, şimdilik sitemapler oluşsun diye böyle bırakıyorum
            { No.BlogTag, new RouteTemplate("Blog","Tag")},

            { No.Service, new RouteTemplate("Service","ServiceDetail")},

            { No.Tour, new RouteTemplate("Tour","TourDetail")},
            { No.TourFilter, new RouteTemplate("Tour","TourFilter")},
            { No.TourCategory, new RouteTemplate("Tour","Index")},

            { No.CustomPage, new RouteTemplate("CustomPage","Index")},
            { No.ForgotPassword, new RouteTemplate("CustomPage","ForgotPassword")},
            { No.ForgotPasswordValidation, new RouteTemplate("CustomPage","ForgotPasswordValidation")},
            { No.Login, new RouteTemplate("CustomPage","Login")},

            { No.Profile, new RouteTemplate("Profile","ProfileMember")},
            { No.AgentInformation, new RouteTemplate("Profile","AgentInformation")},

            { No.UserManagement, new RouteTemplate("Agent","UserManagement")},

            { No.AskForPriceSuccess, new RouteTemplate("Reservation","AskForPriceSuccess")},
            { No.MyReservation, new RouteTemplate("Reservation","MyReservation")},
            { No.ReservationDetail, new RouteTemplate("Reservation","ReservationDetail")},
            { No.ReservationInquiry, new RouteTemplate("Reservation","ReservationInquiry")},

            { No.PaymentBilling, new RouteTemplate("Payment","PaymentBilling")},
            { No.PaymentInformation, new RouteTemplate("Payment","PaymentInformation")},
            { No.PaymentParticipant, new RouteTemplate("Payment","PaymentParticipant")},
            { No.PaymentSuccess, new RouteTemplate("Payment","PaymentSuccess")},

        };
        public static RouteTemplate GetRouteTemplate(No routeTemplateEnum)
        {
            Templates.TryGetValue(routeTemplateEnum, out var routeTemplate);
            if (routeTemplate is not null)
                return routeTemplate;
            else
                return null;
        }
        public enum No
        {
            Blog,
            BlogCategory,
            BlogTag,
            Tour,
            TourFilter,
            Service,
            TourCategory,
            CustomPage,
            ForgotPassword,
            ForgotPasswordValidation,
            Login,
            Profile,
            AgentInformation,
            MyReservation,
            UserManagement,
            AskForPriceSuccess,
            ReservationDetail,
            ReservationInquiry,
            PaymentBilling,
            PaymentInformation,
            PaymentParticipant,
            PaymentSuccess,
        }
    }
    public class RouteTemplate
    {
        public RouteTemplate(string controller, string action)
        {
            Controller = controller;
            Action = action;
        }
        public RouteTemplate()
        {

        }

        public string Controller { get; set; }
        public string Action { get; set; }
    }

}
