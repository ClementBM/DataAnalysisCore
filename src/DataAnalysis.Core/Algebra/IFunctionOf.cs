namespace DataAnalysis.Core.Algebra
{
    public interface IFunctionOf
    {
        double[] FunctionOf { get; set; }
    }

    public interface IFunctionSpeedOf
    {
        KilometerPair[] KilometerPairs { get; set; }
    }
}
