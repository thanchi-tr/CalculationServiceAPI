namespace CalculationTechTest.Services.Parser
{
    public interface IParser
    {

        /// <summary>
        /// Return a Expression Queue: If successfully parse the serialized document
        ///  
        /// Possible Value in Queue:
        ///     1) primitive double
        ///     2) OperationWrapper:
        ///         IsStart: boolean
        ///         MathOperation: 
        ///             +) Func<double, double> : map 1 double to another double
        ///             +) Func<double, double, double> :bi-variatedMathFunction
        /// </summary>
        /// <param name="rawExpression"></param>
        /// <returns></returns>
        public Queue<object> ExtractExpressionQueueFromSerialized(string rawExpression);
    }
}
