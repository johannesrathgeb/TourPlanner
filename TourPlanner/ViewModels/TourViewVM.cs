using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class TourViewVM : ViewModelBase
    {
        public byte[]? RouteImageSource { get; set; }

        private Tour? selectedTour;

        public Tour? SelectedTour
        {
            get => selectedTour;
            set
            {
                if (selectedTour != value)
                {
                    selectedTour = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        private string? filepath;
        public string? Filepath
        {
            get => filepath;
            set
            {
                if (filepath != value)
                {
                    filepath = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        public void updateSelectedTour(Tour tour)
        {
            SelectedTour = tour;

            if (SelectedTour != null)
            {
                Filepath = Path.GetFullPath($"../../../../img/img{SelectedTour.Id}.png");
                using (var stream = File.Open(Filepath, FileMode.Open))
                {
                    RouteImageSource = ReadFully(stream);
                }
                RaisePropertyChangedEvent(nameof(RouteImageSource));


            }
            else
            {
                RouteImageSource = null;
                RaisePropertyChangedEvent(nameof(RouteImageSource));
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
