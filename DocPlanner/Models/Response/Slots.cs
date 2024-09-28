namespace DocPlanner.Models.Response
{
    public record Slots(
        Facility Facility,
        List<DaySlots> DaySlots);
}
