using CalculationTechTest.Services.MathService.Interface;

namespace CalculationTechTest.Services.MathService
{
    public class MathOperator : IMathOperation
    {
        public static Func<double, double, double> add = (x, y) => x + y;
        public static Func<double, double, double> minus = (x, y) => x - y;
        public static Func<double, double, double> multiply = (x, y) => x * y;
        public static Func<double, double, double> mod = (x, y) => x % y;
        public static Func<double, double, double> divide = (x, y) => x / y;
        public static Func<double, double, double> power = (x, y) => Math.Pow(x, y);

        /// <summary>
        /// In a math equation: we can alway partitian into 2 half equation base off of 
        /// operator order
        /// </summary>
        /// <param name="firstInput"></param>
        /// <param name="secondInput"></param>
        /// <param name="primitiveOperate"></param>
        /// <returns></returns>
        public double Operate(
            Func<double, double, double> primitiveOperate,
            double firstInput,
            params double[] secondInput
            )
        {
            return Operate(primitiveOperate, primitiveOperate(firstInput, secondInput[0]));
        }

        /// <summary>
        /// Modification made to the same function
        /// </summary>
        /// <param name="input"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public double Operate(double input, Func<double, double> func)
        {
            return func(input);
        }

    }
}
