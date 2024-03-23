using Core.Entities;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IGuideService : IGenericService<Guide>
    {
        CustomResponseDto<List<GetGuideDto>> GetAllGuides();
        CustomResponseNullDto ApproveGuide(Guid Id);
        CustomResponseNullDto RejectGuide(Guid Id);
        CustomResponseNullDto ChangeGuideStatus(Guid Id);
        List<SelectListOptionDto> GetGuideSelectList();

    }
}
