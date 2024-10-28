using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Kursovaya
{
    public class GameController
    {
        private Point dir;
        private Keys key;
        private Snake _snake;
        private ViewPort _viewPort;
        private GameField _gameField;
        private Collision _collision;
        
        public GameController(ViewPort vp, GameField gf)
        {
            key = 0;
            _viewPort = vp;
            _gameField = gf;
            _snake = new Snake(gf.ElemSize, gf, _viewPort);
            dir = _snake.Dir;
            _collision = new Collision(_snake, _gameField);
        }
        public void Load(List<GameElem> snake, Point dir)
        {
            this.dir =_snake.Dir = dir;
            _snake.Load(snake);
        }
        public List<GameElem> GetGameElems()
        {
            return _snake.GetSnakeElems();
        }
        public void SetKey(Keys k)
        {
            key = k;
            dir = _snake.Dir;
           switch (key)
            {
                case Keys.Up:
                    if (dir.X != 0 && dir.Y != 1)
                    {
                        dir.X = 0;
                        dir.Y = -1;
                    }
                    break;
                case Keys.Down:
                    if (dir.X != 0 && dir.Y != -1)
                    {
                        dir.X = 0;
                        dir.Y = 1;
                    }
                    break;
                case Keys.Right:
                    if (dir.X != -1 && dir.Y != 0)
                    {
                        dir.X = 1;
                        dir.Y = 0;
                    }
                    break;
                case Keys.Left:
                    if(dir.X != 1 && dir.Y != 0)
                    {
                        dir.X = -1;
                        dir.Y = 0;
                    }
                    break;
                default:
                    break;
            }
            _snake.Dir = dir;
        }
        public Point GetDir()
        {
            return dir;
        }
        private Point ReLocPos(Point point, Size size) /// private ???
        {
            if (point.X < 0) { point.X = size.Width - 1; }
            else if(point.X >= size.Width) { point.X = 0; }
            if (point.Y < 0) { point.Y = size.Height - 1; }
            else if (point.Y >= size.Height) { point.Y = 0; }
            return point;
        }
        public bool Update(int speed)
        {
            //List<GameElem> all = new List<GameElem>(_snake.GetSnakeElems().Count + _gameField.GetApples().Count);
            Point temp = _snake.GetSnakeElems()[0].Pos;
            temp.X += GetDir().X;
            temp.Y += GetDir().Y;
            temp = ReLocPos(temp, _gameField.Size); // перепозание змени через границы поля
            GameElem elem = _collision.IsCollide(temp);
            if (elem != null)
            {
                if (elem.Type == "Apple")
                {
                    _snake.AddTail(elem.Val); // add snake body
                    _gameField.AddScore(10 * elem.Val);
                    _viewPort.RemoveViewElem(elem);
                    _gameField.RemoveApple(elem); // remove apple
                }
                else if (elem.Type == "Body")   // если столкнулись с телом
                {
                    // end game
                    return false;
                }
            }
            //
            //_snake.Move(temp);
            _snake.MoveReal(temp);
            _gameField.Update(speed, _viewPort);
            if(_gameField.GetApples().Count == 0) // если кончились яблоки
            {
                // увеличить уровень
                List<int> values = _gameField.IncreaseLevel();
                foreach (int val in values)
                {
                    _viewPort.AddViewElem(_gameField.AddApple(_collision, val));
                }
                //_viewPort.AddViewElem(_gameField.AddApple(_collision, 3));
            }

            return true;
        }

    }
}
