using ChatGptCloneBackend.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatGptCloneBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGptController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ChatGptController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("test")]
        public async Task<IActionResult> test(string data)
        {
            var apiKey = _configuration["ApiKeys:DaVinci"];
            var result = await ChatGptHelper.ConnectApi(data,apiKey);
            return Ok(result);
        }
    }
}
