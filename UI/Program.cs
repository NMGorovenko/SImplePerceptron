using DataAccess.DataSource;
using DataAccess.Dtos;
using Domain;
using UI;
using UI.Helpers;

const string pathToVoiceFile = "voice.csv";

var dataSource = new DataSource();
var recordsStream = dataSource.GetRecordsFromCsv(pathToVoiceFile);
var selections = await SelectionHelper.GetSelections<VoiceDto>(recordsStream, 0.8f, 0.2f);

var inputSize = 20;
var learningRate = 0.0001d;
var epochs = 2000;
var network = PerceptronFactory.GetMultiLayerPerceptron(learningRate, inputSize);

var values = selections.TrainSelection
    .Select(voice => voice.GetProperties().ToArray())
    .ToArray();
var targets = selections.TrainSelection.Select(sensor => (double)sensor.GetAnswer()).ToArray();

network.Train(values: values,
    targets: targets,
    epochs);

var testSelection = selections.TestSelection;

var countRightAnswers = 0;
var errors = 0;
foreach (var voiceDto in testSelection)
{
    var input = voiceDto.GetProperties().ToArray();
    var rightAnswer = voiceDto.GetAnswer();
    var prediction = network.PredictBinary(input);
    
    if (prediction == rightAnswer)
    {
        ++countRightAnswers;
        continue;
    }
    
    // Console.WriteLine($"binaryGuess = {prediction} should be = {rightAnswer}");
    ++errors;
}
var accuracy = (double)countRightAnswers / testSelection.Count * 100;

Console.WriteLine($"Right answers: {countRightAnswers}, errors: {errors}, accuracy = {accuracy}");




    