using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Kursovaya
{
    public class GameField
    {
        private Info info;
        private Random rand = new Random(); // рандомайзер
        private Size _size;
        private int _elemSize;
        private int _scoreToAdd = 0;
        private int _blinkPeriod;
        private List<GameElem> _apples;
        public int ElemSize { get => _elemSize; }

        public Info Info { get => info; }
        public GameField(Size size, int elemSize)
        {
            _size = size;
            _elemSize = elemSize;
            info = new Info(_size.Width, _elemSize);
            _apples = new List<GameElem>();
        }

        public void Load(List<GameElem> apples, int level, int score)
        {
            Info.Load(level, score);
            _apples.Clear();
            _apples = apples;
        }
        public int CalcWidthPix()
        {
            return (_size.Width + 1) * _elemSize + _elemSize / 2;
        }
        public int CalcHeigthPix()
        {
            return (_size.Height + 1) * _elemSize + _elemSize / 2 + 50;
        }
        public Size Size { get => _size; }  
        public GameElem AddApple(Collision col, int val = 1)
        {
            Point rPoint;// = new Point();
            do
            {
                rPoint = RandPositon(); // Генереруем случайную позицию
            } while(!col.IsFree(rPoint)); // пока не попадем в пустую
            //} while (col.IsCollide(rPoint) != null);
            Apple apple = new Apple(rPoint, _elemSize, val);
            _apples.Add(apple);
            return apple;
        }
        public Point RandPositon()
        {
            return new Point(Rand(_size.Width - 1), Rand(_size.Height - 1));
        }
        public int Rand(int max)
        {
            return rand.Next(max);
        }
        public List<GameElem> GetApples()
        {
            return _apples;
        }

        public bool RemoveApple(GameElem Ap)
        {
            return _apples.Remove(Ap);
        }
        public void AddScore(int score)
        {
            _scoreToAdd = score;
        }
        public List<int> IncreaseLevel()
        {
            List<int> values = new List<int>();
            int level = ++info.Level;
            int max = (level > 3) ? 3 : level;
            while (level > 0)
            {
                int val = Rand(max);
                level -= ++val;
                if (level < 0)
                {
                    val += level;
                }
                values.Add(val);
            }
            return values;
        }
        public void Update(int deltaT, ViewPort vp)
        {
            if (_apples.Count == 0)
            {
                // TODO level++ ???
                //AddApple(_elemSize, col, snake);
            }
            foreach (GameElem gE in _apples)
            {
                //Size sz = gE.Picture.Size;
                //sz.Width += _resize*2;
                //sz.Height += _resize*2;
                //gE.Picture.Size = sz;
                //Point pt = gE.Picture.Location;
                //pt.X -= _resize;
                //pt.Y -= _resize;
                //gE.Picture.Location = pt;
                //_resize += _resizeDir;
                //if (_resize > 2) _resizeDir = -1;
                //else if (_resize < -2) _resizeDir = 1;
                if (++_blinkPeriod == 2) // || !gE.Picture.Visible) // Some animation for test
                {
                    _blinkPeriod = 0;
                    //gE.Picture.Visible = !gE.Picture.Visible; 
                }
            }
            info.Update(vp, _scoreToAdd, deltaT);
            if (_scoreToAdd != 0) { _scoreToAdd = 0;  }
        }
    }
}
