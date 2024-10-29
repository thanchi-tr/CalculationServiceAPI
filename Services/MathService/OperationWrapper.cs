namespace CalculationTechTest.Services.MathService
{
    public class OperationWrapper
    {
        public bool IsStart { get; set; }

        // Either Func<double, double> or Func<double, double, double>
        //             1:1 mapping              math function
        public Object Operator { get; set; }
    }
}
