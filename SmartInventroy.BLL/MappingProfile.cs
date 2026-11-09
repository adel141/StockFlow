using AutoMapper;
using SmartInventory.Model.ProductDto;
using SmartInvetory.DAL.EF.Tables;
using SmartInventory.Model.CrudDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartInventroy.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ForMember(d => d.ProductCount, o => o.MapFrom(s => s.Products.Count));
            CreateMap<CategoryDto, Category>().ForMember(d => d.Products, o => o.Ignore());
            CreateMap<Supplier, SupplierDto>().ForMember(d => d.PurchaseOrderCount, o => o.MapFrom(s => s.PurchaseOrders.Count));
            CreateMap<SupplierDto, Supplier>().ForMember(d => d.PurchaseOrders, o => o.Ignore());
            CreateMap<Customer, CustomerDto>().ForMember(d => d.SalesOrderCount, o => o.MapFrom(s => s.SalesOrders.Count));
            CreateMap<CustomerDto, Customer>().ForMember(d => d.SalesOrders, o => o.Ignore());
            CreateMap<Warehouse, WarehouseDto>().ForMember(d => d.ProductCount, o => o.MapFrom(s => s.StockBalances.Select(b => b.ProductId).Distinct().Count()));
            CreateMap<WarehouseDto, Warehouse>().ForMember(d => d.StockBalances, o => o.Ignore()).ForMember(d => d.PurchaseOrders, o => o.Ignore()).ForMember(d => d.SalesOrders, o => o.Ignore()).ForMember(d => d.StockTransactions, o => o.Ignore());
            CreateMap<Product,ProductListModel>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ForMember(d => d.SKU, o => o.MapFrom(s => s.Sku));
            CreateMap<ProductUpdateDto, Product>().ForMember(d => d.Sku, o => o.MapFrom(s => s.SKU)).ForMember(d => d.Category, o => o.Ignore()).ForMember(d => d.StockBalances, o => o.Ignore()).ForMember(d => d.PurchaseOrderDetails, o => o.Ignore()).ForMember(d => d.SalesOrderDetails, o => o.Ignore()).ForMember(d => d.StockTransactions, o => o.Ignore());
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
