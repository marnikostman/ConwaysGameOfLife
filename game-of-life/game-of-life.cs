using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid gameOfLife = new Grid();
            gameOfLife.BaseGridSetup();
            gameOfLife.DrawGrid();
            while (true)
            {
                gameOfLife.ApplyRules();
                Console.Clear();
                gameOfLife.DrawGrid();
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
    class Grid
    {
        public int size = 20;
        public bool[,] baseGrid;
        public bool[,] changeGrid;
        Random rnd = new Random();

        public void BaseGridSetup() //Populates starting (base) grid based on random number generator selecting 0s or 1s
        {
            baseGrid = new bool[size, size];
            changeGrid = new bool[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (rnd.Next(2) > 0) //approximately 1/2 of cells should be live/true
                    {
                        baseGrid[i, j] = true;
                    }
                    else
                    {
                        baseGrid[i, j] = false;
                    }
                }
            }
        }
        public void DrawGrid () //draws baseGrid
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (baseGrid[i, j])
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }
                Console.Write("\n");
            }
        }
        public bool IsWithinRange(int number) //to be used to make sure not counting neighbors of cells that are out of array range
        {
            if (number < 0 || number > 19)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int CountLiveNeighbors(int x, int y) //input x and y axis and count neighbors (will be used on each cell in function below)
        {
            int liveNeighbors = 0; //Looking for LIVING neighbors
            int[] xArray = new int[] { -1, 0, 1, 1, 1, 0, -1, -1 }; //relative distance from x of neighbor
            int[] yArray = new int[] { -1, -1, -1, 0, 1, 1, 1, 0 }; //relative distance from y of neighbor
            for (int i = 0; i < 8; i++)
            {
                if (IsWithinRange(x + xArray[i]) && IsWithinRange(y + yArray[i]))
                {
                    if (baseGrid[(x + xArray[i]), (y + yArray[i])] == true)
                    {
                        liveNeighbors++;
                    }
                }
            }
            return liveNeighbors;
        }
        public void ApplyRules()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++) //Looking at every cell in baseGrid
                {
                    if (baseGrid[i, j]) //if this cell is alive
                    {
                        if (CountLiveNeighbors(i, j) > 3 || CountLiveNeighbors(i, j) < 2)
                        /*Any live cell with more than three live neighbours dies, as if by over-population.
                         * Any live cell with fewer than two live neighbours dies, as if caused by under-population.*/
                        {
                            changeGrid[i, j] = false;
                        }
                        //Any live cell will remain alive if it has exactly two or three neighbors
                    }
                    else //if the cell is dead...
                    {
                        if (CountLiveNeighbors(i, j) == 3)
                        //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                        {
                            changeGrid[i, j] = true;
                        }
                    }
                }
            }
            baseGrid = changeGrid; //changeGrid becomes the new baseGrid for next round of rules implementation
        }
    }
}
