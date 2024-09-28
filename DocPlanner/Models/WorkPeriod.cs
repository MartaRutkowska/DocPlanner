namespace DocPlanner.Models
{
    public record WorkPeriod(
        int StartHour,
        int EndHour,
        int LunchStartHour,
        int LunchEndHour);
}
