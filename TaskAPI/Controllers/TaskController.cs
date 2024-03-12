using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using TaskAPI.Model;

namespace TaskAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TaskController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var getallTasks = new TaskModel().PrepareListTaskModel();
            return Ok(getallTasks);
        }

        [HttpGet("get-tasks-by-id/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(new TaskModel().PrepareListTaskModel().FirstOrDefault(x => x.Id == id));
        }

        [HttpPut("update-tasks-status")]
        public IActionResult UpdateTaskStatus([FromBody] TaskModel updateModel)
        {
            try
            {
                var task = new TaskModel().PrepareListTaskModel().FirstOrDefault(x => x.Id == updateModel.Id);
                task.Status = updateModel.Status;

                //Webhook tetiklenmeli ve bilgi akışı sağlanmalı.
                TriggerWebhook(task.Id);
                
                return Ok(new { payload = new { data = task, Message = "Güncelleme işlemi başarıyla gerçekleşti" } });
            }
            catch (Exception ex)
            {
                return BadRequest(new { payload = new { Message = "Task'ın statü değeri güncellenemedi." } });
            }
        }

        private async void TriggerWebhook(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();

            // Webhook URL olarak MailAPI uygulamasının local adres bilgisini verebiliriz.
            string webhookUrl = "https://localhost:7176";

            await httpClient.PostAsync($"{webhookUrl}/api/mail/send", new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json"));
        }
    }
}
