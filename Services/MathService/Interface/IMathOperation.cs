namespace CalculationTechTest.Services.MathService.Interface
{
    public interface IMathOperation
    {

        public delegate  Double Operate(Double firstInput, Double secondInput,  Func<double, double, double> primitiveOperate);
        

    }
}
