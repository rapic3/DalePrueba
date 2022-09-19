using Dale.Domain;
using Dale.Environment.DTO;
using Dale.Repository.SQLServer;
using Dale.Utils;
using Dale.Utils.Enum;
using Dale.Utils.Resources;

namespace Dale.Environment.Business
{
    public class ClientBusiness : IClientBusiness
    {
        private IRepository<Client> _repository;

        public ClientBusiness(IRepository<Client> repository)
        {
            this._repository = repository;
        }

        public async Task<Returns<IEnumerable<ClientDto>>> Traer()
        {
            try
            {
                var listado = await _repository.GetAllAsync(predicate: source => source.IsActive);
                var listadoConsulta = AutoMapperConfig.GetMapper<ClientBusiness, ClientDto>().Map<List<ClientDto>>(listado);

                return new Returns<IEnumerable<ClientDto>> { Information = listadoConsulta, State = true };
            }
            catch (Exception ex)
            {
                return new Returns<IEnumerable<ClientDto>> { Message = new List<string> { ResourceGeneral.QueryNOK }, State = false, Type = GeneralEnum.EnumTypeReturn.Error };
            }
        }

        public async Task<Returns<ClientDto>> Traer(Guid id)
        {
            try
            {
                var objeto = await _repository.GetByIdAsync(id) ?? new Client();
                var Consulta = AutoMapperConfig.GetMapper<ClientBusiness, ClientDto>().Map<ClientDto>(objeto);

                return new Returns<ClientDto> { Information = Consulta, State = true };
            }
            catch (Exception ex)
            {
                return new Returns<ClientDto> { Message = new List<string> { ResourceGeneral.QueryNOK }, State = false, Type = GeneralEnum.EnumTypeReturn.Error };
            }
        }

        public async Task<Returns<string>> Insertar(ClientRequestDto Dto)
        {
            try
            {
                if (Dto.EsValido)
                {
                    var oMapper = AutoMapperConfig.GetMapper<Client, ClientDto>().Map<Client>(Dto);

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

        public async Task<Returns<bool>> Actualizar(ClientRequestUpdateDto Dto)
        {
            try
            {
                if (Dto.EsValido)
                {
                    var vConsulta = await _repository.GetByIdAsync(Dto.Id);
                    AutoMapperConfig.GetMapper<ClientDto, Client>().Map(Dto, vConsulta);

                    bool respuesta = await _repository.UpdateAsync(vConsulta);

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