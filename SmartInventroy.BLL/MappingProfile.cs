using AutoMapper;
using SmartInventory.Model.ProductDto;
using SmartInvetory.DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartInventroy.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductListModel>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>().ReverseMap();

            CreateMap<Product, ProductDetailsDto>()
               .ForMember(
                   dest => dest.CategoryName,
                   opt => opt.MapFrom(src => src.Category.CategoryName)
               )
               .ForMember(
                   dest => dest.TotalQuantity,
                   opt => opt.MapFrom(src =>
                       src.StockBalances.Sum(s => s.Quantity))
               )
               .ForMember(
                   dest => dest.IsLowStock,
                   opt => opt.MapFrom(src =>
                       src.StockBalances.Sum(s => s.Quantity)
                       <= src.ReorderLevel)
               )
               .ForMember(
                   dest => dest.WarehouseStocks,
                   opt => opt.MapFrom(src => src.StockBalances)
               ).ReverseMap();

            CreateMap<StockBalance, WarehouseStockDto>()
                .ForMember(
                    dest => dest.WarehouseName,
                    opt => opt.MapFrom(src => src.Warehouse.WarehouseName)
                )
                .ForMember(
                    dest => dest.QuantityAvailable,
                    opt => opt.MapFrom(src => src.Quantity)
                ).ReverseMap();

        }

    }
}
