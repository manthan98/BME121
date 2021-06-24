using System;
using static System.Console;

namespace Bme121.Wa4
{
    /// <StudentPlan>Biomedical Engineering</StudentPlan>
    /// <StudentDept>Department of Systems Design Engineering</StudentDept>
    /// <StudentInst>University of Waterloo</StudentInst>
    /// <StudentName>Shah, Manthan Shailesh Kumar</StudentName>
    /// <StudentUserID>mskshah - 20658832</StudentUserID>
    /// <StudentAcknowledgements>
    /// I declare that, except as acknowledged below, this is my original work.
    /// Acknowledged contributions of others:
    /// </StudentAcknowledgements>

    static partial class Program
    {
        static void Main( )
        {
            // Save a dice roll for each player.
            
            Dice player1Dice = new Dice( );
            Dice player2Dice = new Dice( );
            
            // Report info about the dice roll of each player.
            
            WriteLine( );
            
            Write( "Player 1, dice = ({0})", player1Dice.ToString() );
            Write( ", min = {0}, max = {1}", player1Dice.Min(), player1Dice.Max() );
            Write( ", sum = {0}, diff = {1}", player1Dice.Sum(), player1Dice.Difference() );
            Write( ", double = {0}", player1Dice.IsDouble() );
            WriteLine( ); 
            
            Write( "Player 2, dice = ({0})", player2Dice.ToString());
            Write( ", min = {0}, max = {1}", player2Dice.Min(), player2Dice.Max(), player2Dice.Max() );
            Write( ", sum = {0}, diff = {1}", player2Dice.Sum(), player2Dice.Difference() );
            Write( ", double = {0}", player2Dice.IsDouble() );
            WriteLine( ); 
            
            // Report the winner under different criteria.
            
            WriteLine( );
            
            WriteLine( "{0,18}   {1}", "Game", "Winner" );
            
            Write( "{0,18} : ", "Smallest minimum" );
            if(player1Dice.Min() > player2Dice.Min())
            {
                WriteLine( "Player 2" );
            } else if(player2Dice.Min() > player1Dice.Min())
            {
                WriteLine( "Player 1" );
            }
            
            Write( "{0,18} : ", "Largest maximum" );
            if(player1Dice.Max() > player2Dice.Max())
            {
                WriteLine( "Player 1" );
            } else if(player2Dice.Max() > player1Dice.Max())
            {
                WriteLine( "Player 2" );
            } else if(player1Dice.Max() == player2Dice.Max())
            {
                WriteLine("Tie");
            }
            
            Write( "{0,18} : ", "Largest sum" );
            if(player1Dice.Sum() > player2Dice.Sum())
            {
                WriteLine( "Player 1" );
            } else if(player2Dice.Sum() > player1Dice.Sum())
            {
                WriteLine( "Player 2" );
            } else if(player1Dice.Sum() == player2Dice.Sum())
            {
                WriteLine("Tie");
            }
            
            Write( "{0,18} : ", "Largest difference" );
            if(player1Dice.Difference() > player2Dice.Difference())
            {
                WriteLine( "Player 1" );
            } else if(player2Dice.Difference() > player1Dice.Difference())
            {
                WriteLine( "Player 2" );
            } else if(player1Dice.Difference() == player2Dice.Difference())
            {
                WriteLine("Tie");
            }
            
            Write( "{0,18} : ", "Only double" );
            if( player1Dice.IsDouble() == true )
            {
                WriteLine("Player 1");
            } else if(player2Dice.IsDouble() == true)
            {
                WriteLine( "Player 2" );
            } else
            {
                WriteLine("Tie");
            }
            WriteLine( );
        }
    }
    
    // Represent one roll of a pair of dice.
    class Dice
    {
        static Random randomNumberGenerator = new Random( );
        int diceValOne;
        int diceValTwo;
        
        public Dice( )
        {
            diceValOne = randomNumberGenerator.Next(1,7);
            diceValTwo = randomNumberGenerator.Next(1,7);
        }
        
        public int Min( ) 
        { 
            return Math.Min(diceValOne, diceValTwo);
        }
        
        public int Max( ) 
        { 
            return Math.Max(diceValOne, diceValTwo);
        }
        
        public int Sum( ) 
        {
            return Math.Max(diceValOne, diceValTwo) + Math.Min(diceValOne, diceValTwo);
        }
        
        public int Difference( )
        {
            return Math.Max(diceValOne, diceValTwo) - Math.Min(diceValOne, diceValTwo);
        }
        
        public bool IsDouble( ) 
        {
            if( diceValOne == diceValTwo )
            {
                return true;
            } else
            {
                return false;
            }
        }
        
        public override string ToString( ) 
        { 
            return diceValOne + " , " + diceValTwo; 
        }
    }
}
