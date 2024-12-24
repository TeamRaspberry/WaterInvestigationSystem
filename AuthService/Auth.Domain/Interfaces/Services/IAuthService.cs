using Auth.Domain.Dto.UserDto;
using Auth.Domain.Dto;
using Auth.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис предназначенный для регистрации/авторизации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <returns></returns>
        Task<BaseResult<UserDto>> Register(RegisteredUserDto dto);
        /// <summary>
        /// Авторизация пользователя            /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<TokenDto>> Login(LoginUserDto dto);

    }
}
