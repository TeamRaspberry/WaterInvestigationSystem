using AuthService.Core.DTOs;
using AuthService.Core.DTOs.User;
using AuthService.Core.Interfaces;
using AuthService.Core.Models;
using AuthService.Persistance.Database;
using AuthService.Persistance.Entities;
using AuthService.Persistance.Utils;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistance.Services
{
    public class UsersService : IUsersService
    {
        IDBMapper<Role, RoleEntity> roleMapper;
        IDBMapper<User, UserEntity> userMapper;
        DatabaseContext context;

        public UsersService(IDBMapper<Role, RoleEntity> roleMapper, 
            IDBMapper<User, UserEntity> userMapper,
            DatabaseContext context)
        {
            this.roleMapper = roleMapper;
            this.userMapper = userMapper;
            this.context = context;
        }

        public async Task<OperationStatus> AddNewUser(CreateUserDTO dto)
        {
            RoleEntity? userRole; 
            
            if (dto.roleId is null)
            {
                userRole = await context.Roles
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.IsDefault);
            }
            else
            {
                userRole = await context.Roles
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == dto.roleId);
            }

            if (userRole is null)
            {
                return new OperationStatus()
                {
                    IsSuccess = false,
                    Message = ErrorMessages.RoleNotFoundError
                };
            }

            User newUser = new User(dto.username, dto.password, dto.email, roleMapper.MapFromDB(userRole));

            UserEntity entity = userMapper.MapToDB(newUser);
            
            context.Users.Add(entity);

            await context.SaveChangesAsync();

            return new OperationStatus()
            {
                IsSuccess = true,
                Data = newUser.UserKey
            };

        }

        public async Task<OperationStatus> DeleteUser(Guid id)
        {
            UserEntity? entity = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                return new OperationStatus()
                {
                    IsSuccess = false,
                    Message = ErrorMessages.UserNotFoundError
                };
            }

            context.Users.Remove(entity);

            await context.SaveChangesAsync();

            return new OperationStatus() { IsSuccess = true };
        }

        public async Task<OperationStatus> UpdateUser(UpdateUserDTO dto)
        {
            UserEntity? entity = await context.Users.FirstOrDefaultAsync(x => x.Id == dto.id);

            if (entity is null) 
            {
                return new OperationStatus()
                {
                    IsSuccess = false,
                    Message = ErrorMessages.UserNotFoundError
                };
            }

            User user = userMapper.MapFromDB(entity);

            user.Password = entity.Password;
            user.Email = entity.Email;

            entity = userMapper.MapToDB(user);

            context.Users.Update(entity);

            await context.SaveChangesAsync();

            return new OperationStatus() { IsSuccess = true };
        }
    }
}
