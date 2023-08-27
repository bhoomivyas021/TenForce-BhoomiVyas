using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Test_Taste_Console_Application.Domain.DataTransferObjects;
using System.Linq;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Planet
    {
        public string Id { get; set; }
        public float SemiMajorAxis { get; set; }
        public ICollection<Moon> Moons { get; set; }
        //Calculate Averge Moon's grvity for the planet
        //Earlier It was displaying 0.0f by default
        //Converted float to decimal to round off the values
        public decimal AverageMoonGravity
        {
            get => Math.Round(Moons.Select(x => x.Gravity).Average(), 3);
        }

        public Planet(PlanetDto planetDto)
        {
            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
            Moons = new Collection<Moon>();
            if (planetDto.Moons != null)
            {
                foreach (MoonDto moonDto in planetDto.Moons)
                {
                    Moons.Add(new Moon(moonDto));
                }
            }
        }

        public Boolean HasMoons()
        {
            return (Moons != null && Moons.Count > 0);
        }
    }
}
