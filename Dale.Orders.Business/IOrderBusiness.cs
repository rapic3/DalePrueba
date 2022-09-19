using Dale.Orders.DTO;
using Dale.Utils;

namespace Dale.Orders.Business
{
    public interface IOrderBusiness
    {
        Task<Returns<QueryResult<OrderDto>>> Traer(OrderFilterPaginate filter);

        Task<Returns<OrderDto>> OrdenesPorId(string ordenId);

        Task<Returns<string>> Insertar(OrderRequestDto newObj);

        Task<Returns<bool>> Actualizar(OrderRequestDto oObjeto);

        Task<Returns<bool>> Eliminar(Guid id);

    }
}