using AutoMapper;
using FligthPlanner.Core.Models;
using FligthPlanner.Models;

namespace FligthPlanner.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Airport, AirportViewModel>()
                .ForMember
                    (d => d.Airport,
                    o => o.MapFrom(s => s.AirportCode));
            CreateMap<AirportViewModel, Airport>()
                .ForMember
                (d => d.AirportCode,
                    o => o.MapFrom(s => s.Airport));

            CreateMap<AddRequestFlight, Flights>();
            CreateMap<Flights, AddResponseFlight>();
        }
    }
}
