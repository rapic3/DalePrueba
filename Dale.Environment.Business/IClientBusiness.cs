using Dale.Environment.DTO;
using Dale.Utils;

namespace Dale.Environment.Business
{
    public interface IClientBusiness
    {
        Task<Returns<IEnumerable<ClientDto>>> Traer();

        Task<Returns<ClientDto>> Traer(Guid id);

        Task<Returns<string>> Insertar(ClientRequestDto newObj);

        Task<Returns<bool>> Actualizar(ClientRequestUpdateDto oObjeto);

        Task<Returns<bool>> Eliminar(Guid id);
    }
}