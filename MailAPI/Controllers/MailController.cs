using Microsoft.AspNetCore.Mvc;

namespace MailAPI.Controllers
{
    [ApiController]
    [Route("api/mail")]
    public class MailController : ControllerBase
    {
        public MailController()
        {
        }

        [HttpPost("send")]
        public IActionResult Send([FromBody] int id)
        {
            if (id > 0)
            {
                Console.WriteLine($"{id} numaralý task güncellendi");
                return Ok();
            }
            else
            {
                Console.WriteLine($"Task güncellenirken hata oldu.");
                return BadRequest();
            }
        }
    }
}
