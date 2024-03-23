using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.PageComponets
{
    public class GetCommentListDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<CommentListItemDto> Comments { get; set; }
    }

    public class CommentListItemDto
    {
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public string Country { get; set; }
        public string CustomerImagePath { get; set; }
    }
}
