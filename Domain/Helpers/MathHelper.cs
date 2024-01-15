namespace Domain.Helpers;

/// <summary>
/// Math helper.
/// </summary>
public class MathHelper
{
    /// <summary>
    /// Sigmoid.
    /// </summary>
    public static double Sigmoid(double x)
    {
        return 1d / (1 + Math.Exp(-x));
    }
    
    /// <summary>
    /// SigmoidDerivative
    /// </summary>
    public static double SigmoidDerivative(double x)
    {
        double sigmoid = Sigmoid(x);
        return sigmoid * (1 - sigmoid);
    }
    
    public static double Tanh(double x)
    {
        return Math.Tanh(x);
    }
}