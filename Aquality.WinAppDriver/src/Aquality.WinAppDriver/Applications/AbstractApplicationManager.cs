using Aquality.Selenium.Core.Applications;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace Aquality.WinAppDriver.Applications
{
    public abstract class AbstractApplicationManager<TManager, TApplication>
        where TManager : AbstractApplicationManager<TManager, TApplication>
        where TApplication : class, IApplication
    {
        private static readonly ThreadLocal<TApplication> AppContainer = new ThreadLocal<TApplication>();
        private static readonly ThreadLocal<IServiceProvider> ServiceProviderContainer = new ThreadLocal<IServiceProvider>();

        protected AbstractApplicationManager()
        {
        }

        public static bool IsStarted => AppContainer.IsValueCreated && AppContainer.Value.Driver.SessionId != null;

        protected static TApplication GetApplication(Func<IServiceProvider, TApplication> startApplicationFunction, Func<IServiceCollection> serviceCollection = null)
        {
            if (!IsStarted)
            {
                AppContainer.Value = startApplicationFunction(
                    GetServiceProvider(service => GetApplication(startApplicationFunction, serviceCollection), serviceCollection));
            }
            return AppContainer.Value;
        }

        protected static IServiceProvider GetServiceProvider(Func<IServiceProvider, TApplication> applicationSupplier, Func<IServiceCollection> serviceCollectionSupplier = null)
        {
            if (!ServiceProviderContainer.IsValueCreated)
            {
                IServiceCollection services;
                if (serviceCollectionSupplier == null)
                {
                    services = new ServiceCollection();
                    new Startup().ConfigureServices(services, applicationSupplier);
                }
                else
                {
                    services = serviceCollectionSupplier();
                }
                ServiceProviderContainer.Value = services.BuildServiceProvider();
            }
            return ServiceProviderContainer.Value;
        }
    }
}