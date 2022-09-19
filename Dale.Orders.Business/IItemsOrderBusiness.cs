using Dale.Orders.DTO;
using Dale.Utils;

namespace Dale.Orders.Business
{
    public interface IItemsOrderBusiness
    {
        Task<Returns<string>> ActualizarItem(ItemOrderDto dto, string token);
    }
}