using System;
using System.Linq.Expressions;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Domain.Queries.Contracts;

namespace Pasquali.Sisprods.Domain.Queries
{
    public class ProductGetByIdQuery : IQuery
    {
        public ProductGetByIdQuery() { }

        public ProductGetByIdQuery(int id) : base()
        {
            Id = id;
        }

        public int Id { get; set; }

        public static Expression<Func<Product, bool>> Constraint(int id)
        {
            return x => x.ProductId == id;
        }
    }
}