namespace CalculationTechTest.Services.MathService.Interface
{
    /// <summary>
    /// A service that find all of the register math operation
    /// </summary>
    public interface IMathOperation
    {
        /// <summary>
        /// Will split out the exception if attempt to register a new function that already registerd
        /// </summary>
        /// <param name="MappingExpression"></param>
        public IMathOperation Register(string OpsId, object mappingExpression);

        /// <summary>
        /// Extract the operation that register in the service
        /// </summary>
        /// <param name="OpsId">a well define value </param>
        /// <returns> Func<double,double,double> || Func<double, double> || null </returns>
        public object?  ExtractOperation(string OpsId);
    }
}
