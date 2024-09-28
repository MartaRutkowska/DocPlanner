using System.ComponentModel.DataAnnotations;

namespace DocPlanner.Models
{
    public record Availability(
        Facility Facility,
        int SlotDurationMinutes,
        DailyAvailability? Monday,
        DailyAvailability? Tuesday,
        DailyAvailability? Wednesday,
        DailyAvailability? Thursday,
        DailyAvailability? Friday,
        DailyAvailability? Saturday,
        DailyAvailability? Sunday);
}
