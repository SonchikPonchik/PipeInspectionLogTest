using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Business
{
    public class MaxDiameterDeviation
    {
        public double maxDeviation;
        public MaxDiameterDeviation(double defaultDiameter, double diameter1, double diameter2, double? centerDiameter)
        {
            double diameterDeviation1 = GetDifference(defaultDiameter, diameter1);
            double diameterDeviation2 = GetDifference(defaultDiameter, diameter2);
            double diameterDeviationCenter = GetDifference(defaultDiameter, centerDiameter);

            maxDeviation = GetMaxDeviation(diameterDeviation1, diameterDeviation2, diameterDeviationCenter);
        }


        //Метод получения максимального отклонения 
        private double GetMaxDeviation(double diameterDeviation1, double diameterDeviation2, double diameterDeviationCenter)
        {
            double temp = CompareDeviation(diameterDeviation1, diameterDeviation2);

            return CompareDeviation(temp, diameterDeviationCenter);
        }

        //Метод сравнения двух отклонений диаметра
        private double CompareDeviation(double diameterDeviation1, double diameterDeviation2)
        {
            if (Math.Abs(diameterDeviation1) > Math.Abs(diameterDeviation2))
                return diameterDeviation1;
            else return diameterDeviation2;
        }

        //Метод получения разницы между целевым и полученным диаметрами
        private double GetDifference(double defaultDiameter, double? finalDiameter)
        {
            if (finalDiameter != null)
                return (double)finalDiameter - defaultDiameter;
            return 0;
        }
    }
}
