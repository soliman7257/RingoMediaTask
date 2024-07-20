using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Data;
using RingoMediaTask.Services;

namespace RingoMediaTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemindersController : ControllerBase
    {
        private readonly ReminderService _reminderServices;

        public RemindersController(ReminderService departmentService)
        {
            _reminderServices = departmentService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateReminder([FromBody] Reminder reminder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _reminderServices.CreateReminderAsync(reminder);
            return Ok();
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckReminders()
        {
            await _reminderServices.CheckRemindersAsync();
            return Ok();
        }



    }
}
