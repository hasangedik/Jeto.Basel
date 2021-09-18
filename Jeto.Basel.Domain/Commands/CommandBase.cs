using MediatR;

namespace Jeto.Basel.Domain.Commands
{
    public record CommandBase<T> : IRequest<T> where T : class;
}