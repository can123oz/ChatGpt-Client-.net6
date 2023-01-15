using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using ChatGptCloneBackend.Models;

namespace ChatGptCloneBackend.Helpers
{
    public static class DallEHelper
    {
        public static async Task<DallEResponse> Connect(IOpenAIService openAIService, string data, string key)
        {
            var imageResult = await openAIService.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = data,
                N = 2,
                Size = StaticValues.ImageStatics.Size.Size256,
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = "TestUser"
            });
            DallEResponse dallE = new();
            dallE.status = imageResult.Successful;
            if (imageResult.Successful)
            {
                dallE.imageUrl = string.Join("\n", imageResult.Results.Select(r => r.Url));
                return dallE;
            }
            dallE.imageUrl = "";
            return dallE;
        }
    }
}
