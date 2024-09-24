using AuthService.Core.DTOs;
using AuthService.Core.DTOs.Roles;

namespace AuthService.Core.Interfaces
{
    public interface IRolesService
    {
        Task<OperationStatus> GetSystemRoles();
        Task<OperationStatus> CreateRole(CreateRoleDTO dto);
        Task<OperationStatus> DeleteRole(Guid roleId);
    }
}
