using System;
using static System.Console;

namespace BME121.PA1
{
    static partial class Program
    {
    
        /// <StudentPlan>Biomedical Engineering</StudentPlan>
        /// <StudentDept>Department of Systems Design Engineering</StudentDept>
        /// <StudentInst>University of Waterloo</StudentInst>
        /// <StudentName>Shah, Manthan</StudentName>
        /// <StudentUserID>mskshah</StudentUserID>
        /// <StudentAcknowledgements>
        /// I declare that, except as acknowledged below, this is my original work.
        /// Acknowledged contributions of others: None
        /// </StudentAcknowledgements>
        
        static void Main( )
        {
            WriteLine("Microsphere free resonance frequency calculator");
            WriteLine();
            
            WriteLine("Enter the following five parameters: ");
            
            Write("Ambient plus blood-vessel pressures(Pa): ");
            double ambBloodPressure = double.Parse(ReadLine()); // Unit: Pa
            
            Write("Blood density (kg/m^3): ");
            double bloodDensity = double.Parse(ReadLine()); // Unit: kg/m^3
            
            Write("Blood surface tension (N/m): ");
            double bloodSurface = double.Parse(ReadLine()); // Unit: N/m
            
            Write("Polytropic index: ");
            double polyIndex = double.Parse(ReadLine()); // Unit: none
            
            Write("Microsphere radius (m): ");
            double microRadius = double.Parse(ReadLine()); // Unit: m
            
            WriteLine();
            
            // final answer output
            Write("Free resonant frequency is "); 
            Write(calculation(ambBloodPressure, bloodDensity, bloodSurface, polyIndex, microRadius).ToString("E2"));
            Write(" Hz");
            
        }
        
        static double calculation(double ambBloodPressure, double bloodDensity, double bloodSurface, double polyIndex, double microRadius) // obtain the values to be calculated
        {
            return ((Math.Sqrt((1 / (bloodDensity * Math.Pow(microRadius, 2))) * ((3 * polyIndex * ambBloodPressure) + ((3 * polyIndex) - 1) * ((2 * bloodSurface) / microRadius)))) / (2 * Math.PI)); // complete algorithm
        }
        
    }
}
