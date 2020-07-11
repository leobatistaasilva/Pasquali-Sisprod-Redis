using Pasquali.Sisprods.Domain.Handlers;
using Pasquali.Sisprods.Domain.Repositories;
using Pasquali.Sisprods.Infra.Data.Cache;
using Pasquali.Sisprods.Infra.Data.Contexts;
using Pasquali.Sisprods.Infra.Data.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Pasquali.Sisprods.Infra.IoC
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            //Databases
            container.RegisterType<SisprodsContext, SisprodsContext>();
            container.RegisterSingleton<IRedisConnectionFactory, RedisConnectionFactory>();
            container.RegisterType<IDatabase>(
                new PerThreadLifetimeManager(),
                new InjectionFactory(c => new RedisConnectionFactory().Connection().GetDatabase()
            ));

            //Repositories
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IClientRepository, ClientRepository>();            
            container.RegisterType<IProductRepository, ProductCacheRepository>();
            container.RegisterType<IClientRepository, ClientCacheRepository>("ClientCacheRepository");

            //Handlers
            container.RegisterType<ProductCommandHandler, ProductCommandHandler>();
            container.RegisterType<ClientCommandHandler, ClientCommandHandler>();            
            container.RegisterType<ProductQueryHandler, ProductQueryHandler>();
            //container.RegisterType<ClientQueryHandler, ClientQueryHandler>();
            container.RegisterType<ClientQueryHandler>(new InjectionFactory(c =>
            {
                var repository = new ClientRepository(new SisprodsContext());
                var cacheRepository = new ClientCacheRepository(new RedisConnectionFactory().Connection().GetDatabase());
                return new ClientQueryHandler(repository, cacheRepository);
            }));

            //container.RegisterType<ClientQueryHandler>(new InjectionConstructor(
            //        typeof(IProductRepository),
            //        typeof(IProductRepository)
            //    ));

            //container.RegisterType<ClientQueryHandler, ClientQueryHandler>(new InjectionConstructor(
            //        new ResolvedParameter<IClientRepository>("repository"),
            //        new ResolvedParameter<IClientRepository>("cacheRepository")
            //    ));

            // Funcionando!
            //container.RegisterType<ClientQueryHandler>(new InjectionFactory(c =>
            //{
            //    var repository = new ClientRepository(new SisprodsContext());
            //    var cacheRepository = new ClientCacheRepository(new RedisConnectionFactory().Connection().GetDatabase());
            //    return new ClientQueryHandler(repository, cacheRepository);
            //}));


            //Data

            //container.RegisterType<IClientRepository, ClientCacheRepository>(new InjectionConstructor(
            //        new RedisConnectionFactory().Connection().GetDatabase()
            //    ));

            //container.RegisterSingleton<IDatabase>(new InjectionConstructor(
            //        new RedisConnectionFactory().Connection().GetDatabase()
            //    ));


            //services.AddSingleton<IRedisConnectionFactory, RedisConnectionFactory>();
            //public RedisConnectionFactory(IOptions<ConfigurationOptions> redisConfigOptions)


            //container.Resolve<T>();
            //container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            //container.RegisterType<IMyService, MyService>("MyService");
            //container.RegisterType<IMyService, MyService>(new InjectionConstructor(
            //    new ResolvedParameter<IServiceClient>()
            //));

            //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }


    }
}
