using AutoMapper;
using DocPlanner.Models;
using DocPlanner.Models.Response;

namespace DocPlanner.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Availability, Slots>().ConvertUsing<AvailabilitySlotsConverter>();
        }
    }
}
