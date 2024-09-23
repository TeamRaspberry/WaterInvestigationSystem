using AuthService.Core.Interfaces;
using AuthService.Core.Models;
using AuthService.Persistance.Entities;

namespace AuthService.Persistance.Mappers
{
    public class UserMapper : IDBMapper<User, UserEntity>
    {
        IDBMapper<Role, RoleEntity> mapper;
        public UserMapper(IDBMapper<Role, RoleEntity> mapper)
        {
            this.mapper = mapper;
        }
        public User MapFromDB(UserEntity entity)
        {
            return new User(entity.UserName, 
                entity.Password, 
                entity.Email, 
                mapper.MapFromDB(entity.Role));
        }

        public UserEntity MapToDB(User entity)
        {
            return new UserEntity()
            {
                Id = entity.UserKey,
                UserName = entity.UserName,
                Password = entity.Password,
                Email = entity.Email,
                Role = mapper.MapToDB(entity.Role)
            };
        }
    }
}
