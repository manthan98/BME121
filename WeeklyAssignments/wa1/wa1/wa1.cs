using System;
using static System.Console;

namespace BME121.wa1
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
            // three variables that store the three words
            // entered by the user
            string first;
            string second;
            string third;
            
            // TODO (describe what happens here)
            WriteLine("Wa1 words program");
            WriteLine();
            
            // Ask user to input the words
            Write("Enter a word [1 of 3]: ");
            first = ReadLine();
            
            Write("Enter a word [2 of 3]: ");
            second = ReadLine();
            
            Write("Enter a word [3 of 3]: ");
            third = ReadLine();
            
            WriteLine();
            
            // Displaying entered words
            WriteLine("Here are your words in order as entered.");
            WriteLine("word1: " + first);
            WriteLine("word2: " + second);
            WriteLine("word3: " + third);
            
            WriteLine();
            
            // Displaying words seperated by comma
            WriteLine("Here are your words in order on one line comma seperated.");
            WriteLine("words: {0}, {1}, {2}", first, second, third); //use curly braces to link variable and display commas
            
            WriteLine();
            
            // Display words in reverse and upper case
            WriteLine("Here are your words in reverse order and upper case");
            WriteLine("word3: " + third.ToUpper()); // .ToUpper() turns text into upper case
            WriteLine("word2: " + second.ToUpper());
            WriteLine("word1: " + first.ToUpper());
            
            WriteLine();
            
        }
    }
}
