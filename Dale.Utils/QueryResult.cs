using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dale.Utils
{
    /// <summary>
    /// Clase para paginar el contenido
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <author>
    /// Robert Pineda
    /// </author>
    /// <remarks>
    /// 16/09/2022
    /// </remarks>
    public class QueryResult<T> where T : class
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int PageLenght { get => (TotalItems + PageSize - 1) / PageSize; }
        public IEnumerable<T> Items { get; set; }
    }
}
