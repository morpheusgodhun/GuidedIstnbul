using Dto.ApiWebDtos.ApiToWebDtos.About;
using Dto.ApiWebDtos.ApiToWebDtos.Agent;
using Dto.ApiWebDtos.ApiToWebDtos.GeneralPages;
using Dto.ApiWebDtos.ApiToWebDtos.Profile;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GuideWeb.Controllers
{
    public class AgentController : CustomBaseController
    {
        private readonly ICookie _cookie;
        private readonly IFileUpload _fileUploadHandler;

        public AgentController(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie, IFileUpload fileUploadHandler) : base(apiHandler, configuration, cookie)
        {
            _cookie = cookie;
            _fileUploadHandler = fileUploadHandler;
        }

        public IActionResult UserManagement()
        {

            //TODO:eğer bulamazsa agent idsini başka biryere gidelim // aynısını memberdada yapalım

            var memberAgentId = _cookie.getMemberAgentId();
            string url = _url + "WebUserManagement/GetUserManagement/" + memberAgentId + "/1";

            string url1 = _url + "WebUserPage/GetApplyGuide/1";
            CustomResponseDto<GetApplyGuideDto> getApplyGuideDto = _apiHandler.GetApi<CustomResponseDto<GetApplyGuideDto>>(url1);

            CustomResponseDto<GetUserManagementDto> getUserManagementDto = _apiHandler.GetApi<CustomResponseDto<GetUserManagementDto>>(url);
            getUserManagementDto.Data.ConstantValues.AddRange(getApplyGuideDto.Data.ConstantValues);
            getUserManagementDto.Data.LanguageKnowOptions = getApplyGuideDto.Data.LanguageKnowOptions;
            getUserManagementDto.Data.SpecializeCityOptions = getApplyGuideDto.Data.SpecializeCityOptions;

            if (getUserManagementDto is null)
            {
                return View();
            }
            else
            {
                return View(getUserManagementDto.Data);
            }
        }

        // agent kullanıcı ekleme yapacağı zaman arama işlemi
        [HttpPost]
        public IActionResult NewUserInfo(AgentNewUserDto searchUser)
        {
            //AgentNewUserDto agentNewUser = new AgentNewUserDto();
            CustomResponseDto<AgentNewUserDto> getagentNewUser = new CustomResponseDto<AgentNewUserDto>();

            // herkes istek yapamasın sadece agentleri dolu olanlar sorgu atabilsin
            var memberAgentId = _cookie.getMemberAgentId();
            if (memberAgentId == "")
            {
                getagentNewUser.Errors = new List<string> { "You are not authorized" };
                return Json(getagentNewUser);
            }


            if (searchUser.Email == null)
                searchUser.Email = "";
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            Regex regex = new Regex(pattern);
            bool isValid = regex.IsMatch(searchUser.Email);

            if (!isValid)
            {
                getagentNewUser.Errors = new List<string> { "Check email address" };
                return Json(getagentNewUser);
            }

            searchUser.Agent = new Guid(memberAgentId);

            string url = _url + "WebUserManagement/GetNewUserInfo";

            getagentNewUser = _apiHandler.PostApi<CustomResponseDto<AgentNewUserDto>>(searchUser, url);

            return Json(getagentNewUser);

        }


        // Kullanıcı Ekleme İşlemi
        [HttpPost]
        public IActionResult NewUser(AgentNewUserDto AddUser)
        {
            // herkes istek yapamasın sadece agentleri dolu olanlar sorgu atabilsin
            var memberAgentId = _cookie.getMemberAgentId();
            if (memberAgentId == "")
            {
                return RedirectToAction("UserManagement", "Agent");
            }

            AddUser.Email ??= "";

            if (!IsEmailValid(AddUser.Email))
                return RedirectToAction("UserManagement", "Agent");

            if (AddUser.Role == null)
                return RedirectToAction("UserManagement", "Agent");

            if (!Guid.TryParse(AddUser.Role.ToString(), out _))
                return RedirectToAction("UserManagement", "Agent");

            AddUser.Agent = new Guid(memberAgentId);

            string url = _url + "WebUserManagement/AddNewUser";

            var getagentNewUser = _apiHandler.PostApi<CustomResponseDto<bool>>(AddUser, url);

            return RedirectToAction("UserManagement", "Agent");
        }

        [HttpPost]
        public IActionResult NewAgentGuideUser(AddAgentGuideUserDto addAgentUserGuideInfoDto, IFormFile ProfilPhoto, IFormFile LicenseFront, IFormFile LicenseBack)
        {
            var memberAgentId = _cookie.getMemberAgentId();
            if (memberAgentId == "")
                return RedirectToAction("UserManagement", "Agent");

            addAgentUserGuideInfoDto.Agent = new Guid(memberAgentId);

            if (ProfilPhoto != null)
                addAgentUserGuideInfoDto.ProfilPhotoPath = _fileUploadHandler.FileUploads(ProfilPhoto);

            if (LicenseFront != null)
                addAgentUserGuideInfoDto.LicenseFrontImagePath = _fileUploadHandler.FileUploads(LicenseFront);

            if (LicenseBack != null)
                addAgentUserGuideInfoDto.LicenseBackImagePath = _fileUploadHandler.FileUploads(LicenseBack);

            string url = _url + "WebUserManagement/AddNewAgentGuideUser";
            var response = _apiHandler.PostApi<CustomResponseDto<bool>>(addAgentUserGuideInfoDto, url);

            return RedirectToAction("UserManagement", "Agent");

        }

        private bool IsEmailValid(string email)
        {
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            Regex regex = new(pattern);
            return regex.IsMatch(email);
        }

        // Kullanıcı Ekleme İşlemi
        [HttpPost]
        public IActionResult UpdateUser(AgentNewUserDto AddUser)
        {

            // herkes istek yapamasın sadece agentleri dolu olanlar sorgu atabilsin
            var memberAgentId = _cookie.getMemberAgentId();
            if (memberAgentId == "")
            {
                return RedirectToAction("UserManagement", "Agent");
            }


            if (AddUser.Email == null)
                AddUser.Email = "";
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            Regex regex = new Regex(pattern);
            bool isValid = regex.IsMatch(AddUser.Email);

            if (!isValid)
                return RedirectToAction("UserManagement", "Agent");

            if (AddUser.Role == null)
                return RedirectToAction("UserManagement", "Agent");

            if (!Guid.TryParse(AddUser.Role.ToString(), out _))
                return RedirectToAction("UserManagement", "Agent");


            AddUser.Agent = new Guid(memberAgentId);

            string url = _url + "WebUserManagement/UpdateUser";

            var getagentNewUser = _apiHandler.PostApi<CustomResponseDto<AgentNewUserDto>>(AddUser, url);

            return RedirectToAction("UserManagement", "Agent");

        }





        // Kullanıcı pasifleme İşlemi
        [HttpPost]
        public IActionResult PassiveUser(PassiveUserPostDto passiveUser)
        {
            //TODO: Bu  işler aslında hep agent ana admini tarafından yapılması gerekmektedir diye auth koyulması lazım

            // herkes istek yapamasın sadece agentleri dolu olanlar sorgu atabilsin
            var memberAgentId = _cookie.getMemberAgentId();
            if (memberAgentId == "")
            {
                return RedirectToAction("UserManagement", "Agent");
            }


            if (passiveUser.Email == null)
                passiveUser.Email = "";
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            Regex regex = new Regex(pattern);
            bool isValid = regex.IsMatch(passiveUser.Email);

            if (!isValid)
                return RedirectToAction("UserManagement", "Agent");

            /*
            if (passiveUser.Role == null)
                return RedirectToAction("UserManagement", "Agent");

            if (!Guid.TryParse(passiveUser.Role.ToString(), out _))
                return RedirectToAction("UserManagement", "Agent");
            */


            passiveUser.Agent = new Guid(memberAgentId);

            string url = _url + "WebUserManagement/PassiveUser";
            var getagentNewUser = _apiHandler.PostApi<CustomResponseDto<PassiveUserPostDto>>(passiveUser, url);

            return RedirectToAction("UserManagement", "Agent");

        }





    }
}
