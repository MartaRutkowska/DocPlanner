namespace DocPlanner.Models.Response
{
    public record DaySlots(
        string Day,
        List<SingleSlot> Slots);
}
