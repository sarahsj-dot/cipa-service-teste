using TIVIT.CIPA.Api.Domain.Model.Requests;

namespace TIVIT.CIPA.Api.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendUserFirstAccessContentAsync(UserFirstAccessEmailRequest request);
    }
}
