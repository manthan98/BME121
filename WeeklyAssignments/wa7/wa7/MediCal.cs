using System;
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
    
    //------------------------------------------------------------------------------------------------------------------
    // A Drug object holds information about one fee-for-service outpatient drug 
    // reimbursed by Medi-Cal to pharmacies.
    partial class Drug
    {
        // All fields are private.
        string code;            // old Medi-Cal drug code
        string name;            // brand name, strength, dosage form
        string id;              // national drug code number
        double size;            // package size
        string unit;            // unit of measurement
        double quantity;        // number of units dispensed
        double lowest;          // price Medi-Cal is willing to pay
        double ingredientCost;  // estimated ingredient cost
        int    numTar;          // number of claims with a 'treatment authorization request'
        double totalPaid;       // total amount paid
        double averagePaid;     // average paid per prescription
        int    daysSupply;      // total days supply
        int    claimLines;      // total number of claim lines
        
        // Properties providing read-only access to every field.
        public string Code           { get { return code;           } }               
        public string Name           { get { return name;           } }               
        public string Id             { get { return id;             } }                 
        public double Size           { get { return size;           } }             
        public string Unit           { get { return unit;           } }             
        public double Quantity       { get { return quantity;       } }         
        public double Lowest         { get { return lowest;         } }             
        public double IngredientCost { get { return ingredientCost; } }    
        public int    NumTar         { get { return numTar;         } }                
        public double TotalPaid      { get { return totalPaid;      } }          
        public double AveragePaid    { get { return averagePaid;    } }        
        public int    DaysSupply     { get { return daysSupply;     } }            
        public int    ClaimLines     { get { return claimLines;     } }            
        
        // Hide the default constructor by providing a do-nothing private parameterless constructor.  
        // We provide no other constructors so the user must call "ParseFileLine" to get a new "Drug" object.
        Drug( ) { }
        
        // Simple string for debugging purposes, showing only selected fields.
        // We assume the combination of these selected fields is unique for each drug.
        public override string ToString( ) { return $"{id}: {name}, {size}"; }
        
        // Parse a string of the form used for each line in the file of drug data.
        // Mostly there are specific columns in the file for each piece of information.
        // The exception is that 'size' and 'unit' are concatenated.  They are collected
        // together and then separated by noting that 'unit' is always the last two characters.
        // Note:  The document describing the file layout doesn't quite match the file.
        // The field widths of "id" and "averagePaid" are two characters longer than stated.
        // The field "daysSupply" seems to use an exponential notation for numbers of a million or larger.
        // This method has been fully tested on the Medi-Cal quarterly data file "RXQT1503.txt".
        public static Drug ParseFileLine( string line )
        {
            if( line == null ) throw new ArgumentNullException( "String is null.", nameof( line ) );
            if( line.Length != 158 ) throw new ArgumentException( "Length must be 158", nameof( line ) );
            
            Drug newDrug = new Drug( );
            newDrug.code = line.Substring( 0, 7 ).Trim( );
            newDrug.name = line.Substring( 7, 30 ).Trim( );
            newDrug.id = line.Substring( 37, 13 ).Trim( );
            string sizeWithUnit = line.Substring( 50, 14 ).Trim( );
            newDrug.size = double.Parse( sizeWithUnit.Substring( 0 , sizeWithUnit.Length - 2 ) );
            newDrug.unit = sizeWithUnit.Substring( sizeWithUnit.Length - 2, 2 );
            newDrug.quantity = double.Parse( line.Substring( 64, 16 ) );
            newDrug.lowest = double.Parse( line.Substring( 80, 10 ) );
            newDrug.ingredientCost = double.Parse( line.Substring( 90, 12 ) );
            newDrug.numTar = int.Parse( line.Substring( 102, 8 ) );
            newDrug.totalPaid = double.Parse( line.Substring( 110, 14 ) );
            newDrug.averagePaid = double.Parse( line.Substring( 124, 10 ) );
            newDrug.daysSupply = ( int ) double.Parse( line.Substring( 134, 14 ) );  // large values are in 'e' format
            newDrug.claimLines = int.Parse( line.Substring( 148, 10 ) );
            return newDrug;
        }
        
        // Produce a string of the form used for each line in the file of drug data.
        // Note:  The document describing the file layout doesn't quite match the file.
        // The field widths of "id" and "averagePaid" are two characters longer than stated.
        // The field "daysSupply" seems to use an exponential notation for numbers of a million or larger.
        // This method has been fully tested on the Medi-Cal quarterly data file "RXQT1503.txt".
        public string ToFileLine( )
        {
            string sizeWithUnit = string.Concat( size.ToString( "f3" ), unit );
            string daysSupplyFormatted;
            if( daysSupply >= 1000000 ) daysSupplyFormatted = daysSupply.ToString( "0.#####e+000" );
            else daysSupplyFormatted = daysSupply.ToString( "f0" );
            
            return $"{code,-7}{name,-30}{id,-13}{sizeWithUnit,-14}{quantity,-16:f0}"
                + $"{lowest,-10:#.0000;-#.0000}{ingredientCost,-12:#.00;-#.00}{numTar,-8}"
                + $"{totalPaid,-14:#.00;-#.00}{averagePaid,-10:#.00;-#.00}"
                + $"{daysSupplyFormatted,-14}{claimLines,-10}";
        }
    }
}
