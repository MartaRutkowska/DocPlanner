using AutoMapper;
using DocPlanner.Models.Request;
using DocPlanner.Models.Response;

namespace DocPlanner.Services
{
    public class SlotService(ExternalService externalService, IMapper mapper) : ISlotService
    {
        public async Task<Slots?> CreateSlotsAsync(string date)
        {
            var availability = await externalService.GetAvailabilityAsync(date);
            if (availability == null) return null;

            return mapper.Map<Slots>(availability, opts => opts.Items["date"] = date);
        }

        public async Task<bool> BookSlot(Appointment appointment)
        {
            return await externalService.PostAppointment(appointment);
        }
    }
}
