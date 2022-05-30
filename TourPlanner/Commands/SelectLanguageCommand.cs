using System;
using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    internal class SelectLanguageCommand : BaseCommand
    {
        public string Language { get; set; }
        public MainWindowVM MainWindowVM { get; set; }

        public SelectLanguageCommand(MainWindowVM mainwindowvm, string language)
        {
            Language = language;
            MainWindowVM = mainwindowvm;
        }
        public override void Execute(object? parameter)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (Language)
            {
                case "en":
                    dictionary.Source = new Uri("Languages/lan.en.xaml", UriKind.Relative);
                    break;
                case "de":
                    dictionary.Source = new Uri("Languages/lan.de.xaml", UriKind.Relative);
                    break;
                case "at":
                    dictionary.Source = new Uri("Languages/lan.at.xaml", UriKind.Relative);
                    break;
                default:
                    dictionary.Source = new Uri("Languages/lan.en.xaml", UriKind.Relative);
                    break;
            }
            Application application = Application.Current;
            application.Resources.MergedDictionaries.Add(dictionary);
        }
    }
}
