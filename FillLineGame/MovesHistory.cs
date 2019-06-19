using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillLineGame
{
    public class MovesHistory
    {

        Stack<int> moves;

        public MovesHistory()
        {
            moves = new Stack<int>();
        }

        public void Move(int move){
            moves.Push(move);
        }

        public void MoveBack()
        {
            moves.Pop();
        }

        public bool IsMovingBack(int moveDirection)
        {
            return Math.Abs(moveDirection - moves.Peek()) == 2;
        }


    }
}
