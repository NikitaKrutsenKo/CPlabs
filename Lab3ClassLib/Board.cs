using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3ClassLib
{
    public class Board
    {
        public int sizeX;
        public int sizeY;
        public Field[,] board;
        public Board(int sizeX, int sizeY) 
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            board = new Field[sizeX, sizeY];
            InitFields(sizeX, sizeY);
        }

        private void InitFields(int sizeX, int sizeY) 
        {
            for (int i = 0; i < sizeX; i++)
            {
                for(int j = 0; j < sizeY; j++)
                {
                    board[i, j] = new Field();
                }
            }
        }
    }
}
