using System;
using Flunt.Notifications;
using Flunt.Validations;
using Pasquali.Sisprods.Domain.Commands.Contracts;

namespace Pasquali.Sisprods.Domain.Commands
{
    public class DeleteProductCommand : Notifiable, ICommand
    {
        public DeleteProductCommand() { }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }


        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsGreaterThan(Id,0,"Id","Produto sem identificador!")
            );
        }
    }
}