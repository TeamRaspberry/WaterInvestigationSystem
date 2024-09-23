using AuthService.Core.DTOs;
using AuthService.Core.DTOs.User;
using AuthService.Core.Interfaces;

namespace AuthService.Persistance.Services
{
    public class UsersService : IUsersService
    {
        public UsersService()
        {
            
        }

        public Task<OperationStatus> AddNewUser(CreateUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationStatus> DeleteUser<T>(T id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationStatus> UpdateUser(UpdateUserDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
