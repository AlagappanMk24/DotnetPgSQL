using AutoMapper;
using DotnetPgSQL.Application.Contracts.DTOs;
using DotnetPgSQL.Domain.Models.Entities;

namespace DotnetPgSQL.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ForMember is used to handle custom property mapping.
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
        }
    }
}
