using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillLineGame
{

    /// <summary>
    /// Координаты
    /// </summary>
    public class Coords
    {
        public int I, J;

        public Coords(int i, int j)
        {
            I = i;
            J = j;
        }

        public Coords Turn(int direction)
        {
            Coords newCoords;
            
            if(direction == 0)
                newCoords =  new Coords (I, J - 1);
            else if (direction == 1)
                newCoords =  new Coords (I - 1, J );
            else if (direction == 2)
                newCoords =  new Coords (I, J + 1);
            else
                newCoords =  new Coords (I + 1, J );
            Debug.WriteLine(string.Format("Coords turn info: i {0}, j {1}", newCoords.I, newCoords.J));
            return newCoords;
        }

        public void SetX(int i)
        {
            I = i;
        }

        public void SetY(int j)
        {
            J = j;
        }
    }
}
