using AutoMapper;
using DocPlanner.Models;
using DocPlanner.Models.Response;
using System.Globalization;

namespace DocPlanner.Mappers
{
    public class AvailabilitySlotsConverter : ITypeConverter<Availability, Slots>
    {
        public Slots Convert(Availability availability, Slots slots, ResolutionContext context)
        {
            var result = new Slots(availability.Facility, []);

            if (!context.Items.TryGetValue("date", out object? value)) return result;

            if (!DateTime.TryParseExact((string)value, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var baseDate))
                return result;

            ConvertWeek(availability, result, baseDate);

            return result;
        }

        public static DaySlots CreateDaySlots(WorkPeriod? workPeriod, DayOfWeek day, int SlotDuration, DateTime baseDate)
        {
            var dayName = Enum.GetName(typeof(DayOfWeek), day);
            var result = new DaySlots(dayName ?? string.Empty, []);

            if (workPeriod is null) return result;

            var startTime = baseDate.AddHours(workPeriod.StartHour);
            var startBreakTime = baseDate.AddHours(workPeriod.LunchStartHour);
            var endBreakTime = baseDate.AddHours(workPeriod.LunchEndHour);
            var endTime = baseDate.AddHours(workPeriod.EndHour);

            while (startTime < endTime)
            {
                var end = startTime.AddMinutes(SlotDuration);
                if (end <= startBreakTime || startTime >= endBreakTime)
                {
                    result.Slots.Add(new SingleSlot(startTime, end));
                }
                startTime = end;
            }

            return result;
        }

        private static void ConvertWeek(Availability availability, Slots result, DateTime baseDate)
        {
            result.DaySlots.Add(CreateDaySlots(availability.Monday?.WorkPeriod, DayOfWeek.Monday, availability.SlotDurationMinutes, baseDate));
            result.DaySlots.Add(CreateDaySlots(availability.Tuesday?.WorkPeriod, DayOfWeek.Tuesday, availability.SlotDurationMinutes, baseDate));
            result.DaySlots.Add(CreateDaySlots(availability.Wednesday?.WorkPeriod, DayOfWeek.Wednesday, availability.SlotDurationMinutes, baseDate));
            result.DaySlots.Add(CreateDaySlots(availability.Thursday?.WorkPeriod, DayOfWeek.Thursday, availability.SlotDurationMinutes, baseDate));
            result.DaySlots.Add(CreateDaySlots(availability.Friday?.WorkPeriod, DayOfWeek.Friday, availability.SlotDurationMinutes, baseDate));
            result.DaySlots.Add(CreateDaySlots(availability.Saturday?.WorkPeriod, DayOfWeek.Saturday, availability.SlotDurationMinutes, baseDate));
            result.DaySlots.Add(CreateDaySlots(availability.Sunday?.WorkPeriod, DayOfWeek.Sunday, availability.SlotDurationMinutes, baseDate));
        }
    }
}
