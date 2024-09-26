using AuthService.Core.DTOs;
using AuthService.Core.DTOs.Credentials;
using AuthService.Core.Interfaces;
using AuthService.Core.Models;
using AuthService.Persistance.Database;
using AuthService.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistance.Services
{
    public class CredentialsService : ICredentialsService
    {
        IDBMapper<Credentials, CredentialsEntity> credentialsMapper;
        IDBMapper<User, UserEntity> userMapper;
        DatabaseContext context;
        public CredentialsService(IDBMapper<Credentials, CredentialsEntity> credentialsMapper,
            IDBMapper<User, UserEntity> userMapper,
            DatabaseContext context)
        {
            this.credentialsMapper = credentialsMapper;
            this.userMapper = userMapper;
            this.context = context;
        }

        public async Task<OperationStatus> CreateCredentials(CreateCredentialsDTO dto)
        {
            List<User> attachedUsers = await context.Users
                .Where(entity => dto.attachedUsers.Contains(entity.Id))
                .Select(entity => userMapper.MapFromDB(entity))
                .ToListAsync();


            Credentials newCredentials = new Credentials(dto.name, dto.surname, dto.birthDate, attachedUsers);

            CredentialsEntity newCredentialsEntity = credentialsMapper.MapToDB(newCredentials);

            newCredentialsEntity.Id = Guid.NewGuid();

            await context.Credentials.AddAsync(newCredentialsEntity);

            return new OperationStatus()
            {
                IsSuccess = true,
                Data = newCredentialsEntity.Id
            };
        }

        public Task<OperationStatus> DeleteCredentials(Guid credentialsId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationStatus> GetCredentials(GetCredentialsDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationStatus> UpdateCredentials(UpdateCredentialsDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
