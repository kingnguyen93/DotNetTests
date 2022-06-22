using DotNetTests.Application;
using DotNetTests.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace DotNetTests.Tests
{
    [TestClass]
    public abstract class TestBase
    {
        internal IConfigurationRoot _configuration;
        internal IServiceCollection _services;
        internal IServiceProvider _serviceProvider;

        public TestBase()
        {
            _configuration = ConfigurationHelper.GetIConfigurationRoot("Development", Directory.GetCurrentDirectory().Replace(typeof(TestBase).Namespace, "DotNetTests.Api"));

            _services = new ServiceCollection();

            _services.AddEntityFrameworkCore();

            //We load EXACTLY the same settings (DI and others) than API real solution, what is much better for tests.
            _services.ResolveApplicationDependencies();

            _serviceProvider = _services.BuildServiceProvider();
        }

        public T ResolveService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}