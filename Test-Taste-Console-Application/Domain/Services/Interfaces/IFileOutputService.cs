namespace Test_Taste_Console_Application.Domain.Services.Interfaces
{
    ///<summary>
    /// A file output service that can write data from the Solar System OpenData API<see href="https://api.le-systeme-solaire.net/"/>. 
    ///</summary>
    public interface IFileOutputService
    {
        void WriteAllPlanetsAndTheirMoonsToCSVFile();
        void WriteAllMoonsAndTheirMassToCSVFile();
        void WriteAllPlanetsAndTheirAverageMoonTemperatureToCSVFile();
    }
}
