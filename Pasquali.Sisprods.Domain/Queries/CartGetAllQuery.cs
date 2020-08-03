
using System;
using System.Linq.Expressions;
using MediatR;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Domain.Queries.Contracts;

namespace Pasquali.Sisprods.Domain.Queries
{
    public class CartGetAllQuery : IRequest<IQueryResult>,
                                    IQuery
    {
        public CartGetAllQuery() { }

        public static Expression<Func<Client, bool>> Constraint()
        {
            return x => x.ClientId != 0;
        }
    }
}