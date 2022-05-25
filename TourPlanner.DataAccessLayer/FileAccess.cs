using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataAccessLayer
{
    public class FileAccess
    {
        public string GetTourData(string filepath)
        {
            return File.ReadAllText(filepath); 
        }
        public void SaveTourData(string tourcontent, SaveFileDialog sfd)
        {
            StreamWriter writer = new StreamWriter(sfd.OpenFile());
            writer.WriteLine(tourcontent);
            writer.Dispose();
            writer.Close();
        }
    }
}
