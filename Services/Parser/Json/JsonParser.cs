using CalculationTechTest.Services.MathService;
using CalculationTechTest.Services.Parser;
using CalculationTechTest.Services.Parser.Json.DTO;
using System.Text.Json;
using System.Xml;

namespace CalculationTechTest.Services.Parser.Json
{
    public class JsonParser : ParserHandler
    {
        private object ParseTag(string id)
        {
            switch (id)
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

        public Queue<object> ExtractExpressionQueue(MathDTO maths, Queue<object> expressionQueue)
        {

            foreach (var expression in maths.Maths)
            {
                object MathOps = ParseTag(expression.ID);
                expressionQueue.Enqueue(new OperationWrapper
                {
                    IsStart = true,
                    Operator = MathOps
                });
                foreach (var val in expression.Value)
                {
                    expressionQueue.Enqueue(double.Parse(val));
                }
                expressionQueue.Enqueue(new OperationWrapper
                {
                    IsStart = false,
                    Operator = MathOps
                });
            }
            return expressionQueue;
        }



        public override Queue<object> ExtractExpressionQueueFromSerialized(string rawExpression)
        {
            try
            {
                var maths = JsonSerializer.Deserialize<MathDTO>(rawExpression);

                return ExtractExpressionQueue(maths, new Queue<object>());
            }
            catch (Exception ex)
            {
                if (HasNext())
                    return GetNext().ExtractExpressionQueueFromSerialized(rawExpression);

                Console.WriteLine("The appropriate Parse is not found!!");
                return null;
            }

        }
    }
}
