using AutoMapper;
using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.Repositories;
using DeliveryApp.Application.ViewModels.Product;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Entities.Photo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Persistence.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryService _categoryService;
        private readonly IPhotoService _photoService;
        public readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ICategoryService categoryService,
                                IPhotoService photoService, ICompanyService companyService, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
            _photoService = photoService;
            _companyService = companyService;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(ProductCreateVM createVM,string userId)
        {
            var products = await GetAllAsync(userId);
            var company = await _companyService.GetCompanyAsync(userId);
            var category = await _categoryService.GetSingleCategoryAsync(createVM.CategoryId);

            if (products.Any(x => x.Name.ToLower() == createVM.Name.ToLower())) throw new Exception("Name exsist");
            var imageResult = await _photoService.AddPhotoAsync(createVM.Photo);
            
            Product newProduct = new()
            {
                Name=createVM.Name,
                StockCount=createVM.StockCount,
                Price=createVM.Price,
                CategoryId=createVM.CategoryId,
                Description=createVM.Description,
                CompanyId=company.Id,
                ImageUrl = imageResult.SecureUrl.AbsoluteUri,
                ImagePublicId = imageResult.PublicId,
            };

            
            await _productRepository.AddAsync(newProduct);
            return await _productRepository.SaveAsync();

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await GetSingleAsync(id);
            if (product == null) throw new Exception("Not found");
            _productRepository.Remove(product);
            return await _productRepository.SaveAsync();
        }

        public async Task<IQueryable<Product>> GetAllAsync(string userId)
        {
             var company = await _companyService.GetCompanyAsync(userId);
            return  _productRepository.GetWhere(x => x.CompanyId == company.Id);
        }

        public async Task<Product> GetSingleAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(ProductUpdateVM updateVM)
        {
            var product = await GetSingleAsync(updateVM.Id);
            if (product == null) throw new Exception("Not Found");

            if (updateVM.Photo != null)
            {
                var result = await _photoService.DeletePhotoAsync(product.ImagePublicId);
                if (result.Error != null) throw new Exception(result.Error.Message);
                var imageResult = await _photoService.AddPhotoAsync(updateVM.Photo);
                product.ImageUrl = imageResult.SecureUrl.AbsoluteUri;
                product.ImagePublicId = imageResult.PublicId;
            }
            var productName = await _productRepository.GetSingleAsync(x => x.Name.ToLower() == updateVM.Name.ToLower());

            if (productName != null)
            {
                if (product.Name.ToLower() != productName.Name.ToLower())
                {
                    throw new Exception("Name exsist");
                }
            }
            product.Name = updateVM.Name;
            product.Description = updateVM.Description;
            product.Price=updateVM.Price;
            product.StockCount=updateVM.StockCount;
            product.CategoryId=updateVM.CategoryId;

            _productRepository.Update(product);

            return await _productRepository.SaveAsync();
        }
    }
}
