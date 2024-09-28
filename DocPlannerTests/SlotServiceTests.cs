using DocPlanner.Controllers;
using DocPlanner.Models;
using DocPlanner.Models.Request;
using DocPlanner.Models.Response;
using DocPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DocPlannerTests
{
    public class SlotServiceTests
    {
        public readonly SlotsController _slotsController;
        private readonly Mock<ISlotService> _slotService;

        public SlotServiceTests()
        {
            _slotService = new Mock<ISlotService>();
            _slotsController = new SlotsController(_slotService.Object);
        }

        [Fact]
        public async Task GetSlots_Ok()
        {
            var date = "20240930";
            _slotService.Setup(s => s.CreateSlotsAsync(date)).ReturnsAsync(new Slots(new Facility(new Guid(), "name", "address"), []));

            var result = await _slotsController.GetAsync(date);
            var response = result as OkObjectResult;
            Assert.Equal(200, response?.StatusCode);
        }

        [Fact]
        public async Task GetSlots_BadRequest()
        {
            var date = "20240929";
            var result = await _slotsController.GetAsync(date);
            var response = result as BadRequestResult;
            Assert.Equal(400, response?.StatusCode);
        }

        [Fact]
        public async Task BookSlot_Ok()
        {
            var appointment = new Appointment(new Guid(), DateTime.Now, DateTime.Now.AddMinutes(20), "comment", 
                new Patient("Charles", "Darwin", "charles@evolution.com", "888 888 888"));

            _slotService.Setup(s => s.BookSlot(appointment)).Returns(Task.FromResult(true));

            var result = await _slotsController.PostAsync(appointment);
            var response = result as OkObjectResult;

            Assert.Equal(200, response?.StatusCode);
        }

        [Fact]
        public async Task BookSlot_BadRequest()
        {
            var appointment = new Appointment(new Guid(), DateTime.Now, DateTime.Now.AddMinutes(20), "comment",
                new Patient("Charles", "Darwin", "charles@evolution.com", "888 888 888"));

            _slotService.Setup(s => s.BookSlot(appointment)).Returns(Task.FromResult(false));

            var result = await _slotsController.PostAsync(appointment);
            var response = result as BadRequestResult;

            Assert.Equal(400, response?.StatusCode);
        }
    }
}
