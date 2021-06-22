using System;
using System.Collections;
using System.Collections.Generic;

using MediCal;

namespace DataStructures
{
    partial class LinkedList : IEnumerable< Drug >
    {
        partial class Node
        {
            Node next;
            Drug data;
            
            public Node Next { get { return next; } set { next = value; } }
            public Drug Data { get { return data; } }
            
            public Node( Drug data )
            {
                if( data == null ) throw new ArgumentNullException( nameof( data ) );
                this.next = null;
                this.data = data;
            }
        }
        
        Node tail;
        Node head;
        int count;
        
        public int Count { get { return count; } }
        
        public LinkedList( ) { tail = null; head = null; count = 0; }
        
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
