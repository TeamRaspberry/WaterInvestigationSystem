using AuthService.Core.DTOs;
using AuthService.Core.DTOs.Roles;
using AuthService.Core.Interfaces;
using AuthService.Core.Models;
using AuthService.Persistance.Database;
using AuthService.Persistance.Entities;
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

            if (dto.isDefault)
            {
                await context.Roles
                    .Where(x => x.IsDefault)
                    .ForEachAsync(_ => _.IsDefault = false);
            }
        }

        public Task<OperationStatus> DeleteRole(Guid roleId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationStatus> GetSystemRoles()
        {
            throw new NotImplementedException();
        }
    }
}
