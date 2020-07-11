using System;
using Flunt.Notifications;
using Flunt.Validations;
using Pasquali.Sisprods.Domain.Commands.Contracts;

namespace Pasquali.Sisprods.Domain.Commands
{
    public class DeleteClientCommand : Notifiable, ICommand
    {
        public DeleteClientCommand() { }

        public DeleteClientCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }


        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsGreaterThan(Id,0,"Id","Cliente sem identificador!")
            );
        }
    }
}