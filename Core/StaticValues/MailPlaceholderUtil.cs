using Core.Entities;
using Dto.ApiPanelDtos.SendMailDtos;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.StaticClass
{
    public class MailPlaceholderUtil
    {
        public static List<string> ContentPlaceholders = new()
        {
            "[[AdminAnswer]]",
            "[[AdminName]]",
            "[[AgencyName]]",
            "[[Any Special Requests/Notes]]",
            "[[ArrivalDate]]",
            "[[Average Customer Rating for Last Month]]",
            "[[BlogPostTitle]]",
            "[[BriefDescriptionOfBlogPost]]",
            "[[Cancellation]]",
            "[[CancellationReasonSelectListItem]]",
            "[[CancellationReasonText]]",
            "[[Changes]]",
            "[[CommentContent]]",
            "[[CommenterName]]",
            "[[ContactInformation]]",
            "[[ContactName]]",
            "[[CurrentGuide]]",
            "[[CustomerName]]",
            "[[Date]]",
            "[[DepartureDate]]",
            "[[Destination]]",
            "[[Destinations]]",
            "[[DueDate]]",
            "[[Email]]",
            "[[Estimated Tour Sales for Next Month]]",
            "[[Filled form fields and payment amount]]",
            "[[FORM DETAILS]]",
            "[[GatheringPoint]]",
            "[[Generated Password]]",
            "[[Google Maps Review Link]]",
            "[[GuestName]]",
            "[[Guide Contact Information]]",
            "[[GuideName]]",
            "[[If requesting an arrangement, specify the desired changes, if any]]",
            "[[InterestedWith]]",
            "[[Link]]",
            "[[Meeting Point]]",
            "[[Member]]",
            "[[Message]]",
            "[[Name]]",
            "[[Name of the Most Popular Tour]]",
            "[[NewBlogPostTitle]]",
            "[[NewGuide]]",
            "[[Number of Cancellations Received this Month]]",
            "[[Number of Cancellations This Month]]",
            "[[Number of New Agent Registrations this Month]]",
            "[[Number of New Agent Registrations This Month]]",
            "[[Number of New Guide Registrations this Month]]",
            "[[Number of New Guide Registrations This Month]]",
            "[[Number of New Reservations This Month]]",
            "[[Number of Reservations Received this Month]]",
            "[[Number of Tours Conducted Last Month]]",
            "[[NumberOfParticipants]]",
            "[[OperationMember]]",
            "[[Payment]]",
            "[[Phone]]",
            "[[Price]]",
            "[[Profit or Loss for Last Month]]",
            "[[Provide a detailed description of the proposed tour, including itinerary, special features, and any inclusions]]",
            "[[Quotes from Positive Feedback Received this Month]]",
            "[[ReasonForCancellation]]",
            "[[ReferenceNumber]]",
            "[[RelevantTopic]]",
            "[[RequestDate]]",
            "[[RequestForArrangement]]",
            "[[RequestId]]",
            "[[RequestReason]]",
            "[[ReservationCode]]",
            "[[ResetCode]]",
            "[[ResetUrl]]",
            "[[SelectCountry]]",
            "[[SubscriberName]]",
            "[[Surname]]",
            "[[Total Expenses for Last Month]]",
            "[[Total Revenue for Last Month]]",
            "[[TotalAmountDue]]",
            "[[TourDate]]",
            "[[TourGuideName]]",
            "[[TourName]]",
            "[[TourPackageName]]",
            "[[TourSalesPlatformName]]",
            "[[TripAdvisor Review Link]]",
            "[[UpdateReason]]",
            "[[Username]]"
        };

        public static List<string> ExtractPlaceholders(string content)
        {
            List<string> placeholders = new();
            Regex regex = new(@"\[\[\s*(.*?)\s*\]\]", RegexOptions.Singleline);

            placeholders.AddRange(regex.Matches(content).Cast<Match>().Select(match => match.Groups[1].Value));

            return placeholders;
        }
        public static void ReplaceMailContent(SendMailTemplateDto template, PlaceholdersDto placeholderDto)
        {
            List<string> templatePlaceholders = ExtractPlaceholders(template.Content);
            var placeholderProperties = typeof(PlaceholdersDto).GetProperties();
            foreach (string placeholder in templatePlaceholders)
            {
                var matchingProp = placeholderProperties.Where(p => p.Name == placeholder).FirstOrDefault();
                if (matchingProp is not null)
                {
                    var dtoValue = typeof(PlaceholdersDto).GetProperty(matchingProp.Name).GetValue(placeholderDto);
                    //string placeholderNewValue = matchingProp.GetValue(matchingProp).ToString();
                    if (dtoValue is not null)
                        template.Content = template.Content.Replace($"[[{placeholder}]]", dtoValue.ToString());
                }
            }
            template.Content = $@"<div>
      <div marginwidth=""0"" marginheight=""0"" width=""100%""
         style=""border-collapse:collapse;border-spacing:0;margin:0;padding:0;width:100%;height:100%;line-height:100%;background-color:#f0f0f0;color:#000000""
         bgcolor=""#F0F0F0"" text=""#000000"">
         <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""
            style=""border-collapse:collapse;border-spacing:0;margin:0;padding:0;width:100%"">
            <tbody>
               <tr>
                  <td align=""center"" valign=""top"" style=""border-collapse:collapse;border-spacing:0;margin:0;padding:0""
                     bgcolor=""#F0F0F0"">
                     <table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" width=""560""
                        style=""border-collapse:collapse;border-spacing:0;padding:0;width:inherit;max-width:560px"">

                        <tbody>
                           <tr>
                              <td align=""center"" valign=""top""
                                 style=""border-collapse:collapse;border-spacing:0;margin:0;padding:0;padding-left:6.25%;padding-right:6.25%;width:87.5%;padding-top:20px;padding-bottom:20px"">

                                 <div
                                    style=""display:none;overflow:hidden;opacity:0;font-size:1px;line-height:1px;height:0;max-height:0;max-width:0;color:#f0f0f0"">
                                    Your tour order details on Boutique Turkey Tours</div>
                                 <a style=""text-decoration:none""
                                    href=""#m_-6168874969522800507_m_-1586450903272578412_""><img border=""0"" vspace=""0""
                                       hspace=""0""
                                       src=""https://ci3.googleusercontent.com/meips/ADKq_NbExhmAB1Upho3dSS4YkIbu2BEIddsCOd7lLiOGa6Jb2NqUJK1mldUuVATkfLE0BMidQpuXzaPRoCizEFvR1PGEMhvDj9PUIWrShzf670Pm=s0-d-e1-ft#https://www.guidedistanbultours.com/media/logo-black-1.png""
                                       width=""160"" height=""50"" alt=""Logo"" title=""Logo""
                                       style=""color:#000000;font-size:10px;margin:0;padding:0;outline:none;text-decoration:none;border:none;display:block""
                                       class=""CToWUd"" data-bit=""iit""></a>

                              </td>
                           </tr>
                        </tbody>
                     </table>
                     <table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" bgcolor=""#FFFFFF"" width=""560""
                        style=""border-collapse:collapse;border-spacing:0;padding:0;width:inherit;max-width:560px"">
                        <tbody>


                           <tr>
                              <td align=""center"" valign=""top""
                                 style=""border-collapse:collapse;border-spacing:0;margin:0;padding:0;padding-top:20px"">
                                 <a style=""text-decoration:none""
                                    href=""#m_-6168874969522800507_m_-1586450903272578412_""><img border=""0"" vspace=""0""
                                       hspace=""0""
                                       src=""https://ci3.googleusercontent.com/meips/ADKq_NYLR7Um2b5TzJyayOvvb1L368BYPhKXfjMn1aihbfrNejsl75WQCOrMk-3BMx2DUGl8lJiYtzN0fLLtIrpPV6k8uCdqwNi-4B-5gamjbjWSVNwgXBrz2GZHk2c7ujk7otSi=s0-d-e1-ft#https://www.guidedistanbultours.com/media/istanbul_layover_tour_galley_7.jpg""
                                       alt=""Please enable images to view this content"" title=""Istanbul Layover Tour""
                                       width=""560""
                                       style=""width:100%;max-width:560px;color:#000000;font-size:13px;margin:0;padding:0;outline:none;text-decoration:none;border:none;display:block""
                                       class=""CToWUd"" data-bit=""iit""></a>
                              </td>
                           </tr>

                           <tr>
                              <td align=""center"" valign=""top""
                                 style=""border-collapse:collapse;border-spacing:0;margin:0;padding:0;padding-left:6.25%;padding-right:6.25%;width:87.5%;padding-top:25px;padding-bottom:25px"">
                                 <hr color=""#E0E0E0"" align=""center"" width=""100%"" size=""1"" noshade=""""
                                    style=""margin:0;padding:0"">
                              </td>
                           </tr>

                           <tr>
                              <td>
                                 <table align=""center""
                                    style=""padding-top:20px;width:80%;font-family:sans-serif;font-size:13px"">
                                    <tbody>
                                       <tr>
                                          <td>{template.Content}</td>
                                       </tr>
                                      
                                    </tbody>
                                 </table>
                              </td>
                           </tr>

                           <tr>
                              <td align=""center"" valign=""top""
                                 style=""border-collapse:collapse;border-spacing:0;margin:0;padding:0;padding-left:6.25%;padding-right:6.25%;width:87.5%;padding-top:25px"">
                                 <hr color=""#E0E0E0"" align=""center"" width=""100%"" size=""1"" noshade=""""
                                    style=""margin:0;padding:0"">
                              </td>
                           </tr>
                           <tr>
                              <td align=""center"" valign=""top""
                                 style=""border-collapse:collapse;border-spacing:0;margin:0;padding:0;padding-left:6.25%;padding-right:6.25%;width:87.5%;font-size:17px;font-weight:400;line-height:160%;padding-top:20px;padding-bottom:25px;color:#000000;font-family:sans-serif"">
                                 Have a&nbsp;question? <a href=""mailto:gitreservations@gmail.com""
                                    style=""color:#127db3;font-family:sans-serif;font-size:17px;font-weight:400;line-height:160%""
                                    target=""_blank"">gitreservations@gmail.com</a>
                              </td>
                           </tr>
                        </tbody>
                     </table>

                     <table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" width=""560""
                        style=""border-collapse:collapse;border-spacing:0;padding:0;width:inherit;max-width:560px"">

                        <tbody>
                           <tr>
                              <td align=""center"" valign=""top""
                                 style=""border-collapse:collapse;border-spacing:0;margin:0;padding:0;padding-left:6.25%;padding-right:6.25%;width:87.5%;padding-top:25px"">
                                 <table width=""256"" border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center""
                                    style=""border-collapse:collapse;border-spacing:0;padding:0"">
                                    <tbody>
                                       <tr>

                                          <td align=""center"" valign=""middle""
                                             style=""margin:0;padding:0;padding-left:10px;padding-right:10px;border-collapse:collapse;border-spacing:0"">
                                             <a href=""#m_-6168874969522800507_m_-1586450903272578412_""
                                                style=""text-decoration:none""><img border=""0"" vspace=""0"" hspace=""0""
                                                   style=""padding:0;margin:0;outline:none;text-decoration:none;border:none;display:inline-block;color:#000000""
                                                   alt=""F"" title=""Facebook"" width=""39"" height=""39""
                                                   src=""https://ci3.googleusercontent.com/meips/ADKq_NYGqhfmFNznD0G-A4NbyYOwx2buQGvhuKLftw9srqas0rwtNxkLUEEYr2qMPeKNcRB1C9WmLiffp0PBriFhpWbokUgOjm0k0iW7pgI=s0-d-e1-ft#https://www.guidedistanbultours.com/media/facebook.png""
                                                   class=""CToWUd"" data-bit=""iit""></a>
                                          </td>

                                          <td align=""center"" valign=""middle""
                                             style=""margin:0;padding:0;padding-left:10px;padding-right:10px;border-collapse:collapse;border-spacing:0"">
                                             <a href=""#m_-6168874969522800507_m_-1586450903272578412_""
                                                style=""text-decoration:none""><img border=""0"" vspace=""0"" hspace=""0""
                                                   style=""padding:0;margin:0;outline:none;text-decoration:none;border:none;display:inline-block;color:#000000""
                                                   alt=""T"" title=""Twitter"" width=""40"" height=""40""
                                                   src=""https://ci3.googleusercontent.com/meips/ADKq_Nbw3_1--MXvId0Xhhaobkv53clCUxEM-nLlGAxtNb7AK3Aw-N_Khx14Mr4irSXOJ-1xS2KVUSP0UG39tT6YRoJaVZST3_WAF8BE5i-e=s0-d-e1-ft#https://www.guidedistanbultours.com/media/instagram.png""
                                                   class=""CToWUd"" data-bit=""iit""></a>
                                          </td>

                                          <td align=""center"" valign=""middle""
                                             style=""margin:0;padding:0;padding-left:10px;padding-right:10px;border-collapse:collapse;border-spacing:0"">
                                             <a href=""#m_-6168874969522800507_m_-1586450903272578412_""
                                                style=""text-decoration:none""><img border=""0"" vspace=""0"" hspace=""0""
                                                   style=""padding:0;margin:0;outline:none;text-decoration:none;border:none;display:inline-block;color:#000000""
                                                   alt=""G"" title=""Google Plus"" width=""44"" height=""44""
                                                   src=""https://ci3.googleusercontent.com/meips/ADKq_Nbzyj224EE5dHV0u4fqcqCvYkHl7v9NG4Vm8CWcQUZx-LjwMyb6SNvv-vbx-4yIp_rqJEBPiGcjVk2u8pnlesZX1pF5DClCPoh-swfVbAk=s0-d-e1-ft#https://www.guidedistanbultours.com/media/tripadvisor.png""
                                                   class=""CToWUd"" data-bit=""iit""></a>
                                          </td>

                                       </tr>
                                    </tbody>
                                 </table>
                              </td>
                           </tr>

                           <tr>
                              <td align=""center"" valign=""top""
                                 style=""border-collapse:collapse;border-spacing:0;margin:0;padding:0;padding-left:6.25%;padding-right:6.25%;width:87.5%;font-size:13px;font-weight:400;line-height:150%;padding-top:20px;padding-bottom:20px;color:#999999;font-family:sans-serif"">

                                 This email was sent to&nbsp;you because you filled the booking form on <a
                                    href=""https://www.guidedistanbultours.com/""
                                    style=""text-decoration:underline;color:#999999;font-family:sans-serif;font-size:13px;font-weight:400;line-height:150%""
                                    target=""_blank""
                                    data-saferedirecturl=""https://www.google.com/url?q=https://www.guidedistanbultours.com/&amp;source=gmail&amp;ust=1708780455420000&amp;usg=AOvVaw326tBjo9L9mVCGojVIp5Wi"">Guided
                                    Istanbul Tours</a> page. Please contact us if you think you've received this by
                                 mistake.

                              </td>
                           </tr>
                        </tbody>
                     </table>

                  </td>
               </tr>
            </tbody>
         </table>
         <div class=""yj6qo""></div>
         <div class=""adL"">

         </div>
      </div>
      <div class=""adL"">


      </div>
   </div>";

            Console.WriteLine(template.Content);
        }
    }

}

