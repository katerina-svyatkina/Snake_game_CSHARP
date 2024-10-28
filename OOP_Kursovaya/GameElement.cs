using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Kursovaya
{
   abstract public class GameElem
    {
        private Point pos;
        private string type;
        private int val;

        private int size;
 
        public Point Pos
        {
            get => pos; set => pos = value;    
        }
        public string Type { get => type; set => type = value; }
        public int Val { get => val; set => val = value; }

        public int Size { get => size; set => size = value; }

        
    }
    public class Apple : GameElem
    {

        public Apple(Point point, int size, int val)
        {
            Pos = point;
            Type = "Apple";
            Val = val;    // may be different Apples with color and value
            Size = size;

        }
    }
    public class SnakeHead: GameElem
    {
       public SnakeHead(Point point, int size)
        {
            Pos = point;
            Type = "Head";
            Size = size;

        }
    }
    public class SnakeBody: GameElem
    {
        public SnakeBody (Point point, int size)
        {
            Pos = point;
            Type = "Body";
            Size = size;
        }
    }
}
