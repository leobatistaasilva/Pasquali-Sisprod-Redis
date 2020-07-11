using Pasquali.Sisprods.Domain.Commands.Contracts;

namespace Pasquali.Sisprods.Domain.Handlers.Contracts
{
    public interface IHandler<T,R>
    {
        R Handle(T args);
    }
}