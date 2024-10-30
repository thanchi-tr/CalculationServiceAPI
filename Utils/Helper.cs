namespace CalculationTechTest.Utils
{
    public static class Helper
    {
        public static bool IsExist(this object self)
        {
            return self != null;
        }


        public static double toDouble(this object? self)
        {
            try
            {
                return (double)self;
            }
            catch (Exception ex)
            {
                return double.NaN;
            }
        }
        public static bool IsDoubleNumber(this Object? queueNode)
        {
            return queueNode!= null && queueNode.GetType() == typeof(double);
        }

        public static bool IsFuncEquilvalent(this object delegateInstance, object target )
        {
            if (delegateInstance == null || target == null)
                return false;
            if (delegateInstance.GetType() != target.GetType()) return false;


            if (delegateInstance.GetType() == typeof(Func<double, double>))
            {
                Func<double, double> d1, d2;
                d1 = (Func<double, double>)delegateInstance;
                d2 = (Func<double, double>)target;
                return d1.Method == d2.Method && Equals(d1.Target, d2.Target);
            }
            else
            {
                Func<double, double, double> d1, d2;
                d1 = (Func<double, double, double>)delegateInstance;
                d2 = (Func<double, double, double>)target;
                return d1.Method == d2.Method && Equals(d1.Target, d2.Target);
            }
        }
    }
}
