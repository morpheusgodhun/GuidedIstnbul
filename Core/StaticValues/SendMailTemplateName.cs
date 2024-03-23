using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public class SendMailTemplateName
    {
        public static List<SelectListOption> Status = new()
        {
            new () { ID = 1, Value = "Yeni Üyelik Oluşturuldu" }, //
            new () { ID = 2, Value = "Rezervasyon Oluşturuldu Customer" },
            new () { ID = 3, Value = "Teklif Oluşturuldu" },
            new () { ID = 4, Value = "Şifremi Unuttum" }, //
            #region rezervasyon iptal talebi
            new () { ID = 5, Value = "Rezervasyon İptal Talebi Alındı Admin" },
            new () { ID = 6, Value = "Rezervasyon Güncelleme Talebi Alındı Admin" },

            new () { ID = 7, Value = "Rezervasyon Güncelleme Talebi Onaylandı Customer" },
            new () { ID = 8, Value = "Rezervasyon Güncelleme Talebi Reddedildi Customer" },

            new () { ID = 9, Value = "Rezervasyon İptal Talebi Onaylandı Customer" },
            new () { ID = 10, Value ="Rezervasyon İptal Talebi Reddedildi Member" },
            #endregion 
            #region bize ulaşın
            new () { ID = 11, Value ="Bize Ulaşın Operation" },
            new () { ID = 12, Value ="Bize Ulaşın Manager" },
            new () { ID = 13, Value ="Bize Ulaşın Customer" },
            new () { ID = 14, Value ="Bize Ulaşın Member" },
            #endregion
            #region custom tur talebi
            new () { ID = 15, Value ="Custom Tur Talebi Alındı Operation" },
            new () { ID = 16, Value ="Custom Tur Talebi Alındı Customer" },
            new () { ID = 17, Value ="Custom Tur Talebi Alındı Member" },
            new () { ID = 18, Value ="Custom Tur Talebi Alındı Manager" },
            new () { ID = 19, Value ="Custom Tur Talebi Alındı Acente" },
            #endregion
            #region rezervasyon iptal talebi devam
            new () { ID = 20, Value ="Rezervasyon İptal Talebi Alındı Operation" },
            new () { ID = 21, Value ="Rezervasyon İptal Talebi Alındı Manager" },
            new () { ID = 22, Value ="Rezervasyon İptal Talebi Alındı Agent" },
            new () { ID = 23, Value ="Rezervasyon İptal Talebi Alındı Customer/Member" },

            new () { ID = 24, Value ="Rezervasyon İptal Talebi Onaylandı Operation" },
            new () { ID = 25, Value ="Rezervasyon İptal Talebi Onaylandı Manager" },
            new () { ID = 26, Value ="Rezervasyon İptal Talebi Onaylandı Agent" },

            new () { ID = 27, Value ="Rezervasyon İptal Talebi Reddedildi Operation" },
            new () { ID = 28, Value ="Rezervasyon İptal Talebi Reddedildi Manager" },
            new () { ID = 29, Value ="Rezervasyon İptal Talebi Reddedildi Agent" },
            #endregion
            #region mail değişikliği onay
            new () { ID = 30, Value ="Mail Değişikliği Onay" },
            #endregion
            #region rehber / guide başvurusu
            new () { ID = 31, Value ="Rehber Başvurusu Alındı Başvuran" },
            new () { ID = 32, Value ="Rehber Başvurusu Alındı Yönetici" },
            new () { ID = 33, Value ="Rehber Başvurusu Alındı Operasyon" },

            new () { ID = 34, Value ="Rehber Başvurusu Onaylandı Başvuran" },
            new () { ID = 35, Value ="Rehber Başvurusu Onaylandı Yönetici" },
            new () { ID = 36, Value ="Rehber Başvurusu Onaylandı Admin" },
            new () { ID = 37, Value ="Rehber Başvurusu Onaylandı Operasyon" },

            new () { ID = 38, Value ="Rehber Başvurusu Reddedildi Başvuran" },
            new () { ID = 39, Value ="Rehber Başvurusu Reddedildi Yönetici" },
            #endregion
            #region acente / agent başvurusu
            new () { ID = 40, Value ="Acente Başvurusu Alındı Başvuran" },
            new () { ID = 41, Value ="Acente Başvurusu Alındı Yönetici" },

            new () { ID = 42, Value ="Acente Başvurusu Onaylandı Başvuran" },
            new () { ID = 43, Value ="Acente Başvurusu Onaylandı Yönetici" },
            new () { ID = 44, Value ="Acente Başvurusu Onaylandı Admin" },
            new () { ID = 45, Value ="Acente Başvurusu Onaylandı Operasyon" },

            new () { ID = 46, Value ="Acente Başvurusu Reddedildi Operasyon" },
            new () { ID = 47, Value ="Acente Başvurusu Reddedildi Operasyon" },
            #endregion
            #region custom tur talebi teklifleri
            new () { ID = 48, Value ="Custom Tur Talebi Teklif Olusturuldu Customer" },
            new () { ID = 49, Value ="Custom Tur Talebi Teklif Olusturuldu Member" },
            new () { ID = 50, Value ="Custom Tur Talebi Teklif Olusturuldu Operasyon" },
            new () { ID = 51, Value ="Custom Tur Talebi Teklif Olusturuldu Manager" },
            //şimdilik acentelere mail göndermeyeceğiz. şuanlık teklif oluştururken acenteye bağlı bir durum yok. ileride teklife acenteyi dahil ettiğimizde kullanabiliriz
            new () { ID = 52, Value ="Custom Tur Talebi Teklif Olusturuldu Acente" },

            new () { ID = 53, Value ="Custom Tur Talebi Teklif Değişiklik İstendi Customer" },
            new () { ID = 54, Value ="Custom Tur Talebi Teklif Değişiklik İstendi Member" },
            new () { ID = 55, Value ="Custom Tur Talebi Teklif Değişiklik İstendi Operasyon" },
            new () { ID = 56, Value ="Custom Tur Talebi Teklif Değişiklik İstendi Yönetici" },
            //şimdilik acentelere mail göndermeyeceğiz. şuanlık teklif oluştururken acenteye bağlı bir durum yok. ileride teklife acenteyi dahil ettiğimizde kullanabiliriz
            new () { ID = 57, Value ="Custom Tur Talebi Teklif Değişiklik İstendi Acente" },

            new () { ID = 58, Value ="Custom Tur Talebi Teklif Reddedildi Customer" },
            new () { ID = 59, Value ="Custom Tur Talebi Teklif Reddedildi Member" },
            new () { ID = 60, Value ="Custom Tur Talebi Teklif Reddedildi Operasyon" },
            new () { ID = 61, Value ="Custom Tur Talebi Teklif Reddedildi Yönetici" },
            //şimdilik acentelere mail göndermeyeceğiz. şuanlık teklif oluştururken acenteye bağlı bir durum yok. ileride teklife acenteyi dahil ettiğimizde kullanabiliriz
            new () { ID = 62, Value ="Custom Tur Talebi Teklif Reddedildi Acente" },

            new () { ID = 63, Value ="Custom Tur Talebi Teklif Onaylandı Customer" },
            new () { ID = 64, Value ="Custom Tur Talebi Teklif Onaylandı Member" },
            new () { ID = 65, Value ="Custom Tur Talebi Teklif Onaylandı Operasyon" },
            new () { ID = 66, Value ="Custom Tur Talebi Teklif Onaylandı Yönetici" },
            //şimdilik acentelere mail göndermeyeceğiz. şuanlık teklif oluştururken acenteye bağlı bir durum yok. ileride teklife acenteyi dahil ettiğimizde kullanabiliriz
            new () { ID = 67, Value ="Custom Tur Talebi Teklif Onaylandı Acente" },
            #endregion
            #region rezervasyon oluşturuldu devam
            new () { ID = 68, Value = "Rezervasyon Oluşturuldu Member" },
            new () { ID = 69, Value = "Rezervasyon Oluşturuldu Operasyon" },
            new () { ID = 70, Value = "Rezervasyon Oluşturuldu Yönetici" },
            //şimdilik acentelere mail göndermeyeceğiz. şuanlık rezervasyon alındığında acenteye bağlı bir durum yok. ileride kullanabiliriz
            new () { ID = 71, Value = "Rezervasyon Oluşturuldu Acente" },
            new () { ID = 72, Value = "Rezervasyon Onaylandı Customer" },
            new () { ID = 73, Value = "Rezervasyon Onaylandı Member" },
            new () { ID = 74, Value = "Rezervasyon Onaylandı Operasyon" },
            new () { ID = 75, Value = "Rezervasyon Onaylandı Yönetici" },
            //şimdilik acentelere mail göndermeyeceğiz. şuanlık rezervasyon onaylandığında acenteye bağlı bir durum yok. ileride kullanabiliriz
            new () { ID = 76, Value = "Rezervasyon Onaylandı Acente" },
            #endregion
            #region rezervasyon devam
            new () { ID = 77, Value = "Rezervasyon Güncelleme Talebi Alındı Operasyon" },
            new () { ID = 78, Value = "Rezervasyon Güncelleme Talebi Alındı Yönetici" },
            new () { ID = 79, Value = "Rezervasyon Güncelleme Talebi Alındı Acente" },
            new () { ID = 80, Value = "Rezervasyon Güncelleme Talebi Alındı Customer" },
            new () { ID = 81, Value = "Rezervasyon Güncelleme Talebi Alındı Member" },

            new () { ID = 82, Value = "Rezervasyon Güncelleme Talebi Onaylandı Member" },

            new () { ID = 83, Value ="Rezervasyon İptal Talebi Reddedildi Customer" },
            new () { ID = 84, Value = "Rezervasyon Güncelleme Talebi Reddedildi Member" },
            #endregion
            #region rezervasyon ödeme alındı 
            new () { ID = 85, Value = "Rezervasyon Ödeme Alındı Customer" },
            new () { ID = 86, Value = "Rezervasyon Ödeme Alındı Member" },
            new () { ID = 87, Value = "Rezervasyon Ödeme Alındı Operasyon" },
            new () { ID = 88, Value = "Rezervasyon Ödeme Alındı Yönetici" },
            new () { ID = 89, Value = "Rezervasyon Ödeme Alındı Acente" },
            #endregion
            new () { ID = 90, Value = "Tur Rehberi Değişti Operasyon" },
            new () { ID = 91, Value = "Tur Rehberi Değişti Yönetici" },
            new () { ID = 92, Value = "Tur Rehberi Değişti Rehber" },

            new () { ID = 93, Value = "Rezervasyon Ödeme Tarihi Yaklaştı Acente" },
            new () { ID = 94, Value = "Rezervasyon Ödeme Tarihi Yaklaştı Operasyon" },
            new () { ID = 95, Value = "Rezervasyon Ödeme Tarihi Yaklaştı Manager" },

            new () { ID = 96, Value = "Turdan 2 Gün Önce Rehber/Guide Bilgilendirmesi" },
            new () { ID = 97, Value = "Turdan 2 Gün Önce Member Bilgilendirmesi" },
            new () { ID = 98, Value = "Turdan 2 Gün Önce Customer Bilgilendirmesi" },

            new () { ID = 99, Value = "Tur Tamamlandı Müşteriden/Customer Linkli Yorum İste " },
            new () { ID = 100, Value = "Tur Tamamlandı Üyeden/Member Linkli Yorum İste " },

            new () { ID = 101, Value = "Tur Tamamlandı - Üyeden/Member Linksiz Yorum İste " },
            new () { ID = 102, Value = "Tur Tamamlandı - Üyeden/Member Linksiz Yorum İste " },

            new () { ID = 103, Value = "Tur Tamamlandı - Katılımcılardan Yorum İstendi Operasyon" },
            new () { ID = 104, Value = "Tur Tamamlandı - Katılımcılardan Yorum İstendi Yönetici" },

            new () { ID = 105, Value = "Ayın İlk Günü Admin Raporu" },
            new () { ID = 106, Value = "Ayın İlk Günü Operasyon Raporu" },
            new () { ID = 107, Value = "Ayın İlk Günü Yönetici Raporu" },

            new () { ID = 108, Value = "Newsletter Eklendi Subscribers/Aboneler" },
            new () { ID = 109, Value = "Newsletter Eklendi Operasyon" },
            new () { ID = 110, Value = "Newsletter Eklendi Yönetici" },

            new () { ID = 111, Value = "Blog Post Yazısı Yorum Aldı Operasyon" },

            new () { ID = 112, Value = "Rezervasyon Ask For Price Talebi Müşteri Bilgilendirme" },


            /*
             * Operasyon mailleri gelecek -- operasyona guide/driver ataması
             * Agent tarafından eklenen kullanıcılara mail  -- userManagement sayfasından eklenen kullanıcılar için kullanıcı istekleri oluşturuluyor
             * Panelden eklenen drivera mail
             * */

        };
        public static string GetValue(int id)
        {
            return Status.FirstOrDefault(x => x.ID == id).Value;
        }
        public enum No
        {
            YeniUyelikOlusturuldu = 1,
            RezervasyonOlusturulduCustomer = 2,
            TeklifOlusturuldu = 3,
            SifremiUnuttum = 4,

            RezervasyonIptalTalebiReceivedAdmin = 5,
            RezervasyonGuncellemeTalebiReceivedAdmin = 6,

            RezervasyonGuncellemeTalebiApprovedCustomer = 7,
            RezervasyonGuncellemeTalebiRejectedCustomer = 8,

            RezervasyonIptalTalebiApprovedCustomer = 9,
            RezervasyonIptalTalebiRejectedMember = 10,

            BizeUlasinOperation = 11,
            BizeUlasinManager = 12,
            BizeUlasinCustomer = 13,
            BizeUlasinMember = 14,

            CustomTurTalebiReceivedOperation = 15,
            CustomTurTalebiReceivedCustomer = 16,
            CustomTurTalebiReceivedMember = 17,
            CustomTurTalebiReceivedManager = 18,
            CustomTurTalebiReceivedAgent = 19,

            RezervasyonIptalTalebiReceivedOperation = 20,
            RezervasyonIptalTalebiReceivedManager = 21,
            RezervasyonIptalTalebiReceivedAgent = 22,
            RezervasyonIptalTalebiReceivedCustomerMember = 23,

            RezervasyonIptalTalebiApprovedOperation = 24,
            RezervasyonIptalTalebiApprovedManager = 25,
            RezervasyonIptalTalebiApprovedAgent = 26,

            RezervasyonIptalTalebiRejectedOperation = 27,
            RezervasyonIptalTalebiRejectedManager = 28,
            RezervasyonIptalTalebiRejectedAgent = 29,

            MailDegisikligiOnay = 30,

            GuideApplicationReceivedApplicant = 31, //applicant -> guide adayı
            GuideApplicationReceivedManager = 32,
            GuideApplicationReceivedOperation = 33,

            GuideApplicationApprovedApplicant = 34,
            GuideApplicationApprovedManager = 35,
            GuideApplicationApprovedAdmin = 36,
            GuideApplicationApprovedOperation = 37,

            GuideApplicationRejectedApplicant = 38,
            GuideApplicationRejectedManager = 39,

            AgentApplicationReceivedApplicant = 40,
            AgentApplicationReceivedManager = 41,

            AgentApplicationApprovedApplicant = 42,
            AgentApplicationApprovedManager = 43,
            AgentApplicationApprovedAdmin = 44,
            AgentApplicationApprovedOperation = 45,

            AgentApplicationRejectedApplicant = 46,
            AgentApplicationRejectedManager = 47,

            CustomTurTalebiOfferCreatedCustomer = 48,
            CustomTurTalebiOfferCreatedMember = 49,
            CustomTurTalebiOfferCreatedOperation = 50,
            CustomTurTalebiOfferCreatedManager = 51,
            CustomTurTalebiOfferCreatedAgent = 52,

            CustomTurTalebiOfferChangeRequestedCustomer = 53,
            CustomTurTalebiOfferChangeRequestedMember = 54,
            CustomTurTalebiOfferChangeRequestedOperation = 55,
            CustomTurTalebiOfferChangeRequestedManager = 56,
            CustomTurTalebiOfferChangeRequestedAgent = 57,

            CustomTurTalebiOfferRejectedCustomer = 58,
            CustomTurTalebiOfferRejectedMember = 59,
            CustomTurTalebiOfferRejectedOperation = 60,
            CustomTurTalebiOfferRejectedManager = 61,
            CustomTurTalebiOfferRejectedAgent = 62,

            CustomTurTalebiOfferApprovedCustomer = 63,
            CustomTurTalebiOfferApprovedMember = 64,
            CustomTurTalebiOfferApprovedOperation = 65,
            CustomTurTalebiOfferApprovedManager = 66,
            CustomTurTalebiOfferApprovedAgent = 67,

            RezervasyonOlusturulduMember = 68,
            RezervasyonOlusturulduOperation = 69,
            RezervasyonOlusturulduManager = 70,
            RezervasyonOlusturulduAgent = 71,

            RezervasyonConfirmedCustomer = 72,
            RezervasyonConfirmedMember = 73,
            RezervasyonConfirmedOperation = 74,
            RezervasyonConfirmedManager = 75,
            RezervasyonConfirmedAgent = 76,

            RezervasyonGuncellemeTalebiReceivedOperation = 77,
            RezervasyonGuncellemeTalebiReceivedManager = 78,
            RezervasyonGuncellemeTalebiReceivedAgent = 79,
            RezervasyonGuncellemeTalebiReceivedCustomer = 80,
            RezervasyonGuncellemeTalebiReceivedMember = 81,

            RezervasyonGuncellemeTalebiApprovedMember = 82,

            RezervasyonIptalTalebiRejectedCustomer = 83,
            RezervasyonGuncellemeTalebiRejectedMember = 84,

            RezervasyonOdemeAlindiCustomer = 85,
            RezervasyonOdemeAlindiMember = 86,
            RezervasyonOdemeAlindiOperation = 87,
            RezervasyonOdemeAlindiManager = 88,
            RezervasyonOdemeAlindiAgent = 89,

            TourGuideChangedOperation = 90,
            TourGuideChangedManager = 91,
            TourGuideChangedGuide = 92,

            ReservationPaymentDateApproachingAgent = 93,
            ReservationPaymentDateApproachingOperation = 94,
            ReservationPaymentDateApproachingManager = 95,

            TwoDaysBeforeTourGuideInform = 96,
            TwoDaysBeforeTourMemberInform = 97,
            TwoDaysBeforeTourCustomerInform = 98,

            TourCompletedAskForCommentCustomerWithLink = 99,
            TourCompletedAskForCommentMemberWithLink = 100,
            TourCompletedAskForCommentCustomerWithoutLink = 101,
            TourCompletedAskForCommentMemberWithoutLink = 102,

            TourCompletedAskedForCommentInformationOperation = 103,
            TourCompletedAskedForCommentInformationManager = 104,

            MonthFirstDayReportAdmin = 105,
            MonthFirstDayReportOperation = 106,
            MonthFirstDayReportManager = 107,

            NewsletterAddedSubscribers = 108,
            NewsletterAddedOperation = 109,
            NewsletterAddedManager = 110,
            BlogPostReceivedCommentOperation = 111,

            ReservationCreatedAskForPriceCustomer = 112,// rezervasyon yaparken fiyat sıfırsa ask for price panele düşzcek kullanıcıya talebiniz alındaı maili gidecek

            PaymentLinkMail = 113
        }
        //rezervasyon güncelleme talebi onaylandı sadece customer ve member var. operation manager ve agent yok. eklenecek
    }
}
