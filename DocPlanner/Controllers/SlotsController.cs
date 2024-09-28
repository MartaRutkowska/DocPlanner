using DocPlanner.Models.Request;
using DocPlanner.Models.Response;
using DocPlanner.Services;
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
        public async Task<IActionResult> GetAsync(string date)
        {
            //data validation could be added but I aint got time

            var result = await slotService.CreateSlotsAsync(date);
            return result != null? Ok(result) : BadRequest();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] Appointment appointment)
        {
            // appointment validation could be added but I aint got time

            var result = await slotService.BookSlot(appointment);
            return result? Ok(result) : BadRequest();
        }

    }
}
