
using Core.IService;
using Core.StaticValues;
using Core.StaticValues.Operation;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelOtherOptionController : ControllerBase
    {
        readonly ISupplierService _supplierService;
        readonly IVehicleTypeService _vehicleTypeService;
        readonly IGuideService _guideService;
        readonly IVehicleService _vehicleService;
        readonly IDriverService _driverService;

        public PanelOtherOptionController(ISupplierService supplierService, IVehicleTypeService vehicleTypeService, IGuideService guideService, IVehicleService vehicleService, IDriverService driverService)
        {
            _supplierService = supplierService;
            _vehicleTypeService = vehicleTypeService;
            _guideService = guideService;
            _vehicleService = vehicleService;
            _driverService = driverService;
        }

        //Tag Category - Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOption>> TagCategorySelectList()
        {
            TagCategory tagCategoryEnum = new TagCategory();
            List<SelectListOption> tagCategorySelectListDtos = tagCategoryEnum.Categories;

            return CustomResponseDto<List<SelectListOption>>.Success(200, tagCategorySelectListDtos);
        }

        //PanelPages - SelectList
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> PanelPagesSelectList()
        {
            List<SelectListOptionDto> panelPageSelectList = new()
            {
                //new SelectListOptionDto()
                //{
                //    OptionID = "3",
                //    Option = "Faq Management"
                //},
                //new SelectListOptionDto()
                //{
                //    OptionID = "4",
                //    Option = "Blog Management"
                //},
                //new SelectListOptionDto()
                //{
                //    OptionID = "5",
                //    Option = "Product Management"
                //},
                //new SelectListOptionDto()
                //{
                //    OptionID = "6",
                //    Option = "Log"
                //},
                //new SelectListOptionDto()
                //{
                //    OptionID = "7",
                //    Option = "Operation"
                //},
            };
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, panelPageSelectList);
        }

        //Supplier Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> SupplierSelectList()
        {
            List<SelectListOptionDto> supplierSelectList = _supplierService.GetSupplierSelectList();
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, supplierSelectList);
        }

        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> SupplierSelectListForVehicle() //sadece supplierTypeı service olan supplierlar gelmeli
        {
            List<SelectListOptionDto> supplierSelectList = _supplierService.GetAll().Where(x => x.SupplierType == (int)SupplierTypeList.No.Service).Select(x => new SelectListOptionDto()
            {
                OptionID = x.Id,
                Option = x.Name,
            }).ToList();
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, supplierSelectList);
        }

        //Vehicle Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> VehicleTypeSelectList()
        {
            List<SelectListOptionDto> vehicleSelectList = _vehicleTypeService.GetAll().Select(x => new SelectListOptionDto()
            {
                OptionID = x.Id,
                Option = x.Type
            }).ToList();
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, vehicleSelectList);
        }

        //City
        [HttpGet]
        public CustomResponseDto<List<SelectListOption>> CitySelectList()
        {
            CityList cityList = new CityList();
            List<SelectListOption> citySelectList = cityList.Cities;
            return CustomResponseDto<List<SelectListOption>>.Success(200, citySelectList);
        }

        //Country
        [HttpGet]
        public CustomResponseDto<List<SelectListOption>> CountrySelectList()
        {
            List<SelectListOption> countrySelectList = CountryList.Countries;
            return CustomResponseDto<List<SelectListOption>>.Success(200, countrySelectList);
        }

        //StartTime
        [HttpGet]
        public CustomResponseDto<List<SelectListOption>> StartTimeSelectList()
        {
            List<SelectListOption> startTimeSelectList = StartTimeList.StartTimes;
            return CustomResponseDto<List<SelectListOption>>.Success(200, startTimeSelectList);
        }

        //Role Template
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> RoleTemplateSelectList()
        {
            var roleTemplates = ConstantRoles.SpecialRoleTemplatesGuid.ToList();
            roleTemplates.RemoveAll(x => x.OptionID == ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Admin).OptionID);

            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, roleTemplates);
        }


        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> GuideSelectList()
        {
            var data = _guideService.GetGuideSelectList();
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, data);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<List<SelectListOptionDto>> VehicleSelectList(string id) //supplierId
        {
            var data = _vehicleService.GetVehicleSelectList(id);
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, data);
        }
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> DriverSelectList()
        {
            var data = _driverService.DriverSelectList();
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, data);
        }
        [HttpGet]
        public CustomResponseDto<List<SelectListOption>> ShopStatusSelectList()
        {
            var data = OperationShopStatus.Status;
            return CustomResponseDto<List<SelectListOption>>.Success(200, data);
        }
    }
}
