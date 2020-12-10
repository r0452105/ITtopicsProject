using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Projectml.net.Views;

namespace Projectml.net.ViewModels
{
    public class MainViewModel : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            //returnwaarde true -> methode mag uitgevoerd worden
            //returnwaarde false -> methode mag niet uitgevoerd worden
            switch (parameter.ToString())
            {
                case "Genre": return true;
                case "Anders": return true;
            }
            return true;
        }
        public void Execute(object parameter)
        {
            //Via parameter kom je te weten op welke knop er gedrukt is,  
            //instelling via CommandParameter in xaml
            switch (parameter.ToString())
            {
                case "Genre": OpenGenre(); break;
                case "Anders": OpenAnders(); break;
            }
        }

        public void OpenGenre()
        {
            GenreScherm view = new GenreScherm();
            view.Show();
        }
        public void OpenAnders()
        {
            AnderScherm view = new AnderScherm();
            view.Show();
        }

    }
}

