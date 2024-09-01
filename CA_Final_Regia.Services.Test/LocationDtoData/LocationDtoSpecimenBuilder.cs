using AutoFixture.Kernel;
using CA_Final_Regia.Services.DTOs;

namespace CA_Final_Regia.Services.Test.LocationDtoData
{
    internal class LocationDtoSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(LocationDto))
            {
                return new LocationDto
                {
                    City = "Jonava",
                    Street = "Basanavičiaus",
                    HouseNr = "35A",
                    ApartmentNr = "18B"
                };
            }
            return new NoSpecimen();
        }
    }
}
