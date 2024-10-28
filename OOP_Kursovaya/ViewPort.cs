using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Kursovaya
{
    public class ViewPort
    {
        private readonly Color[] AppleTypeColor = new Color[3] { Color.Yellow, Color.Magenta, Color.Red };
        private Control.ControlCollection _control; //container of controls from mainFrame
        private GameField _gameField;
        private Dictionary<GameElem, Control> _view; // карта соответствия игрового элемента и его граф. отображения
        private TextBox _score;
        private TextBox _level;
        private TextBox _time;
        public ViewPort(Control.ControlCollection cc, GameField gf) // в конструктор передяется контейнер элементов управления mainForm
        {
            _control = cc;
            _gameField = gf;
            _view = new Dictionary<GameElem, Control>();
            _score = new TextBox();
            _level = new TextBox();
            _time = new TextBox();
            int sizeX = _gameField.CalcWidthPix();
            // TODO Calculate Location
            _score.Location = new Point(10, 10);
            _score.Enabled = false;
            // TODO 
            if (cc != null)
                cc.Add(_score);

            _level.Location = new Point(200, 10);   // TEMP NEED CALCULATE
            _level.Enabled = false;
            if (cc != null)
                cc.Add(_level);

            _time.Location = new Point(400, 10);    // TEMP
            _time.Enabled = false;
            if (cc != null)
                cc.Add(_time);

            ResetInfo();
        }

        public void AddViewElem(GameElem ge)
        {
            if (!_view.ContainsKey(ge)) // если нет такого ключа(gameElemet)
            {
                Control contr = GetPicture(ge); // создаем картинку 
                _view.Add(ge, contr); // заносим в карту
                if (_control != null)
                    _control.Add(contr); // добавляем в Controls mainForm
            }
        }
        public void AddViewElements(List<GameElem> apples, List<GameElem> snake)
        {
            ClearView();

            foreach(GameElem ge in apples)
            {
                AddViewElem(ge);
            }
            foreach (GameElem ge in snake)
            {
                AddViewElem(ge);
            }
        }
        public void ClearView()
        {
            foreach (KeyValuePair<GameElem, Control> pair in _view)
            {
                //GameElem ge = pair.Key;
                //Control c = pair.Value;
                if (_control != null)
                    _control.Remove(_view[pair.Key]);
            }
            _view.Clear();
        }
        public void ClearInfo()
        {
            if (_control != null)
            {
                _control.Remove(_score);
                _control.Remove(_level);
                _control.Remove(_time);
            }
        }
        public bool RemoveViewElem(GameElem ge)
        {
            if (_view.ContainsKey(ge)) // если есть такой GameElem
            {
                if (_control != null) 
                    _control.Remove(_view[ge]); // убираем из Controls
                _view.Remove(ge); //и из карты
                return true; // Ок
            }
            return false; // нет такого элемента
        }
        public bool ChangeViewElem(GameElem ge)
        {
            if (_view.ContainsKey(ge))
            {
                _view[ge].BackColor = GetColor(ge);
            }
            return false;
        }
        private Point CalcLocation(Point p, int s)
        {
            return new Point(s / 2 + p.X * s, 30 + s / 2 + p.Y * s);
        }
        private Size CalcSize(int s)
        {
            return new Size(s - 1, s - 1);
        }
        private Color GetColor(GameElem ge)
        {
            Color color;
            switch (ge.Type)
            {
                case "Apple":
                    int val = ge.Val;
                    if (--val < 0) val = 0;
                    else if (val > 2) val = 2;
                    color = AppleTypeColor[val];
                    //
                    break;
                case "Head":
                    color = Color.Green;
                    //
                    break;
                case "Body":
                    color = Color.Blue;
                    //
                    break;
                default:
                    color = Color.WhiteSmoke;
                    break;
            }
            return color;
        }
        private Control GetPicture(GameElem ge)
        {
            PictureBox pic = new PictureBox();
            pic.Location = CalcLocation(ge.Pos, ge.Size);
            pic.Size = CalcSize(ge.Size);
            pic.BackColor = GetColor(ge);
            return pic;
        }
        private void ResetInfo()
        {
            _score.Text = "Score : 0";
            _level.Text = "Level : 1";
            _time.Text = "Time 0:0:0";
        }
        public void ShowScore (string score)
        {
            _score.Text = "Score : " + score;
        }
        public void ShowLevel (string level)
        {
            _level.Text = "Level : " + level;
        }
        public void ShowTime (string time)
        {
            _time.Text = "Time : " + time;
        }
    }
}
