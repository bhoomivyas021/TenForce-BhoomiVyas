using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Test_Taste_Console_Application.Domain.DataTransferObjects;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Planet
    {
        public string Id { get; set; }
        public float SemiMajorAxis { get; set; }
        public ICollection<Moon> Moons { get; set; }

        public Planet()
        {
        }

        public Planet(PlanetDto planetDto)
        {
            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
            Moons = new Collection<Moon>();
            foreach(MoonDto moonDto in planetDto.Moons)
            {
                Moons.Add(new Moon(moonDto));
            }
        }

        public float GetAvgTemperatureOfMoons()
        {
            return 0.0f;
        }
    }
}
