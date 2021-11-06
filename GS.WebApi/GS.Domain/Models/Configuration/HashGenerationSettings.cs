namespace GS.Domain.Models.Configuration
{
    public class HashGenerationSettings
    {
        public string Salt { get; set; }

        public int IterationCount { get; set; }

        public int BytesNumber { get; set; }
    }
}
