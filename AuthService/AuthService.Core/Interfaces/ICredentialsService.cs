using AuthService.Core.DTOs;
using AuthService.Core.DTOs.Credentials;

namespace AuthService.Core.Interfaces
{
    public interface ICredentialsService
    {
        Task<OperationStatus> GetCredentials(GetCredentialsDTO dto);
        Task<OperationStatus> CreateCredentials(CreateCredentialsDTO dto);
        Task<OperationStatus> UpdateCredentials(UpdateCredentialsDTO dto);
        Task<OperationStatus> DeleteCredentials(Guid credentialsId);
    }
}
