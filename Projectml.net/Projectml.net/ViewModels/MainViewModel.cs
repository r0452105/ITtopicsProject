using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Projectml.net.Views;

namespace Projectml.net.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public override string this[string columnName]
        {
            get
            {
                return "";
            }
        }

        public override bool CanExecute(object parameter)
        {

            //returnwaarde true -> methode mag uitgevoerd worden
            //returnwaarde false -> methode mag niet uitgevoerd worden
            switch (parameter.ToString())
            {
                case "Genre": return true;
                case "Writer": return true;
            }
            return true;
        }
        public override void Execute(object parameter)
        {
            //Via parameter kom je te weten op welke knop er gedrukt is,  
            //instelling via CommandParameter in xaml
            switch (parameter.ToString())
            {
                case "Genre": OpenGenre(); break;
                case "Writer": OpenWriter(); break;
            }
        }

        public void OpenGenre()
        {
            GenreView genreView = new GenreView();
            GenreViewModel genreViewModel = new GenreViewModel();
            genreView.DataContext = genreViewModel;
            genreView.Show();
        }
        public void OpenWriter() // temporary
        {
            WriterView writerView = new WriterView();
            WriterViewModel writerViewModel = new WriterViewModel();
            writerView.DataContext = writerViewModel;
            writerView.Show();
        }

    }
}

