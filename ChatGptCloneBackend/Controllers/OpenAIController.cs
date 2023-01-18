using ChatGptCloneBackend.Helpers;
using Microsoft.AspNetCore.Mvc;
using OpenAI.GPT3.Interfaces;

namespace ChatGptCloneBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOpenAIService _openAIService;

        public OpenAIController(IConfiguration configuration, IOpenAIService openAIService)
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

        [HttpGet("DallETypeB64")]
        public async Task<IActionResult> DallETypeB64(string data)
        {
            var result = await DallEHelper.ImageBase64Generator(_openAIService, data);
            if (result.status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("DallETypeUrl")]
        public async Task<IActionResult> DallETypeUrl(string data)
        {
            var result = await DallEHelper.ImageUrlGenerator(_openAIService, data);
            if (result.status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}