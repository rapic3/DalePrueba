using Dale.Orders.DTO;
using Dale.Utils;
using System.Linq.Expressions;

namespace Dale.Orders.Business
{
    public class OrderFilterPaginate : FilterPaginate
    {
        public Dictionary<string, Expression<Func<OrderDto, object>>> ColumnsMap
        {
            get => new Dictionary<string, Expression<Func<OrderDto, object>>>()
            {
                ["CreationDate"] = x => x.CreatedDate,
            };
        }

        public string? Client { get; set; }
    }
}
