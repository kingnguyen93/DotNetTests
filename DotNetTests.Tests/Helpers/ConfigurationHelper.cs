using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Tests
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string environment, string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(!string.IsNullOrEmpty(outputPath) ? outputPath : Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
