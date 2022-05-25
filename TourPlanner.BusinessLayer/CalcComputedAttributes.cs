using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public class CalcComputedAttributes
    {
        public int CalcPopularity(Tour tour)
        {
            if(tour.Tourlogs.Count == 0)
            {
                return 0; 
            }

            if (tour.Tourlogs.Count < 2)
            {
                return 1; 
            }
            else if (tour.Tourlogs.Count < 4 && tour.Tourlogs.Count >= 2)
            {
                return 2;
            }
            else if (tour.Tourlogs.Count < 6 && tour.Tourlogs.Count >= 4)
            {
                return 3;
            }
            else if (tour.Tourlogs.Count < 8 && tour.Tourlogs.Count >= 6)
            {
                return 4;
            }
            else
            {
                return 5;
            }
        }
        public int CalcChildFriendliness(Tour tour)
        {
            if(tour.Tourlogs.Count == 0)
            {
                return 0; 
            }

            int difficulty = CalcDifficulty(tour);
            int totaltimes = CalcTotalTimes(tour);
            int distance = CalcDistance((int)Convert.ToDouble(tour.TourDistance, CultureInfo.InvariantCulture.NumberFormat));


            return (((difficulty + totaltimes + distance) / 3) - 5) * (-1);
        }
        public int CalcDifficulty(Tour tour)
        {
            int difficulty = 0;

            if(tour.Tourlogs.Count == 0)
            {
                return 0; 
            }

            foreach (Tourlog tourlog in tour.Tourlogs)
            {
                difficulty += tourlog.Difficulty;
            }
            return difficulty / tour.Tourlogs.Count;
        }
        public int CalcTotalTimes(Tour tour)
        {
            int totaltimes = 0;

            if (tour.Tourlogs.Count == 0)
            {
                return 0;
            }

            foreach (Tourlog tourlog in tour.Tourlogs)
            {
                totaltimes += int.Parse(tourlog.Totaltime);
            }
            totaltimes /= tour.Tourlogs.Count;

            if(totaltimes < 30)
            {
                return 1; 
            } else if(totaltimes < 60 && totaltimes >= 30)
            {
                return 2; 
            } else if(totaltimes < 120 && totaltimes >= 60)
            {
                return 3;
            } else if(totaltimes < 150 && totaltimes >= 120)
            {
                return 4;
            } else
            {
                return 5;
            }

        }
        public int CalcDistance(int distance)
        {
            if(distance < 50)
            {
                return 1; 
            } else if(distance < 100 && distance >= 50)
            {
                return 2; 
            } else if (distance < 150 && distance >= 100)
            {
                return 3;
            } else if (distance < 200 && distance >= 150)
            {
                return 4;
            } else
            {
                return 5; 
            }
        }
    }
}
