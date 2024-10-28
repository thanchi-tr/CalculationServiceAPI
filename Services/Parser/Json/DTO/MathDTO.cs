namespace CalculationTechTest.Services.Parser.Json.DTO
{
    public class MathDTO
    {
        /// <summary>
        /// I Assume there an error with the Sample JSON format,
        ///  the Maths is of type array
        /// </summary>
        public OperationDTO[] Maths { get; set; }
    }
}
