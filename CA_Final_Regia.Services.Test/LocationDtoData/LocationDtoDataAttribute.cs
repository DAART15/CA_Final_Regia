using AutoFixture;
using AutoFixture.Xunit2;

namespace CA_Final_Regia.Services.Test.LocationDtoData
{
    internal class LocationDtoDataAttribute : AutoDataAttribute
    {
        public LocationDtoDataAttribute() : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new LocationDtoSpecimenBuilder());
            return fixture;
        })
        {
        }
    }
}
