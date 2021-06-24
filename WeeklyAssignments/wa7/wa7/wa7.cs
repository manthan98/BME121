using System;
using static System.Console;
using System.IO;
using MediCal;

namespace Bme121.Wa7
{
    static partial class Program
    {
        static void Main( )
        {
            //DrugRecords lastYear = new DrugRecords( "RXQT1404.txt", "RXQT1502.txt", "RXQT1502.txt", "RXQT1503.txt" );
            DrugRecords lastYear = new DrugRecords( "RXQT1503-100.txt" );
            
            int count = 0;
            
            // foreach( TYPE reference in COLLECTION )
            
            foreach( Drug drug in lastYear.DrugList ) 
            {
                //WriteLine( $"{count,6:n0} - {drug}" );
                
                // calculate how many drugs contain VITAMIN in them
                if( drug.Name.Contains( "VITAMIN" ) )
                {
                    count ++;
                }
            }
            
            Drug[ ] vit = new Drug[ count ];
            
            int arrayIndex = 0;
            foreach( Drug drug in lastYear.DrugList )
            {
                if( drug.Name.Contains( "VITAMIN" ) )
                {
                    vit[ arrayIndex ] = drug;
                    arrayIndex ++;
                }    
            }
            
            for( int i = 0; i < vit.Length; i ++ )
            {
                WriteLine( $"{vit[i]}" );
            }
        }
    }
}
