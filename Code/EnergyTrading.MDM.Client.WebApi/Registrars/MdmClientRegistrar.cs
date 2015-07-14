using System.Configuration;

using EnergyTrading.Caching;
using EnergyTrading.Caching.InMemory.Registrars;
using EnergyTrading.Container.Unity;
using EnergyTrading.Mdm.Client.Services;
using EnergyTrading.Mdm.Client.WebApi.WebApiClient;
using EnergyTrading.Mdm.Client.WebClient;
using EnergyTrading.Mdm.Contracts;

using Microsoft.Practices.Unity;

namespace EnergyTrading.Mdm.Client.WebApi.Registrars
{
    public class MdmClientRegistrar : IContainerRegistrar
    {
        public virtual int MdmCacheTimeout
        {
            get
            {
                int cacheTimeout;
                return int.TryParse(ConfigurationManager.AppSettings["MdmCacheTimeout"], out cacheTimeout)
                           ? cacheTimeout
                           : 120;
            }
        }

        public virtual bool MdmCaching
        {
            get
            {
                bool cache;
                return bool.TryParse(ConfigurationManager.AppSettings["MdmCaching"], out cache) && cache;
            }
        }

        public virtual string BaseUri
        {
            get { return ConfigurationManager.AppSettings["MdmUri"]; }
        }

        public virtual void Register(IUnityContainer container)
        {
            // Http clients
            container.RegisterType<IHttpClientFactory, HttpClientFactory>();


            // Register Cache service
            InMemoryCacheRegistrar.Register(container);
            container.RegisterType<IMdmClientCacheRepository, DefaultMdmClientCacheRepository>();

            // Requester
            container.RegisterType<IMessageRequester, MessageRequester>();

            // MDM Entity Services
            RegisterMdmService<SourceSystem>(container, "sourcesystem");
            RegisterEntityServices(container);
            RegisterReferenceDataService(container);

            // MDM Client service
            container.RegisterType<IMdmEntityServiceFactory, LocatorMdmEntityServiceFactory>();
            container.RegisterType<IMdmService, MdmService>();

            // Register the IMdmEntityLocators factory
            container.RegisterType<IMdmEntityLocatorService, MdmEntityLocatorFactory>();

            // Standard locator, no chain
            container.RegisterType(typeof(IMdmEntityLocator<>), typeof(MdmEntityServiceFactoryMdmEntityLocator<>));

            container.RegisterType<IMdmModelEntityService, MdmModelEntityService>();
            container.RegisterType<IMdmModelEntityServiceFactory, LocatorMdmModelEntityServiceFactory>();
            RegisterModelEntityServices(container);
        }

        protected virtual void RegisterEntityServices(IUnityContainer container)
        {
        }

        protected virtual void RegisterModelEntityServices(IUnityContainer container)
        {
        }

        protected virtual void RegisterMdmService<T>(IUnityContainer container, string url, uint version = 0)
            where T : class, IMdmEntity
        {
            if (MdmCaching)
            {
                var cachekey = "Mdm." + typeof(T).Name + (version > 0 ? "V" + version : string.Empty);

                container.RegisterAbsoluteCacheItemPolicyFactory(cachekey);

                // url is different for different versions so can still use it here
                container.RegisterType<IMdmEntityService<T>, MdmEntityService<T>>(
                    url, 
                    new InjectionConstructor(
                        BaseUri + "/" + url, 
                        new ResolvedParameter<IMessageRequester>()));

                // Singleton as we have a cache but named according to version if necessary
                if (version == 0)
                {
                    container.RegisterType<IMdmEntityService<T>, CachePolicyMdmEntityService<T>>(new ContainerControlledLifetimeManager(), new InjectionConstructor(new ResolvedParameter<IMdmEntityService<T>>(url), new ResolvedParameter<ICacheItemPolicyFactory>(cachekey), new ResolvedParameter<IMdmClientCacheRepository>(), version));
                }
                else
                {
                    container.RegisterType<IMdmEntityService<T>, CachePolicyMdmEntityService<T>>("V" + version, new ContainerControlledLifetimeManager(), new InjectionConstructor(new ResolvedParameter<IMdmEntityService<T>>(url), new ResolvedParameter<ICacheItemPolicyFactory>(cachekey), new ResolvedParameter<IMdmClientCacheRepository>(), version));
                }
            }
            else
            {
                if (version == 0)
                {
                    container.RegisterType<IMdmEntityService<T>, MdmEntityService<T>>(new InjectionConstructor(BaseUri + "/" + url, new ResolvedParameter<IMessageRequester>()));
                }
                else
                {
                    container.RegisterType<IMdmEntityService<T>, MdmEntityService<T>>("V" + version, new InjectionConstructor(BaseUri + "/" + url, new ResolvedParameter<IMessageRequester>()));
                }

            }
        }

        protected virtual void RegisterReferenceDataService(IUnityContainer container)
        {
            container.RegisterType<IReferenceDataService, ReferenceDataService>(
                new InjectionConstructor(BaseUri + "/referencedata", new ResolvedParameter<IMessageRequester>()));
        }
    }
}