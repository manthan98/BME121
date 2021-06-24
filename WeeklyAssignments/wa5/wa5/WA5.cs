using System;
using static System.Console;
using System.IO;

namespace Bme121.Wa5
{
    /// <StudentPlan>Biomedical Engineering</StudentPlan>
    /// <StudentDept>Department of Systems Design Engineering</StudentDept>
    /// <StudentInst>University of Waterloo</StudentInst>
    /// <StudentName>Shah, Manthan</StudentName>
    /// <StudentUserID>20658832</StudentUserID>
    /// <StudentAcknowledgements>
    /// I declare that, except as acknowledged below, this is my original work.
    /// Acknowledged contributions of others:
    /// </StudentAcknowledgements>
    
    public class Program
    {
        public static void Main( )
        {
            FileStream inFile = new FileStream( @"file.txt", FileMode.Open, FileAccess.Read );
            StreamReader read = new StreamReader( inFile );
            string startEndLines;
            
            while(!(startEndLines = read.ReadLine( )).Equals( "THE CANTERVILLE GHOST" ))
            {
                
            }
            
            int counter1 = 0;
            int counter2 = 0;
            int counter3 = 0;
            int counter4 = 0;
            int counter5 = 0;
            int counter6 = 0;
            int counter7 = 0;
            int counter8 = 0;
            int counter9 = 0;
            int counter10 = 0;
            int counter11 = 0;
            int counter12 = 0;
            int counter13 = 0;
            int counter14 = 0;
            int counter15 = 0;
            int counter16 = 0;
            int counter17 = 0;
            int counter18 = 0;
            int counter19 = 0;
            int counter20 = 0;
            int counter21 = 0;
            int counter22 = 0;
            int counter23 = 0;
            int counter24 = 0;
            int counter25 = 0;
            int lineIndex = 0;
            while( !(read.ReadLine( )).Equals( "Virginia blushed." ) )
            {
                string line = read.ReadLine( );
                
                char[] lineWordSplit = { ' ', '-' };
                string[] lineSplit = line.Split( lineWordSplit );
                char[] delimiterChars = { ' ', ',', '.', '?', '!', ';', ':', '{', '}', '(', ')', '|', '_', '+', '"', '\''};
                
                for( int i = 0; i < lineSplit.Length; i ++ )
                {
                    lineSplit[i] = lineSplit[i].Trim( delimiterChars );
                    if( lineSplit[i].Length == 1 ) { counter1 += 1; }
                    if( lineSplit[i].Length == 2 ) { counter2 += 1; }
                    if( lineSplit[i].Length == 3 ) { counter3 += 1; }
                    if( lineSplit[i].Length == 4 ) { counter4 += 1; }
                    if( lineSplit[i].Length == 5 ) { counter5 += 1; }
                    if( lineSplit[i].Length == 6 ) { counter6 += 1; }
                    if( lineSplit[i].Length == 7 ) { counter7 += 1; }
                    if( lineSplit[i].Length == 8 ) { counter8 += 1; }
                    if( lineSplit[i].Length == 9 ) { counter9 += 1; }
                    if( lineSplit[i].Length == 10 ) { counter10 += 1; }
                    if( lineSplit[i].Length == 11 ) { counter11 += 1; }
                    if( lineSplit[i].Length == 12 ) { counter12 += 1; }
                    if( lineSplit[i].Length == 13 ) { counter13 += 1; }
                    if( lineSplit[i].Length == 14 ) { counter14 += 1; }
                    if( lineSplit[i].Length == 15 ) { counter15 += 1; }
                    if( lineSplit[i].Length == 16 ) { counter16 += 1; }
                    if( lineSplit[i].Length == 17 ) { counter17 += 1; }
                    if( lineSplit[i].Length == 18 ) { counter18 += 1; }
                    if( lineSplit[i].Length == 19 ) { counter19 += 1; }
                    if( lineSplit[i].Length == 20 ) { counter20 += 1; }
                    if( lineSplit[i].Length == 21 ) { counter21 += 1; }
                    if( lineSplit[i].Length == 22 ) { counter22 += 1; }
                    if( lineSplit[i].Length == 23 ) { counter23 += 1; }
                    if( lineSplit[i].Length == 24 ) { counter24 += 1; }
                    if( lineSplit[i].Length == 25 ) { counter25 += 1; }
                }
                lineIndex ++;
            }
            WriteLine( "Words with 1 letters: {0}", counter1 );
            WriteLine( "Words with 2 letters: {0}", counter2 );
            WriteLine( "Words with 3 letters: {0}", counter3 );
            WriteLine( "Words with 4 letters: {0}", counter4 );
            WriteLine( "Words with 5 letters: {0}", counter5 );
            WriteLine( "Words with 6 letters: {0}", counter6 );
            WriteLine( "Words with 7 letters: {0}", counter7 );
            WriteLine( "Words with 8 letters: {0}", counter8 );
            WriteLine( "Words with 9 letters: {0}", counter9 );
            WriteLine( "Words with 10 letters: {0}", counter10 );
            WriteLine( "Words with 11 letters: {0}", counter11 );
            WriteLine( "Words with 12 letters: {0}", counter12 );
            WriteLine( "Words with 13 letters: {0}", counter13 );
            WriteLine( "Words with 14 letters: {0}", counter14 );
            WriteLine( "Words with 15 letters: {0}", counter15 );
            WriteLine( "Words with 16 letters: {0}", counter16 );
            WriteLine( "Words with 17 letters: {0}", counter17 );
            WriteLine( "Words with 18 letters: {0}", counter18 );
            WriteLine( "Words with 19 letters: {0}", counter19 );
            WriteLine( "Words with 20 letters: {0}", counter20 );
            WriteLine( "Words with 21 letters: {0}", counter21 );
            WriteLine( "Words with 22 letters: {0}", counter22 );
            WriteLine( "Words with 23 letters: {0}", counter23 );
            WriteLine( "Words with 24 letters: {0}", counter24 );
            WriteLine( "Words with 25 letters: {0}", counter25 );
            
            read.Dispose( );
            inFile.Dispose( );
        }
    }
}
