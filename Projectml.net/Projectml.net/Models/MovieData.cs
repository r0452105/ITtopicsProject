using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace Projectml.net.Models
{
    public class MovieData //inhoud stolen
    {

        [LoadColumn(0), ColumnName("Label")]
        public bool IsSpam;

        [LoadColumn(1)]
        public string Title;

    }

    public class MovieDataPrediction : MovieData   //inhoud stolen
    {

        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }
    }
}
