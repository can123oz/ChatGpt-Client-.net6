using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using ChatGptCloneBackend.Models;
using System.Buffers.Text;

namespace ChatGptCloneBackend.Helpers
{
    public static class DallEHelper
    {
        public static async Task<DallEResponseUrl> ImageUrlGenerator(IOpenAIService openAIService, string data)
        {
            var imageResult = await openAIService.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = data,
                N = 2,
                Size = StaticValues.ImageStatics.Size.Size512,
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = "TestUser"
            });
            DallEResponseUrl dallE = new();
            dallE.status = imageResult.Successful;
            if (imageResult.Successful)
            {
                dallE.imageUrl = string.Join("\n", imageResult.Results.Select(r => r.Url));
                return dallE;
            }
            return dallE;
        }

        public static async Task<DallEResponseBase64> ImageBase64Generator(IOpenAIService openAIService, string data)
        {
            var imageResult = await openAIService.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = data,
                N = 1,
                Size = StaticValues.ImageStatics.Size.Size512,
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Base64,
                User = "TestUser"
            });
            DallEResponseBase64 dallE = new()
            {
                status = imageResult.Successful,
                CreatedAt = imageResult.CreatedAt,
            };
            if (imageResult.Successful)
            {
                dallE.Results = string.Join("\n", imageResult.Results.Select(r => r.B64));
                return dallE;
            }
            dallE.Results = "error";
            return dallE;
        }


    }
}
