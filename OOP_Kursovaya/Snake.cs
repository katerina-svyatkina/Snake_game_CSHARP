using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Kursovaya
{
    public class Snake
    {
        private List<GameElem> _snake;
        private int _tailToAdd;
        private int _size;
        private GameField _gameField;
        private ViewPort _viewPort;
        private Point _dir;
        public Point Dir { get => _dir; set => _dir = value; }
        public Snake(int size, GameField gf, ViewPort vp)
        {
            _size = size;
            _gameField = gf;
            _viewPort = vp;
            _snake = new List<GameElem>();
            Point point = _gameField.RandPositon();
            do // получение случайного направления
            {
                _dir.X = _gameField.Rand(2) - 1;
                if (_dir.X == 0)
                {
                    _dir.Y = _gameField.Rand(2) - 1;
                }
            } while (_dir.X == 0 && _dir.Y == 0);
            _tailToAdd = 2; // длина хвоста/туловища 
            _snake.Add(new SnakeHead(point, size));
            _viewPort.AddViewElem(_snake[0]);
        }
        public void Load(List<GameElem> snake)
        {
            _snake.Clear();
            _snake = snake;
        }
        public List<GameElem> GetSnakeElems()
        {
            return _snake;
        }
        public void Move(Point point)   /// For Tests, TO DELETE !!!!!
        {
            for(int i = _snake.Count - 1; i > 0; --i )
            {
                _snake[i].Pos = _snake[i - 1].Pos;
            }
            _snake[0].Pos = point;
        }
        public void MoveReal(Point pos)
        {
            _snake.Insert(0, new SnakeHead(pos, _size));    // add new head
            _viewPort.AddViewElem(_snake[0]);// add head to viewport

            _snake[1].Type = "Body";    // change type old head
            _viewPort.ChangeViewElem(_snake[1]); // and view
            if (_tailToAdd == 0)
            {  
                _viewPort.RemoveViewElem(_snake[_snake.Count - 1]);// remove last body from controls
                _snake.RemoveAt(_snake.Count - 1);  // and from List
            }
            else
            {
                _tailToAdd--;
            }
        }
        public void AddTail(int num)
        {
            _tailToAdd += num;
        }
    }
}
