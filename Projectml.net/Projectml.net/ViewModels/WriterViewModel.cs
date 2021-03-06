﻿using Projectml.net.Models;
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
using System.Windows;
using GalaSoft.MvvmLight.Command;

namespace Projectml.net.ViewModels
{
    public class WriterViewModel : BaseViewModel
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


        private string isSaved = "";
        public string IsSaved
        {
            get { return this.isSaved; }
            set
            {
                this.isSaved = value;
                NotifyPropertyChanged();
            }
        }


        private string textTitle = "";
        public string TextTitle
        {
            get { return this.textTitle; }
            set
            {
                this.textTitle = value;
                NotifyPropertyChanged();
            }
        }


        private string textActors = "";
        public string TextActors
        {
            get { return this.textActors; }
            set
            {
                this.textActors = value;
                NotifyPropertyChanged();
            }
        }

        private string textDirector = "";
        public string TextDirector
        {
            get { return this.textDirector; }
            set
            {
                this.textDirector = value;
                NotifyPropertyChanged();
            }
        }

        private string textGenre = "";
        public string TextGenre
        {
            get { return this.textGenre; }
            set
            {
                this.textGenre = value;
                NotifyPropertyChanged();
            }
        }

        private string textProduction_Company = "";
        public string TextProduction_Company
        {
            get { return this.textProduction_Company; }
            set
            {
                this.textProduction_Company = value;
                NotifyPropertyChanged();
            }
        }

        private string textDescription = "";
        public string TextDescription
        {
            get { return this.textDescription; }
            set
            {
                this.textDescription = value;
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

        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "IMDBDATASETMEDIUM.txt");
        static readonly string _modelPath = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "..", "..", "Data", "MLModelWriter.zip");
        MLContext mlContext = new MLContext();
        TrainTestData splitDataView = new TrainTestData();
        ITransformer model;

        public void CheckSentiment()
        {
            MovieData singleIssue = new MovieData()
            {
                Title = this.TextTitle,
                Actors = this.TextActors,
                Description = this.TextDescription,
                Director = this.TextDirector,
                Genre = this.TextGenre,
                Production_company = this.TextProduction_Company
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
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "writer", outputColumnName: "Label")
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: "title", outputColumnName: "TitleFeaturized"))
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: "actors", outputColumnName: "ActorsFeaturized"))
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: "description", outputColumnName: "DescriptionFeaturized"))
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: "director", outputColumnName: "DirectorFeaturized"))
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: "genre", outputColumnName: "GenreFeaturized"))
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: "production_company", outputColumnName: "Production_CompanyFeaturized"))
                .Append(mlContext.Transforms.Concatenate("Features", "TitleFeaturized", "ActorsFeaturized", "DescriptionFeaturized", "DirectorFeaturized", "GenreFeaturized", "Production_CompanyFeaturized"))
                .AppendCacheCheckpoint(mlContext);

            var estimator = pipeline.Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var model = estimator.Fit(splitTrainSet);

            return model;
        }

        public void SaveModel(ITransformer model, DataViewSchema modelInputSchema)
        {
            mlContext.Model.Save(model, modelInputSchema, _modelPath);
            this.IsSaved = "Saved";
        }

        public void Clearing()
        {
            this.Result = "";
            this.TextActors = "";
            this.TextTitle = "";
            this.TextDescription = "";
            this.TextDirector = "";
            this.TextGenre = "";
            this.TextProduction_Company = "";
        }

        public WriterViewModel()
        {
            this.CloseWindowCommand = new RelayCommand<Window>(this.CloseWindow);
        }

        public RelayCommand<Window> CloseWindowCommand { get; private set; }


        private void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
