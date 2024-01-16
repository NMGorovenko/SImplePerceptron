using Domain;
using Domain.Interfaces;

namespace UI;

public static class PerceptronFactory
{
    public static IPerceptron GetSimplePerceptron(double learningRate, int inputSize)
    {
        var network = new SimplePerceptron(learningRate, inputSize);

        return network;
    }

    public static IPerceptron GetMultiLayerPerceptron(double learningRate, int inputSize)
    {
        var network = new MultiLayerPerceptron(inputSize: inputSize,
            hiddenSize: 10,
            outputSize: 1,
            learningRate,
            momentum: 0.95);

        return network;
    }
}