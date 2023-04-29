using BUS;
using DTO.ViewModels.Catalog.ProductCategories;
using DTO.ViewModels.Catalog.Products;
using DTO.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thegioididong.Model.Models;
using Thegioididong.Model.ViewModels.Catalog.ProductCategories;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [Route("Get")]
        [HttpGet]
        public PagedResult<ProductViewModel> Get([FromQuery] ProductPagingManageGetRequest request)
        {
            return _productService.Get(request);
        }

        [Route("Create")]
        [HttpPost]
        public ApiResult<ProductCreateRequest> Create([FromBody] ProductCreateRequest request)
        {
            try
            {
                bool result = _productService.Create(request);
                return new ApiResult<ProductCreateRequest>(201, "Tạo mới loại sản phẩm thành công!", request);
            }
            catch (Exception ex)
            {
                return new ApiResult<ProductCreateRequest>(400, "Tạo mới loại sản phẩm thất bại!", request);
            }
        }

        [Route("Update")]
        [HttpPut]
        public ApiResult<string> Update([FromForm] ProductUpdateRequest request)
        {
            try
            {
                bool result = _productService.Update(request);
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
                bool result = _productService.Delete(id);
                return new ApiSuccessResult<string>("Xóa sản phẩm thành công!");
            }
            catch (Exception ex)
            {
                return new ApiSuccessResult<string>("Xóa sản phẩm thất bại!");
            }
        }

        [Route("DeleteMulti")]
        [HttpDelete]
        public ApiResult<string> DeleteMulti(List<int> ids)
        {
            try
            {
                bool result = _productService.DeleteMulti(ids);
                return new ApiSuccessResult<string>("Xóa danh sách loại sản phẩm thành công!");
            }
            catch (Exception ex)
            {
                return new ApiSuccessResult<string>("Xóa danh sách loại sản phẩm thất bại!");
            }
        }
    }
}
