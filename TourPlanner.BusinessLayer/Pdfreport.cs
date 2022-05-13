using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public class Pdfreport
    {

        public void TourReport(Tour tour)
        {
            string TARGET_PDF = Path.GetFullPath($"../../../../reports/Tourreport-{tour.Name}.pdf");

            PdfWriter writer = new PdfWriter(TARGET_PDF);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            Paragraph tableHeader = new Paragraph(tour.Name)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(18)
                    .SetBold()
                    .SetFontColor(ColorConstants.BLACK);
            document.Add(tableHeader);
            Table table = new Table(UnitValue.CreatePercentArray(6)).UseAllAvailableWidth();
            table.AddHeaderCell(getHeaderCell("Description"));
            table.AddHeaderCell(getHeaderCell("From"));
            table.AddHeaderCell(getHeaderCell("To"));
            table.AddHeaderCell(getHeaderCell("Transporttype"));
            table.AddHeaderCell(getHeaderCell("Distance"));
            table.AddHeaderCell(getHeaderCell("Est. Time"));
            table.SetFontSize(14).SetBackgroundColor(ColorConstants.WHITE);
            table.AddCell(tour.TourDescription);
            table.AddCell(tour.From);
            table.AddCell(tour.To);
            table.AddCell(tour.TransportType.ToString());
            table.AddCell(tour.TourDistance + " km");
            table.AddCell(tour.EstimatedTime);
            document.Add(table);

            if (tour.Tourlogs.Count > 0)
            {
                Paragraph tableHeader2 = new Paragraph("Tourlogs for " + tour.Name)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                .SetFontSize(18)
                .SetBold()
                .SetFontColor(ColorConstants.BLACK);
                document.Add(tableHeader2);

                Table table2 = new Table(UnitValue.CreatePercentArray(5)).UseAllAvailableWidth();
                table2.AddHeaderCell(getHeaderCell("Date"));
                table2.AddHeaderCell(getHeaderCell("Comment"));
                table2.AddHeaderCell(getHeaderCell("Difficulty"));
                table2.AddHeaderCell(getHeaderCell("Totaltime"));
                table2.AddHeaderCell(getHeaderCell("Rating"));
                table2.SetFontSize(14).SetBackgroundColor(ColorConstants.WHITE);

                foreach (Tourlog tourlog in tour.Tourlogs)
                {
                    table2.AddCell(tourlog.Date);
                    table2.AddCell(tourlog.Comment);
                    table2.AddCell(tourlog.Difficulty.ToString());
                    table2.AddCell(tourlog.Totaltime);
                    table2.AddCell(tourlog.Rating.ToString());
                }
                document.Add(table2);
            }
            Paragraph imageHeader = new Paragraph("Route for " + tour.Name)
            .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
            .SetFontSize(18)
            .SetBold()
            .SetFontColor(ColorConstants.BLACK);
            document.Add(imageHeader);
            string TOUR_PNG = Path.GetFullPath($"../../../../img/img{tour.Id}.png");
            ImageData imageData = ImageDataFactory.Create(TOUR_PNG);
            document.Add(new Image(imageData));

            //document.Add(new AreaBreak()); 
            document.Close();
        }

        public void SummarizedReport(ObservableCollection<Tour> tourlist)
        {
            string TARGET_PDF = Path.GetFullPath($"../../../../reports/SummarizedReport.pdf");

            PdfWriter writer = new PdfWriter(TARGET_PDF);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);


            Paragraph tableHeader = new Paragraph("Summarized Report")
            .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
            .SetFontSize(18)
            .SetBold()
            .SetFontColor(ColorConstants.BLACK);
            document.Add(tableHeader);

            Table table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
            table.AddHeaderCell(getHeaderCell("Tourname"));
            table.AddHeaderCell(getHeaderCell("Avg. Distance"));
            table.AddHeaderCell(getHeaderCell("Avg. Time"));
            table.AddHeaderCell(getHeaderCell("Avg. Rating"));
            table.SetFontSize(14).SetBackgroundColor(ColorConstants.WHITE);

            foreach (Tour tour in tourlist) {
                if (tour.Tourlogs.Count > 0)
                {
                    var distance = tour.TourDistance;
                    float avgtime = 0;
                    float avgrating = 0; 

                    foreach (Tourlog tourlog in tour.Tourlogs)
                    {
                        avgtime += int.Parse(tourlog.Totaltime);
                        avgrating += tourlog.Rating; 
                    }

                    avgtime = avgtime / tour.Tourlogs.Count;
                    avgrating = avgrating / tour.Tourlogs.Count;

                    table.AddCell(tour.Name);
                    table.AddCell(distance + " km");
                    table.AddCell(avgtime.ToString() + " m");
                    table.AddCell(avgrating.ToString());
                }
            }
            document.Add(table);
            document.Close();
        }

        private static Cell getHeaderCell(String s)
        {
            return new Cell().Add(new Paragraph(s)).SetBold().SetBackgroundColor(ColorConstants.GRAY);
        }
    }
    
}