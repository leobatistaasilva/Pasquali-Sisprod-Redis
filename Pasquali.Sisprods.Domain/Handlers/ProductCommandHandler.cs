using Flunt.Notifications;
using Pasquali.Sisprods.Domain.Commands;
using Pasquali.Sisprods.Domain.Commands.Contracts;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Domain.Handlers.Contracts;
using Pasquali.Sisprods.Domain.Repositories;

namespace Pasquali.Sisprods.Domain.Handlers
{
    public class ProductCommandHandler :
        Notifiable,
        IHandlerCommand<CreateProductCommand>,
        IHandlerCommand<UpdateProductCommand>,
        IHandlerCommand<DeleteProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly string _genericErrorText;
        private readonly string _genericSuccessText;

        public ProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
            _genericErrorText = "Ops, parece que os dados do produto estão errados!";
            _genericSuccessText = "Produto salvo com sucesso!";
        }

        public ICommandResult Handle(CreateProductCommand command)
        {           
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _genericErrorText, command.Notifications);

            var product = new Product(command.Name);

            // Salva no banco
            _repository.Create(product);

            // Retorna o resultado
            return new GenericCommandResult(true, _genericSuccessText, product);
        }

        public ICommandResult Handle(UpdateProductCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _genericErrorText, command.Notifications);

            // Recupera 
            var product = _repository.GetById(command.Id);

            // modificacoes
            product.UpdateName(command.Name);

            // salva no banco
            _repository.Update(product);

            // Retorna o resultado
            return new GenericCommandResult(true, _genericSuccessText, product);
        }

        public ICommandResult Handle(DeleteProductCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _genericErrorText, command.Notifications);

            // Recupera 
            var product = _repository.GetById(command.Id);
            
            // apaga no banco
            _repository.Delete(product);

            // Retorna o resultado
            return new GenericCommandResult(true, "Produto apagado com sucesso!", product);
        }


    }
}