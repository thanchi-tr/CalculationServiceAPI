using CalculationTechTest.Services.MathService;
using CalculationTechTest.Services.MathService.Interface;
using CalculationTechTest.Services.Parser;
using CalculationTechTest.Utils;
using System.Xml;

namespace CalculationTechTest.Services.Parser.Xml
{
    public sealed class XmlParser : ParserHandler
    {
        private readonly IMathOperation _mathOperatorCenter;

        public XmlParser(IMathOperation mathOperatorCenter)
        {
            _mathOperatorCenter = mathOperatorCenter;
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
            object mathMathOperator = null;
            if (node.IsExist() && node.Attributes["ID"].IsExist())
            {
                try
                {
                    mathMathOperator = _mathOperatorCenter.ExtractOperation(node?.Attributes?["ID"]?.Value);

                    queue.Enqueue(
                            new OperationWrapper
                            {
                                IsStart = true,
                                Operator = mathMathOperator
                            }
                    );
                    
                    
                    queue.Enqueue(
                            new OperationWrapper
                            {
                                IsStart = false,
                                Operator = mathMathOperator
                            }
                    );


                }
                catch(InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            else
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    ExtractExpressionQueue(child, queue);// Recursively add values or operations
                }
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
