using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;
using Test_Taste_Console_Application.Constants;
using Test_Taste_Console_Application.Domain.Services;
using Test_Taste_Console_Application.Domain.Services.Interfaces;
using Test_Taste_Console_Application.Utilities;

namespace Test_Taste_Console_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            //The ConfigureServices function configures the services.
            ConfigureServices(serviceCollection);
            
            //The RunServiceOperations function executes the code that can create the outputs.
            RunServiceOperations(serviceCollection);
        }

        private static void RunServiceOperations(IServiceCollection serviceCollection)
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();

            //The service provider gets the services.
            var fileOutputService = serviceProvider.GetService<IFileOutputService>();
            var screenOutputService = serviceProvider.GetService<IScreenOutputService>();

            try
            {
                screenOutputService.PrintAllPlanetsAndTheirAverageMoonTemperatureToConsole();
                screenOutputService.PrintAllMoonsAndTheirMassToConsole();
                screenOutputService.PrintAllPlanetsAndTheirMoonsToConsole();
            }
            catch (Exception exception)
            {
                //The users and developers can see the thrown exceptions.
                Logger.Instance.Error($"{LoggerMessage.ScreenOutputOperationFailed}{exception.Message}");
                Console.WriteLine($"{ExceptionMessage.ScreenOutputOperationFailed}{exception.Message}");
            }

            try
            {
                fileOutputService.WriteAllPlanetsAndTheirAverageMoonTemperatureToCSVFile();
                fileOutputService.WriteAllMoonsAndTheirMassToCSVFile();
                fileOutputService.WriteAllPlanetsAndTheirMoonsToCSVFile();
            }
            catch (Exception exception)
            {
                //The users and developers can see the thrown exceptions.
                Logger.Instance.Error($"{LoggerMessage.FileOutputOperationFailed}{exception.Message}");
                Console.WriteLine($"{ExceptionMessage.FileOutputOperationFailed}{exception.Message}");
            }

            serviceProvider.Dispose();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            //The function configures all the services.
            XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()),
                new FileInfo(ConfigurationFileName.Logger));
            serviceCollection.AddHttpClient<HttpClientService>();
            serviceCollection.AddSingleton<IPlanetService, PlanetService>();
            serviceCollection.AddSingleton<IScreenOutputService, ScreenOutputService>();
            serviceCollection.AddSingleton<IMoonService, MoonService>();
            serviceCollection.AddSingleton<IFileOutputService, FileOutputService>();
        }
    }
}
