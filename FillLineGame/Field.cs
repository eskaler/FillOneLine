using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillLineGame
{
    /// <summary>
    /// Поле уровня
    /// </summary>
    public class Field
    {
        public Coords startPoint;

        private Cell[,] field;
        private int fieldWidth;
        private int fieldHeight;

        public Field(string fieldFilePath)
        {

            //чтение уровня из текстового файла
            var Lines = File.ReadLines(fieldFilePath, Encoding.UTF8);

            fieldHeight = Lines.Count();
            fieldWidth = Lines.First().Count();

            field = new Cell[fieldHeight, fieldWidth];

            for(int i = 0; i < fieldHeight; i++)
            {
                string line = Lines.ElementAt(i);
                for(int j = 0; j < fieldWidth; j++)
                {
                    
                    int cellType = int.Parse(line.ElementAt(j).ToString());
                    Debug.WriteLine(cellType);
                    if (cellType == 2)
                    {
                        startPoint = new Coords(i, j);
                        field[i, j] = new Cell(2);
                    }
                    else
                        field[i, j] = new Cell(cellType);
                }

            }
        }

        public int GetFieldWidth()
        {
            return fieldWidth;
        }

        public int GetFieldHeight()
        {
            return fieldHeight;
        }

        public Coords GetStartPoint()
        {
            return startPoint;
        }

        public bool NoEmptyCellsLeft()
        {
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    if (field[i, j].IsEmpty())
                        return false;
                }
            }

            return true;
        }

        public bool IsStart(Coords coords)
        {
            return coords.I == startPoint.I && coords.J == startPoint.J;
        }

        public bool IsCellAnObstacle(Coords coords)
        {
            Debug.WriteLine(string.Format("IsCellAnObstacle info: x {0}, y {1}, io {2}", coords.I, coords.J, field[coords.I, coords.J].IsObstacle()));
            return field[coords.I, coords.J].IsObstacle();
        }

        public bool IsCellFilled(Coords coords)
        {
            Debug.WriteLine(string.Format("IsCellFilled info: x {0}, y {1}, if {2}", coords.I, coords.J, field[coords.I, coords.J].IsFilled()));
            return field[coords.I, coords.J].IsFilled();
        }

        public bool IsCellInBounds(Coords coords)
        {
            Debug.WriteLine(string.Format("IsCellInbound info: x {0}, fw {1}, y {2}, fh {3}", coords.I, fieldWidth, coords.J, fieldHeight));
            return
                coords.I >= 0 &&
                coords.I < fieldHeight &&
                coords.J >= 0 &&
                coords.J < fieldWidth;

        }

        public int IsCellAvailable(Coords coords)
        {
            if (IsCellInBounds(coords))
            {
                Debug.WriteLine("Step1: cell is inbound");
                if (IsCellFilled(coords))
                {
                    Debug.WriteLine("Step2: cell is filled");
                    return 2;
                }
                else if (IsCellAnObstacle(coords))
                {
                    Debug.WriteLine("Step3: cell is obstacle");
                    return -1;
                }
                else
                {
                    Debug.WriteLine("Step4: cell is good to go");
                    return 0;
                }

            }
            else
            {
                Debug.WriteLine("Step0: cell is out of bounds");
                return -1;
            }
            
        }

        public void SetEmptyCell(Coords coords)
        {
            field[coords.I, coords.J] = new Cell(0);
        }

        public void FillCell(Coords coords)
        {
            field[coords.I, coords.J] = new Cell(2);
        }

    }
}
