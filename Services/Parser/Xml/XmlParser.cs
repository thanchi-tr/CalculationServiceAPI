using CalculationTechTest.Services.MathService;
using CalculationTechTest.Services.Parser;
using System.Xml;

namespace CalculationTechTest.Services.Parser.Xml
{
    public sealed class XmlParser : ParserHandler
    {
        public XmlParser() { }



        private Func<double, double, double> ParseTag(XmlNode node)
        {
            switch (node?.Attributes?["ID"]?.Value)
            {

                case "Plus":
                    return MathOperator.add;
                case "Multiplication":
                    return MathOperator.multiply;
                case "Minus":
                    return MathOperator.minus;
                case "Mod":
                    return MathOperator.mod;
                case "Divide":
                    return MathOperator.divide;
                case "Power":
                    return MathOperator.power;
                default:
                    return null;
            }
        }
        private Func<double, double> ParseTagSingleVariable(XmlNode node)
        {
            switch (node?.Attributes?["ID"]?.Value)
            {

                case "SquareRoot":
                    return Math.Sqrt;
                case "Absolute":
                    return Math.Abs;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 
        /// traverse the expression tree (XML format)
        /// push to our express queue the corresponse 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="queue"></param>
        /// <returns></returns>
        public Queue<object> ExtractExpressionQueue(XmlNode node, Queue<object> queue)
        {
            if (node.Name == "Value")
            {
                queue.Enqueue(double.Parse(node.InnerText));
                return queue;
            }

            object mathMathOperator = (object)ParseTag(node) ?? ParseTagSingleVariable(node);
            if (mathMathOperator != null)
            {
                queue.Enqueue(
                    new OperationWrapper
                    {
                        IsStart = true,
                        Operator = mathMathOperator
                    }
                );
            }
            foreach (XmlNode child in node.ChildNodes)
            {
                ExtractExpressionQueue(child, queue);// Recursively add values or operations
            }
            if (mathMathOperator != null)
            {
                queue.Enqueue(
                    new OperationWrapper
                    {
                        IsStart = false,
                        Operator = mathMathOperator
                    }
                );
            }
            return queue;
        }

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
        /// <param name="rawExpression"> format the document according to a specific standard (XML,HTML, CSv)</param>
        /// <returns></returns>
        public override Queue<object>? ExtractExpressionQueueFromSerialized(string rawExpression)
        {
            try
            {
                rawExpression = rawExpression.Trim();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(rawExpression);

                // push the 
                return ExtractExpressionQueue(doc.DocumentElement, new Queue<object>());
            }
            catch (Exception e)
            {
                if (HasNext())
                    return GetNext().ExtractExpressionQueueFromSerialized(rawExpression);

                Console.WriteLine("The appropriate Parse is not found!!"); // place for logger
                return null;
            }
        }






    }
}
