 using CalculationTechTest.Services.MathService.Interface;

namespace CalculationTechTest.Services.MathService
{
    public class MathServiceCenter : IMathOperation
    {
        /// <summary>
        /// Primitive math arithmetic
        /// </summary>
        public static Func<double, double, double> add = (x, y) => x + y;
        public static Func<double, double, double> minus = (x, y) => x - y;
        public static Func<double, double, double> multiply = (x, y) => x * y;
        public static Func<double, double, double> mod = (x, y) => x % y;
        public static Func<double, double, double> divide = (x, y) => x / y;
        public static Func<double, double, double> power = (x, y) => Math.Pow(x, y);

        private readonly Dictionary<string, Object> _registerMathService = new Dictionary<string, Object>();


        public MathServiceCenter()
        {
            this.Register("Plus", add)
                .Register("Multiplication", multiply)
                .Register("Minus", minus)
                .Register("Mod", mod)
                .Register("Divide", divide)
                .Register("Power", power);
        }


        /// <summary>
        /// A call to register new math service
        /// </summary>
        /// <param name="OpsId"></param>
        /// <param name="mappingExpression"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException">
        ///     
        /// </exception>
        public IMathOperation Register(string OpsId, object mappingExpression)
        {
            if(OpsId == null) throw new ArgumentNullException((OpsId));
            
            if(mappingExpression == null) throw new ArgumentNullException($"Attempt to register an Invalid Expression for ID-{OpsId}-");

            if (mappingExpression.GetType() != typeof(Func<double, double>) ||
                mappingExpression.GetType() != typeof(Func<double, double, double>)
                )
                throw new ArgumentException($"Invalid definition of Register Expresion:: " +
                    $"Type of Expression receive {mappingExpression.GetType()}. " +
                    $"Expected: {typeof(Func<double, double, double>)} or {typeof(Func<double, double>)}.");

            if (_registerMathService[OpsId] != null) throw new InvalidOperationException($"Attempt to Register a new service with Existed ID-{OpsId}-");

            _registerMathService[OpsId] = mappingExpression;
            return this;
        }

        public object? ExtractOperation(string OpsId)
        {
            var Ops =  _registerMathService[OpsId];

            if (Ops == null) throw new InvalidOperationException($"Register Math Operator under ID-{OpsId} is not found!");
            return Ops;
        }

        
    }
}
