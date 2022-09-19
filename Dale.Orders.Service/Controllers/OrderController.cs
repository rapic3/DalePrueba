using Dale.Orders.Business;
using Dale.Orders.DTO;
using Dale.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BSSC.Ordenes.Servicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _oRepository;
        private readonly IItemsOrderBusiness _itemOrdenRepositorio;

        public OrderController(IOrderBusiness oRepository, IItemsOrderBusiness itemOrdenRepositorio)
        {
            _oRepository = oRepository;
            _itemOrdenRepositorio = itemOrdenRepositorio;
        }

        [Route("/api/health")]
        [HttpGet]
        public string GetOk()
        {
            return "Ok";
        }

        [HttpGet]
        public async Task<Returns<QueryResult<OrderDto>>> Get([FromQuery] OrderFilterPaginate input)
        {
            return await _oRepository.Traer(input);
        }

        [HttpGet("{id}")]
        public async Task<Returns<OrderDto>> Get(string id)
        {
            return await _oRepository.OrdenesPorId(id);
        }

        [HttpPost]
        public async Task<Returns<string>> Post(OrderRequestDto order)
        {
            return await _oRepository.Insertar(order);
        }

        [HttpPut]
        public async Task<Returns<bool>> Put(OrderRequestDto Edit)
        {
            return await _oRepository.Actualizar(Edit);
        }

        [HttpDelete("{id}")]
        public async Task<Returns<bool>> Delete(Guid id)
        {
            return await _oRepository.Eliminar(id);
        }

    }
}