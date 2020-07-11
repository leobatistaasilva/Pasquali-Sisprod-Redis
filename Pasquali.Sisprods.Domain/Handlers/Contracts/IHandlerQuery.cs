
using Pasquali.Sisprods.Domain.Queries.Contracts;

namespace Pasquali.Sisprods.Domain.Handlers.Contracts
{
    public interface IHandlerQuery<Q> : IHandler<Q, IQueryResult>
                        where Q : IQuery
    {
        IQueryResult Handle(Q query);
    }
}