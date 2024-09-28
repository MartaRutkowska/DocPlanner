using DocPlanner.Models.Request;
using DocPlanner.Models.Response;

namespace DocPlanner.Services
{
    public interface ISlotService
    {
        public Task<Slots?> CreateSlotsAsync(string date);
        public Task<bool> BookSlot(Appointment appointment);
    }
}
