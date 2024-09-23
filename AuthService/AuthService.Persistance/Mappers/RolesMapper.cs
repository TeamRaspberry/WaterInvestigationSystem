using AuthService.Core.Interfaces;
using AuthService.Core.Models;
using AuthService.Persistance.Entities;

namespace AuthService.Persistance.Mappers
{
    public class RolesMapper : IDBMapper<Role, RoleEntity>
    {
        public Role MapFromDB(RoleEntity entity)
        {
            return new Role(entity.Name, entity.IsDefault, entity.IsAdmin);
        }

        public RoleEntity MapToDB(Role entity)
        {
            return new RoleEntity()
            {
                Id = entity.RoleKey,
                Name = entity.Name,
                IsAdmin = entity.IsAdmin,
                IsDefault = entity.IsDefault
            };
        }
    }
}
