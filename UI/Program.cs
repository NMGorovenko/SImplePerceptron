using Domain;
using UI.DataSource;
using UI.Dtos;
using UI.Helpers;

const string pathToVoiceFile = "voice.csv";

var dataSource = new DataSource();
var recordsStream = dataSource.GetRecordsFromCsv(pathToVoiceFile);
var selections = await SelectionHelper.GetSelections<VoiceDto>(recordsStream, 0.8f, 0.2f);

var inputSize = 20;
var learningRate = 0.01d;
var epochs = 50;
var simplePerceptron = new SimplePerceptron(learningRate, inputSize);

// simplePerceptron.Train(selections.TrainSelection.Concat(selections.TestSelection), epochs);
//
// var countRightAnswers = 0;
// var errors = 0;
// var testSelection = selections.TestSelection;
// foreach (var voiceDto in testSelection)
// {
//     var guessBinary = simplePerceptron.PredictBinary(voiceDto);
//     var guess = simplePerceptron.Predict(voiceDto.GetSensors());
//     var answer = voiceDto.GetAnswer();
//     Console.WriteLine($"guess = {guess} guessBinary = {guessBinary} should be = {answer}");
//     if (guessBinary == answer)
//     {
//         ++countRightAnswers;
//         continue;
//     }
//     ++errors;
// }
// var accuracy = (double)countRightAnswers / testSelection.Count * 100;
//
// var re = SimplePerceptron.meanError2 / SimplePerceptron.iter;
// Console.WriteLine($"Right answers: {countRightAnswers}, errors: {errors}, accuracy = {accuracy}");


var network = new MultiLayerPerceptron(inputSize: 20,
    hiddenSize: 10,
    outputSize: 1,
    learnRate: 0.0001,
    momentum: 0.95);

var values = selections.TrainSelection
    .Select(x => x.GetSensors().Select(sensor => sensor.Value).ToArray())
    .ToArray();
var targets = selections.TrainSelection.Select(sensor => new double[] {sensor.GetAnswer()}).ToArray();
network.Train(values: values,
    targets: targets,
    1000);

var testSelection = selections.TestSelection;

var countRightAnswers = 0;
var errors = 0;
foreach (var voiceDto in testSelection)
{
    var input = voiceDto.GetSensors().Select(x => x.Value).ToArray();
    var rightAnswer = voiceDto.GetAnswer();
    var compute = network.Compute(input)[0];
    var networkAnswer = network.GetBinaryResult(compute);
    
    if (Math.Abs(networkAnswer - rightAnswer) < 0.05)
    {
        ++countRightAnswers;
        continue;
    }
    
    Console.WriteLine($"guess = {compute}, binaryGuess = {networkAnswer} should be = {rightAnswer}");
    ++errors;
}
var accuracy = (double)countRightAnswers / testSelection.Count * 100;

var re = SimplePerceptron.meanError2 / SimplePerceptron.iter;
Console.WriteLine($"Right answers: {countRightAnswers}, errors: {errors}, accuracy = {accuracy}");




    