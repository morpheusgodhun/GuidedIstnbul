using Dto.ApiPanelDtos.SeoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class RouteTemplateDto
    {
        public RouteTemplateDto(Guid id, string controller, string action, Guid entityId)
        {
            Id = id;
            Controller = controller;
            Action = action;
            EntityId = entityId;
        }
        public RouteTemplateDto(string controller, string action, Guid entityId)
        {
            Controller = controller;
            Action = action;
            EntityId = entityId;
        }

        public RouteTemplateDto(string controller, string action)
        {
            Controller = controller;
            Action = action;
        }
        public RouteTemplateDto(Guid id, string controller, string action)
        {
            Id = id;
            Controller = controller;
            Action = action;
        }
        public RouteTemplateDto()
        {

        }
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public List<SeoListByRouteIdDto> SeoListDtos { get; set; }
    }
}
