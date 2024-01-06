using System.Collections.Generic;
namespace MouseMaze
{
    public class Cell
    {
        private int type;
        private int hold;
        private readonly int x;
        private readonly int y;
        private int[] walls;
        private bool visited;
        private List<Cell> ConnectCells;
        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
            walls = new int[4];
            for(int i = 0; i < walls.Length; i++)
                walls[i] = 1;
            type = 0;
            hold = 0;
            visited= false;
            ConnectCells = new();
        }
        public int GetCellType()
        {
            return type;
        }
        public void SetType(int type)
        {
            this.type = type;
        }
        public int GetHold()
        {
            return hold;
        }
        public void SetHold(int hold)
        {
            this.hold = hold;
        }
        public bool GetVisited()
        {
            return visited;
        }
        public void SetVisited(bool visited)
        {
            this.visited = visited;
        }
        public int GetX()
        {
            return x;
        }
        public int GetY()
        {
            return y;
        }
        public int GetWallIndex(int index)
        {
            return walls[index];
        }
        public void SetWallIndex(int index, int type)
        {
            this.walls[index] = type;
        }
        public void AddConnectCell(Cell cell)
        {
            ConnectCells.Add(cell);
        }
        public Cell GetConnectCell(int index)
        {
            return ConnectCells[index];
        }
        public void SetConnectCells(List<Cell> ConnectCells)
        {
            this.ConnectCells = ConnectCells;
        }
        public List<Cell> GetConnectCells()
        {
            return ConnectCells;
        }
    }
}
