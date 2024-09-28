namespace DocPlanner.Helpers
{
    public static class DateHelper
    {
        public static DateTime GetLastMonday(DateTime date)
        {
            return date.AddDays(-(int)date.DayOfWeek + 1);
        }
    }
}
