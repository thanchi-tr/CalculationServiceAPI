using CalculationTechTest.Services.MathService.Interface;
using CalculationTechTest.Services.Parser;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace CalculationTechTest.Controllers
{
    [ApiController]
    [Route("Math")]
    public class MathController : ControllerBase
    {
        private readonly ParserHandler _chain;
        private readonly ICalculateServcie _calService;
        public MathController(IServiceProvider serviceProvider, ICalculateServcie calculateServcie)
        {
            _calService = calculateServcie;
            // Discover all handlers that implement IHandler.
            var handlers = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
                .Where(t => typeof(ParserHandler).IsAssignableFrom(t) && !t.IsAbstract)
                .Select(handlerType => (ParserHandler)serviceProvider.GetRequiredService(handlerType))
                .ToList();

            // Chain the handlers together dynamically.
            for (int i = 0; i < handlers.Count - 1; i++)
            {
                handlers[i].SetNext(handlers[i + 1]);
            }

            // Set the first handler as the entry point of the chain.
            _chain = handlers.FirstOrDefault();
        }

        [HttpPost("")]
        public async Task<IActionResult> Calculate([FromBody] string SerializedMathExpression)
        {
            var result = _calService.Calculate(_chain, SerializedMathExpression);
            return
                (result.status) ? Ok(result.result) :
                BadRequest(result.message);
        }
    }
}
