using System;
using Flunt.Notifications;
using Flunt.Validations;
using Pasquali.Sisprods.Domain.Commands.Contracts;

namespace Pasquali.Sisprods.Domain.Commands
{
    public class CreateProductCommand : Notifiable, ICommand
    {
        public CreateProductCommand() { }

        public CreateProductCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Name, 3, "Name", "Por favor, digite o nome do produto!")
                    .HasMaxLen(Name, 150, "Name", "Limite ultrapasado do nome do produto!")
            );
        }
    }
}