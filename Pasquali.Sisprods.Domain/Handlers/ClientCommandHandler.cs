using Flunt.Notifications;
using Pasquali.Sisprods.Domain.Commands;
using Pasquali.Sisprods.Domain.Commands.Contracts;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Domain.Handlers.Contracts;
using Pasquali.Sisprods.Domain.Repositories;

namespace Pasquali.Sisprods.Domain.Handlers
{
    public class ClientCommandHandler : Handler,
                                                IHandlerCommand<CreateClientCommand>,
                                                IHandlerCommand<UpdateClientCommand>,
                                                IHandlerCommand<DeleteClientCommand>,
                                                IHandlerCommand<AddProductsClientCommand>
    {
        private readonly IClientRepository _repository;
        private readonly string _genericErrorText;
        private readonly string _genericSuccessText;

        public ClientCommandHandler(IClientRepository repository)
        {
            _repository = repository;
            _genericErrorText = "Ops, parece que os dados do cliente estão errados!";
            _genericSuccessText = "Cliente salvo com sucesso!";
        }

        public ICommandResult Handle(CreateClientCommand command)
        {           
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _genericErrorText, command.Notifications);

            var client = new Client(command.Name);

            // Salva no banco
            _repository.Create(client);

            INVALIDATE_ONE_CACHE = true;
            INVALIDATE_ALL_CACHE = true;

            // Retorna o resultado
            return new GenericCommandResult(true, _genericSuccessText, client);
        }

        public ICommandResult Handle(UpdateClientCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _genericErrorText, command.Notifications);

            // Recupera 
            var client = _repository.GetById(command.Id);

            // modificacoes
            client.UpdateName(command.Name);

            // salva no banco
            _repository.Update(client);

            INVALIDATE_ONE_CACHE = true;
            INVALIDATE_ALL_CACHE = true;

            // Retorna o resultado
            return new GenericCommandResult(true, _genericSuccessText, client);
        }

        public ICommandResult Handle(DeleteClientCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _genericErrorText, command.Notifications);

            // Recupera 
            var client = _repository.GetById(command.Id);
            
            // apaga no banco
            _repository.Delete(client);

            INVALIDATE_ONE_CACHE = true;
            INVALIDATE_ALL_CACHE = true;

            // Retorna o resultado
            return new GenericCommandResult(true, "Cliente apagado com sucesso!", client);
        }

        public ICommandResult Handle(AddProductsClientCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _genericErrorText, command.Notifications);

            // Recupera 
            var client = _repository.GetById(command.ClientId);

            // modificacoes
            client.AddProducts(command.Products);

            // salva no banco
            _repository.Update(client);

            INVALIDATE_ONE_CACHE = true;
            INVALIDATE_ALL_CACHE = true;

            // Retorna o resultado
            return new GenericCommandResult(true, "Produtos adicionados com sucesso para este cliente!", client);
        }

    }
}