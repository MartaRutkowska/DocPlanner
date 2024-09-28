using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DocPlanner.Models.Request
{
    public record Patient(
        string Name,
        string SecondName,
        string Email,
        string Phone);
}
