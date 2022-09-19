using Dale.Products.DTO;
using Dale.Utils;

namespace Dale.Products.Business
{
    public interface IProductBusiness
    {
        Task<Returns<QueryResult<ProductDto>>> Traer(ProductoFilterPaginate filter);

        Task<Returns<ProductDto>> TraerDetalle(Guid id);

        Task<Returns<string>> Insertar(ProductRequestDto newProduct);

        Task<Returns<bool>> Actualizar(ProductRequestUpdateDto updateProduct);

        Task<Returns<bool>> Eliminar(Guid id);
    }
}