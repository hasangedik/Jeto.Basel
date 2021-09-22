using System.Threading.Tasks;
using Jeto.Basel.Domain.Contracts;

namespace Jeto.Basel.Application.Features.Email
{
    public interface IEmailService
    {
        Task Send(EmailContract contract);
    }
}
