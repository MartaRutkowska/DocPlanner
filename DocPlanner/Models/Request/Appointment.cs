using System.ComponentModel.DataAnnotations;

namespace DocPlanner.Models.Request
{
    public record Appointment(
        Guid FacilityId,
        DateTime Start,
        DateTime End,
        string? Comments,
        Patient Patient);
}
