using Flunt.Notifications;
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

namespace Pasquali.Sisprods.Domain.Handlers
{
    public class ClientQueryHandler : Handler,
                                                IHandlerQuery<ClientGetAll>,
                                                IHandlerQuery<ClientGetByIdQuery>
    {
        private readonly IClientRepository _repository;
        private readonly IClientRepository _cacheRepository;
        private readonly string _genericErrorText;
        private readonly string _genericSuccessText;

        public ClientQueryHandler(IClientRepository repository, IClientRepository cacheRepository)
        {
            _repository = repository;
            _cacheRepository = cacheRepository;
            _genericErrorText = "Ops, parece que os dados do cliente estão errados!";
            _genericSuccessText = "Cliente retornados com sucesso!";
        }

        public IQueryResult Handle(ClientGetAll query)
        {

            var cached = _cacheRepository.GetAll();

            if (cached != null && !INVALIDATE_ALL_CACHE)
                return new GenericQueryResult<IEnumerable<Client>>(cached, success: true, message: _genericSuccessText);

            var clients = (IEnumerable<Client>)_repository.GetAll().ToList();

            //essa eu tive que comentar em portugues 
            //eu ia fazer um if(_INVALIDATE_CACHE) Cache.Del antes aqui
            //mas descobri que nao precisa ele atualiza o valor dada a mesma chave
            //isso eh tecnologia pura :)
            if ((clients != null) && (cached == null || INVALIDATE_ALL_CACHE))
                _cacheRepository.Bind<IEnumerable<Client>>(clients, "Clients");

            INVALIDATE_ALL_CACHE = false;

            // Retorna o resultado
            return new GenericQueryResult<IEnumerable<Client>>(clients, success: true, message: _genericSuccessText);
        }

        public IQueryResult Handle(ClientGetByIdQuery query)
        {

            var cached = _cacheRepository.GetById(query.Id);

            if (cached != null && !INVALIDATE_ONE_CACHE)
                return new GenericQueryResult<Client>(cached, success: true, message: _genericSuccessText);

            // Recupera 
            var client = _repository.GetById(query.Id);

            //checa se o cliente foi apagado da base
            bool clientDeleted = (client == null && cached != null);

            //se cliente foi apagado instrucoes de controle do cache
            if (clientDeleted)
            {
                _cacheRepository.Delete(cached);
                INVALIDATE_ONE_CACHE = false; //reseta flag pra nao add novamente objeto no cache
                INVALIDATE_ALL_CACHE = true; //invalida cache pra listas
            }                

            if ((client != null) && (cached == null || INVALIDATE_ONE_CACHE))
                _cacheRepository.Update(client); // cria ou atualiza no cache

            INVALIDATE_ONE_CACHE = false;

            // Retorna o resultado
            return new GenericQueryResult<Client>(client, success: true, message: _genericSuccessText);
        }

    }
}