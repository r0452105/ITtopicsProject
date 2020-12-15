using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projectml.net.Models
{
    public class MovieData
    {
        [ColumnName("imdb_title_id"), LoadColumn(0)]
        public string Imdb_title_id { get; set; }


        [ColumnName("title"), LoadColumn(1)]
        public string Title { get; set; }


        [ColumnName("original_title"), LoadColumn(2)]
        public string Original_title { get; set; }


        [ColumnName("year"), LoadColumn(3)]
        public float Year { get; set; }


        [ColumnName("date_published"), LoadColumn(4)]
        public string Date_published { get; set; }


        [ColumnName("genre"), LoadColumn(5)]
        public string Genre { get; set; }


        [ColumnName("duration"), LoadColumn(6)]
        public float Duration { get; set; }


        [ColumnName("country"), LoadColumn(7)]
        public string Country { get; set; }


        [ColumnName("language"), LoadColumn(8)]
        public string Language { get; set; }


        [ColumnName("director"), LoadColumn(9)]
        public string Director { get; set; }


        [ColumnName("writer"), LoadColumn(10)]
        public string Writer { get; set; }


        [ColumnName("production_company"), LoadColumn(11)]
        public string Production_company { get; set; }


        [ColumnName("actors"), LoadColumn(12)]
        public string Actors { get; set; }


        [ColumnName("description"), LoadColumn(13)]
        public string Description { get; set; }


        [ColumnName("avg_vote"), LoadColumn(14)]
        public float Avg_vote { get; set; }


        [ColumnName("votes"), LoadColumn(15)]
        public float Votes { get; set; }


        [ColumnName("reviews_from_users"), LoadColumn(16)]
        public float Reviews_from_users { get; set; }


        [ColumnName("reviews_from_critics"), LoadColumn(17)]
        public float Reviews_from_critics { get; set; }

    }

    public class MovieDataPrediction : MovieData
    {
        [ColumnName("PredictedLabel")]
        public String Prediction { get; set; }
    }
}
