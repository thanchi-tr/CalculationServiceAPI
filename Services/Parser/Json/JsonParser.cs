using CalculationTechTest.Services.MathService;
using CalculationTechTest.Services.MathService.Interface;
using CalculationTechTest.Services.Parser.Json.DTO;
using System.Text.Json;

namespace CalculationTechTest.Services.Parser.Json
{
    public class JsonParser : ParserHandler
    {
        private readonly IMathOperation _mathOperatorCenter;

        public JsonParser(IMathOperation mathOperatorCenter)
        {
            _mathOperatorCenter = mathOperatorCenter;
        }

        

        public Queue<object> ExtractExpressionQueue(MathDTO maths, Queue<object> expressionQueue)
        {

            foreach (var expression in maths.Maths)
            {
                try
                {
                    object MathOps = _mathOperatorCenter.ExtractOperation(expression.ID);
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
                /// Will be throw if the for some reason we cant find the mapped math operator for the id
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

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
