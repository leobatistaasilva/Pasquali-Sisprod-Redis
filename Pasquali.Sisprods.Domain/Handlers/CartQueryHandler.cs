using Flunt.Notifications;
using MediatR;
using Pasquali.Sisprods.Domain.Commands;
using Pasquali.Sisprods.Domain.Commands.Contracts;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Domain.Handlers.Contracts;
using Pasquali.Sisprods.Domain.Queries;
using Pasquali.Sisprods.Domain.Queries.Contracts;
using Pasquali.Sisprods.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pasquali.Sisprods.Domain.Handlers
{
    public class CartQueryHandler : Handler,
                                                IRequestHandler<CartGetAllQuery, IQueryResult>
    {
        private readonly ICartRepository _repository;
        private readonly ICartRepository _cacheRepository;
        private readonly string _genericErrorText;
        private readonly string _genericSuccessText;

        public CartQueryHandler(ICartRepository repository, ICartRepository cacheRepository)
        {
            _repository = repository;
            _cacheRepository = cacheRepository;
            _genericErrorText = "Ops, parece que os dados do carrinho estão errados!";
            _genericSuccessText = "Carrinho retornados com sucesso!";
        }

   
        public Task<IQueryResult> Handle(CartGetAllQuery query, CancellationToken cancellationToken)
        {
            //pega do cache (Redis)
            var cached = _cacheRepository.GetAll();

            //avalia se trouxe e o cache nao ta desatualizado
            if (cached != null && !INVALIDATE_ALL_CACHE)
                return Task.FromResult<IQueryResult>(new GenericQueryResult<IEnumerable<Cart>>(cached, success: true, message: _genericSuccessText));

            //pega do banco de dados (SQLServer)
            var carts = (IEnumerable<Cart>)_repository.GetAll().ToList();

            //avalia se trouxe e o cache ta desatualizado
            if ((carts != null) && (cached == null || INVALIDATE_ALL_CACHE))
                _cacheRepository.Bind<IEnumerable<Cart>>(carts, "Carts");

            //como o cache foi salvo acima seta o cache como atualizado
            INVALIDATE_ALL_CACHE = false;

            // Retorna o resultado
            return Task.FromResult<IQueryResult>(new GenericQueryResult<IEnumerable<Cart>>(carts, success: true, message: _genericSuccessText));
        }

    }
}