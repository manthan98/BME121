using System;
using static System.Console;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using MediCal;
using DataStructures;

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

// Classes implementing a singly linked list of Medi-Cal drug records.
namespace DataStructures
{
    // An object of the "LinkedList" class holds the user interface to a linked list of "Node" objects.
    // Data stored in the list is of type "Drug".
    partial class LinkedList : IEnumerable< Drug >
    {
    
        public LinkedList RemoveAll( Func< Drug, bool > test )
        {
            // new linked list to store removed nodes
            LinkedList x = new LinkedList( );
            
            int i = 0;
            Node current = head;
            
            while( i < this.count )
            {
                // run the func method on a drug object
                if( test.Invoke( current.Data ) )
                {
                    // move current reference to the next node before we delete the node
                    current = current.Next;
                    
                    // get rid of node i, and put its data into this x linked list
                    x.Append( this.RemoveAt( i ) );
                } else
                {
                    i ++;
                    current = current.Next;
                }
            }
            
            return x;
        }
        
        public Drug RemoveHead()
		{
			Drug data;
			if(count == 0)
			{
				data = null;
			}
			else if(count == 1)
			{
				// retrieve the data from the head node
				data = head.Data;
				// unlink the node
				head = null;
				tail = null;
				count --;
			}
			else
			{
				// create a temporary reference to the head node
				Node old = head;
				// point head reference to the next node
				head = head.Next;
				// unlink the old node
				old.Next = null;
				// retrieve the data from the old node
				data = old.Data;
				count --;
			}
			return data;
		}
        
        public Drug RemoveTail()
		{
            Drug data;
            if( count == 0 )
            {
                data = null;
            } else if( count == 1 )
            {
                // retrieve data in the node
                data = tail.Data;
                
                // set references to null
                head = null;
                tail = null;
                count --;
            } else
            {
                Node previous = head;
                Node current = head.Next;
                
                while( current != tail )
                {
                    previous = current;
                    current = current.Next;
                }
                
                tail = previous;
                tail.Next = null;
                
                data = current.Data;
                
                count --;
            }
            return data;
		}
        
        public Drug RemoveAt(int index)
		{
			if(index < 0 || index >= count)
			{
				return null;
			}
			else if(index == 0)
			{
				return RemoveHead();
			}
			else if(index == count - 1)
			{
				return RemoveTail();
			}
			else
			{
                Node previous = head;
				Node current = head.Next;
				int i = 1;
				
				// move both references together down the linked list
				// when i == index, then current == the node we want to remove
				while(i < index)
				{
					previous = current;
					current = current.Next;
					i ++;
				}
				
				// update the links
				previous.Next = current.Next;
				// unlink the old node
				current.Next = null;
				
				count --;
				
				return current.Data;
			}
		}
        
        partial class Node
        {
            Node next;
            Drug data;
            
            public Node Next { get { return next; } set { next = value; } }
            public Drug Data { get { return data; } }
            
            // User data must be provided when constructing a node.
            public Node( Drug data )
            {
                if( data == null ) throw new ArgumentNullException( nameof( data ) );
                this.next = null;
                this.data = data;
            }
        }
        
        Node tail;  // Last node in the linked list
        Node head;  // First node in the linked list
        int count;  // Number of nodes in the linked list
        
        public int Count  { get { return count; } }
        public int Length { get { return count; } }
        
        public LinkedList( ) { tail = null; head = null; count = 0; }
        
        // Add a new node at the head of the list.
        public void AddFirst( Drug newData ) { Prepend( newData ); }
        public void Prepend( Drug newData )
        {
            if( newData == null ) throw new ArgumentNullException( nameof( newData ) );
            
            Node oldHead = head;
            Node newNode = new Node( newData );
            
            if( tail == null ) tail = newNode;
            head = newNode;
            newNode.Next = oldHead;
            count ++;
        }
        
        // Add a new node at the tail of the list.
        public void AddLast( Drug newData ) { Append( newData ); }
        public void Append( Drug newData )
        {
            if( newData == null ) throw new ArgumentNullException( nameof( newData ) );
            
            Node oldTail = tail;
            Node newNode = new Node( newData );
            
            tail = newNode;
            if( head == null ) head = newNode;
            else oldTail.Next = newNode;
            count ++;
        }
        
        // Return a (shallow) copy of the list contents in an array.
        public Drug[ ] ToArray( )
        {
            Drug[ ] result = new Drug[ count ];
            
            Node currentNode = head;
            int currentIndex = 0;
            while( currentNode != null )
            {
                result[ currentIndex ] = currentNode.Data;
                currentNode = currentNode.Next;
                currentIndex ++;
            }
            
            return result;
        }
        
        // This is the simplest way to make the linked list implement the IEnumerable< Drug > interface
        // so the list itself may be the target of a foreach loop or use Linq.Enumerable extension methods.
        // You don't need to understand this code for BME 121.
        IEnumerator IEnumerable.GetEnumerator( ) { return ( ( IEnumerable< Drug > ) this ).GetEnumerator( ); }
        IEnumerator< Drug > IEnumerable< Drug >.GetEnumerator( )
        {
            Node currentNode = head;
            while( currentNode != null ) 
            {
                yield return currentNode.Data;
                currentNode = currentNode.Next;
            }
        }
    }
}

namespace Bme121.Pa4
{
    /// <StudentPlan>Biomedical Engineering</StudentPlan>
    /// <StudentDept>Department of Systems Design Engineering</StudentDept>
    /// <StudentInst>University of Waterloo</StudentInst>
    /// <StudentName>Last, First</StudentName>
    /// <StudentUserID>abcdefgh</StudentUserID>
    /// <StudentAcknowledgements>
    /// I declare that, except as acknowledged below, this is my original work.
    /// Acknowledged contributions of others:
    /// </StudentAcknowledgements>
    
    static partial class Program
    {
        // Test the "DataStructures.LinkedList.RemoveAll" method.
        static void Main( )
        {
            // use DrugRecords class to process the file
            DrugRecords drugs = new DrugRecords( @"RXQT1503-100.txt" ); // add file
            
            // store all drugs from text file in list
            LinkedList list = new LinkedList( );
            
            // note for self on foreach loops
            // "records" from defined above, "DrugList refers to collection"
            foreach( Drug d in drugs.DrugList  )
            {
                list.Append( d );
            }
            
            // Test case 1
            // show count of drugs in the original linked list before removals
            WriteLine( "original: {0}", list.Count );
            WriteLine( );
            // use criterion for removing nodes
            LinkedList removedNodes = list.RemoveAll( IsVitamin );
            
            // display the removed drugs
            foreach( Drug drug in removedNodes )
            {
                WriteLine( drug );
            }
            
            // display count of each list
            WriteLine( );
            WriteLine( "Int original: {0}", list.Count );
            WriteLine( "Int removed: {0}", removedNodes.Count );
        }
        
        // Func< Drug,bool >
        // Func is full type of method
        // last thing in <> is the return type of the method
        // everything before that are the input types, in order
        
        static bool IsVitamin( Drug x )
        {
            return x.Name.Contains( "VITAMIN" );
        }
    }
}
