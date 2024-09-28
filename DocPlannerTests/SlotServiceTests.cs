using DocPlanner.Controllers;
using DocPlanner.Helpers;
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
            var date = DateTime.Now.AddMinutes(20);
            var monday = DateHelper.GetLastMonday(date);

            _slotService.Setup(s => s.CreateSlotsAsync(monday.ToString("yyyyMMdd"))).ReturnsAsync(
                new Slots(
                    new Facility(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "name", "address"), []));

            var result = await _slotsController.GetAsync(date);
            var response = result as OkObjectResult;
            Assert.Equal(200, response?.StatusCode);
        }

        [Fact]
        public async Task GetSlots_BadRequest()
        {
            var date = DateTime.Now.AddMinutes(20);
            _slotService.Setup(s => s.CreateSlotsAsync(date.ToString("yyyyMMdd"))).ReturnsAsync(() => null);

            var result = await _slotsController.GetAsync(date);
            var response = result as BadRequestResult;
            Assert.Equal(400, response?.StatusCode);
        }

        [Fact]
        public async Task BookSlot_Ok()
        {
            var appointment = new Appointment(
                new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"),
                DateTime.Now.AddMinutes(20), DateTime.Now.AddMinutes(30), "comment", 
                new Patient("Charles", "Darwin", "charles@evolution.com", "888 888 888"));

            _slotService.Setup(s => s.BookSlot(appointment)).Returns(Task.FromResult(true));

            var result = await _slotsController.PostAsync(appointment);
            var response = result as OkObjectResult;

            Assert.Equal(200, response?.StatusCode);
        }

        [Fact]
        public async Task BookSlot_BadRequest()
        {
            var appointment = new Appointment(
                new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"),
                DateTime.Now.AddMinutes(20), DateTime.Now.AddMinutes(30), "comment",
                new Patient("Charles", "Darwin", "charles@evolution.com", "888 888 888"));

            _slotService.Setup(s => s.BookSlot(appointment)).Returns(Task.FromResult(false));

            var result = await _slotsController.PostAsync(appointment);
            var response = result as BadRequestResult;

            Assert.Equal(400, response?.StatusCode);
        }
    }
}
