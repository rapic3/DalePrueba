using Dale.Products.Business;
using Dale.Products.DTO;
using Dale.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BSSC.Producto.Servicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness _oRepository;

        public ProductController(IProductBusiness oRepository)
        {
            _oRepository = oRepository;
        }

        [Route("/api/health")]
        [HttpGet]
        public string GetOk()
        {
            return "Ok";
        }

        [HttpGet]
        public async Task<Returns<QueryResult<ProductDto>>> Get([FromQuery] ProductoFilterPaginate filter)
        {
            return await _oRepository.Traer(filter);
        }

        [HttpGet("{id}")]
        public async Task<Returns<ProductDto>> Get(Guid id)
        {
            return await _oRepository.TraerDetalle(id);
        }

        [HttpPost]
        public async Task<Returns<string>> Post(ProductRequestDto newProduct)
        {
            return await _oRepository.Insertar(newProduct);
        }

        [HttpPut]
        public async Task<Returns<bool>> Put(ProductRequestUpdateDto EditProduct)
        {
            return await _oRepository.Actualizar(EditProduct);
        }

        [HttpDelete("{id}")]
        public async Task<Returns<bool>> Delete(Guid id)
        {
            return await _oRepository.Eliminar(id);
        }
    }
}
