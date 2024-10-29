using CalculationTechTest.Model.DTO;
using CalculationTechTest.Services.Parser;

namespace CalculationTechTest.Services.MathService.Interface
{
    public interface ICalculateServcie
    {
        public BusinessToPresentationDTO<double> Calculate(ParserHandler parser, string serializedString);
    }
}
