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
                    //this.CheckSentiment();
                    break;
                case "Train":
                    //this.Train();
                    break;
                case "SaveModel":
                    //this.SaveModel(model, mlContext.Data.LoadFromTextFile<MovieData>(_dataPath, hasHeader: true).Schema);
                    break;
            }
        }

       
    }
}

