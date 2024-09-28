using DocPlanner.Helpers;
using DocPlanner.Models.Request;
using DocPlanner.Models.Response;
using DocPlanner.Services;
using DocPlanner.Validators;
using Microsoft.AspNetCore.Mvc;

namespace DocPlanner.Controllers
{
    [ApiController]
    [Route("slots")]
    public class SlotsController(ISlotService slotService) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(DateTime date)
        {
            var dateValid = new DateValidator().Validate(date.Date);
            if (!dateValid.IsValid) return BadRequest("Provide current or future date");

            var monday = DateHelper.GetLastMonday(date);
            var result = await slotService.CreateSlotsAsync(monday.ToString("yyyyMMdd"));
            return result != null? Ok(result) : BadRequest();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] Appointment appointment)
        {
            var appInfoValid = new AppointmentValidator().Validate(appointment);
            if (!appInfoValid.IsValid) return BadRequest("Please provide correct dates needed for an appointment and proper email address");

            var result = await slotService.BookSlot(appointment);
            return result? Ok(result) : BadRequest();
        }

    }
}
