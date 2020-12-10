using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace Projectml.net.Models
{
    public class MovieData
    {
        [LoadColumn(0)]
        public string TitleId;

        [LoadColumn(1)]
        public string Title;

        [LoadColumn(2)]
        public string OriginalTitle;

        [LoadColumn(3)]
        public int Year;

        [LoadColumn(4)]
        public DateTime PublishDate;

        [LoadColumn(5), ColumnName("Label")]
        public string Genre;

        [LoadColumn(6)]
        public TimeSpan Duration;

        [LoadColumn(7)]
        public string Country;

        [LoadColumn(8)]
        public string Language;

        [LoadColumn(9)]
        public string Director;

        [LoadColumn(10)]
        public string Writer;

        [LoadColumn(11)]
        public string ProductionCompany;

        [LoadColumn(12)]
        public string Actors; //mss een list ofzo idk

        [LoadColumn(13)]
        public string Description;

        [LoadColumn(14)]
        public double AverageVote;

        [LoadColumn(15)]
        public int Totalvotes;

        [LoadColumn(16)]
        public int UserReviews;

        [LoadColumn(17)]
        public int CriticsReviews;
    }
    public class MoviePrediction : MovieData
    {
        [ColumnName("PredictedLabel")]
        public string Prediction { get; set; }
    }
}
