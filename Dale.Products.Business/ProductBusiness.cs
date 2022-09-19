using Dale.Domain;
using Dale.Products.DTO;
using Dale.Repository;
using Dale.Repository.SQLServer;
using Dale.Utils;
using Dale.Utils.Enum;
using Dale.Utils.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dale.Products.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IRepository<Product> _repository;

        public int PageLenght { get; set; }

        public ProductBusiness(IRepository<Product> repository, IConfiguration configuration)
        {
            _repository = repository;
            PageLenght = Convert.ToInt32(configuration.GetSection("Filter:PageSize").Value);
        }

        public async Task<Returns<QueryResult<ProductDto>>> Traer(ProductoFilterPaginate filter)
        {
            try
            {
                IQueryable<Product> listado = null;
                var result = new QueryResult<ProductDto> { };

                if (filter is null)
                {
                    PageLenght = PageLenght;

                    listado = await _repository.GetAllAsync(predicate: source => source.IsActive);
                }
                else
                {
                    PageLenght = filter?.PageSize == 0 ? PageLenght : filter.PageSize;

                    listado = string.IsNullOrEmpty(filter.Search) ? await _repository.GetAllAsync(predicate: source => source.IsActive)
                     : await _repository.GetAllAsync(predicate: source =>
                         source.IsActive &&
                         (
                             source.Name.Contains(filter.Search) || source.ReferenceCode.Contains(filter.Search)
                         )
                     );

                    result.PageSize = PageLenght;
                    result.TotalItems = await listado.CountAsync();
                    listado = listado.ApplyOrdering(filter.ColumnsMap, filter);

                    if (filter?.SortProduct != null)
                    {
                        if (filter.SortProduct.ToLower().Equals("asc"))
                        {
                            listado = listado.OrderBy(x => x.CreatedAt);
                        }
                        else
                        {
                            listado = listado.OrderByDescending(x => x.CreatedAt);
                        }
                    }

                    if (filter?.SortInventory != null)
                    {
                        if (filter.SortInventory.ToLower().Equals("asc"))
                        {
                            listado = listado.OrderBy(x => x.Id);
                        }
                        else
                        {
                            listado = listado.OrderByDescending(x => x.Id);
                        }
                    }

                    listado = listado.ApplyPaging(filter);
                }

                var listadoConsulta = AutoMapperConfig.GetMapper<Product, ProductDto>().Map<List<ProductDto>>(listado);
                result.Items = listadoConsulta;

                return new Returns<QueryResult<ProductDto>> { State = true, Information = result };
            }
            catch (Exception ex)
            {
                return new Returns<QueryResult<ProductDto>> { Message = new List<string> { ResourceGeneral.QueryNOK }, State = false, Type = GeneralEnum.EnumTypeReturn.Error };
            }
        }


        public async Task<Returns<ProductDto>> TraerDetalle(Guid id)
        {
            try
            {
                var objeto = await _repository.FirstOrDefaultAsync(predicate: source => source.Id.Equals(id));

                var Consulta = AutoMapperConfig.GetMapper<Product, ProductDto>().Map<ProductDto>(objeto);

                return new Returns<ProductDto> { State = true, Information = Consulta };
            }
            catch (Exception ex)
            {
                return new Returns<ProductDto> { Message = new List<string> { ResourceGeneral.QueryNOK }, State = false, Type = GeneralEnum.EnumTypeReturn.Error };
            }
        }

        public async Task<Returns<string>> Insertar(ProductRequestDto Dto)
        {
            try
            {
                if (Dto.EsValido)
                {
                    var oMapper = AutoMapperConfig.GetMapper<Product, ProductDto>().Map<Product>(Dto);

                    string respuesta = await _repository.AddAndReturnIdAsync(oMapper);

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

        public async Task<Returns<bool>> Actualizar(ProductRequestUpdateDto Dto)
        {
            try
            {
                if (Dto.EsValido)
                {
                    //Get Product On BD
                    var product = await _repository.FirstOrDefaultAsync(predicate: source => source.Id.Equals(Dto.Id));
                    if (product == null)
                    {
                        throw new Exception("No existe el producto con el id " + Dto.Id);
                    }

                    AutoMapperConfig.GetMapper<ProductRequestUpdateDto, Product>().Map(Dto, product);

                    bool respuesta = await _repository.UpdateAsync(product);

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
                bool respuesta = await _repository.DeleteAsync(id);

                return new Returns<bool> { Information = respuesta, State = true, Message = new List<string> { ResourceGeneral.DeleteOK }, Type = GeneralEnum.EnumTypeReturn.OK };
            }
            catch (Exception ex)
            {
                return new Returns<bool> { Message = new List<string> { ResourceGeneral.DeleteNOK }, State = false, Type = GeneralEnum.EnumTypeReturn.Error };
            }
        }
    }
}