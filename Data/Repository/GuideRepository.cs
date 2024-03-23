using Core;
using Core.Entities;
using Core.IRepository;
using Dto.ApiPanelDtos;
using Dto.ApiPanelDtos.GuideDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class GuideRepository : GenericRepository<Guide>, IGuideRepository
    {
        public GuideRepository(Context context) : base(context)
        {
        }

        public List<GetGuideDto> GetAllGuides()
        {
            #region YEDEK
            //var GetGuideDtos = (from guide in _context.Guides
            //                    join guideCity in _context.GuideCities on guide.Id equals guideCity.GuideId
            //                    into guideCityJoin
            //                    from guideCity in guideCityJoin.DefaultIfEmpty()
            //                    join guideLanguage in _context.GuideLanguages on guide.Id equals guideLanguage.GuideId
            //                    into guideLanguageJoin
            //                    from guideLanguage in guideLanguageJoin.DefaultIfEmpty()
            //                        //join systemOptionLanguageItem in _context.SystemOptionLanguageItems
            //                        //on guideLanguage.LanguageId equals systemOptionLanguageItem.SystemOptionId
            //                    select new GetGuideDto
            //                    {
            //                        Name = guide.Name,
            //                        Email = guide.Email,
            //                        ApproveConstent = guide.ApproveConstent,
            //                        BirthDate = guide.BirthDate,
            //                        GuideCities = guideCityJoin.Select(x => new GetGuideCityDto { GuideId = x.Id, LanguageId = x.LanguageId }).ToList() /*_context.GuideCities.Where(x => x.GuideId == guide.Id).Select(x => new GetGuideCityDto { GuideId = x.Id, LanguageId = x.LanguageId }).ToList()*/,
            //                        Id = guide.Id,
            //                        GuideLanguages = guideLanguageJoin.Select(x => new GetGuideLanguageDto { GuideId = x.GuideId, LanguageId = x.LanguageId }).ToList() /*_context.GuideLanguages.Where(x => guide.Id == x.GuideId).Select(x => new GetGuideLanguageDto { GuideId = x.GuideId, LanguageId = x.LanguageId }).ToList()*/,
            //                        ApproveStatus = guide.ApproveStatus,
            //                        IsDeleted = guide.IsDeleted,
            //                        LicenseBackImagePath = guide.LicenseBackImagePath,
            //                        LicenseFrontImagePath = guide.LicenseFrontImagePath,
            //                        Phone = guide.Phone,
            //                        ProfilPhotoPath = guide.ProfilPhotoPath,
            //                        RegisteredDirectoryRoom = guide.RegisteredDirectoryRoom,
            //                        Status = guide.Status,
            //                        Surname = guide.Surname,
            //                        Tc = guide.Tc,
            //                        TurebLicenseNumber = guide.TurebLicenseNumber,
            //                    }).ToList();
            #endregion

            #region YEDEK2
            //var GetGuideDtos = (from guide in _context.Guides
            //                    join guideCity in _context.GuideCities
            //                    on guide.Id equals guideCity.GuideId
            //                    join guideLanguage in _context.GuideLanguages
            //                    on guide.Id equals guideLanguage.GuideId
            //                    //join systemOptionLanguageItem in _context.SystemOptionLanguageItems
            //                    //on guideLanguage.LanguageId equals systemOptionLanguageItem.SystemOptionId
            //                    select new GetGuideDto
            //                    {
            //                        Name = guide.Name,
            //                        Email = guide.Email,
            //                        ApproveConstent = guide.ApproveConstent,
            //                        BirthDate = guide.BirthDate,
            //                        GuideCities = _context.GuideCities.Where(x => x.GuideId == guide.Id).Select(x => new GetGuideCityDto { GuideId = x.Id, LanguageId = x.LanguageId }).ToList(),
            //                        Id = guide.Id,
            //                        GuideLanguages = _context.GuideLanguages.Where(x => guide.Id == x.GuideId).Select(x => new GetGuideLanguageDto { GuideId = x.GuideId, LanguageId = x.LanguageId }).ToList(),
            //                        ApproveStatus = guide.ApproveStatus,
            //                        IsDeleted = guide.IsDeleted,
            //                        LicenseBackImagePath = guide.LicenseBackImagePath,
            //                        LicenseFrontImagePath = guide.LicenseFrontImagePath,
            //                        Phone = guide.Phone,
            //                        ProfilPhotoPath = guide.ProfilPhotoPath,
            //                        RegisteredDirectoryRoom = guide.RegisteredDirectoryRoom,
            //                        Status = guide.Status,
            //                        Surname = guide.Surname,
            //                        Tc = guide.Tc,
            //                        TurebLicenseNumber = guide.TurebLicenseNumber,
            //                    }).ToList();

            //foreach (var GetGuideDto in GetGuideDtos)
            //{
            //    GetGuideDto.CityNames = GetGuideDto.GuideCities.Select(y => new GetGuideCityNameDto { Name = _context.SystemOptionLanguageItems.FirstOrDefault(x => x.SystemOptionId == y.LanguageId).Name }).ToList();

            //    GetGuideDto.LanguageNames = GetGuideDto.GuideLanguages.Select(y => new GetLanguageNameDto { Name = _context.SystemOptionLanguageItems.FirstOrDefault(x => x.SystemOptionId == y.LanguageId && x.LanguageID == 2).Name }).ToList();
            //} 
            #endregion

            var GetGuideDtos = (from guide in _context.Guides
                                where guide.ApproveStatus!=(int)Enums.ApproveStatus.Reddedildi
                                select new GetGuideDto
                                {
                                    Name = guide.Name,
                                    Email = guide.Email,
                                    ApproveConstent = guide.ApproveConstent,
                                    BirthDate = guide.BirthDate,
                                    GuideCities = _context.GuideCities.Where(x => x.GuideId == guide.Id).Select(x => new GetGuideCityDto { GuideId = x.Id, LanguageId = x.LanguageId }).ToList(),
                                    Id = guide.Id,
                                    GuideLanguages = _context.GuideLanguages.Where(x => guide.Id == x.GuideId).Select(x => new GetGuideLanguageDto { GuideId = x.GuideId, LanguageId = x.LanguageId }).ToList(),
                                    ApproveStatus = guide.ApproveStatus,
                                    IsDeleted = guide.IsDeleted,
                                    LicenseBackImagePath = guide.LicenseBackImagePath,
                                    LicenseFrontImagePath = guide.LicenseFrontImagePath,
                                    Phone = guide.Phone,
                                    ProfilPhotoPath = guide.ProfilPhotoPath,
                                    RegisteredDirectoryRoom = guide.RegisteredDirectoryRoom,
                                    Status = guide.Status,
                                    Surname = guide.Surname,
                                    Tc = guide.Tc,
                                    TurebLicenseNumber = guide.TurebLicenseNumber,
                                }).ToList();

            foreach (var GetGuideDto in GetGuideDtos)
            {
                GetGuideDto.CityNames = GetGuideDto.GuideCities.Select(y => new GetGuideCityNameDto { Name = _context.SystemOptionLanguageItems.FirstOrDefault(x => x.SystemOptionId == y.LanguageId).Name }).ToList();

                GetGuideDto.LanguageNames = GetGuideDto.GuideLanguages.Select(y => new GetLanguageNameDto { Name = _context.SystemOptionLanguageItems.FirstOrDefault(x => x.SystemOptionId == y.LanguageId && x.LanguageID == 2).Name }).ToList();
            }

            return GetGuideDtos;
        }
    }
}
