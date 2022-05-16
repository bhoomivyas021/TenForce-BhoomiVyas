using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using log4net;
using Test_Taste_Console_Application.Constants;
using Test_Taste_Console_Application.Domain.DataTransferObjects;
using Test_Taste_Console_Application.Domain.Objects;
using Test_Taste_Console_Application.Domain.Services.Interfaces;
using Test_Taste_Console_Application.Utilities;


namespace Test_Taste_Console_Application.Domain.Services
{
    /// <inheritdoc />
    public class FileOutputService : IFileOutputService
    {
        private readonly IPlanetService _planetService;

        private readonly IMoonService _moonService;


        public FileOutputService(IPlanetService planetService, IMoonService moonService)
        {
            _planetService = planetService;
            _moonService = moonService;
        }

        public void WriteAllPlanetsAndTheirMoonsToCSVFile()
        {
            //The service gets all planets and their moons from the API.
            var planets = _planetService.GetAllPlanets().ToArray();

            //If the planets aren't found, then the function stops and tells that to the user via the console.
            if (!planets.Any())
            {
                Console.WriteLine(OutputString.NoPlanetsFound);
                return;
            }

            //The string builder creates the header with the correct labels for the CSV file.
            var stringBuilder =
                new StringBuilder(
                        $"{OutputString.PlanetNumber};{OutputString.PlanetId};{OutputString.PlanetSemiMajorAxis};{OutputString.TotalMoons};{OutputString.MoonNumber};{OutputString.MoonId}")
                    .AppendLine();

            //The string builder adds the data of the planets.
            for (int i = 0, j = 1; i < planets.Length; i++, j++)
            {
                for (int k = 0, l = 1; k < planets[i].Moons.Count; k++, l++)
                {
                    stringBuilder.Append(
                            $"{j};{CultureInfoUtility.TextInfo.ToTitleCase(planets[i].Id)};{planets[i].SemiMajorAxis};{planets[i].Moons.Count};{l};{planets[i].Moons.ElementAt(k).Id}")
                        .AppendLine();
                }
            }

            //The string from the builder is passed to the WriteStringToFile function. The file is created with the correct file name.
            WriteStringToFile(stringBuilder.ToString(), PathName.AllPlanetsAndTheirMoonsFile);
            stringBuilder.Clear();
        }

        public void WriteAllMoonsAndTheirMassToCSVFile()
        {
            //The service gets all the moons from the API.
            var moons = _moonService.GetAllMoons().ToArray();

            //If the moons aren't found, then the function stops and tells that to the user via the console.
            if (!moons.Any())
            {
                Console.WriteLine(OutputString.NoMoonsFound);
                return;
            }

            //The string builder creates the header with the correct labels for the CSV file.
            var stringBuilder =
                new StringBuilder(
                        $"{OutputString.MoonNumber};{OutputString.MoonId};{OutputString.MoonMassExponent};{OutputString.MoonMassValue}")
                    .AppendLine();

            //The string builder adds the data of the moons.
            for (int i = 0, j = 1; i < moons.Length; i++, j++)
            {
                stringBuilder.Append(
                        $"{j};{CultureInfoUtility.TextInfo.ToTitleCase(moons[i].Id)};{moons[i].MassExponent};{moons[i].MassValue}")
                    .AppendLine();
            }

            //The string from the builder is passed to the WriteStringToFile function. The file is created with the correct file name.
            WriteStringToFile(stringBuilder.ToString(), PathName.AllMoonsAndTheirMassFile);
            stringBuilder.Clear();
        }

        private void WriteStringToFile(string content, string fileName)
        {
            try
            {
                //If the output folder doesn't exist it will be created.
                if (!Directory.Exists(PathName.PathToOutputFolder))
                {
                    CreateOutputFolder();
                }

                //The file is created in the output folder.
                File.WriteAllText(
                    Path.Combine(PathName.PathToOutputFolder, fileName),
                    content);
                Console.WriteLine($"{OutputString.FileCreated}{PathName.PathToOutputFolder}");
            }
            catch (Exception exception)
            {
                //The logger creates a message if the exception is thrown.
                Logger.Instance.Error(exception.Message);
                throw;
            }
        }

        public void WriteAllPlanetsAndTheirAverageMoonTemperatureToCSVFile()
        {
            //The service gets all the moons from the API.
            var planets = _planetService.GetAllPlanets().ToArray();

            //If the moons aren't found, then the function stops and tells that to the user via the console.
            if (!planets.Any())
            {
                Console.WriteLine(OutputString.NoPlanetsFound);
                return;
            }

            //The string builder creates the header with the correct labels for the CSV file.
            var stringBuilder =
                new StringBuilder(
                        $"{OutputString.PlanetId};{OutputString.PlanetMoonAverageTemperature}")
                    .AppendLine();

            foreach(Planet planet in planets)
            {
                stringBuilder.AppendLine($"{planet.Id};{planet.GetAvgTemperatureOfMoons()}");
            }

            //The string from the builder is passed to the WriteStringToFile function. The file is created with the correct file name.
            WriteStringToFile(stringBuilder.ToString(), PathName.AllPlanetsAndTheirAverageMoonTemperatureFile);
            stringBuilder.Clear();
        }

        private void CreateOutputFolder()
        {
            try
            {
                //The output folder is created in the correct location.
                Directory.CreateDirectory(PathName.PathToOutputFolder);
                Console.WriteLine($"{OutputString.FolderCreated}{PathName.ProjectDirectory}");
            }
            catch (Exception exception)
            {
                //The logger creates a message if the exception is thrown.
                Logger.Instance.Error(exception.Message);
                throw;
            }
        }
    }
}
