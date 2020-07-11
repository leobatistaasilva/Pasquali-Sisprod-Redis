using Pasquali.Sisprods.Domain.Commands;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Domain.Handlers;
using Pasquali.Sisprods.Domain.Queries;
using Pasquali.Sisprods.Infra.Data.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Pasquali.Sisprods.Api.Controllers
{
    public class ClientController : ApiController
    {
        private readonly ClientRepository _repository;
        private readonly ClientCommandHandler _handlerCommand;
        private readonly ClientQueryHandler _handlerQuery;
        private readonly IDatabase _cacheDB;
        private readonly string _instanceCacheName;
        private static bool _INVALIDATE_CACHE;

        public ClientController(ClientRepository repository, ClientCommandHandler handlerCommand, ClientQueryHandler handlerQuery)
        {
            _repository = repository;
            _handlerCommand = handlerCommand;
            _handlerQuery = handlerQuery;
            _cacheDB = RedisConnectorHelper.Connection.GetDatabase();
            _instanceCacheName = "Product_";
        }

        // GET: api/Client
        [ResponseType(typeof(IEnumerable<Client>))]
        public IHttpActionResult GetClients()
        {

            var query = (GenericQueryResult<IEnumerable<Client>>)_handlerQuery.Handle(new ClientGetAll());

            if (query.Entity == null)
                return NotFound();

            return Ok(query.Entity);
        }

        // GET: api/Client/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(int id)
        {
            //Client client = _repository.GetById(id);
            var query = (GenericQueryResult<Client>)_handlerQuery.Handle(new ClientGetByIdQuery(id));

            if (query.Entity == null)
                return NotFound();

            return Ok(query.Entity);
        }

        // PUT: api/Client/5
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult PutClient([FromUri] int id, [FromBody] UpdateClientCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != command.Id)
                return BadRequest();

            try
            {
                var result = (GenericCommandResult)_handlerCommand.Handle(command);

                if (!result.Success)
                    return Content(HttpStatusCode.BadRequest, result.Data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    //return StatusCode(HttpStatusCode.InternalServerError);
                    return InternalServerError(ex);
                }
            }

        }

        // POST: api/Client
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult PostClient([FromBody] CreateClientCommand command)
        {
            if (!ModelState.IsValid || command == null)
                return BadRequest(ModelState);

            try
            {
                var result = (GenericCommandResult)_handlerCommand.Handle(command);

                if (!result.Success)
                    return Content(HttpStatusCode.BadRequest, result.Data);

                //var query = (GenericQueryResult<Client>)_handlerQuery.Handle(new ClientGetByIdQuery(result.Data.Id));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // DELETE: api/Client/5
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult DeleteClient([FromUri] int id, [FromBody] DeleteClientCommand command)
        {
            if (!ModelState.IsValid || command == null)
                return BadRequest(ModelState);

            if (id != command.Id)
                return BadRequest();

            try
            {
                var result = (GenericCommandResult)_handlerCommand.Handle(command);

                if (!result.Success)
                    return Content(HttpStatusCode.BadRequest, result.Data);

                var query = (GenericQueryResult<Client>)_handlerQuery.Handle(new ClientGetByIdQuery(id));

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    //return StatusCode(HttpStatusCode.InternalServerError);
                    return InternalServerError(ex);
                }
            }
        }

        // PUT: api/Client/5
        [Route("{id}/products")]
        [HttpPut]
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult PutClientProducts([FromUri] int id, [FromBody] AddProductsClientCommand command)
        {
            if (!ModelState.IsValid || command == null)
                return BadRequest(ModelState);

            if (id != command.ClientId)
                return BadRequest();

            try
            {
                var result = (GenericCommandResult)_handlerCommand.Handle(command);

                if (!result.Success)
                    return Content(HttpStatusCode.BadRequest, result.Data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (!ClientExists(id))
                    return NotFound();

                return InternalServerError(ex);
                //return StatusCode(HttpStatusCode.InternalServerError);
            }
        }


        private bool ClientExists(int id)
        {
            var clientExists = (GenericQueryResult<Client>)_handlerQuery.Handle(new ClientGetByIdQuery(id));
            return (clientExists != null || clientExists.Entity.ClientId > 0);
        }
    }
}