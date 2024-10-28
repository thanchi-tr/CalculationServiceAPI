using System.Text.Json.Serialization;

namespace CalculationTechTest.Services.Parser.Json.DTO
{
    public class OperationDTO
    {
        [JsonPropertyName("@ID")]
        public string? ID { get; set; }
        public string[] Value { get; set; }
    }
}
