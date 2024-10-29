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

    }
}
