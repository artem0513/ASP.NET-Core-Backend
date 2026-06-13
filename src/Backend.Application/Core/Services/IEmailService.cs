using Backend.Application.Core.Models;

namespace Backend.Application.Core.Services
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}