using System.Web.Http;
using Microsoft.Practices.Unity;
using RestMessage.App;
using RestMessage.App.Repositories;
using RestMessage.API.Facades;
using RestMessage.API.Interfaces;
using Unity.WebApi;

namespace RestMessage.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IMessageRepository<IMessage>, InMemoryMessageRepository>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IMessageFacade, MessageFacade>(new ContainerControlledLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}