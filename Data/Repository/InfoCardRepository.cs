using Core.Entities;
using Core.IRepository;
using Data.Migrations;
using Dto.ApiWebDtos.ApiToWebDtos.PageComponets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class InfoCardRepository : GenericRepository<InfoCard>, IInfoCardRepository
    {

        Context _context;
        DbSet<InfoCard> infoCards;
        DbSet<InfoCardLanguageItem> infoCardLanguageItems;

        public InfoCardRepository(Context context) : base(context)
        {
            _context = context;
            infoCards = _context.Set<InfoCard>();
            infoCardLanguageItems = _context.Set<InfoCardLanguageItem>();
        }

        public List<InfoCardDto> GetInfoCardDtoList(int languageId)
        {
            List<InfoCardDto> infoCardDtos = (from card in infoCards.Where(x => !x.IsDeleted && x.Status).ToList()
                                              join cardLanguage in infoCardLanguageItems.Where(x => x.LanguageID == languageId).ToList()
                                              on card.Id equals cardLanguage.InfoCardID
                                              select new InfoCardDto()
                                              {
                                                  IconPath = card.IconPath,
                                                  Title = cardLanguage.Title,
                                                  Content = cardLanguage.Content
                                              }).ToList();
            return infoCardDtos;
        }
    }
}
