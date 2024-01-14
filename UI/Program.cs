// See https://aka.ms/new-console-template for more information

using Domain;
using UI.DataSource;
using UI.Dtos;
using UI.Helpers;

const string pathToVoiceFile = "voice.csv";

var dataSource = new DataSource();
var recordsStream = dataSource.GetRecordsFromCsv(pathToVoiceFile);
var selections = await SelectionHelper.GetSelections<VoiceDto>(recordsStream, 0.8f, 0.2f);

var inputSize = 20;
var learningRate = 0.1d;
var epochs = 100;
var simplePerceptron = new SimplePerceptron(learningRate , inputSize);

simplePerceptron.Train(selections.TrainSelection, epochs);

var countRightAnswers = 0;
var errors = 0;
foreach (var voiceDto in selections.TestSelection)
{
    var guess = simplePerceptron.Guess(voiceDto);
    if (guess == voiceDto.GetAnswer())
    {
        ++countRightAnswers;
        continue;
    }

    ++errors;
}

Console.WriteLine($"Right answers: {countRightAnswers}, errors: {errors}");
