using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dale.Utils
{
    /// <summary>
    /// Clase para filtros
    /// </summary>
    /// <author>
    /// Robert Pineda
    /// </author>
    /// <remarks>
    /// 16/09/2022
    /// </remarks>
    public class FilterPaginate
    {
        public int? Id { get; set; }
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
