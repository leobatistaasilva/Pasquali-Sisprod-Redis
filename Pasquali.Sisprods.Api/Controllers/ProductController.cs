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
    public class ProductController : ApiController
    {
        private readonly ProductRepository _repository;
        private readonly ProductCommandHandler _handlerCommand;
        private readonly ProductQueryHandler _handlerQuery;
        private readonly IDatabase _cacheDB;
        private readonly string _instanceCacheName;
        private static bool _INVALIDATE_CACHE;

        public ProductController(ProductRepository repository, ProductCommandHandler handlerCommand, ProductQueryHandler handlerQuery)
        {
            _repository = repository;
            _handlerCommand = handlerCommand;
            _handlerQuery = handlerQuery;
            _cacheDB = RedisConnectorHelper.Connection.GetDatabase();
            _instanceCacheName = "Product_";
        }

        // GET: api/Product
        [ResponseType(typeof(IEnumerable<Product>))]
        public IHttpActionResult GetProducts()
        {
            var cached = RedisCache.GetObject(_cacheDB, "Products");

            if (cached != null && !_INVALIDATE_CACHE)
                return Ok(cached);

            var products = (IEnumerable<Product>)_repository.GetAll().ToList();
            //var products = _repository.GetAll().ToList();            

            //essa eu tive que comentar em portugues 
            //eu ia fazer um if(_INVALIDATE_CACHE) Cache.Del antes aqui
            //mas descobri que nao precisa ele atualiza o valor dada a mesma chave
            //isso eh tecnologia pura :)
            if (cached == null || _INVALIDATE_CACHE)
                RedisCache.SetObjectLoopIgnore<IEnumerable<Product>>(_cacheDB, "Products", products);

            _INVALIDATE_CACHE = false;
            return Ok((IEnumerable<Product>)_repository.GetAll().ToList());
        }

        //// GET: api/Product/5
        //[ResponseType(typeof(Product))]
        //public IHttpActionResult GetProduct(int id)
        //{
        //    Product product = _repository.GetById(id);

        //    if (product == null)
        //        return NotFound();

        //    return Ok(product);
        //}


        // GET: api/Product/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            //Product product = _repository.GetById(id);

            var cached = RedisCache.GetObject(_cacheDB, _instanceCacheName + id);

            if (cached != null)
                return Ok(cached);

            var query = (GenericQueryResult<Product>) _handlerQuery.Handle(new ProductGetByIdQuery(id));

            if (query.Entity == null)
                return NotFound();

            if(cached==null)
                RedisCache.SetObjectLoopIgnore<Product>(_cacheDB, _instanceCacheName + query.Entity.ProductId, query.Entity);

            return Ok(query.Entity);
        }


        // POST: api/Product
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult PostProduct([FromBody] CreateProductCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = (GenericCommandResult)_handlerCommand.Handle(command);

            if (!result.Success)
                return Content(HttpStatusCode.BadRequest, result.Data);

            var product = (Product)result.Data;
            RedisCache.SetObject<Product>(_cacheDB, _instanceCacheName + product.ProductId, product);
            _INVALIDATE_CACHE = true;

            return Ok(result);
        }

        // PUT: api/Product/5
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult PutProduct([FromUri] int id, [FromBody] UpdateProductCommand command)
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
                
                //esta linha nao eh mais necessaria dado a mesma chave o valor eh atualizado :)
                //RedisCache.DelObject(_cacheDB, _instanceCacheName + id);
                var product = (Product) result.Data;
                RedisCache.SetObjectLoopIgnore<Product>(_cacheDB, _instanceCacheName+ product.ProductId, product);
                _INVALIDATE_CACHE = true;

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (!ProductExists(id))
                    return NotFound();
                
                return InternalServerError(ex);
            }

        }


        // DELETE: api/Product/5
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult DeleteProduct([FromUri] int id, [FromBody] DeleteProductCommand command)
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

                RedisCache.DelObject(_cacheDB, _instanceCacheName + id);
                _INVALIDATE_CACHE = true;

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (!ProductExists(id))
                    return NotFound();

                return InternalServerError(ex);
            }
        }

        private bool ProductExists(int id)
        {
            var productExists = _repository.GetById(id);
            return (productExists != null || productExists.ProductId > 0);
        }
    }
}