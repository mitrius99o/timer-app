using Microsoft.AspNetCore.Mvc;
using TimerApp.Models;
using TimerApp.Services;

namespace TimerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimerController : ControllerBase
    {
        private readonly TimerService _timerService;

        public TimerController(TimerService timerService)
        {
            _timerService = timerService;
        }

        [HttpGet]
        public ActionResult<List<TimerResponse>> GetAllTimers()
        {
            return Ok(_timerService.GetAllTimers());
        }

        [HttpGet("{id}")]
        public ActionResult<TimerResponse> GetTimer(int id)
        {
            var timer = _timerService.GetTimer(id);
            if (timer == null)
                return NotFound();

            return Ok(timer);
        }

        [HttpPost]
        public ActionResult<TimerResponse> CreateTimer([FromBody] TimerRequest request)
        {
            if (request.Hours < 0 || request.Minutes < 0 || request.Seconds < 0)
                return BadRequest("Время не может быть отрицательным");

            if (request.Hours == 0 && request.Minutes == 0 && request.Seconds == 0)
                return BadRequest("Время должно быть больше нуля");

            var timer = _timerService.CreateTimer(request);
            return CreatedAtAction(nameof(GetTimer), new { id = timer.Id }, timer);
        }

        [HttpPost("{id}/start")]
        public ActionResult<TimerResponse> StartTimer(int id)
        {
            var timer = _timerService.StartTimer(id);
            if (timer == null)
                return NotFound();

            return Ok(timer);
        }

        [HttpPost("{id}/stop")]
        public ActionResult<TimerResponse> StopTimer(int id)
        {
            var timer = _timerService.StopTimer(id);
            if (timer == null)
                return NotFound();

            return Ok(timer);
        }

        [HttpPost("{id}/reset")]
        public ActionResult<TimerResponse> ResetTimer(int id)
        {
            var timer = _timerService.ResetTimer(id);
            if (timer == null)
                return NotFound();

            return Ok(timer);
        }

        [HttpPut("{id}")]
        public ActionResult<TimerResponse> UpdateTimer(int id, [FromBody] TimerRequest request)
        {
            if (request.Hours < 0 || request.Minutes < 0 || request.Seconds < 0)
                return BadRequest("Время не может быть отрицательным");

            if (request.Hours == 0 && request.Minutes == 0 && request.Seconds == 0)
                return BadRequest("Время должно быть больше нуля");

            var timer = _timerService.UpdateTimer(id, request);
            if (timer == null)
                return NotFound();

            return Ok(timer);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTimer(int id)
        {
            var deleted = _timerService.DeleteTimer(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
} 