using Core.IService;
using Core.StaticValues;
using Dto.ApiPanelDtos.SeoDtos;
using Dto.ApiPanelDtos.UserDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Core.StaticValues.ConstantRoles;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelUserController : ControllerBase
    {
        readonly IUserService _userService;


        public PanelUserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public CustomResponseDto<List<UserListDto>> UserList()
        {
            var data = _userService.GetUserList();
            return CustomResponseDto<List<UserListDto>>.Success(200, data);
        }
        [HttpGet]
        public CustomResponseDto<List<UserListDto>> PanelUserList()
        {
            List<UserListDto> userListDtos = _userService.GetPanelUserList();
            return CustomResponseDto<List<UserListDto>>.Success(200, userListDtos);
        }

        [HttpGet("{email}")]
        public CustomResponseDto<AddUserDto> FindUser(string email)
        {
            var user = _userService.GetUserForEditByEmail(email);
            if (user is null)
                return CustomResponseDto<AddUserDto>.Fail(404, "User not found");

            //admin & customer 
            var excludedConstantRoles = ConstantRoles.SpecialRoleTemplatesGuid.Where(x => x.Option != ConstantRoles.No.Member.ToString()).Select(x => x.OptionID).ToList();

            //emaili girilen kullanıcı admin veya customer ise !
            if (user.Roles.Any(roleId => excludedConstantRoles.Contains(Guid.Parse(roleId))))
            {
                return CustomResponseDto<AddUserDto>.Fail(403, null);
            }
            else
            {
                AddUserDto addUserDto = new()
                {
                    UserID = user.UserID,
                    Name = user.Name,
                    Surname = user.Surname,
                    Roles = user.Roles,
                };
                return CustomResponseDto<AddUserDto>.Success(200, addUserDto);
            }
        }

        [HttpPost]
        public CustomResponseNullDto AddUserToPanel(AddUserDto addUserDto)
        {
            _userService.UpdateUserRoleTemplate(addUserDto.UserID, addUserDto.Roles.FirstOrDefault());
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditUserDto> EditUser(string id)
        {
            EditUserDto editUserDto = _userService.GetUserForEditById(id);
            return CustomResponseDto<EditUserDto>.Success(200, editUserDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditUser(EditUserDto editUserDto)
        {
            _userService.UpdateUserRoleTemplate(editUserDto.UserID, editUserDto.Roles.FirstOrDefault());
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleUserStatus(string id)
        {
            _userService.ToggleStatus(Guid.Parse(id));
            return CustomResponseNullDto.Success(204);
        }
    }
}
