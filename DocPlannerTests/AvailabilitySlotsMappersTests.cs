using AutoMapper;
using DocPlanner.Mappers;
using DocPlanner.Models;
using DocPlanner.Models.Response;

namespace DocPlannerTests
{
    public class AvailabilitySlotsMappersTests
    {
        private readonly IMapper _mapper;

        public AvailabilitySlotsMappersTests()
        {
            var conf = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = conf.CreateMapper();
        }

        [Fact]
        public void Facility_IsValid()
        {
            var facility = new Facility(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "hospital", "hospital street");

            var av = new Availability(
                facility,
                10,
                null, null, null, null, null, null, null);

            var result = _mapper.Map<Slots>(av, opts => opts.Items["date"] = "20240909");

            Assert.Equivalent(result.Facility, av.Facility);
        }

        [Fact]
        public void DaySlots_AreValid()
        {
            var facility = new Facility(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "hospital", "hospital street");

            var av = new Availability(
                facility,
                60,
                new DailyAvailability(new WorkPeriod(10,13,11,12)), null, null, null, null, null, null);

            var slot1 = new SingleSlot(DateTime.Parse("2024-09-09 10:00:00"), DateTime.Parse("2024-09-09 11:00:00"));
            var slot2 = new SingleSlot(DateTime.Parse("2024-09-09 12:00:00"), DateTime.Parse("2024-09-09 13:00:00"));

            var expectedSlots = new DaySlots("Monday", [slot1, slot2]);

            var result = _mapper.Map<Slots>(av, opts => opts.Items["date"] = "20240909");

            Assert.Equivalent(expectedSlots, result.DaySlots[0]);
        }

        [Fact]
        public void DaySlots_CountValid()
        {
            var facility = new Facility(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "hospital", "hospital street");

            var av = new Availability(
                facility,
                55,
                new DailyAvailability(new WorkPeriod(10, 18, 12, 13)), null, null, null, null, null, null);

            var result = _mapper.Map<Slots>(av, opts => opts.Items["date"] = "20240909");

            Assert.Equal(7, result.DaySlots.First(x => x.Day == "Monday").Slots.Count);
        }
    }
}
