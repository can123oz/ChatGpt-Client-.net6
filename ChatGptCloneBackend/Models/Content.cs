namespace ChatGptCloneBackend.Models
{
    public class RequestBody
    {
        public string model { get; set; }
        public string prompt { get; set; }
        public int temperature { get; set; }
        public int max_tokens { get; set; }
    }

    public class ResponseBody
    {
        public string id { get; set; }
        public double created { get; set; }
        public string model { get; set; }
        public choices[] choices { get; set; }
        public bool status { get; set; }
    }

    public class choices
    {
        public string text { get; set; }
        public string finish_reason { get; set; }
        public int index { get; set; }
    }

    public class DallEResponse 
    {
        public bool status { get; set; }
        public string imageUrl { get; set; }

    }
}
