using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;
using Flunt.Validations;
using Pasquali.Sisprods.Domain.Commands.Contracts;
using Pasquali.Sisprods.Domain.Entities;

namespace Pasquali.Sisprods.Domain.Commands
{
    public class AddProductsClientCommand : Notifiable, ICommand
    {
        public AddProductsClientCommand() 
        {
            Products = new List<Product>();
        }

        public AddProductsClientCommand(int clientId, IEnumerable<Product> products)
        {
            ClientId = clientId;
            Products = products;
        }

        public int ClientId { get; set; }
        public IEnumerable<Product> Products { get; private set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsGreaterThan(ClientId, 0,"Id","Cliente sem identificador")
                    .IsNotNull(Products,"Products", "Por favor, adicione produtos!")
                    .IsGreaterThan(Products.Count(), 1, "Products", "Por favor, adicione ao menos um produto!")
                    .IsLowerThan(Products.Count(), 15, "Products", "Limite ultrapasado de 15 produtos do cliente!")
            );
        }
    }
}