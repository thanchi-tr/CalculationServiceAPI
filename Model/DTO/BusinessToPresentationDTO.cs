
using Swashbuckle.AspNetCore.Annotations;

namespace CalculationTechTest.Model.DTO
{
    public class BusinessToPresentationDTO<T>
    {
            [SwaggerSchema(Description = "Indication the completeness of service")]
            public bool status { get; set; }

            [SwaggerSchema(Description = "Incase of failure, error message will be included in here")]
            public string? message { get; set; }

            [SwaggerSchema(Description = "Case success. return the result of the service")]
            public T[]? result { get; set; }
    }
}
