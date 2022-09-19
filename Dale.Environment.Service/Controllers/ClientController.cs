using Dale.Environment.Business;
using Dale.Environment.DTO;
using Dale.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BSSC.Entorno.Servicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientBusiness _oRepository;

        public ClientController(IClientBusiness oRepository)
        {
            _oRepository = oRepository;
        }

        [HttpGet]
        public async Task<Returns<IEnumerable<ClientDto>>> Get()
        {
            var respuesta = await _oRepository.Traer();

            if (respuesta.State)
            {
                return new Returns<IEnumerable<ClientDto>> { Information = respuesta.Information, State = true };
            }
            else
            {
                return new Returns<IEnumerable<ClientDto>> { Message = respuesta.Message, State = false, Type = respuesta.Type };
            }
        }

        [HttpGet("{id}")]
        public async Task<Returns<ClientDto>> Get(Guid id)
        {
            var respuesta = await _oRepository.Traer(id);

            if (respuesta.State)
            {
                return new Returns<ClientDto> { Information = respuesta.Information, State = true };
            }
            else
            {
                return new Returns<ClientDto> { Message = respuesta.Message, State = false, Type = respuesta.Type };
            }
        }

        [HttpPost]
        public async Task<Returns<string>> Post(ClientRequestDto newTercero)
        {
            var respuesta = await _oRepository.Insertar(newTercero);

            if (respuesta.State)
            {
                return new Returns<string> { Message = respuesta.Message, Information = respuesta.Information, State = true, Type = respuesta.Type };
            }
            else
            {
                return new Returns<string> { Message = respuesta.Message, State = false, Type = respuesta.Type };
            }
        }

        [HttpPut]
        public async Task<Returns<bool>> Put(ClientRequestUpdateDto EditTercero)
        {
            var respuesta = await _oRepository.Actualizar(EditTercero);

            if (respuesta.State)
            {
                return new Returns<bool> { Message = respuesta.Message, Information = respuesta.Information, State = true, Type = respuesta.Type };
            }
            else
            {
                return new Returns<bool> { Message = respuesta.Message, State = false, Type = respuesta.Type };
            }
        }

        [HttpDelete("{id}")]
        public async Task<Returns<bool>> Delete(Guid id)
        {
            var respuesta = await _oRepository.Eliminar(id);

            if (respuesta.State)
            {
                return new Returns<bool> { Message = respuesta.Message, Information = respuesta.Information, State = true, Type = respuesta.Type };
            }
            else
            {
                return new Returns<bool> { Message = respuesta.Message, State = false, Type = respuesta.Type };
            }
        }
    }
}