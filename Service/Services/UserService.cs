using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.CustomerDtos;
using Dto.ApiPanelDtos.UserDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.Mail;
using Service.Security.Hashing;
using Service.Security.JWT;
using System.Security.Claims;
using static Core.StaticValues.ConstantRoles;

namespace Service.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionRepository _roleRepository;
        private IConfiguration _config;
        private JwtSettings JwtSettings;
        private readonly IMany_Role_RoleTemplateRepository _many_role_template_repository;
        private readonly IRoleTemplateRepository _roleTemplateRepository;
        private readonly ISendMailService _sendMailService;
        private readonly IMailSenderService _mailSenderService;
        private readonly ISendMailTemplateService _sendMailTemplateService;
        private readonly IUserExtensionMailRepository _userExtensionMailRepository;
        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IRolePermissionRepository roleRepository, IConfiguration config, IOptions<JwtSettings> jwtSettings, IMany_Role_RoleTemplateRepository many_role_template_repository, IRoleTemplateRepository roleTemplateRepository, ISendMailService sendMailService, IMailSenderService mailSenderService, ISendMailTemplateService sendMailTemplateService, IUserExtensionMailRepository userExtensionMailRepository) : base(repository, unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _config = config;
            JwtSettings = jwtSettings.Value;
            _many_role_template_repository = many_role_template_repository;
            _roleTemplateRepository = roleTemplateRepository;
            _sendMailService = sendMailService;
            _mailSenderService = mailSenderService;
            _sendMailTemplateService = sendMailTemplateService;
            _userExtensionMailRepository = userExtensionMailRepository;
        }
        public Guid AddUsers(RegisterPostDto registerDto)
        {
            byte[] passwordHash, passwordSalt;
            HashHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);
            User user = new()
            {
                Email = registerDto.Email,
                Name = registerDto.Name.Trim(),
                Surname = registerDto.Surname.Trim(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                GuideId = registerDto.GuideId,
                RoleTemplateId = registerDto.RoleTemplateId == Guid.Empty ? ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Member).OptionID
: registerDto.RoleTemplateId,
            };
            _userRepository.Add(user);
            _unitOfWork.saveChanges();


            //sadece member oluşturursak mail atma işlemini yapalım yoksa gerek yok
            if (user.RoleTemplateId == ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Member).OptionID)
            {
                var template = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.YeniUyelikOlusturuldu);

                MailPlaceholderUtil.ReplaceMailContent(template, new() { Email = registerDto.Email, Name = registerDto.Name, Surname = registerDto.Surname });

                SendMail mailForMember = new()
                {
                    CreateDate = DateTime.Now,
                    Content = template.Content,
                    Subject = template.Subject,
                    Email = registerDto.Email,
                    IsDeleted = false,
                    Status = true,
                    SendTime = DateTime.Now.AddMinutes(5),
                };
                _mailSenderService.ScheduleMailForSent(mailForMember); //mail gonderildi
            }
            return user.Id;
        }
        public bool IsEmailExist(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user is null)
                return false;
            return true;
        }
        public string LoginUser(Dto.ApiWebDtos.WebToApiDtos.LoginPostDto loginPostDto)
        {
            var user = _userRepository.GetUserByEmail(loginPostDto.Email);

            // kullanıcının rolü nedir buradan çekelim
            if (user != null)
            {
                user.RoleTemplate = _roleTemplateRepository.Where(rt => rt.Id == user.RoleTemplateId).FirstOrDefault();
            }

            if (user != null)
            {
                if (!HashHelper.VerifyPasswordHash(loginPostDto.Password, user.PasswordHash, user.PasswordSalt))
                    return null;

                List<string> userRole = (from many in _many_role_template_repository.Where(x => x.RoleTemplateId == user.RoleTemplateId && !x.IsDeleted)
                                         join role in _roleRepository.GetAll(x => !x.IsDeleted)
                                         on many.RolePermissionId equals role.Id
                                         select role.Name).ToList();

                string nameSurname = user.Name + " " + user.Surname;

                //LoginPostDto login = new LoginPostDto()
                //{
                //    MemberType = userRole,
                //    MemberId = user.Id
                //    CompanyId = user.CompanyId,
                //    Name = user.Name,
                //    Surname = user.Surname
                //};

                var claims = new List<Claim>
                {
                    new (ClaimTypes.Email, loginPostDto.Email),
                    new (ClaimTypes.Name, nameSurname),
                    new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new (ClaimTypes.Role, (user.RoleTemplate?.Name ?? "Member")),
                    //new("RoleTemplateId", user.RoleTemplateId.ToString())
                };

                // kullanıcı eğer agente bağlıysa onuda claim olarak ekleyelim.
                if (user.AgentId != null)
                {
                    claims.Add(new("Agent", user.AgentId.ToString()));
                }

                if (user.GuideId != null)
                {
                    claims.Add(new("Guide", user.GuideId.ToString()));
                }

                foreach (var item in userRole)
                {
                    claims.Add(new("Permission", item));
                    //claims.Add(new Claim(ClaimTypes.Role, item));
                }
                var token = JwtHelper.GetJwtToken(nameSurname, JwtSettings, new TimeSpan(0, 60, 0), claims.ToArray());
                //login.Token = token;
                return token;
            }
            return null;
        }
        public bool ChangePassword(ChangePasswordPostDto changePasswordDto)
        {
            var user = _userRepository.Where(x => x.Id == Guid.Parse(changePasswordDto.UserID)).FirstOrDefault();

            if (user is null)
                return false;

            if (!HashHelper.VerifyPasswordHash(changePasswordDto.Password, user.PasswordHash, user.PasswordSalt))
                return false;


            HashHelper.CreatePasswordHash(changePasswordDto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepository.Update(user);
            _unitOfWork.saveChanges();
            return true;
        }
        public List<UserListDto> GetUserList()
        {
            var userList = _userRepository.GetUserList();
            var userListDto = userList.Select(x => new UserListDto()
            {

                UserID = x.Id.ToString(),
                Email = x.Email,
                FullName = $"{x.Name} {x.Surname}",
                Phone = x.Phone,
                Status = x.Status,
                Roles = new()
                {
                    x.RoleTemplate.Name
                }
            }).ToList();
            return userListDto;
        }
        //panel methods
        public List<CustomerListDto> GetCustomerList()
        {
            var userList = _userRepository.GetUserList();
            var roles = ConstantRoles.SpecialRoleTemplatesGuid.Where(x => x.Option == No.Customer.ToString() || x.Option == No.Member.ToString()).Select(role => role.OptionID).ToList();

            return userList.Where(x => roles.Contains(x.RoleTemplateId)).Select(x => new CustomerListDto
            {
                Email = x.Email,
                MembershipStatus = x.RoleTemplate.Name,
                NameSurname = $"{x.Name} {x.Surname}",
            }).ToList();
        }
        public EditUserDto GetUserForEditById(string id)
        {
            var user = _userRepository.GetById(Guid.Parse(id));
            EditUserDto userDto = new()
            {
                UserID = user.Id.ToString(),
                Name = user.Name,
                Surname = user.Surname,
                Roles = new()
                {
                    user.RoleTemplateId.ToString()
                },
            };
            return userDto;
        }
        public EditUserDto GetUserForEditByEmail(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user is not null)
                return new EditUserDto()
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Roles = new(){
                        user.RoleTemplateId.ToString()
                    },
                    UserID = user.Id.ToString(),
                };
            else return null;
        }
        public List<UserListDto> GetPanelUserList()
        {
            var userList = _userRepository.GetUserList();
            var excludedRoleIds = ConstantRoles.SpecialRoleTemplatesGuid.Select(x => x.OptionID).ToList();

            var panelUsers = userList.Where(x => !excludedRoleIds.Contains(x.RoleTemplateId)).Select(x => new UserListDto
            {
                UserID = x.Id.ToString(),
                Email = x.Email,
                FullName = $"{x.Name} {x.Surname}",
                Phone = x.Phone,
                Roles = new()
                {
                    x.RoleTemplate.Name
                },
                Status = x.Status,
            }).ToList();
            return panelUsers;
        }
        public void UpdateUserRoleTemplate(string userId, string roleTemplateId)
        {
            var user = _userRepository.GetById(Guid.Parse(userId));
            user.RoleTemplateId = Guid.Parse(roleTemplateId);
            user.UpdateDate = DateTime.Now;
            _userRepository.Update(user);
            _unitOfWork.saveChanges();
        }
        public void SendForgotPasswordMail(ForgotPasswordPostDto forgotPasswordPostDto)
        {
            var user = _userRepository.GetUserByEmail(forgotPasswordPostDto.Email);
            var sendedUserForgotPasswordMails = _userExtensionMailRepository.GetUserMails(user.Id.ToString()).Where(x => x.MailTemplateType == 4).ToList();
            bool anyMailStatusChanged = false;
            foreach (var sendedForgotPasswordMail in sendedUserForgotPasswordMails)
            {
                if (sendedForgotPasswordMail.Status)
                {
                    sendedForgotPasswordMail.Status = false;
                    anyMailStatusChanged = true;
                }
            }
            if (anyMailStatusChanged)
                _unitOfWork.saveChanges();

            var forgotPasswordMailTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.SifremiUnuttum);

            var passwordResetCode = new Random().Next(100000, 999999).ToString();
            var uniqueUrlCode = Guid.NewGuid();

            string resetPasswordUrl = $"{_config["MailResetPasswordBaseUrl"]}{uniqueUrlCode}";
            resetPasswordUrl = resetPasswordUrl.Replace("{language}", forgotPasswordPostDto.LanguagePrefix);

            MailPlaceholderUtil.ReplaceMailContent(forgotPasswordMailTemplate, new(name: user.Name, surname: user.Surname, resetCode: passwordResetCode, link: resetPasswordUrl));

            SendMail forgotPasswordMail = new()
            {
                CreateDate = DateTime.Now,
                Content = forgotPasswordMailTemplate.Content,
                Subject = forgotPasswordMailTemplate.Subject,
                Email = forgotPasswordPostDto.Email,
                IsDeleted = false,
                Status = true,
                SendTime = DateTime.Now,
            };

            _mailSenderService.SendInstantMail(forgotPasswordMail);

            var sendedMail = new UserExtensionMail()
            {
                UserId = user.Id,
                MailTemplateType = forgotPasswordMailTemplate.Template,
                ExpireDate = DateTime.Now.AddDays(1),
                ResetCode = passwordResetCode,
                UrlCode = uniqueUrlCode,
            };
            _userExtensionMailRepository.Add(sendedMail);
            _unitOfWork.saveChanges();
        }
        public bool IsResetPasswordUrlCodeValid(string urlCode)
        {
            var mail = _userExtensionMailRepository.GetByUrlCode(urlCode);

            var forgotPasswordTemplateType = SendMailTemplateName.Status.Where(x => x.ID == (int)SendMailTemplateName.No.SifremiUnuttum).FirstOrDefault().ID;

            if (mail is null)
                return false;

            if (mail.MailTemplateType != forgotPasswordTemplateType)
                return false;

            if (mail.ExpireDate < DateTime.Now)
                return false;

            if (!mail.Status)
                return false;

            return true;
        }
        public CustomResponseNullDto ForgotPasswordCheckValidation(ResetPasswordValidationPostDto dto)
        {
            if (!IsResetPasswordUrlCodeValid(dto.UrlCode))
                return CustomResponseNullDto.Fail(404, "Invalid Url");

            var sendedMail = _userExtensionMailRepository.GetByUrlCode(dto.UrlCode);

            if (sendedMail.ResetCode != dto.ResetCode)
                return CustomResponseNullDto.Fail(400, "Invalid Code");


            return CustomResponseNullDto.Success(200);
        }
        public void ResetPassword(ResetPasswordPostDto resetPasswordPostDto)
        {
            HashHelper.CreatePasswordHash(resetPasswordPostDto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            var user = GetById(Guid.Parse(resetPasswordPostDto.UserId));
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userRepository.Update(user);

            var sendedMail = _userExtensionMailRepository.GetByUrlCode(resetPasswordPostDto.UrlCode);
            sendedMail.Status = false;
            _userExtensionMailRepository.Update(sendedMail);
            _unitOfWork.saveChanges();
        }
        public List<UserDto> GetUserListByRoleTemplate(string roleTemplateId)
        {
            return _userRepository.GetUserByRoleTemplate(Guid.Parse(roleTemplateId)).Select(x => new UserDto
            {
                UserID = x.Id.ToString(),
                Email = x.Email,
                Name = x.Name,
                Surname = x.Surname,
            }).ToList();
        }
        public CustomResponseDto<string> PanelUserLogin(LoginPostDto loginDto)
        {
            var user = _userRepository.GetUserByEmail(loginDto.Email);

            var panelNotAccessibleRoles = ConstantRoles.SpecialRoleTemplatesGuid.Where(x => x.OptionID != ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Admin).OptionID).Select(t => t.OptionID).ToList();

            if (user is null)
                return CustomResponseDto<string>.Fail(404, "User not found");

            else if (panelNotAccessibleRoles.Contains(user.RoleTemplateId))
                return CustomResponseDto<string>.Fail(401, "Unauthorized");

            if (!HashHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
                return CustomResponseDto<string>.Fail(400, "Wrong password");

            List<string> userRolePermissions = (from many in _many_role_template_repository.Where(x => x.RoleTemplateId == user.RoleTemplateId && !x.IsDeleted)
                                                join role in _roleRepository.GetAll(x => !x.IsDeleted)
                                                on many.RolePermissionId equals role.Id
                                                select role.Name).ToList();

            string nameSurname = user.Name + " " + user.Surname;

            var claims = new List<Claim>()
            {
              new (ClaimTypes.Email, loginDto.Email),
              new (ClaimTypes.Name, nameSurname),
              new (ClaimTypes.NameIdentifier, user.Id.ToString()),
              new (ClaimTypes.Role, user.RoleTemplate.Name)
            };
            userRolePermissions.ForEach(permission =>
            {
                claims.Add(new("Permission", permission));
            });

            var token = JwtHelper.GetJwtToken(nameSurname, JwtSettings, new TimeSpan(0, 60, 0), claims.ToArray());

            return CustomResponseDto<string>.Success(200, token);
        }
    }
}

