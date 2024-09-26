using AuthService.Core.DTOs;
using AuthService.Core.DTOs.Roles;
using AuthService.Core.Interfaces;
using AuthService.Core.Models;
using AuthService.Persistance.Database;
using AuthService.Persistance.Entities;
using AuthService.Persistance.Utils;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistance.Services
{
    public class RolesService : IRolesService
    {
        IDBMapper<Role, RoleEntity> mapper;
        DatabaseContext context;

        public RolesService(IDBMapper<Role, RoleEntity> mapper, DatabaseContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<OperationStatus> CreateRole(CreateRoleDTO dto)
        {
            Role newRole;

            bool isDefaultExists = context.Roles.Any(x => x.IsDefault);

            if (dto.isDefault && isDefaultExists)
            {
                await context.Roles
                    .Where(x => x.IsDefault)
                    .ForEachAsync(_ => _.IsDefault = false);
            }

            newRole = new Role(dto.name, dto.isDefault, dto.isAdmin);

            RoleEntity newRoleEntity = mapper.MapToDB(newRole);

            newRoleEntity.Id = Guid.NewGuid();

            await context.Roles.AddAsync(newRoleEntity);

            return new OperationStatus()
            {
                IsSuccess = true,
                Data = newRoleEntity.Id
            };
        }

        public async Task<OperationStatus> DeleteRole(Guid roleId)
        {
            RoleEntity? roleForDelete = await context.Roles
                .FirstOrDefaultAsync(x => x.Id == roleId);

            if (roleForDelete is null)
            {
                return new OperationStatus() 
                { IsSuccess = false, Message = ErrorMessages.RoleNotFoundError };
            }

            if (context.Roles.Where(x => x.IsDefault).Count() == 1
                && roleForDelete.IsDefault)
            {
                return new OperationStatus()
                { IsSuccess = false, Message = ErrorMessages.SingleDefaultRoleError };
            }

            if (context.Roles.Where(x => x.IsAdmin).Count() == 1
                && roleForDelete.IsAdmin)
            {
                return new OperationStatus()
                { IsSuccess = false, Message = ErrorMessages.SingleAdminRoleError };
            }

            context.Roles.Remove(roleForDelete);

            await context.SaveChangesAsync();

            return new OperationStatus()
            {
                IsSuccess = true
            };
        }

        public async Task<OperationStatus> GetSystemRoles(int page = 1)
        {
            IQueryable<Role> roles = context.Roles
                .AsNoTracking()
                .Skip(10 * (page - 1))
                .Take(10)
                .Select(x => mapper.MapFromDB(x));

            return new OperationStatus()
            {
                IsSuccess = true,
                Data = await roles.ToListAsync()
            };
        }

        public async Task<OperationStatus> UpdateRoleState(UpdateRoleStateDTO dto)
        {
            RoleEntity? roleForUpdate = await context.Roles.FirstOrDefaultAsync(x => x.Id == dto.roleId);

            if (roleForUpdate is null)
            {
                return new OperationStatus()
                {
                    IsSuccess = false,
                    Message = ErrorMessages.RoleNotFoundError
                };
            }

            if (roleForUpdate.IsDefault)
            {
                return new OperationStatus()
                {
                    IsSuccess = true,
                    Message = ErrorMessages.RoleIsDefaultError
                };
            }

            if (roleForUpdate.IsAdmin)
            {
                return new OperationStatus()
                {
                    IsSuccess = true,
                    Message = ErrorMessages.RoleIsAdminError
                };
            }

            if (dto.isDefault)
            {
                await context.Roles.Where(x => x.IsDefault).ForEachAsync(x => x.IsDefault = false);
            }

            roleForUpdate.IsDefault = dto.isDefault;
            roleForUpdate.IsAdmin = dto.isAdmin;

            context.Update(roleForUpdate);

            await context.SaveChangesAsync();

            return new OperationStatus()
            {
                IsSuccess = true,
            };

        }
    }
}
