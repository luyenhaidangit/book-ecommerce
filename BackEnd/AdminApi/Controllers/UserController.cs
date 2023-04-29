using BUS;
using BUS.System;
using DTO.ViewModels.Common;
using DTO.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thegioididong.Model.ViewModels.Catalog.ProductCategories;
using Thegioididong.Model.ViewModels.System.Users;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [Route("Login")]
        [HttpPost]
        public ApiResult<UserClaim> Login([FromQuery] LoginRequest request)
        {
            try
            {
                UserClaim result = _userService.Login(request);
                if (result == null)
                {
                    return new ApiResult<UserClaim>(401, "Tên tài khoản hoặc mật khẩu không hợp lệ!", result);
                }
                return new ApiResult<UserClaim>(200, "Đăng nhập thành công", result);
            }
            catch (Exception ex)
            {
                return new ApiResult<UserClaim>(400, "Lỗi: " + ex.Message, null);
            }
        }

        [Route("Create")]
        [HttpPost]
        public ApiResult<UserCreateRequest> Create([FromBody] UserCreateRequest request)
        {
            try
            {
                bool result = _userService.Create(request);
                return new ApiResult<UserCreateRequest>(201, "Tạo mới loại sản phẩm thành công!", request);
            }
            catch (Exception ex)
            {
                return new ApiResult<UserCreateRequest>(400, "Tạo mới loại sản phẩm thất bại!", request);
            }
        }
    }
}
