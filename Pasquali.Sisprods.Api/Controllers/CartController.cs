
using MediatR;
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
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Pasquali.Sisprods.Api.Controllers
{
    public class CartController : ApiController
    {
        private readonly CartCommandHandler _handlerCommand;
        private readonly IMediator _mediator;

        public CartController(CartCommandHandler handlerCommand, IMediator mediator)
        {
            _handlerCommand = handlerCommand;
            _mediator = mediator;
        }

        // GET: api/Cart
        [ResponseType(typeof(IEnumerable<Cart>))]
        public Task<IHttpActionResult> GetCarts()
        {
            var query = _mediator.Send(new CartGetAllQuery());

            if (query == null)
                return Task.FromResult<IHttpActionResult>(NotFound());

            return Task.FromResult< IHttpActionResult>(Ok(query));
        }



        // GET: api/Cart/5
        //[ResponseType(typeof(Cart))]
        //public IHttpActionResult GetCart(int id)
        //{
        //    //Cart Cart = _repository.GetById(id);
        //    var query = (GenericQueryResult<Cart>)_handlerQuery.Handle(new CartGetByIdQuery(id));

        //    if (query.Entity == null)
        //        return NotFound();

        //    return Ok(query.Entity);
        //}

        // PUT: api/Cart/5
        //[ResponseType(typeof(GenericCommandResult))]
        //public IHttpActionResult PutCart([FromUri] int id, [FromBody] UpdateCartCommand command)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (id != command.Id)
        //        return BadRequest();

        //    try
        //    {
        //        var result = (GenericCommandResult)_handlerCommand.Handle(command);

        //        if (!result.Success)
        //            return Content(HttpStatusCode.BadRequest, result.Data);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!CartExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            //return StatusCode(HttpStatusCode.InternalServerError);
        //            return InternalServerError(ex);
        //        }
        //    }

        //}

        // POST: api/Cart
        [ResponseType(typeof(GenericCommandResult))]
        public Task<IHttpActionResult> PostCart([FromBody] CreateCartCommand command)
        {
            if (!ModelState.IsValid || command == null)
                return Task.FromResult<IHttpActionResult>(BadRequest(ModelState));

            try
            {
                var result = _mediator.Send(command);

                if (!((GenericCommandResult)result.Result).Success)
                    return Task.FromResult<IHttpActionResult>(Content(HttpStatusCode.BadRequest, ((GenericCommandResult)result.Result).Data));

                return Task.FromResult<IHttpActionResult>(Ok(((GenericCommandResult)result.Result)));
            }
            catch (Exception ex)
            {
                return Task.FromResult<IHttpActionResult>(InternalServerError(ex));
            }

        }

    }
}