using Pasquali.Sisprods.Domain.Commands.Contracts;

namespace Pasquali.Sisprods.Domain.Handlers.Contracts
{
    public interface IHandlerCommand<C> : IHandler<C,ICommandResult>  
                        where C : ICommand  
    {
        ICommandResult Handle(C command);
    }
}