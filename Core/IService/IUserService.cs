using Core.Entities;
using Dto.ApiPanelDtos.CustomerDtos;
using Dto.ApiPanelDtos.UserDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Agent;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IUserService : IGenericService<User>
    {
        CustomResponseDto<string> PanelUserLogin(LoginPostDto loginDto);
        string LoginUser(LoginPostDto loginPostDto);
        bool IsEmailExist(string email);
        Guid AddUsers(RegisterPostDto registerDto);
        bool ChangePassword(ChangePasswordPostDto changePasswordDto);
        void SendForgotPasswordMail(ForgotPasswordPostDto forgotPasswordPostDto);
        CustomResponseNullDto ForgotPasswordCheckValidation(ResetPasswordValidationPostDto dto);
        List<UserListDto> GetUserList();
        List<UserDto> GetUserListByRoleTemplate(string roleTemplateId);
        //panel methods
        List<UserListDto> GetPanelUserList();
        List<CustomerListDto> GetCustomerList();
        EditUserDto GetUserForEditById(string id);
        EditUserDto GetUserForEditByEmail(string email);
        void UpdateUserRoleTemplate(string userId, string newRoleTemplateId);
        void ResetPassword(ResetPasswordPostDto resetPasswordPostDto);

    }
}
