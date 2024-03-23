using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticValues;
using Dto.ApiPanelDtos.Driver;
using Dto.ApiPanelDtos.DriverDtos;
using Dto.ApiWebDtos.GeneralDtos;
using Service.Security.Hashing;

namespace Service.Services
{
    /// <summary>
    /// driver işlemleri artık user tablosundan yürütülüyor, o yüzden generic service ve generic repo methodlarını kullanmıyorum
    /// uow kullanmak için yapıyı değiştirmedim
    /// -mehmet ali
    /// </summary>
    public class DriverService : GenericService<Driver>, IDriverService
    {
        readonly IRoleTemplateService _roleTemplateService;
        readonly IUserRepository _userRepository;
        public DriverService(IGenericRepository<Driver> repository, IUnitOfWork unitOfWork, IDriverRepository driverRepository, IRoleTemplateService roleTemplateService, IUserRepository userRepository) : base(repository, unitOfWork)
        {
            _roleTemplateService = roleTemplateService;
            _userRepository = userRepository;
        }

        public void AddDriver(AddDriverDto addDriverDto)
        {

            var driverRoleTemplateId = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Driver).OptionID;

            HashHelper.CreatePasswordHash("00123456", out byte[] passwordHash, out byte[] passwordSalt); //default password
            _userRepository.Add(new User
            {
                Email = addDriverDto.Email,
                Name = addDriverDto.Name,
                Surname = addDriverDto.Surname,
                Phone = addDriverDto.PhoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleTemplateId = driverRoleTemplateId,
            });

            //send mail to driver user

            _unitOfWork.saveChanges();
        }

        public List<SelectListOptionDto> DriverSelectList()
        {
            List<SelectListOptionDto> driverSelectList = _userRepository.Where(x => x.RoleTemplateId == ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Driver).OptionID).Select(x => new SelectListOptionDto
            {
                Option = $"{x.Name} {x.Surname}",
                OptionID = x.Id
            }).ToList();
            return driverSelectList;
        }

        public void EditDriver(EditDriverDto editDriverDto)
        {
            var driverUser = _userRepository.GetById(Guid.Parse(editDriverDto.UserId));

            driverUser.Email = editDriverDto.Email;
            driverUser.Name = editDriverDto.Name;
            driverUser.Surname = editDriverDto.Surname;
            driverUser.Phone = editDriverDto.PhoneNumber;
            _userRepository.Update(driverUser);
            _unitOfWork.saveChanges();
        }

        public List<DriverListDto> GetDrivers()
        {
            var drivers = _userRepository.Where(x => x.RoleTemplateId == ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Driver).OptionID).ToList();
            return drivers.Select(x => new DriverListDto
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Surname = x.Surname,
                PhoneNumber = x.Phone,
                Status = x.Status,
                Email = x.Email,
            }).ToList();
        }

        public EditDriverDto GetEditDriver(string id)
        {
            var driverUser = _userRepository.GetById(Guid.Parse(id));
            EditDriverDto dto = new()
            {
                UserId = id,
                Email = driverUser.Email,
                Name = driverUser.Name,
                Surname = driverUser.Surname,
                PhoneNumber = driverUser.Phone,
            };
            return dto;
        }

        public void ToggleDriverStatus(Guid id)
        {
            _userRepository.ToggleStatus(id);
            _unitOfWork.saveChanges();
        }


    }
}
