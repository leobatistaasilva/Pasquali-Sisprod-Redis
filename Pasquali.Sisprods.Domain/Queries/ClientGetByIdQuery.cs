using System;
using System.Linq.Expressions;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Domain.Queries.Contracts;

namespace Pasquali.Sisprods.Domain.Queries
{
    public class ClientGetByIdQuery : IQuery
    {
        public ClientGetByIdQuery() { }

        public ClientGetByIdQuery(int id) : base()
        {
            Id = id;
        }

        public int Id { get; set; }

        public static Expression<Func<Client, bool>> Constraint(int id)
        {
            return x => x.ClientId == id;
        }
    }
}