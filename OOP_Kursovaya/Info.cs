using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace OOP_Kursovaya
{
    public class Info
    {
        private int _score;
        private int _level;
        private DateTime _time;
        public int Level { get => _level; set => _level = value; }
        public int Score { get => _score; set => _score = value; }
        public Info(int width, int size)
        {
            Clear();
        }
        public void Load(int level, int score)
        {
            _level = level; _score = score;
            
        }
        public void Clear()
        {
            _score = 0;
            _level = 0;
            //_time = 0;
            _time = new DateTime();
        }
        public void Update(ViewPort vp, int addScore, int deltaTime)
        {
            _time = _time.AddMilliseconds(deltaTime); // As Timer Set
            _score += addScore;
            vp.ShowScore(_score.ToString());
            vp.ShowLevel(_level.ToString());
            vp.ShowTime(_time.Hour.ToString() +
                ":" + _time.Minute.ToString() + ":" +_time.Second.ToString());
        }
    }
}
