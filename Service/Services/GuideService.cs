using Core;
using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticValues;
using Dto.ApiPanelDtos;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;

namespace Service.Services
{
    public class GuideService : GenericService<Guide>, IGuideService
    {
        private readonly IGuideRepository _guideRepository;
        private readonly IUserService _userService;
        public GuideService(IGenericRepository<Guide> repository, IUnitOfWork unitOfWork, IGuideRepository guideRepository, IUserService userService) : base(repository, unitOfWork)
        {
            _guideRepository = guideRepository;
            _userService = userService;
        }
        public CustomResponseDto<List<GetGuideDto>> GetAllGuides()
        {
            var Guides = _guideRepository.GetAllGuides();
            return CustomResponseDto<List<GetGuideDto>>.Success(200, Guides);
        }
        public CustomResponseNullDto ApproveGuide(Guid Id)
        {
            var selectedGuide = GetById(Id);
            selectedGuide.ApproveStatus = (int)Enums.ApproveStatus.Onaylandi;
            _userService.AddUsers(new RegisterPostDto
            {
                Email = selectedGuide.Email,
                Name = selectedGuide.Name,
                Password = "00123456",
                PasswordAgain = "00123456",
                Surname = selectedGuide.Surname,
                GuideId = selectedGuide.Id,
                RoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Guide).OptionID
            });
            _unitOfWork.saveChanges();
            return CustomResponseNullDto.Success(200);
        }
        public CustomResponseNullDto RejectGuide(Guid Id)
        {
            var selectedGuide = GetById(Id);
            selectedGuide.ApproveStatus = (int)Enums.ApproveStatus.Reddedildi;
            return CustomResponseNullDto.Success(200);
        }
        public CustomResponseNullDto ChangeGuideStatus(Guid Id)
        {
            var selectedGuide = GetById(Id);
            selectedGuide.Status = !selectedGuide.Status;
            _unitOfWork.saveChanges();
            return CustomResponseNullDto.Success(200);
        }

        public List<SelectListOptionDto> GetGuideSelectList()
        {
            var guideSelectList = _repository.GetAll().Select(x => new SelectListOptionDto
            {
                Option = x.Name,
                OptionID = x.Id
            }).ToList();
            return guideSelectList;
        }
    }
}
