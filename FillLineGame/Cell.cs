using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillLineGame
{
    /// <summary>
    /// Ячейка
    /// </summary>
    class Cell
    {
        private bool isObstacle = false;    //препятствие
        private bool isFilled = false;      //заполнена

        public Cell(int cellInfo)
        {
            if (cellInfo == 1)
                isObstacle = true;
            else if (cellInfo == 2)
                isFilled = true;
        }

        public bool IsObstacle()
        {
            return isObstacle;
        }

        public bool IsFilled()
        {
            return isFilled;
        }

        public bool IsEmpty()
        {
            return !isObstacle && !isFilled;
        }
    }
}
