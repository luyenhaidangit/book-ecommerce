using DAO.Repositories;
using DTO.ViewModels.Catalog.Products;
using DTO.ViewModels.Common;
using DTO.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Thegioididong.Model.ViewModels.System.Users;
using static Thegioididong.Common.Constants.SystemConstant;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BUS.System
{
    public partial interface IUserService
    {
        bool Create(UserCreateRequest request);

        UserClaim Login(LoginRequest request);
    }

    public partial class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public bool Create(UserCreateRequest request)
        {
            return _userRepository.Create(request);
        }

        public UserClaim Login(LoginRequest request)
        {
            UserClaim user = _userRepository.Login(request);

            if (user == null)
            {
                user = null;
                return user;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretConfiguration.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tmp = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tmp);

            user.Token = token;

            return user;
        }
    }
}
