using Flunt.Notifications;
using Pasquali.Sisprods.Domain.Commands;
using Pasquali.Sisprods.Domain.Commands.Contracts;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Domain.Handlers.Contracts;
using Pasquali.Sisprods.Domain.Queries;
using Pasquali.Sisprods.Domain.Queries.Contracts;
using Pasquali.Sisprods.Domain.Repositories;
using System;

namespace Pasquali.Sisprods.Domain.Handlers
{
    public class ProductQueryHandler : 
        IHandlerQuery<ProductGetByIdQuery>
    {
        private readonly IProductRepository _repository;
        private readonly string _genericErrorText;
        private readonly string _genericSuccessText;

        public ProductQueryHandler(IProductRepository repository)
        {
            _repository = repository;
            _genericErrorText = "Ops, parece que os dados do produto estão errados!";
            _genericSuccessText = "Produto retornados com sucesso!";
        }

        public IQueryResult Handle(ProductGetByIdQuery query)
        {
            // Recupera 
            var product = _repository.GetById(query.Id);

            // Retorna o resultado
            return new GenericQueryResult<Product>(product, success: true, message: _genericSuccessText);
        }

    }
}