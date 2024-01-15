using Domain.Helpers;
using Domain.Interfaces;

namespace Domain;

public class SimplePerceptron
{
    private double LearningRate { get; set; }

    private double[] weights { get; set; }
    private readonly Random random;
    
     
    /// <summary>
    /// Constructor.
    /// </summary>
    public SimplePerceptron(double learningRate, int inputSize)
    {
        LearningRate = learningRate;
        weights = new double[inputSize];
        random = new Random();
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = random.NextDouble() * 2 - 1;
        }
        bias = random.NextDouble() * 2 - 1;
        
    }
    
    private double bias { get; set; }

    public void Train(IEnumerable<IInputData> inputDatas, int epochs)
    {
        var totalError = 0D;
        foreach (var epoch in Enumerable.Range(0, epochs))
        {
            var epochError = 0D;
            foreach (var inputData in inputDatas)
            { 
                var trainResult = Train(inputData);
                epochError += Math.Abs(trainResult.LocalError);
            }
            Console.WriteLine($"epoch: {epoch}: epochError = {epochError / inputDatas.Count()}");
            totalError += epochError / inputDatas.Count();
        }
        var meanError = totalError / epochs;
        Console.WriteLine($"meanError = {meanError}");
        
        Console.WriteLine();
        Console.WriteLine("Trains ended. Statistic!");
        Console.WriteLine($"Weight: {string.Join(",", weights)}");
        Console.WriteLine($"bias = {bias}");
        Console.WriteLine();
    }

    private record TrainResult(double LocalError);
    
    private TrainResult Train(IInputData inputData)
    {
        var target = inputData.GetAnswer();
        var inputs = inputData.GetSensors();
        var prediction = Predict(inputs);
        var localError = target - prediction;
        
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] += LearningRate * inputs[i].Value * localError * (prediction * (1 - prediction));
        }

        bias += localError * LearningRate * prediction * (1 - prediction);
        biasHistory.Add(bias);
        
        return new TrainResult(localError);
    }
    
    public static List<double> biasHistory = new List<double>();

    

    public double Predict(Sensor[] input)
    {
        var weightedSum = GetWeightSum(input);
        weightedSum += bias;

        return ActivatedFunction(weightedSum);
    }

    private double ActivatedFunction(double weightedSum)
    {
        return MathHelper.Sigmoid(weightedSum);
    }

    public static double meanError2 = 0;
    public static int iter = 0; 

    public int PredictBinary(IInputData inputs)
    {
        var prediction = Predict(inputs.GetSensors());
        return prediction > 0.5 ? 1 : -1;
    }

    public double GetWeightSum(Sensor[] inputs)
    {
        double sum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += inputs[i].Value * weights[i];
        }

        return sum;
    }
}