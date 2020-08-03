using Flunt.Notifications;
using MediatR;
using Pasquali.Sisprods.Domain.Commands;
using Pasquali.Sisprods.Domain.Commands.Contracts;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Domain.Handlers.Contracts;
using Pasquali.Sisprods.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Pasquali.Sisprods.Domain.Handlers
{
    public class CartCommandHandler : Handler,
                                                IRequestHandler<CreateCartCommand, ICommandResult>
                                                //IHandlerCommand<CreateCartCommand>
    {
        private readonly ICartRepository _repository;
        private readonly string _genericErrorText;
        private readonly string _genericSuccessText;

        public CartCommandHandler(ICartRepository repository)
        {
            _repository = repository;
            _genericErrorText = "Ops, parece que os dados do carrinho estão errados!";
            _genericSuccessText = "CArrinho salvo com sucesso!";
        }

        public Task<ICommandResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
        {
            //validando comando preenchido
            if (command == null)
                return Task.FromResult<ICommandResult>(new GenericCommandResult(false, _genericErrorText, null));
            //avalia comando valido
            command.Validate();
            if (command.Invalid)
                return Task.FromResult <ICommandResult>(new GenericCommandResult(false, _genericErrorText, command.Notifications));
            //cria o objeto
            var cart = new Cart();

            // Salva no banco
            _repository.Create(cart);
            //invalida o cache
            INVALIDATE_ONE_CACHE = true;
            INVALIDATE_ALL_CACHE = true;

            // Retorna o resultado
            return Task.FromResult<ICommandResult>(new GenericCommandResult(true, _genericSuccessText, cart));
        }

        //public ICommandResult Handle(UpdateClientCommand command)
        //{
        //    if (command == null)
        //        return new GenericCommandResult(false, _genericErrorText, null);

        //    command.Validate();
        //    if (command.Invalid)
        //        return new GenericCommandResult(false, _genericErrorText, command.Notifications);

        //    // Recupera 
        //    var client = _repository.GetById(command.Id);

        //    // modificacoes
        //    client.UpdateName(command.Name);

        //    // salva no banco
        //    _repository.Update(client);

        //    INVALIDATE_ONE_CACHE = true;
        //    INVALIDATE_ALL_CACHE = true;

        //    // Retorna o resultado
        //    return new GenericCommandResult(true, _genericSuccessText, client);
        //}

        //public ICommandResult Handle(DeleteClientCommand command)
        //{
        //    if (command == null)
        //        return new GenericCommandResult(false, _genericErrorText, null);

        //    command.Validate();
        //    if (command.Invalid)
        //        return new GenericCommandResult(false, _genericErrorText, command.Notifications);

        //    // Recupera 
        //    var client = _repository.GetById(command.Id);

        //    // apaga no banco
        //    _repository.Delete(client);

        //    INVALIDATE_ONE_CACHE = true;
        //    INVALIDATE_ALL_CACHE = true;

        //    // Retorna o resultado
        //    return new GenericCommandResult(true, "Cliente apagado com sucesso!", client);
        //}

        //public ICommandResult Handle(AddProductsClientCommand command)
        //{
        //    if (command == null)
        //        return new GenericCommandResult(false, _genericErrorText, null);

        //    command.Validate();
        //    if (command.Invalid)
        //        return new GenericCommandResult(false, _genericErrorText, command.Notifications);

        //    // Recupera 
        //    var client = _repository.GetById(command.ClientId);

        //    // modificacoes
        //    client.AddProducts(command.Products);

        //    // salva no banco
        //    _repository.Update(client);

        //    INVALIDATE_ONE_CACHE = true;
        //    INVALIDATE_ALL_CACHE = true;

        //    // Retorna o resultado
        //    return new GenericCommandResult(true, "Produtos adicionados com sucesso para este cliente!", client);
        //}

    }
}