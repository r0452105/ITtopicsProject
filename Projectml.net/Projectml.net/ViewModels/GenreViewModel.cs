using Microsoft.ML;
using Projectml.net.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private string text = "";
        public string Text
        {
            get { return this.text; }
            set
            {
                this.text = value;
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
            }
        }

        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "IMBD DATABASESET ALL.txt");
        static readonly string _modelPath = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "..", "..", "Data", "model.zip");
        MLContext mlContext = new MLContext();
        TrainTestData splitDataView = new TrainTestData();
        ITransformer model;
        public void CheckSentiment()
        {
            MovieData singleIssue = new MovieData()
            {
                Genre= this.Text //problem
            };

            ITransformer savedModel = mlContext.Model.Load(_modelPath, out var modelInputSchema);

            PredictionEngine<MovieData, MoviePrediction> predictionEngine = mlContext.Model.CreatePredictionEngine<MovieData, MoviePrediction>(savedModel);

            var prediction = predictionEngine.Predict(singleIssue);

            this.Result = prediction.ToString(); //maybeeee
        }

        public void Train()
        {
            Console.WriteLine("we get here");
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
            var estimator = mlContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(MovieData.Genre)) // problem
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features")); //geen binary

            var model = estimator.Fit(splitTrainSet);
            Console.WriteLine("we get here");

            return model;
        }

        public void SaveModel(ITransformer model, DataViewSchema modelInputSchema)
        {
            mlContext.Model.Save(model, modelInputSchema, _modelPath);
            Console.WriteLine("we get here");
        }
    }
}
