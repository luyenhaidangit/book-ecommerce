﻿using BUS;
using DTO.ViewModels.Catalog.ProductCategories;
using DTO.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thegioididong.Model.Models;
using Thegioididong.Model.ViewModels.Catalog.ProductCategories;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("Get")]
        [HttpGet]
        public PagedResult<ProductCategory> Get([FromQuery] ProductCategoryPagingManageGetRequest request)
        {
            return _productCategoryService.GetProductCategories(request);
        }

        [Route("Create")]
        [HttpPost]
        public ApiResult<ProductCategoryCreateRequest> Create([FromBody] ProductCategoryCreateRequest request)
        {
            try
            {
                bool result = _productCategoryService.Create(request);
                return new ApiResult<ProductCategoryCreateRequest>(201, "Tạo mới loại sản phẩm thành công!", request);
            }
            catch (Exception ex)
            {
                return new ApiResult<ProductCategoryCreateRequest>(400, "Tạo mới loại sản phẩm thất bại!", request);
            }
        }

        [Route("Update")]
        [HttpPut]
        public ApiResult<string> Update([FromForm] ProductCategoryUpdateRequest request)
        {
            try
            {
                bool result = _productCategoryService.Update(request);
                return new ApiSuccessResult<string>("Cập nhật loại sản phẩm thành công!");
            }
            catch (Exception ex)
            {
                return new ApiSuccessResult<string>("Cập nhật loại sản phẩm thất bại!");
            }
        }

        [Route("Delete")]
        [HttpDelete]
        public ApiResult<string> Update([FromQuery] int id)
        {
            try
            {
                bool result = _productCategoryService.Delete(id);
                return new ApiSuccessResult<string>("Xóa loại sản phẩm thành công!");
            }
            catch (Exception ex)
            {
                return new ApiSuccessResult<string>("Xóa loại sản phẩm thất bại!");
            }
        }

        [Route("DeleteMulti")]
        [HttpDelete]
        public ApiResult<string> DeleteMulti(List<int> ids)
        {
            try
            {
                bool result = _productCategoryService.DeleteMulti(ids);
                return new ApiSuccessResult<string>("Xóa danh sách loại sản phẩm thành công!");
            }
            catch (Exception ex)
            {
                return new ApiSuccessResult<string>("Xóa danh sách loại sản phẩm thất bại!");
            }
        }
    }
}
