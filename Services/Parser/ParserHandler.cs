using CalculationTechTest.Services.Parser.Interface;

namespace CalculationTechTest.Services.Parser
{
    public abstract class ParserHandler : IParserHandler
    {
        protected IParserHandler? _next = null;

        public abstract Queue<object> ExtractExpressionQueueFromSerialized(string rawExpression);

        public IParserHandler GetNext()
        {
            return _next;
        }

        public bool HasNext()
        {
            return _next != null;
        }

        public IParserHandler SetNext(IParserHandler nextHandler)
        {
            _next = nextHandler;
            return this;
        }


    }
}
