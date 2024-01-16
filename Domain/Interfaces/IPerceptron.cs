namespace Domain.Interfaces;

public interface IPerceptron
{
    void Train(double[][] values, double[] targets, int numEpochs);
    int PredictBinary(double[] inputs);
}