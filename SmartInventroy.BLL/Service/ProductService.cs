using AutoMapper;
using SmartInventory.Model.ProductDto;
using SmartInvetory.DAL;
using SmartInvetory.DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;
namespace SmartInventroy.BLL.Service
{
    public class ProductService
    {
        private readonly DataAccessFactory _factory;
        private readonly IMapper _mapper;
        public ProductService(DataAccessFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }
        public List<ProductListModel> GetAllProducts()
        {
            var productRepository = _factory.GetProductRepository();
            var products = productRepository.GetAll();
            return _mapper.Map<List<ProductListModel>>(products);
        }
        public ProductListModel GetProductById(int productId)
        {
            var productRepository = _factory.GetProductRepository();
            var product = productRepository.GetByID(productId);
            return _mapper.Map<ProductListModel>(product);
        }
        public ProductDetailsDto GetProductByIdWithDetails(int productId)
        {
            var productRepository = _factory.GetProductRepositoryWithDetails();
            var product = productRepository.GetProductDetails(productId);
            return _mapper.Map<ProductDetailsDto>(product);
        }
        public bool AddProduct(ProductCreateDto productCreateDto)
        {
           
            var productRepository = _factory.GetProductRepository();
            var product = _mapper.Map<Product>(productCreateDto);
            return productRepository.Add(product);
        }
        public bool UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            if(productUpdateDto == null)
            {
                return false;
            }
            var productRepository = _factory.GetProductRepository();
            var product = productRepository.GetByID(id);
            if (product == null)
            {
                return false;
            }
            
            _mapper.Map(productUpdateDto, product);
            return productRepository.Update(product);
        }
        public bool DeleteProduct(int productId)
        {
            var productRepository = _factory.GetProductRepository();
            return productRepository.Delete(productId);
        }
        public List<ProductListModel> GetProductsByCategory(int categoryId)
        {
            var productRepository = _factory.GetProductRepositoryWithDetails();
            var products = productRepository.GetProductsByCategory(categoryId);
            return _mapper.Map<List<ProductListModel>>(products);
        }
        public List<ProductListModel> GetProductsByName(string name)
        {
            var productRepository = _factory.GetProductRepositoryWithDetails();
            var products = productRepository.GetProductsByName(name);
            return _mapper.Map<List<ProductListModel>>(products);
        }
        public List<ProductListModel> GetProductsBySku(string sku)
        {
            var productRepository = _factory.GetProductRepositoryWithDetails();
            var products = productRepository.GetProductsBySku(sku);
            return _mapper.Map<List<ProductListModel>>(products);
        }
        public List<ProductListModel> GetProductsByBarcode(string barcode)
        {
            var productRepository = _factory.GetProductRepositoryWithDetails();
            var products = productRepository.GetProductsByBarcode(barcode);
            return _mapper.Map<List<ProductListModel>>(products);
        }
        public List<ProductListModel> GetActiveProducts()
        {
            var productRepository = _factory.GetProductRepositoryWithDetails();
            var products = productRepository.GetActiveProducts();
            return _mapper.Map<List<ProductListModel>>(products);
        } 
        public List<ProductListModel> GetInactiveProducts()
        {
            var productRepository = _factory.GetProductRepositoryWithDetails();
            var products = productRepository.GetInactiveProducts();
            return _mapper.Map<List<ProductListModel>>(products);
        }
        public List<ProductListModel> GetLowStockProducts()
        {
            var productRepository = _factory.GetProductRepositoryWithDetails();
            var products = productRepository.GetLowStockProducts();
            return _mapper.Map<List<ProductListModel>>(products);
        }
    }
}
