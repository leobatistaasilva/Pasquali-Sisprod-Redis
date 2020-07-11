using System;
using Flunt.Notifications;
using Flunt.Validations;
using Pasquali.Sisprods.Domain.Commands.Contracts;

namespace Pasquali.Sisprods.Domain.Commands
{
    public class CreateClientCommand : Notifiable, ICommand
    {
        public CreateClientCommand() { }

        public CreateClientCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Name, 3, "Name", "Por favor, digite o nome do cliente!")
                    .HasMaxLen(Name, 150, "Name", "Limite ultrapasado do nome do cliente!")
            );
        }
    }
}