using Dale.Domain;
using Dale.Utils;
using System.Linq.Expressions;

namespace Dale.Products.Business
{
    public class ProductoFilterPaginate : FilterPaginate
    {
        public string? SortProduct { get; set; }
        public string? SortInventory { get; set; }
        public string? Status { get; set; }


        public Dictionary<string, Expression<Func<Product, object>>> ColumnsMap
        {
            get => new Dictionary<string, Expression<Func<Product, object>>>()
            {
                ["Nombre"] = x => x.Name,
            };
        }
    }
}
