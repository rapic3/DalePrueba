using AutoMapper;
using Dale.Domain;
using Dale.Environment.DTO;

namespace Dale.Environment.Business
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper<T1, T2>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>().ReverseMap();
                cfg.CreateMap<Guid, string>().ConvertUsing(o => o.ToString());
                cfg.CreateMap<Client, ClientDto>();
                cfg.CreateMap<ClientDto, Client>();
                cfg.CreateMap<Client, ClientRequestDto>();
                cfg.CreateMap<ClientRequestDto, Client>();
                cfg.CreateMap<Client, ClientRequestUpdateDto>();
                cfg.CreateMap<ClientRequestUpdateDto, Client>();
            });

            IMapper mapper = config.CreateMapper();

            return mapper;
        }
    }
}