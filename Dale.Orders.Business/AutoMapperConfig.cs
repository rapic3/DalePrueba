using AutoMapper;
using Dale.Domain;
using Dale.Orders.DTO;

namespace Dale.Orders.Business
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper<T1, T2>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>().ReverseMap();
                cfg.CreateMap<Guid, string>().ConvertUsing(o => o.ToString());

                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<OrderDto, Order>();
                cfg.CreateMap<ItemOrder, ItemOrderDto>();
                cfg.CreateMap<ItemOrderDto, ItemOrder>();
                cfg.CreateMap<ItemOrderRequestDto, ItemOrder>();
                cfg.CreateMap<ItemOrder, ItemOrderRequestDto>();
                cfg.CreateMap<Order, OrderRequestDto>();
                cfg.CreateMap<OrderRequestDto, Order>();
                cfg.CreateMap<ClientDto, Client>();
                cfg.CreateMap<Client, ClientDto>();
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<ProductDto, Product>();

            });

            IMapper mapper = config.CreateMapper();

            return mapper;
        }
    }
}