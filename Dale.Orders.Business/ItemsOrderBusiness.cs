using Dale.Domain;
using Dale.Orders.DTO;
using Dale.Repository.SQLServer;
using Dale.Utils;
using Dale.Utils.Enum;
using Dale.Utils.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Dale.Orders.Business
{
    public class ItemsOrderBusiness : IItemsOrderBusiness
    {
        private IRepository<ItemOrder> _repositoryOrden;
        private IConfiguration _configuration;

        public ItemsOrderBusiness(IConfiguration configuration, IRepository<ItemOrder> repositoryOrden)
        {
            _repositoryOrden = repositoryOrden;
            _configuration = configuration;
        }

        public async Task<Returns<string>> ActualizarItem(ItemOrderDto dto, string token)
        {
            try
            {

                return new Returns<string> {Message = new List<string> { ResourceGeneral.UpdateOK }, Information = "El item de la Order fue actualizado con exito.", State = true };
            }
            catch (Exception ex)
            {
                return new Returns<string> { Message = new List<string> { ResourceGeneral.UpdateNOK }, State = false, Type = GeneralEnum.EnumTypeReturn.Error };
            }
        }

    }
}