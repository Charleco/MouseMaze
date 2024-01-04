using PrimitiveBuddy;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MouseMaze
{
    public class Maze
    {
        private readonly int width;
        private readonly int height;
        private readonly Cell[,] cells;
        private readonly int scale;
        private Stack<Cell> stack;
        private List<Mice> MouseList;
        private readonly List<Cell> CheeseList;
        public Maze(int scale, int width, int height )
        {
            this.scale = scale;
            this.width = width/scale;
            this.height = height/scale;
            cells = new Cell[this.height,this.width];
            stack= new Stack<Cell>();
            MouseList = new List<Mice>();
            CheeseList = new List<Cell>();
        }
        public void AddCheese(int x, int y)
        {
            if (cells[y, x].GetHold() == 0)
            {
                cells[y, x].SetHold(1);
                CheeseList.Add(cells[y, x]);
            }
        }
        public void AddMouse(int x, int y)
        {
            if (cells[y, x].GetHold() == 0)
            {
                Mice mouse = new(x, y, cells[y,x]);
                MouseList.Add(mouse);
            }
        }
        public void MouseListUpdate(float dt)
        {
            foreach(Mice mouse in MouseList)
            {
                int x = mouse.GetX();
                int y = mouse.GetY();
                cells[y, x].SetHold(2);
                if (Mice.CheeseFind(CheeseList))
                {
                    mouse.CheeseMove(cells[y, x], mouse.NearestCheese(CheeseList), dt);
                    if (Mice.CheeseCheck(cells[y, x], CheeseList))
                    {
                        cells[y, x].SetHold(0);
                        CheeseList.Remove(mouse.NearestCheese(CheeseList));
                    }
                }
                else
                {
                    mouse.Wander(cells[y, x], dt);
                }
                    
            }
        }   
        public int GetScale()
        {
            return scale;
        }
        public void InitMaze()
        {
            for(int y = 0; y< height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    cells[y, x] = new Cell(x, y);
                    cells[y, x].SetType(0);
                }
            }
            MazeGen();
        }
        public void MazeGen()
        {
            cells[1,1].SetVisited(true);
            stack.Push(cells[1, 1]);
            while(stack.Count > 0)
            {
                Cell current = stack.Pop();
                List<Cell> neighbors = GetNeighbors(current);
                if(neighbors.Count > 0)
                {
                    stack.Push(current);
                    Cell next = neighbors[new Random().Next(neighbors.Count)];
                    RemoveWalls(current, next);
                    next.SetVisited(true);
                    stack.Push(next);
                }
            }
        }
        public List<Cell> GetNeighbors(Cell cell)
        {
            List<Cell> neighbors = new();
            int x = cell.GetX();
            int y = cell.GetY();
            if(x > 0 && !cells[y, x - 1].GetVisited())
                neighbors.Add(cells[y, x - 1]);

            if(x < width - 1 && !cells[y, x + 1].GetVisited())
                neighbors.Add(cells[y, x + 1]);

            if(y > 0 && !cells[y - 1, x].GetVisited())
                neighbors.Add(cells[y - 1, x]);

            if(y < height - 1 && !cells[y + 1, x].GetVisited())
                neighbors.Add(cells[y + 1, x]);

            return neighbors;
        }
        public static void RemoveWalls(Cell current, Cell next)
        {
            int x = current.GetX() - next.GetX();
            if(x == 1)
            {
                current.SetWallIndex(3, 0);
                current.ConnectCells.Add(next);
                next.SetWallIndex(1, 0);
                next.ConnectCells.Add(current);
            }
            else if(x == -1)
            {
                current.SetWallIndex(1, 0);
                current.ConnectCells.Add(next);
                next.SetWallIndex(3,0);
                next.ConnectCells.Add(current);
            }
            int y = current.GetY() - next.GetY();
            if(y == 1)
            {
                current.SetWallIndex(0, 0);
                current.ConnectCells.Add(next);
                next.SetWallIndex(2, 0);
                next.ConnectCells.Add(current);
            }
            else if(y == -1)
            {
                current.SetWallIndex(2, 0);
                current.ConnectCells.Add(next);
                next.SetWallIndex(0, 0);
                next.ConnectCells.Add(current);
            }
        }
        public void DrawMaze(Primitive prim)
        {
            for(int y = 0; y < height; y++) 
            {
                for(int x = 0; x<width;x++)
                {
                    for(int i = 0; i < 4; i++)
                    {
                        if (cells[y, x].GetWallIndex(i) == 1)
                        {
                            Color WallColor = new(95, 204, 134);
                            if(i == 0)
                            {
                                prim.Line(new Vector2(x * scale, y * scale), new Vector2((x * scale)+scale , (y * scale)), WallColor);
                            }
                            else if(i == 1)
                            {
                                prim.Line(new Vector2((x * scale)+scale , y * scale), new Vector2((x * scale)+scale, (y * scale)+scale ), WallColor);
                            }
                            else if(i == 2)
                            {
                                prim.Line(new Vector2((x * scale), (y * scale)+scale ), new Vector2((x * scale)+scale, (y * scale) + scale), WallColor);
                            }
                            else if(i == 3)
                            {
                                prim.Line(new Vector2(x * scale, (y * scale)), new Vector2(x * scale, (y * scale) + scale), WallColor);
                            }
                        }
                    }
                }
            }
        }
        public void DrawSprites(SpriteBatch Batch, Texture2D MouseText, Texture2D CheeseText)
        {
            foreach(Mice mouse in MouseList)
            {
                Batch.Draw(MouseText, new Vector2((mouse.GetX() * scale)+scale/2, (mouse.GetY() * scale)+scale/2),null, Color.White,0f,new Vector2(MouseText.Width/2,MouseText.Height/2),new Vector2(scale/ MouseText.Width, scale/ MouseText.Height),SpriteEffects.None,0f);
            }
            foreach (Cell cell in CheeseList)
            {
                Batch.Draw(CheeseText, new Vector2((cell.GetX() * scale) + scale / 2, (cell.GetY() * scale) + scale / 2), null, Color.White, 0f, new Vector2(CheeseText.Width / 2, CheeseText.Height / 2), new Vector2(scale / CheeseText.Width, scale / CheeseText.Height), SpriteEffects.None, 0f);
            }
        }

    }
}
