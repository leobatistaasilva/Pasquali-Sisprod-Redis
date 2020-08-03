using System;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Pasquali.Sisprods.Domain.Commands.Contracts;

namespace Pasquali.Sisprods.Domain.Commands
{
    public class CreateCartCommand : Notifiable, 
                                            IRequest<ICommandResult>,
                                            ICommand
    {
        public CreateCartCommand() { }

        //public string Name { get; set; }


        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    //.IsNotEmpty(Id, "Id", "Carrinho sem identificador da sessao.")
            );
        }
    }
}