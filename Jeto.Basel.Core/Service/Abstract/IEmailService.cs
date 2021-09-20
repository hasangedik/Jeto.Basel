using System.Threading.Tasks;
using Jeto.Basel.Domain.Contracts;

namespace Jeto.Basel.Core.Service.Abstract
{
    public interface IEmailService
    {
        Task Send(EmailContract contract);
    }
}
