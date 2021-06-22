using System;
using static System.Console;

namespace Bme121.Pa2
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

    // Procedural-programming implementation of the game Connect Four.
    // Note: Connect Four is (C) Hasbro, Inc.
    // This version is for educational use only.
    
    static partial class Program
    {
        // For random moves by AI players.
        static Random randomNumberGenerator = new Random( );
        
        // The game board and intuitive names for its size.
        // Each element of the game board is initially null.
        // In valid play, an element may become "O" or "X".
        static string[ , ] gameBoard = new string[ 6, 7 ];
        static readonly int gameRows = gameBoard.GetLength( 0 );
        static readonly int gameCols = gameBoard.GetLength( 1 );
        
        // The playing piece colors can be altered.
        static readonly ConsoleColor fgColor = ForegroundColor;
        static readonly ConsoleColor xColor = ConsoleColor.Cyan;
        static readonly ConsoleColor oColor = ConsoleColor.Magenta;
        
        // Save the two player's names and kinds (human or AI).
        static string xName, oName;
        static string xKind, oKind;
        
        // The symbol (O or X), name, and kind of the current player.
        static string currentPlayer;
        static string currentPlayerName;
        static string currentPlayerKind;
    
        // Play the game, largely by calling game methods.
        static void Main( )
        {
            WriteLine( );
            WriteLine( "BME 121 Connect Four!" );
            WriteLine( );
            WriteLine( "The game of Connect Four is (C) Hasbro, Inc." );
            WriteLine( "This version is for educational use only." );
            WriteLine( );
            WriteLine( "Play by stacking your token in any column with available space." );
            WriteLine( "Win with four-in-a-row vertically, horizontally, or diagonally." );
            
            DrawGameBoard( );
            
            GetPlayerNames( );
            GetPlayerKinds( );
            GetFirstPlayer( );
            
            while( ! IsBoardFull( ) )
            {
                GetPlayerMove( );
                DrawGameBoard( );
                
                if( CurrentPlayerWins( ) )
                {
                    WriteLine( );
                    Write( $"{currentPlayerName} - " );
                    ColorWrite( currentPlayer );
                    WriteLine( " - wins!" );
                    WriteLine( );
                    return;
                }
                
                SwitchPlayers( );
            }
            
            WriteLine( );
            WriteLine( "Game is a draw!" );
            WriteLine( );
        }
        
        // Get the displayed names of the two players.
        static void GetPlayerNames( )
        {
            WriteLine( );
            Write( "Enter player O name: " );
            oName = ReadLine( );
            Write( "Enter player X name: " );
            xName = ReadLine( );
        }
        
        // Get the kinds (human or ai) of the two players.
        static void GetPlayerKinds( )
        {
            WriteLine( );
            
            while( true )
            {
                Write( "Enter player O kind [human ai]: " );
                oKind = ReadLine( ).ToLower( );
                if( oKind == "human" ) break;
                if( oKind == "ai" ) break;
                WriteLine( "Must be one of 'human' or 'ai'." );
                WriteLine( "Please try again." );
            }
            
            while( true )
            {
                Write( "Enter player X kind [human ai]: " );
                xKind = ReadLine( ).ToLower( );
                if( xKind == "human" ) break;
                if( xKind == "ai" ) break;
                WriteLine( "Must be one of 'human' or 'ai'." );
                WriteLine( "Please try again." );
            }
        }
        
        // Get and set up the player who will play first.
        static void GetFirstPlayer( )
        {
            WriteLine( );
            
            while( true )
            {
                Write( "Enter first to play [O X]: " );
                currentPlayer = ReadLine( ).ToUpper( );
                if( currentPlayer == "O" ) break;
                if( currentPlayer == "X" ) break;
                WriteLine( "Must be one of 'O' or 'X'." );
                WriteLine( "Please try again." );
            }
            
            if( currentPlayer == "O" )
            {
                currentPlayerName = oName;
                currentPlayerKind = oKind;
            }
            
            if( currentPlayer == "X" )
            {
                currentPlayerName = xName;
                currentPlayerKind = xKind;
            }
        }
        
        // Get and perform the desired move by the current player.
        static void GetPlayerMove( )
        {
            if( currentPlayerKind == "ai" ) 
            {
                WriteLine( );
                Write( $"{currentPlayerName} - " );
                ColorWrite( currentPlayer );
                Write( " - choose a column: " );
                int column = SelectRandomColumn( );
                System.Threading.Thread.Sleep( 1000 );
                Write( column );
                System.Threading.Thread.Sleep( 1000 );
                WriteLine( );
                PlayInColumn( column );
            }
            
            if( currentPlayerKind == "human" )
            {
                while( true )
                {
                    WriteLine( );
                    Write( $"{currentPlayerName} - " );
                    ColorWrite( currentPlayer );
                    Write( " - choose a column: " );
                    int column;
                    if( ! int.TryParse( ReadLine( ), out column ) || ! IsValidPlay( column ) )
                    {
                        WriteLine( "Not a valid column or column is full." );
                        WriteLine( "Please try again." );
                    }
                    else
                    {
                        PlayInColumn( column );
                        break;
                    }
                }
            }
        }
        
        // Detect whether the current player has won by looking for a vertical,
        // horizontal, or diagonal run of four of the current player's symbols.
        static bool CurrentPlayerWins( )
        {
            // vertical, horizontal check from row 0 to 2 and column 0 to 3
            for(int i = 0; i <= 2; i ++) // loops rows
            { 
                for(int j = 0; j <= 3; j ++) // loops columns
                {
                    if(gameBoard[i,j] == currentPlayer) // takes loop values and checks for looks at different possibilities for victory
                    {
                        if(gameBoard[i+1,j] == currentPlayer && gameBoard[i+2,j] == currentPlayer && gameBoard[i+3,j] == currentPlayer) // checks for horizontal (row) right victory
                        {
                            return true;
                        } else if(gameBoard[i,j+1] == currentPlayer && gameBoard[i,j+2] == currentPlayer && gameBoard[i,j+3] == currentPlayer) // checks for vertical, column victory
                        {
                            return true;
                        } else if(gameBoard[i+1,j+1] == currentPlayer && gameBoard[i+2,j+2] == currentPlayer && gameBoard[i+3,j+3] == currentPlayer) // checks for right diagonal victory
                        {
                            return true;
                        }
                    }
                }
            }
            
            // vertical, horizontal check from row 0 to 2, and column 3 to 6
            for(int i = 0; i <= 2; i ++)
            {
                for(int j = 3; j <= 6; j ++)
                {
                    if(gameBoard[i,j] == currentPlayer)
                    {
                        if(gameBoard[i+1,j] == currentPlayer && gameBoard[i+2,j] == currentPlayer && gameBoard[i+3,j] == currentPlayer) // horizontal rig
                        {
                            return true;
                        } else if(gameBoard[i,j-1] == currentPlayer && gameBoard[i,j-2] == currentPlayer && gameBoard[i,j-3] == currentPlayer)
                        {
                            return true;
                        } else if(gameBoard[i+1,j-1] == currentPlayer && gameBoard[i+2,j-2] == currentPlayer && gameBoard[i+3,j-3] == currentPlayer)
                        {
                            return true;
                        }
                    }
                }
            }
            
            for(int i = 3; i <= 5; i ++) //vertical, horizontal, diagonal check from row 3 to 5, and column 0 to 3
            {
                for(int j = 0; j <= 3; j ++)
                {
                    if(gameBoard[i,j] == currentPlayer)
                    {
                        if(gameBoard[i-1,j] == currentPlayer && gameBoard[i-2,j] == currentPlayer && gameBoard[i-3,j] == currentPlayer)
                        {
                            return true;
                        } else if(gameBoard[i,j+1] == currentPlayer && gameBoard[i,j+2] == currentPlayer && gameBoard[i,j+3] == currentPlayer)
                        {
                            return true;
                        } else if(gameBoard[i-1,j+1] == currentPlayer && gameBoard[i-2,j+2] == currentPlayer && gameBoard[i-3,j+3] == currentPlayer)
                        {
                            return true;
                        }
                    }
                }
            }
            
            for(int i = 3; i <= 5; i ++) //vertical, horizontal, diagonal check from row 3 to 5, and column 3 to 6
            {
                for(int j = 3; j <= 6; j ++)
                {
                    if(gameBoard[i,j] == currentPlayer)
                    {
                        if(gameBoard[i-1,j] == currentPlayer && gameBoard[i-2,j] == currentPlayer && gameBoard[i-3,j] == currentPlayer)
                        {
                            return true;
                        } else if(gameBoard[i,j-1] == currentPlayer && gameBoard[i,j-2] == currentPlayer && gameBoard[i,j-3] == currentPlayer)
                        {
                            return true;
                        } else if(gameBoard[i-1,j-1] == currentPlayer && gameBoard[i-2,j-2] == currentPlayer && gameBoard[i-3,j-3] == currentPlayer)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        
        // Detect whether the game board is completely filled.
        static bool IsBoardFull( )
        {
            // check every column row 5
            if( gameBoard[5,0] != null && gameBoard[5,1] != null && gameBoard[5,2] != null && gameBoard[5,3] != null && gameBoard[5,4] != null && gameBoard[5,5] != null )
            {
                return true;
            } else
            {
                return false;
            }
        }
        
        // Detect whether given column is on the board and has space remaining.
        static bool IsValidPlay( int col )
        {
            // as long as row 5 is empty there is space in the column
            while( gameBoard[5,col] == null )
            {
                return true;
            }
            return false;
        }
        
        // Play current player's symbol on top of existing plays in the selected column.
        static void PlayInColumn( int col )
        {
        
            int i = 0;
            while( i < 6 ) //loop to find empty/null spaces and display symbol
            {
                if(gameBoard[i,col] == null)
                {
                    gameBoard[i,col] = currentPlayer; // while null fill in space with symbol
                    Write("{0}", gameBoard[i,col]);
                    break;
                } else if( gameBoard[i,col] != null ) // if not null, check next row
                {
                    i++;
                }
            }
        }
        
        // Select a column at random until a valid play is found.
        static int SelectRandomColumn( )
        {
            int colVal;
            do
            {
                colVal = randomNumberGenerator.Next(0,7);
            } while ( IsValidPlay(colVal) != true );
            return colVal;
        }
        
        // Change the current player from player O to player X or vice versa.
        static void SwitchPlayers( )
        {
            if( currentPlayer == "X" )// TO DO (1):  Complete this method.
            {
                currentPlayer = "O";
                currentPlayerName = oName;
                currentPlayerKind = oKind;
            } else if( currentPlayer == "O" )
            {
                currentPlayer = "X";
                currentPlayerName = xName;
                currentPlayerKind = xKind;
            }
        }
        
        // Display the current game board on the console.
        // This version uses only ASCII characters for portability.
        static void DrawGameBoard( )
        {
            WriteLine( );
            for( int row = gameRows - 1; row >= 0; row -- )
            {
                Write( "   |" );
                for( int col = 0; col < gameCols; col ++ ) Write( "   |" ); 
                WriteLine( );
                Write( $"{row,2} |" );
                for( int col = 0; col < gameCols; col ++ ) 
                {
                    Write( " " );
                    ColorWrite( gameBoard[ row, col ] );
                    Write( " |" );
                }
                WriteLine( );
            }
            Write( "   |" );
            for( int col = 0; col < gameCols; col ++ ) Write( "___|" ); 
            WriteLine( );
            WriteLine( );
            Write( "    " );
            for( int col = 0; col < gameCols; col ++ ) Write( $"{col,2}  " ); 
            WriteLine( );
        }
        
        // Display O or X in their special color.
        static void ColorWrite( string symbol )
        {
            if( symbol == "O" ) ForegroundColor = oColor;
            if( symbol == "X" ) ForegroundColor = xColor;
            // Empty cells in the game board use null but
            // we still want them to display using one space.
            Write( $"{symbol,1}" );
            ForegroundColor = fgColor;
        }
    }
}
