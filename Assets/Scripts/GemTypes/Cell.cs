using UnityEngine;
using Grid = Base.Grid;

namespace GemTypes
{
    public class Cell
    {
        public Grid GridObject { get; set; }
        public CellTypes CellType { get; set; }
        public enum CellTypes
        {
            Bomb = 0,
            Horizontal = 1,
            Vertical = 2,
            Normal = 3
        }
    }
}