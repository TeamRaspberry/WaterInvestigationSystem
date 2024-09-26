using AuthService.Core.DTOs;
using AuthService.Core.DTOs.User;

namespace AuthService.Core.Interfaces
{
    public interface IUsersService
    {
        Task<OperationStatus> AddNewUser(CreateUserDTO dto);
        Task<OperationStatus> UpdateUser(UpdateUserDTO dto);
        Task<OperationStatus> DeleteUser(Guid id);
    }
}
