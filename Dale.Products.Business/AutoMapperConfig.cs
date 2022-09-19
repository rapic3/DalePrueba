using AutoMapper;
using Dale.Domain;
using Dale.Products.DTO;

namespace Dale.Products.Business
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper<T1, T2>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>().ReverseMap();
                cfg.CreateMap<Guid, string>().ConvertUsing(o => o.ToString());

                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<ProductDto, Product>();
                cfg.CreateMap<Product, ProductRequestDto>();
                cfg.CreateMap<ProductRequestDto, Product>();
                cfg.CreateMap<Product, ProductRequestUpdateDto>();
                cfg.CreateMap<ProductRequestUpdateDto, Product>();
            });

            IMapper mapper = config.CreateMapper();

            return mapper;
        }
    }
}