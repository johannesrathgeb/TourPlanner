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
    }
}
