using AuthService.Core.Interfaces;
using AuthService.Core.Models;
using AuthService.Persistance.Entities;

namespace AuthService.Persistance.Mappers
{
    public class CredentialsMapper : IDBMapper<Credentials, CredentialsEntity>
    {
        IDBMapper<User, UserEntity> mapper;
        public CredentialsMapper(IDBMapper<User, UserEntity> mapper)
        {
            this.mapper = mapper;
        }
        public Credentials MapFromDB(CredentialsEntity entity)
        {
            return new Credentials(entity.Id, entity.Name, entity.Surname, entity.BirthDate, entity.AttachedUsers
                .Select(obj => mapper.MapFromDB(obj)).ToList());
        }

        public CredentialsEntity MapToDB(Credentials entity)
        {
            return new CredentialsEntity()
            {
                Name = entity.Name,
                Surname = entity.Surname,
                Id = entity.CredentialsKey,
                BirthDate = entity.BirthDate,
                AttachedUsers = entity.GetAttachedUsers().Select(obj => mapper.MapToDB(obj)).ToList()
            };
        }
    }
}
