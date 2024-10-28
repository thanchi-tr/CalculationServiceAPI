namespace CalculationTechTest.Services.Parser
{
    /// <summary>
    /// Chain of responsibility: 
    ///     a concrete handler will take turn try to parse the document.
    /// </summary>
    public interface IParserHandler : IParser
    {
        public IParserHandler SetNext(IParserHandler nextHandler);
        public bool HasNext();
        public IParserHandler GetNext();

    }
}
