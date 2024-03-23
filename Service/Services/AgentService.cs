using Core;
using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticValues;
using Data.Repository;
using Dto.ApiPanelDtos.AgentDtos;
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using Dto.ApiPanelDtos.TagManagementDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using Service.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AgentService : GenericService<Agent>, IAgentService
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        public AgentService(IGenericRepository<Agent> repository, IUnitOfWork unitOfWork, IUserService userService, IProductService productService) : base(repository, unitOfWork)
        {
            _userService = userService;
            _productService = productService;
        }
        public void AddAgent(ApplyAgentPostDto addAgentDto)
        {
            Agent entity = new Agent
            {
                AgentTitle = addAgentDto.AgentTitle,
                Email = addAgentDto.Email,
                Phone = addAgentDto.Phone,
                ContactPerson = addAgentDto.ContactPerson,
                CountryID = addAgentDto.CountryID,
                //CityID = addAgentDto.CityID,
                TaxAdministration = addAgentDto.TaxAdministration,
                TaxNumber = addAgentDto.TaxNumber,
                CompanyManager = addAgentDto.CompanyManager,
                TradeRegistryNumber = addAgentDto.TradeRegistryNumber,
                WebsiteLink = addAgentDto.WebsiteLink,
                Address = addAgentDto.Address,
                LogoPath = addAgentDto.LogoPath,
            };
            _repository.Add(entity);
            _unitOfWork.saveChanges();
            entity.Status = false;
            _repository.Update(entity);
            _unitOfWork.saveChanges();

        }
        public void ApproveAgent(AgentApprove model)
        {
            var entity = _repository.GetById(model.AgentId);
            entity.ApproveStatus = (int)Enums.ApproveStatus.Onaylandi;
            entity.ServicesDiscountRate = (int)model.Discount;
            entity.DefaultTourDiscount = (int)model.Discount;
            entity.Status = true;
            Random rnd = new Random();
            string password = "00123456"; // rnd.Next(1000000, 9999999).ToString();
            byte[] passwordHash, passwordSalt;
            HashHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            User user = new User()
            {
                AgentId = entity.Id,
                Email = entity.Email,
                Name = entity.ContactPerson.Trim(),
                Surname = entity.ContactPerson.Trim(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleTemplateId = ConstantRoles.SpecialRoleTemplatesGuid.Where(x => x.Option.Equals("Agent")).FirstOrDefault().OptionID //TODO: buradaki idyi merkezden çekelim //new ConstantRoles().GetRoleTemplate( (int)ConstantRoles.No.Member )  
            };
            _userService.Add(user);
            _repository.Update(entity);
            _unitOfWork.saveChanges();
        }
        public void RejectAgent(AgentApprove model)
        {
            var entity = _repository.GetById(model.AgentId);
            entity.ApproveStatus = 3;
            entity.Status = false;
            _repository.Update(entity);
            _unitOfWork.saveChanges();
        }
        public CustomResponseDto<List<ProductNameAndIdDto>> GetProductAdvanceDiscount()
        {
            var ProductInfos = _productService.GetAll().OrderByDescending(x => x.IsTour).Select(x => new ProductNameAndIdDto { Name = x.ProductName, Id = x.Id }).ToList();
            return CustomResponseDto<List<ProductNameAndIdDto>>.Success(200, ProductInfos);
        }
    }
}
