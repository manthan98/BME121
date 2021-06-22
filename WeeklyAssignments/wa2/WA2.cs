using System;
using static System.Console;

namespace BME121.WA2
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
        
            WriteLine("Simple Calculator!");
            WriteLine();
            
            //Obtain first input value
            string numberOrConstant;
            double firstValue;
            
            Write("Enter a number or constant [e or pi]: ");
            numberOrConstant = ReadLine();
            
            //Check what type and assign value
            if( numberOrConstant == "e" )
            {
                firstValue = Math.E;
            }
            else if( numberOrConstant == "pi" )
            {
                firstValue = Math.PI;
            }
            else
            {
                firstValue = double.Parse(numberOrConstant);
            }
            
            //Obtain second input
            string enterOperator;
            double secondValue;
            
            Write("Enter an operator [+, -, *, /, %, cos, log, sqrt]: ");
            enterOperator = ReadLine();
            
            //Check if binary (or not)
            if( enterOperator == "cos" )
            {
                secondValue = Math.Cos(firstValue);
                WriteLine("The final answer is {0}.", secondValue);
            }
            else if( enterOperator == "sqrt" )
            {
                secondValue = Math.Sqrt(firstValue);
                WriteLine("The final answer is {0}.", secondValue);
            }
            else if( enterOperator == "log" )
            {
                secondValue = Math.Log10(firstValue);
                WriteLine("The final answer is {0}.", secondValue);
            }
            //Procedure for a binary operator input
            else
            {
                string enterThird;
                double thirdValue;
                double finalAns;
                
                //Obtain inputted binary operation
                Write("Enter another number or constant [e or pi]: ");
                enterThird = ReadLine();
                
                //Check what type, assign value, and display
                if( enterOperator == "+" )
                {
                    //Check for pi and e inputs every time
                    if( enterThird == "pi" )
                    {
                        thirdValue = Math.PI;
                        finalAns = firstValue + thirdValue;
                        WriteLine();
                        WriteLine("The final answer is {0}.", finalAns);
                    }
                    else if( enterThird == "e" )
                    {
                        thirdValue = Math.E;
                        finalAns = firstValue + thirdValue;
                        WriteLine();
                        WriteLine("The final answer is {0}", finalAns);
                    }
                    else
                    {
                    thirdValue = double.Parse(enterThird);
                    finalAns = thirdValue + firstValue;
                    WriteLine();
                    WriteLine("The final answer is {0}.", finalAns);
                    }
                }
                else if( enterOperator == "-" )
                {
                    if( enterThird == "pi" )
                    {
                        thirdValue = Math.PI;
                        finalAns = firstValue - thirdValue;
                        WriteLine();
                        WriteLine("The final answer is {0}.", finalAns);
                    }
                    else if( enterThird == "e" )
                    {
                        thirdValue = Math.E;
                        finalAns = firstValue - thirdValue;
                        WriteLine();
                        WriteLine("The final answer is {0}.", finalAns);
                    }
                    else
                    {
                    thirdValue = double.Parse(enterThird);
                    finalAns = firstValue - thirdValue;
                    WriteLine();
                    WriteLine("The final answer is {0}.", finalAns);
                    }
                }
                else if( enterOperator == "*" )
                {
                    if( enterThird == "pi" )
                    {
                        thirdValue = Math.PI;
                        finalAns = firstValue * thirdValue;
                        WriteLine();
                        WriteLine("The final answer is {0}.", finalAns);
                    }
                    else if( enterThird == "e" )
                    {
                        thirdValue = Math.E;
                        finalAns = firstValue * thirdValue;
                        WriteLine();
                        WriteLine("The final answer is {0}.", finalAns);
                    }
                    else
                    {
                    thirdValue = double.Parse(enterThird);
                    finalAns = firstValue * thirdValue;
                    WriteLine();
                    WriteLine("The final answer is {0}.", finalAns);
                    }
                }
                else if( enterOperator == "/" )
                {
                    if( enterThird == "pi" )
                    {
                        thirdValue = Math.PI;
                        finalAns = firstValue / thirdValue;
                        WriteLine();
                        WriteLine("The final answer is {0}.", finalAns);
                    }
                    else if( enterThird == "e" )
                    {
                        thirdValue = Math.E;
                        finalAns = firstValue / thirdValue;
                        WriteLine();
                        WriteLine("The final answer is {0}.", finalAns);
                    }
                    else
                    {
                    thirdValue = double.Parse(enterThird);
                    finalAns = firstValue / thirdValue;
                    WriteLine();
                    WriteLine("The final answer is {0}.", finalAns);
                    }
                }
                else if( enterOperator == "%" )
                {
                    if( enterThird == "pi" )
                    {
                        thirdValue = Math.PI;
                        finalAns = firstValue % thirdValue;
                        WriteLine();
                        WriteLine("The final answer is {0}.", finalAns);
                    }
                    else if( enterThird == "e" )
                    {
                        thirdValue = Math.E;
                        finalAns = firstValue % thirdValue;
                        WriteLine();
                        WriteLine("The final answer is {0}.", finalAns);
                    }
                    else
                    {
                    thirdValue = double.Parse(enterThird);
                    finalAns = firstValue % thirdValue;
                    WriteLine();
                    WriteLine("The final answer is {0}.", finalAns);
                    }
                }
            }
            
        }
    }
}
