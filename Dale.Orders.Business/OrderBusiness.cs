using Dale.Domain;
using Dale.Orders.DTO;
using Dale.Repository;
using Dale.Repository.SQLServer;
using Dale.Utils;
using Dale.Utils.Enum;
using Dale.Utils.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dale.Orders.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private IRepository<Order> _repositoryOrden;
        private IConfiguration _configuration;

        public int PageLenght { get; set; }

        public OrderBusiness(IConfiguration configuration, IRepository<Order> repositoryOrden)
        {
            _repositoryOrden = repositoryOrden;
            _configuration = configuration;
            this.PageLenght = Convert.ToInt32(configuration.GetSection("Filter:PageSize").Value);
        }

        public async Task<Returns<QueryResult<OrderDto>>> Traer(OrderFilterPaginate filter)
        {
            try
            {
                IQueryable<Order> listado = null;
                var result = new QueryResult<OrderDto> { PageSize = PageLenght };

                if (filter is null)
                {
                    PageLenght = PageLenght;

                    listado = await _repositoryOrden.GetAllAsync(include: source => source.Include(x => x.Client).Include(x => x.Items).ThenInclude(x => x.Product), predicate: source => source.IsActive);
                }
                else
                {
                    PageLenght = filter?.PageSize == 0 ? PageLenght : filter.PageSize;

                    listado = string.IsNullOrEmpty(filter.Search) ? await _repositoryOrden.GetAllAsync(include: source => source.Include(x => x.Client).Include(x => x.Items).ThenInclude(x => x.Product), predicate: source => source.IsActive)
                     : await _repositoryOrden.GetAllAsync(predicate: source =>
                         source.IsActive &&
                         (
                             source.Client.Nombres.Contains(filter.Client) || source.Client.Apellidos.Contains(filter.Client) || source.Factura.Contains(filter.Search)
                         )
                     );

                    //listado = listado.ApplyOrdering(filter.ColumnsMap, filter);

                    if (filter?.SortBy != null)
                    {
                        if (filter.SortBy.ToLower().Equals("asc"))
                        {
                            listado = listado.OrderBy(x => x.CreatedAt);
                        }
                        else
                        {
                            listado = listado.OrderByDescending(x => x.CreatedAt);
                        }
                    }

                    listado = listado.ApplyPaging(filter);
                }

                listado = listado.ApplyPaging(filter);
                result.TotalItems = await listado.CountAsync();

                var listadoConsulta = AutoMapperConfig.GetMapper<Order, OrderDto>().Map<List<OrderDto>>(listado);
                result.Items = listadoConsulta;

                return new Returns<QueryResult<OrderDto>> { State = true, Information = result };
            }
            catch (Exception ex)
            {
                return new Returns<QueryResult<OrderDto>> { Message = new List<string> { ResourceGeneral.QueryNOK, ex.Message }, State = false, Type = GeneralEnum.EnumTypeReturn.Error };
            }
        }


        public async Task<Returns<OrderDto>> OrdenesPorId(string ordenId)
        {
            try
            {
                var orders = await _repositoryOrden.GetAllAsync(predicate: z => z.Id.Equals(ordenId));

                var Consulta = AutoMapperConfig.GetMapper<Order, OrderDto>().Map<OrderDto>(orders);

                return new Returns<OrderDto> { Information = Consulta, State = true };
            }
            catch (Exception ex)
            {
                return new Returns<OrderDto> { Message = new List<string> { ResourceGeneral.QueryNOK, ex.Message }, State = false, Type = GeneralEnum.EnumTypeReturn.Error };
            }
        }

        public async Task<Returns<bool>> Actualizar(OrderRequestDto Dto)
        {
            try
            {
                if (Dto.IsValid)
                {
                    var vConsulta = await _repositoryOrden.GetByIdAsync(Dto.Id);
                    AutoMapperConfig.GetMapper<OrderRequestDto, Order>().Map(Dto, vConsulta);

                    bool respuesta = await _repositoryOrden.UpdateAsync(vConsulta);

                    return new Returns<bool> { Information = respuesta, State = true, Message = new List<string> { ResourceGeneral.UpdateOK }, Type = GeneralEnum.EnumTypeReturn.OK };
                }
                else
                {
                    return new Returns<bool> { State = false, Message = Dto.GetErrores(), Type = GeneralEnum.EnumTypeReturn.Warning };
                }
            }
            catch (Exception ex)
            {
                return new Returns<bool> { Message = new List<string> { ResourceGeneral.UpdateNOK }, State = false, Type = GeneralEnum.EnumTypeReturn.Error };
            }
        }

        public async Task<Returns<bool>> Eliminar(Guid id)
        {
            try
            {
                bool respuesta = await _repositoryOrden.DeleteAsync(id);

                return new Returns<bool> { Information = respuesta, State = true, Message = new List<string> { ResourceGeneral.DeleteOK }, Type = GeneralEnum.EnumTypeReturn.OK };
            }
            catch (Exception ex)
            {
                return new Returns<bool> { Message = new List<string> { ResourceGeneral.DeleteNOK }, State = false, Type = GeneralEnum.EnumTypeReturn.Error };
            }
        }

        public async Task<Returns<string>> Insertar(OrderRequestDto Dto)
        {
            try
            {
                if (Dto.EsValido)
                {
                    var oMapper = AutoMapperConfig.GetMapper<Order, OrderRequestDto>().Map<Order>(Dto);

                    string respuesta = await _repositoryOrden.AddAndReturnIdAsync(oMapper);

                    return new Returns<string> { Information = respuesta, State = true, Message = new List<string> { ResourceGeneral.InsertOK }, Type = GeneralEnum.EnumTypeReturn.OK };
                }
                else
                {
                    return new Returns<string> { State = false, Message = Dto.GetErrores(), Type = GeneralEnum.EnumTypeReturn.Warning };
                }
            }
            catch (Exception ex)
            {
                return new Returns<string> { Message = new List<string> { ResourceGeneral.InsertNOK }, State = false, Type = GeneralEnum.EnumTypeReturn.Error };
            }
        }
    }
}