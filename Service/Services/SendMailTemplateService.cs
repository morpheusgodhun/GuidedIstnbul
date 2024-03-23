using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticValues;
using Dto.ApiPanelDtos.MailTemplateDtos;
using Dto.ApiPanelDtos.SendMailDtos;
using Dto.ApiWebDtos.GeneralDtos;

namespace Service.Services
{
    public class SendMailTemplateService : GenericService<SendMailTemplate>, ISendMailTemplateService
    {
        private readonly ISendMailTemplateRepository _sendMailTemplateRepository;
        private readonly ISendMailTemplateLanguageRepository _sendMailTemplateLanguageRepository;

        public SendMailTemplateService(IGenericRepository<SendMailTemplate> repository, IUnitOfWork unitOfWork, ISendMailTemplateRepository sendMailTemplateRepository, ISendMailTemplateLanguageRepository sendMailTemplateLanguageRepository) : base(repository, unitOfWork)
        {
            _sendMailTemplateRepository = sendMailTemplateRepository;
            _sendMailTemplateLanguageRepository = sendMailTemplateLanguageRepository;
        }

        public (bool isAdded, Guid addedId, string templateName) AddMailTemplate(int templateNo)
        {
            SendMailTemplate template = new()
            {
                Template = templateNo,
            };
            _sendMailTemplateRepository.Add(template);
            var isAdded = _unitOfWork.saveChanges() > 0;
            if (isAdded)
                return (isAdded, template.Id, SendMailTemplateName.GetValue(templateNo));
            else
                return (isAdded, Guid.Empty, string.Empty);
        }

        public void AddMailTemplateItem(AddMailTemplateItemDto dto)
        {
            SendMailTemplateLanguageItem turkishLangItem = new()
            {
                Content = dto.TurkishContent,
                LanguageId = 1,
                SendMailTemplateId = dto.SendMailTemplateId,
                Subject = dto.TurkishSubject
            };
            _sendMailTemplateLanguageRepository.Add(turkishLangItem);
            SendMailTemplateLanguageItem engilshLangItem = new()
            {
                Content = dto.EnglishContent,
                LanguageId = 2,
                SendMailTemplateId = dto.SendMailTemplateId,
                Subject = dto.EnglishSubject
            };
            _sendMailTemplateLanguageRepository.Add(engilshLangItem);

            _unitOfWork.saveChanges();
        }

        public SendMailTemplateDto GetTemplateByNo(int no)
        {
            return _sendMailTemplateRepository.GetTemplateByNo(no);
        }

        public List<MailTemplateListDto> GetTemplateList()
        {
            return _sendMailTemplateRepository.GetTemplateListDtos();
        }

        public List<SelectListOption> GetTemplateListToCreateLanguageItem()
        {
            var createData = _sendMailTemplateRepository.GetTemplateListToCreateLanguageItem();
            return createData;
        }
    }
}
