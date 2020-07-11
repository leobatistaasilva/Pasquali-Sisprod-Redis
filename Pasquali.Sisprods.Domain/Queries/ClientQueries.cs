using System;
using System.Linq;
using System.Linq.Expressions;
using Pasquali.Sisprods.Domain.Entities;

namespace Pasquali.Sisprods.Domain.Queries
{
    public static class ClientQueries
    {
        public static Expression<Func<Client, bool>> GetById(int id)
        {
            return x => x.ClientId == id;
        }

        public static Expression<Func<Client, bool>> GetByIdWithProducts(int id)
        {
            //return x => x.Id == id && x.ClientClasses.All(y => y.ClientId == id);
            return null;
        }

    }
}