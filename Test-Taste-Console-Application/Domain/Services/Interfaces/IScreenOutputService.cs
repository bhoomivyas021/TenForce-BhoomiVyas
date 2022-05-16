namespace Test_Taste_Console_Application.Domain.Services.Interfaces
{
    ///<summary>
    /// A screen output service that can show data from the Solar System OpenData API<see href="https://api.le-systeme-solaire.net/"/> to a user via the console. 
    ///</summary>
    public interface IScreenOutputService
    {
        void PrintAllPlanetsAndTheirMoonsToConsole();
        void PrintAllMoonsAndTheirMassToConsole();
        void PrintAllPlanetsAndTheirAverageMoonTemperatureToConsole();
    }
}
