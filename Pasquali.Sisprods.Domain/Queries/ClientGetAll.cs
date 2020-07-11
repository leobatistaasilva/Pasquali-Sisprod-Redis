
using System;
using System.Linq.Expressions;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Domain.Queries.Contracts;

namespace Pasquali.Sisprods.Domain.Queries
{
    public class ClientGetAll : IQuery
    {
        public ClientGetAll() { }

        public static Expression<Func<Client, bool>> Constraint()
        {
            return x => x.ClientId != 0;
        }
    }
}