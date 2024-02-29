using System;
using System.Collections.Generic;

namespace MouseMaze
{
    public class Mice
    {
        private int X;
        private int Y;
        private float sum = 0;
        private Cell OldCell;
        private Cell Target;
        private List<Cell> Visited;
        private List<Cell> Path;
        public Mice(int X, int Y, Cell OldCell)
        {
            this.X = X;
            this.Y = Y;
            this.OldCell = OldCell;
            Visited = new();
            Path = new();
        }
 
        public void Wander(Cell current, float dt) // Random Mouse Algorithm
        {
            Random rand = new();
            sum += dt;
            if (sum > .2)
            {
                sum = 0;
                Cell NextCell = current.GetConnectCell(rand.Next(current.GetConnectCells().Count));
                if (current.GetConnectCells().Count > 1 && current.GetConnectCell(0) != Target)
                    while (NextCell == OldCell)
                    {
                        NextCell = current.GetConnectCell(rand.Next(current.GetConnectCells().Count));
                    }
                X = NextCell.GetX();
                Y = NextCell.GetY();
                OldCell = current;
                current.SetHold(0);
            }
        }
        public bool CheeseHunt(Cell current, Cell target, float dt) // Depth First Search Path Generation to Cheese
        {
            if (Visited.Contains(current)) return false;
            Visited.Add(current);
            Path.Add(current);
            if (current == target) return true;
            foreach (Cell cell in current.GetConnectCells())
            if(CheeseHunt(cell, target, dt)) return true;
            Path.RemoveAt(Path.Count - 1);
            return false;
        }
        public void CheeseMove(Cell current, Cell target, float dt)
        {
            if (Path.Count > 0 && target == Path[^1]) // If the target is still the same, continue moving
            {
                sum += dt;
                if (sum > .2)
                {
                    sum = 0;
                    Cell NextCell = Path[0];
                    Path.RemoveAt(0);
                    X = NextCell.GetX();
                    Y = NextCell.GetY();
                    OldCell = current;
                    current.SetHold(0);
                }
            }
            else // else, generate a new path
            {
                Path = new();
                Visited = new();
                CheeseHunt(current, target, dt);
            }
        }
        public Cell NearestCheese(List<Cell> CheeseList) // Uses Manhattan Distance to find the nearest cheese
        {
            Cell nearest = CheeseList[0];
            foreach (Cell cheese in CheeseList)
            {
                if (ManHatDist(OldCell, cheese) < ManHatDist(OldCell, nearest))
                {
                    nearest = cheese;
                }
            }
            return nearest;
        }
        public static bool CheeseCheck(Cell current, List<Cell> CheeseList) // Checks if the mouse's current cell has cheese
        {
            foreach (Cell cheese in CheeseList)
            {
                if (current == cheese)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CanCheeseFind(List<Cell> list) // Is Cheese in the Maze
        {
            return list.Count > 0;
        }
        public static int ManHatDist(Cell current, Cell target)
        {
            return Math.Abs(current.GetX() - target.GetX()) + Math.Abs(current.GetY() - target.GetY());
        }
        public int GetX()
        {
            return X;
        }
        public int GetY()
        {
            return Y;
        }
    }
}

