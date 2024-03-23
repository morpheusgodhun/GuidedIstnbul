using Core.Entities;
using Core.IRepository;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.MailTemplateDtos;
using Dto.ApiPanelDtos.SendMailDtos;
using Dto.ApiWebDtos.GeneralDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SendMailTemplateRepository : GenericRepository<SendMailTemplate>, ISendMailTemplateRepository
    {
        public SendMailTemplateRepository(Context context) : base(context)
        {

        }

        public SendMailTemplateDto GetTemplateByNo(int no, int languageId = 2) //TODO: dil parametre gönderelim //todo mailTemplatelerin languageId si ters eklenmiş. 2 ler ing 1 ler tr. düzeltilecek
        {

            SendMailTemplateDto mailTemplate = (from template in _context.SendMailTemplates
                                                join templateLang in _context.SendMailTemplateLanguageItems
                                                on template.Id equals templateLang.SendMailTemplateId
                                                where template.Template == no && templateLang.LanguageId == languageId
                                                select new SendMailTemplateDto
                                                {
                                                    Template = template.Template,
                                                    Subject = templateLang.Subject,
                                                    Content = templateLang.Content,
                                                }).FirstOrDefault();

            /*.DefaultIfEmpty(new SendMailTemplateDto
                                                {
                                                    Template = 0,
                                                    Subject = "",
                                                    Content = "",
                                                })*/
            return mailTemplate;
        }

        public List<MailTemplateListDto> GetTemplateListDtos()
        {
            var templatesWithLanguageItems = _context.SendMailTemplates
             .Include(t => t.SendMailTemplateLanguageItems)
             .ToList();

            List<MailTemplateListDto> templateList = new();

            foreach (var templateWithLanguageItems in templatesWithLanguageItems)
            {
                foreach (var languageItem in templateWithLanguageItems.SendMailTemplateLanguageItems)
                {
                    MailTemplateListDto dto = new()
                    {
                        MailTemplateId = templateWithLanguageItems.Id.ToString(),
                        TemplateNo = templateWithLanguageItems.Template.ToString(),
                        Subject = languageItem.Subject,
                        Content = languageItem.Content,
                        LanguageId = languageItem.LanguageId,
                        TemplateName = SendMailTemplateName.GetValue(templateWithLanguageItems.Template)
                    };

                    templateList.Add(dto);
                }
            }

            return templateList;
        }

        //return SelectListOption as templateId, templateConstValue
        public List<SelectListOption> GetTemplateListToCreateLanguageItem()
        {
            var sendMailTemplates = SendMailTemplateName.Status.ToList(); //hepsi
            //dbde var olanlar
            var existingTemplates = _context.SendMailTemplates.Include(x => x.SendMailTemplateLanguageItems).Select(x => new SelectListOption
            {
                ID = x.Template,
                Value = SendMailTemplateName.GetValue(x.Template)
            }).ToList();

            List<int> returnListIds = sendMailTemplates.Select(x => x.ID).Except(existingTemplates.Select(x => x.ID)).ToList();

            var returnList = sendMailTemplates.Where(x => returnListIds.Contains(x.ID)).ToList();
            return returnList;
        }


        string EscapeCsvField(string field)
        {
            return field.Replace("\"", "\"\"");
        }
    }
}
