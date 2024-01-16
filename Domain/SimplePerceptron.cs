using Domain.Helpers;
using Domain.Interfaces;

namespace Domain;

public class SimplePerceptron : IPerceptron
{
    private static readonly Random Random = new Random();
    
    public int InputSize { get; set; }
    public double LearningRate { get; set; }
    public double[] Weights { get; set; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public SimplePerceptron(double learningRate, int inputSize)
    {
        LearningRate = learningRate;
        InputSize = inputSize;
        Weights = new double[inputSize];
        
        for (int i = 0; i < Weights.Length; i++)
        {
            Weights[i] = GetRandomDouble();
        }
        bias = GetRandomDouble();
    }
    
    private double bias { get; set; }

    public void Train(double[][] values, double[] targets, int numEpochs)
    {
        foreach (var epoch in Enumerable.Range(0, numEpochs))
        {
            for (var i = 0; i < values.Length; ++i)
            {
                Train(values[i], targets[i]);
            }
        }
    }
    
    private void Train(double[] inputs, double target)
    {
        var prediction = ForwardPropagate(inputs);
        BackPropagate(inputs, target, prediction);
    }

    private double ForwardPropagate(double[] inputs)
    {
        var weightedSum = GetWeightSum(inputs) + bias;

        return ActivatedFunction(weightedSum);
    }

    private void BackPropagate(double[] inputs, double target, double prediction)
    {
        var loss = target - prediction;
        
        for (int i = 0; i < Weights.Length; i++)
        {
            Weights[i] += LearningRate * inputs[i] * loss * (prediction * (1 - prediction));
        }

        bias += loss * LearningRate * prediction * (1 - prediction);
    }

    private double ActivatedFunction(double weightedSum)
    {
        return MathHelper.Sigmoid(weightedSum);
    }

    public int PredictBinary(double[] inputs)
    {
        var prediction = ForwardPropagate(inputs);
        return prediction > 0.15 ? 1 : -1;
    }

    public double GetWeightSum(double[] inputs)
    {
        double sum = 0;
        for (int i = 0; i < Weights.Length; i++)
        {
            sum += inputs[i] * Weights[i];
        }

        return sum;
    }
    
    public static double GetRandomDouble()
    {
        return 2 * Random.NextDouble() - 1;
    }

}