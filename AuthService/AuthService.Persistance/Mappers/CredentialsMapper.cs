using AuthService.Core.Interfaces;
using AuthService.Core.Models;
using AuthService.Persistance.Entities;

namespace AuthService.Persistance.Mappers
{
    public class CredentialsMapper : IDBMapper<Credentials, CredentialsEntity>
    {
        public Credentials MapFromDB(CredentialsEntity entity)
        {
            throw new NotImplementedException();
        }

        public CredentialsEntity MapToDB(Credentials entity)
        {
            throw new NotImplementedException();
        }
    }
}
