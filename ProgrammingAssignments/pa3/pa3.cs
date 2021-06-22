using System;
using static System.Console;
using System.IO;
using System.Linq;

namespace Bme121.Pa3
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
    
    static partial class Program
    {
        static void Main( )
        {
            string inputFile  = @"21_training.csv";
            string outputFile = @"21_training_edges.csv";
            int height;  // image height (number of rows)
            int width;  // image width (number of columns)
            Color[ , ] inImage;
            Color[ , ] outImage;
            
            // Read the input image from its csv file.
            
            FileStream inputCSV = new FileStream( inputFile, FileMode.Open, FileAccess.Read ); 
            StreamReader read = new StreamReader( inputCSV );
            
            height = int.Parse( read.ReadLine( ) ); // obtain total number of rows
            width = int.Parse( read.ReadLine( ) ); // obtain total number of columns
            
            inImage = new Color[ height, width ]; // initialize array by total number of rows and columns
            outImage = new Color[ height, width ];
            
            for( int row = 0; row < height; row ++ )
            {
                string line = read.ReadLine( );
                string[ ] lineSplit = line.Split( ',' ); // split the line by commas
                for( int col = 0; col < width; col ++ )
                {
                    // look through line by every 4 numbers, and store the 4 as part of the inImage array
                    // stored values represent input pixel values
                    int a = int.Parse( lineSplit[ 4 * col + 0 ] );
                    int b = int.Parse( lineSplit[ 4 * col + 1 ] );
                    int c = int.Parse( lineSplit[ 4 * col + 2 ] );
                    int d = int.Parse( lineSplit[ 4 * col + 3 ] );
                    
                    inImage[ row,col ] = Color.FromArgb( a,b,c,d );
                }
            }
            
            read.Dispose( );
            inputCSV.Dispose( );
            
            // Generate the output image using Kirsch edge detection.
            
            //outImage = inImage; // replace output pixel values with input
            
            for( int row = 0; row < height; row ++ )
            {
                for( int col = 0; col < width; col ++ )
                {
                    // check around peripheries of the image file
                    if( row == 0 || row == height - 1 || col == 0 || col == width - 1 )
                    {
                        outImage[ row,col ] = inImage[ row,col ];
                    } else
                    {
                        outImage[ row,col ] = GetKirschEdgeValue( inImage[ row-1,col-1 ], inImage[ row-1,col ], inImage[ row-1,col+1 ], inImage[ row,col-1 ] ,inImage[ row,col ], inImage[ row,col+1 ], inImage[ row+1,col-1 ], inImage[ row+1,col ], inImage[ row+1,col+1 ] );
                    }
                }
            }
            
            // Write the output image to its csv file.
            
            FileStream outFile = new FileStream( outputFile, FileMode.Create, FileAccess.Write );
            StreamWriter write = new StreamWriter( outFile );
            
            write.WriteLine( height );
            write.WriteLine( width );
            for( int i = 0; i < height; i ++ )
            {
                string[ ] word = new string[ width ]; // array for line for output
                for( int j = 0; j < width; j ++ )
                {
                    Color pixel = outImage[ i,j ];
                    word[ j ] = pixel.A + "," + pixel.R + "," + pixel.G + "," + pixel.B; // get the A,R,G,B values and store in array
                }
                write.WriteLine( string.Join( ",", word ) );
            }
            
            write.Dispose( );
            outFile.Dispose( );
        }
        
        // This method computes the Kirsch edge-detection value for pixel color
        // at the centre location given the centre-location pixel color and the
        // colors of its eight neighbours.  These are numbered as follows.
        // The resulting color has the same alpha as the centre pixel, 
        // and Kirsch edge-detection intensities which are computed separately
        // for each of the red, green, and blue components using its eight neighbours.
        // c1 c2 c3
        // c4    c5
        // c6 c7 c8
        static Color GetKirschEdgeValue( 
            Color c1, Color c2,     Color c3, 
            Color c4, Color centre, Color c5, 
            Color c6, Color c7,     Color c8 )
        {
            // obtain the RGB values that form each output pixel
            int red = GetKirschEdgeValue( c1.R, c2.R, c3.R, c4.R, c5.R, c6.R, c7.R, c8.R );
            int green = GetKirschEdgeValue( c1.G, c2.G, c3.G, c4.G, c5.G, c6.G, c7.G, c8.G );
            int blue = GetKirschEdgeValue( c1.B, c2.B, c3.B, c4.B, c5.B, c6.B, c7.B, c8.B );
        
            return Color.FromArgb( 255,red,green,blue );
        }
        
        // This method computes the Kirsch edge-detection value for pixel intensity
        // at the centre location given the pixel intensities of the eight neighbours.
        // These are numbered as follows.
        // i1 i2 i3
        // i4    i5
        // i6 i7 i8
        static int GetKirschEdgeValue( 
            int i1, int i2, int i3, 
            int i4,         int i5, 
            int i6, int i7, int i8 )
        {
            // convulation values stored in array
            int[ ] valSum = new int[8];
            valSum[ 0 ] = 5 * ( i1 + i2 + i3 ) - 3 * ( i5  + i8 + i7 + i6 + i4 );
            valSum[ 1 ] = 5 * ( i2 + i3 + i5 ) - 3 * ( i1 + i4 + i6 + i7 + i8 );
            valSum[ 2 ] = 5 * ( i3 + i5 + i8 ) - 3 * ( i2 + i1 + i4 + i6 + i7 );
            valSum[ 3 ] = 5 * ( i5 + i8 + i7 ) - 3 * ( i3 + i2 + i1 + i4 + i6 );
            valSum[ 4 ] = 5 * ( i6 + i7 + i8 ) - 3 * ( i4 + i1 + i2 + i3 + i5 );
            valSum[ 5 ] = 5 * ( i4 + i6 + i7 ) - 3 * ( i1 + i2 + i3 + i5 + i8 );
            valSum[ 6 ] = 5 * ( i1 + i4 + i6 ) - 3 * ( i2 + i3 + i5 + i8 + i7 );
            valSum[ 7 ] = 5 * ( i4 + i1 + i2 ) - 3 * ( i3 + i5 + i8 + i7 + i6 );
            return Math.Max( 0,Math.Min( valSum.Max( ), 255 ) ); // max from operation to determine output image value for particular colour
        }
    }
    
    // Implementation of part of System.Drawing.Color.
    // This is needed because .Net Core doesn't seem to include the assembly 
    // containing System.Drawing.Color even though docs.microsoft.com claims 
    // it is part of the .Net Core API.
    struct Color
    {
        int alpha;
        int red;
        int green;
        int blue;
        
        public int A { get { return alpha; } }
        public int R { get { return red;   } }
        public int G { get { return green; } }
        public int B { get { return blue;  } }
        
        public static Color FromArgb( int alpha, int red, int green, int blue )
        {
            Color result = new Color( );
            result.alpha = alpha;
            result.red   = red;
            result.green = green;
            result.blue  = blue;
            return result;
        }
    }
}

