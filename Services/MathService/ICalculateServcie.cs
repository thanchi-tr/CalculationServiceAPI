using CalculationTechTest.Model.DTO;
using CalculationTechTest.Services.Parser;
using static CalculationTechTest.Services.Parser.IParser;

namespace CalculationTechTest.Services.MathService
{
    public interface ICalculateServcie
    {
        public BusinessToPresentationDTO<double> Calculate(ParserHandler parser, string serializedString);
    }
}
