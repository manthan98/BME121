using System;
using static System.Console;

namespace Bme121.Wa3
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
            //setup variables for four numbers and overall loop
            long n; 
            long a;
            long b;
            long c;
            long d;
            
            //Loop every number from 1 to 999
            for(n = 0; n < 1000; n++)
            {
                double nDouble = Convert.ToDouble(n);
                WriteLine(string.Format("{0:F2}: ", (nDouble/100)));
                
                //Loops to determine values of four numbers
                for(a = 0; a < n; a++)
                {
                    double aDouble = Convert.ToDouble(a);
                    
                    for(b = 0; b < a; b++)
                    {
                        double bDouble = Convert.ToDouble(b);
                        
                        for(c = 0; c < b; c++)
                        {
                            double cDouble = Convert.ToDouble(c);
                            
                            //Final loop step to determine numbers that display correct values
                            for(d = n - a - b - c; d < c && a * b * c * d == n * 100 * 100 * 100; d++)
                            {
                                double dDouble = Convert.ToDouble(d);
                                WriteLine(string.Format("   {0:F2}, {1:F2}, {2:F2}, {3:F2}", /*(nDouble/100)*/ (aDouble/100), (bDouble/100), (cDouble/100), (dDouble/100)));
                            }
                        }
                    }
                }
            }
        }
    }
}
