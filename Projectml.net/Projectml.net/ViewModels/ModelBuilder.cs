using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms.Text;
using Projectml.net.Models;

namespace Projectml.net.ViewModels
{
    public class ModelBuilder
    {
            private static string _appPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            private static string _trainDataPath => Path.Combine(_appPath, "..", "..", "..", "Data", "IMDBDATASETMEDIUM.txt");
            private static string _testDataPath => Path.Combine(_appPath, "..", "..", "..", "Data", "IMDBDATASETSMALL.txt");

        static readonly string _modelPath = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "..", "..", "Data", "model.zip");

        private static MLContext _mlContext;
            private static PredictionEngine<MovieData, MovieDataPrediction> _predEngine;
            private static ITransformer _trainedModel;
            static IDataView _trainingDataView;

            static void Stuff()
            {
                _mlContext = new MLContext(seed: 0);

                _trainingDataView = _mlContext.Data.LoadFromTextFile<MovieData>(_trainDataPath, hasHeader: true);

                var pipeline = ProcessData();

                var trainingPipeline = BuildAndTrainModel(_trainingDataView, pipeline);

                ITransformer model = Evaluate(_trainingDataView.Schema);

                PredictIssue(model);
            }
            public static IEstimator<ITransformer> ProcessData()
            {
                var pipeline = _mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "genre", outputColumnName: "Label")
                    .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Title", outputColumnName: "TitleFeaturized"))
                    .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Description", outputColumnName: "DescriptionFeaturized"))
                    .Append(_mlContext.Transforms.Concatenate("Features", "TitleFeaturized", "DescriptionFeaturized"))
                    .AppendCacheCheckpoint(_mlContext);

                return pipeline;
            }

            public static IEstimator<ITransformer> BuildAndTrainModel(IDataView trainingDataView, IEstimator<ITransformer> pipeline)
            {
                var trainingPipeline = pipeline.Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                    .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

                _trainedModel = trainingPipeline.Fit(trainingDataView);

                _predEngine = _mlContext.Model.CreatePredictionEngine<MovieData, MovieDataPrediction>(_trainedModel);

                MovieData data = new MovieData()
                {
                    Title = "WebSockets communication is slow in my machine",
                    Description = "The WebSockets communication used under the covers by SignalR looks like is going slow in my development machine.."
                };

                var prediction = _predEngine.Predict(data);

                Console.WriteLine($"=============== Single Prediction just-trained-model - Result: {prediction.Prediction} ===============");

                return trainingPipeline;

            }

            public static ITransformer Evaluate(DataViewSchema trainingDataViewSchema)
            {
                var testDataView = _mlContext.Data.LoadFromTextFile<MovieData>(_testDataPath, hasHeader: true);

                var testMetrics = _mlContext.MulticlassClassification.Evaluate(_trainedModel.Transform(testDataView));

                Console.WriteLine($"*************************************************************************************************************");
                Console.WriteLine($"*       Metrics for Multi-class Classification model - Test Data     ");
                Console.WriteLine($"*------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"*       MicroAccuracy:    {testMetrics.MicroAccuracy:0.###}");
                Console.WriteLine($"*       MacroAccuracy:    {testMetrics.MacroAccuracy:0.###}");
                Console.WriteLine($"*       LogLoss:          {testMetrics.LogLoss:#.###}");
                Console.WriteLine($"*       LogLossReduction: {testMetrics.LogLossReduction:#.###}");
                Console.WriteLine($"*************************************************************************************************************");

                return _trainedModel;
            }
            private static void PredictIssue(ITransformer model)
            {

                MovieData singleIssue = new MovieData() { Title = "Bump version", Description = "This change bumps the major version of all packages" };

                _predEngine = _mlContext.Model.CreatePredictionEngine<MovieData, MovieDataPrediction>(model);

                var prediction = _predEngine.Predict(singleIssue);

                Console.WriteLine($"=============== Single Prediction - Result: {prediction.Prediction} ===============");
            }
        
    }
}
