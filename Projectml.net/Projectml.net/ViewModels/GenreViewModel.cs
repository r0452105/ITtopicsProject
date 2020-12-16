using Projectml.net.Models;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms.Text;
using static Microsoft.ML.DataOperationsCatalog;

namespace Projectml.net.ViewModels
{
    public class GenreViewModel : BaseViewModel
    {

        #region GettersAndSetters
        private string isTraining = "";
        public string IsTraining
        {
            get { return this.isTraining; }
            set
            {
                this.isTraining = value;
                NotifyPropertyChanged();
            }
        }
        private string texttitle = "";
        public string TextTitle
        {
            get { return this.texttitle; }
            set
            {
                this.texttitle = value;
                NotifyPropertyChanged();
            }
        }
        private string textactors = "";
        public string TextActors
        {
            get { return this.textactors; }
            set
            {
                this.textactors = value;
                NotifyPropertyChanged();
            }
        }
        private string result = "";
        public string Result
        {
            get { return this.result; }
            set
            {
                this.result = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        public override string this[string columnName]
        {
            get
            {
                return "";
            }
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "CheckSentiment":
                    this.CheckSentiment();
                    break;
                case "Train":
                    this.Train();
                    break;
                case "SaveModel":
                    this.SaveModel(model, mlContext.Data.LoadFromTextFile<MovieData>(_dataPath, hasHeader: true).Schema);
                    break;
                case "Clear":
                    this.Clearing();
                    break;
            }
        }

        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "IMDBDATASETSMALL.txt");
        static readonly string _modelPath = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "..", "..", "Data", "MLModel.zip");
        MLContext mlContext = new MLContext();
        TrainTestData splitDataView = new TrainTestData();
        ITransformer model;

        public void CheckSentiment()
        {
            MovieData singleIssue = new MovieData()
            {
                Title = this.TextTitle,
                Actors = this.TextActors
            };

            ITransformer savedModel = mlContext.Model.Load(_modelPath, out var modelInputSchema);

            PredictionEngine<MovieData, MovieDataPrediction> predictionEngine = mlContext.Model.CreatePredictionEngine<MovieData, MovieDataPrediction>(savedModel);

            var prediction = predictionEngine.Predict(singleIssue);

            this.Result = prediction.Prediction.ToString();
        }

        public void Train()
        {
            this.IsTraining = "Training ...";

            splitDataView = LoadData(mlContext);

            model = BuildAndTrainModel(mlContext, splitDataView.TrainSet);

            this.IsTraining = "Ready !";
        }

        public static TrainTestData LoadData(MLContext mlContext)
        {
            IDataView dataView = mlContext.Data.LoadFromTextFile<MovieData>(_dataPath, hasHeader: true);

            TrainTestData splitDataView = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.1);

            return splitDataView;
        }

        public static ITransformer BuildAndTrainModel(MLContext mlContext, IDataView splitTrainSet)
        {
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "genre", outputColumnName: "Label")
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: "title", outputColumnName: "TitleFeaturized"))
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: "actors", outputColumnName: "ActorsFeaturized"))
                .Append(mlContext.Transforms.Concatenate("Features", "TitleFeaturized", "ActorsFeaturized"))
                .AppendCacheCheckpoint(mlContext);

            var estimator = pipeline.Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var model = estimator.Fit(splitTrainSet);

            return model;
        }

        public void SaveModel(ITransformer model, DataViewSchema modelInputSchema)
        {
            mlContext.Model.Save(model, modelInputSchema, _modelPath);
        }

        public void Clearing()
        {
            this.Result = "";
        }
    }
}