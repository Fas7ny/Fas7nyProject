using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Application.DTOs.SearchLog.Response
{
    public class SearchResultDto
    {
        public string Keyword { get; set; }
        public DateTime SearchedAt { get; set; }
    }

}
