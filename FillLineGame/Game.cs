using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace FillLineGame
{
    public class Game
    {

        public Field GameField;
        public MovesHistory Moves;
        private Coords playerCoords;

        private int movesCount;
        

        public Game(string path)
        {
            GameField = new Field(path);
            Moves = new MovesHistory();
            playerCoords = GameField.GetStartPoint();
            movesCount = 0;

            
        }



        public int GetMovesCount()
        {
            return movesCount;
        }

        
        public bool IsWin()
        {
            return GameField.NoEmptyCellsLeft();
            
        }

        //Обработка движения
        public void Move(int moveDirection)
        {
            Coords tempCoords = playerCoords.Turn(moveDirection);
            int cellCheckResult = GameField.IsCellAvailable(tempCoords);

            if ( cellCheckResult == 2 && //hit itself
                Moves.IsMovingBack(moveDirection)) // and moving back
            {
                Debug.WriteLine(string.Format("AHEAD: filled ({0}, {1})", tempCoords.I, tempCoords.J));
                GameField.SetEmptyCell(playerCoords);
                playerCoords = tempCoords;
                Moves.MoveBack();
                movesCount++;
            }
            else if(cellCheckResult == 0)
            {
                Debug.WriteLine(string.Format("AHEAD: Empty ({0}, {1})", tempCoords.I, tempCoords.J));
                Moves.Move(moveDirection);
                playerCoords = tempCoords;
                GameField.FillCell(playerCoords);
                movesCount++;
            }
        }

        
    }
}
