using MediatR;

namespace Jeto.Basel.Domain.Messages
{
    public record CommandBase<T> : IRequest<T> where T : class;
}