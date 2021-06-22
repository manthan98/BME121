using System.Collections.Generic;
using System.IO;

// Classes related to California Department of Health Care Services
// Medi-Cal (a Medicaid welfare program for low-income individuals).
// See http://medi-cal.ca.gov or http://www.dhcs.ca.gov for more information.
namespace MediCal
{
    //------------------------------------------------------------------------------------------------------------------
    // A DrugRecords object holds a list of Drug objects built from one or more
    // Medi-Cal quarterly drug-utilization files.  
    partial class DrugRecords
    {
        List< Drug > drugList;
        
        // The "IEnumerable< Drug >" type represents the minimal support needed for some
        // collection of "Drug" objects to be the object of a "foreach" loop (i.e., it can
        // start at the beginning and return one element at a time).  This is done using 
        // something called an iterator (essentially an object which keeps track of where 
        // it is located in some collection).  A loop with "yield return" is the simplest
        // way to implement an iterator.  When that statement is reached, the value is 
        // returned but the current execution state is retained so the loop starts up
        // where it left off each time.  This is a slightly advanced topic which you can
        // read about later but which won't be tested in BME 121.
        public IEnumerable< Drug > DrugList
        {
            get
            {
                foreach( Drug drug in drugList )
                {
                    yield return drug;
                }
            }
        }
        
        // The "params" keyword lets the caller pass arguments either as one array or as
        // zero or more separate arguments of the array element type.  This is a detail
        // topic which may be handy to know about but which won't be tested in BME 121.
        public DrugRecords( params string[ ] drugFilePaths )
        {
            drugList = new List< Drug >( );
            
            foreach( string drugFilePath in drugFilePaths )
            using( FileStream inFile = File.Open( drugFilePath, FileMode.Open, FileAccess.Read ) )
            using( StreamReader reader = new StreamReader( inFile ) )
            {
                while( ! reader.EndOfStream )
                {
                    drugList.Add( Drug.ParseFileLine( reader.ReadLine( ) ) );
                }
            }
        }
    }
}    
