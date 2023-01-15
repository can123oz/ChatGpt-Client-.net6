using ChatGptCloneBackend.Helpers;
using Microsoft.AspNetCore.Mvc;
using OpenAI.GPT3.Interfaces;

namespace ChatGptCloneBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGptController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOpenAIService _openAIService;

        public ChatGptController(IConfiguration configuration, IOpenAIService openAIService)
        {
            _configuration = configuration;
            _openAIService = openAIService;
        }

        [HttpGet("ChatGpt")]
        public async Task<IActionResult> ChatGpt(string data)
        {
            var apiKey = "Bearer " + _configuration["ApiKeys:OpenAI"];
            var result = await ChatGptHelper.ConnectApi(data, apiKey);
            if (result.status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("DallE")]
        public async Task<IActionResult> DallE(string data)
        {
            var apiKey = _configuration["ApiKeys:OpenAI"];
            var result = await DallEHelper.Connect(_openAIService, data, apiKey);
            if (result.status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}